using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate.WebAPILayer.Tools
{
    public class JwtTokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public TokenResponse CreateToken(int userId, string username, string? roleName, int? employeeId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            if (!string.IsNullOrWhiteSpace(roleName))
                claims.Add(new Claim(ClaimTypes.Role, roleName));

            if (employeeId is not null)
                claims.Add(new Claim("EmployeeId", employeeId.Value.ToString()));

            var keyBytes = Encoding.UTF8.GetBytes(_settings.Key);
            var signingKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(_settings.ExpireDays);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            return new TokenResponse(handler.WriteToken(token), expireDate);
        }
    }
}
