using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.AccountDTOs;
using RealEstate.WebAPILayer.Tools;

namespace RealEstate.WebAPILayer.Repositories.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DapperContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(DapperContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResult> LoginAsync(CreateLoginDTO dto)
        {
            string userQuery = "SELECT TOP(1) u.UserId, u.Username, u.Password, u.NameSurname, u.Email, u.RoleId, r.RoleName FROM Users u INNER JOIN Roles r ON u.RoleId = r.RoleId WHERE u.Username = @username";
            var parameters = new DynamicParameters();
            parameters.Add("@username", dto.Username);

            using (var connection = _context.CreateConnection())
            {
                var userRow = await connection.QueryFirstOrDefaultAsync<UserRow>(userQuery, parameters);
                if (userRow is null)
                    return new AuthResult { StatusCode = StatusCodes.Status401Unauthorized };

                var passwordOk = PasswordHasher.VerifyOrPlainMatch(dto.Password, userRow.Password);
                if (!passwordOk)
                    return new AuthResult { StatusCode = StatusCodes.Status401Unauthorized };

                if (!PasswordHasher.IsHashed(userRow.Password))
                {
                    try
                    {
                        var newHash = PasswordHasher.Hash(dto.Password);
                        await connection.ExecuteAsync(
                            "UPDATE Users SET Password = @password WHERE UserId = @userId",
                            new { password = newHash, userId = userRow.UserId }
                        );
                    }
                    catch (SqlException ex) when (IsTruncationError(ex))
                    {
                    }
                }

                int? employeeId = null;
                if (string.Equals(userRow.RoleName, "Employee", StringComparison.OrdinalIgnoreCase))
                {
                    employeeId = await connection.QueryFirstOrDefaultAsync<int?>(
                        "SELECT TOP(1) EmployeeId FROM Employee WHERE EmployeeStatus = 1 AND EmployeeNameSurname = @nameSurname",
                        new { nameSurname = userRow.NameSurname });
                    if (employeeId is null && !string.IsNullOrWhiteSpace(userRow.Email))
                    {
                        employeeId = await connection.QueryFirstOrDefaultAsync<int?>(
                            "SELECT TOP(1) EmployeeId FROM Employee WHERE EmployeeStatus = 1 AND EmployeeEmail = @email",
                            new { email = userRow.Email });
                    }
                }

                var token = _jwtTokenService.CreateToken(userRow.UserId, userRow.Username, userRow.RoleName, userRow.RoleId, employeeId);
                return new AuthResult { StatusCode = StatusCodes.Status200OK, Token = token };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterDTO dto)
        {
            using (var connection = _context.CreateConnection())
            {
                var usernameExists = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM Users WHERE Username = @username",
                    new { username = dto.Username }
                );
                if (usernameExists > 0)
                    return new AuthResult { StatusCode = StatusCodes.Status409Conflict, Error = "Kullanıcı adı zaten kullanılıyor." };

                const int defaultRoleId = 2;
                var passwordHash = PasswordHasher.Hash(dto.Password);
                string insertQuery = "INSERT INTO Users (Username, Password, NameSurname, Email, RoleId) VALUES (@username, @password, @nameSurname, @email, @roleId); SELECT CAST(SCOPE_IDENTITY() AS int);";

                int userId;
                try
                {
                    userId = await connection.ExecuteScalarAsync<int>(
                        insertQuery,
                        new
                        {
                            username = dto.Username,
                            password = passwordHash,
                            nameSurname = dto.NameSurname,
                            email = dto.Email,
                            roleId = defaultRoleId
                        }
                    );
                }
                catch (SqlException ex) when (IsTruncationError(ex))
                {
                    return new AuthResult
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Error = "Users.Password alanı hash için kısa. Password kolonunu NVARCHAR(200) veya NVARCHAR(MAX) yapmalısın."
                    };
                }

                var registeredRoleName = await connection.QueryFirstOrDefaultAsync<string>(
                    "SELECT RoleName FROM Roles WHERE RoleId = @roleId",
                    new { roleId = defaultRoleId });

                var token = _jwtTokenService.CreateToken(userId, dto.Username, registeredRoleName ?? "Employee", defaultRoleId, null);
                return new AuthResult { StatusCode = StatusCodes.Status200OK, Token = token };
            }
        }

        private static bool IsTruncationError(SqlException ex) =>
            ex.Message.Contains("String or binary data would be truncated", StringComparison.OrdinalIgnoreCase);

        private class UserRow
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string NameSurname { get; set; }
            public string Email { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
