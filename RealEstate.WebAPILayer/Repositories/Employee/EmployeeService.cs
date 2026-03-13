using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.EmployeeDTOs;

namespace RealEstate.WebAPILayer.Repositories.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DapperContext _context;

        public EmployeeService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
        {
            string query = "INSERT INTO Employee (EmployeeNameSurname, EmployeeTitle, EmployeeEmail, EmployeePhoneNumber, EmployeeStatus, EmployeeImageUrl) VALUES (@employeeNameSurname, @employeeTitle, @employeeEmail, @employeePhoneNumber, @employeeStatus, @employeeImageUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeNameSurname", createEmployeeDTO.EmployeeNameSurname);
            parameters.Add("@employeeTitle", createEmployeeDTO.EmployeeTitle);
            parameters.Add("@employeeEmail", createEmployeeDTO.EmployeeEmail);
            parameters.Add("@employeePhoneNumber", createEmployeeDTO.EmployeePhoneNumber);
            parameters.Add("@employeeStatus", createEmployeeDTO.EmployeeStatus);
            parameters.Add("@employeeImageUrl", createEmployeeDTO.EmployeeImageUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            string query = "DELETE FROM Employee WHERE EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultEmployeeDTO>> GetAllEmployeeAsync()
        {
            string query = "SELECT E.EmployeeNameSurname,E.EmployeeTitle,E.EmployeeEmail,E.EmployeePhoneNumber,E.EmployeeStatus,E.EmployeeImageUrl,(SELECT COUNT(*) FROM Products P WHERE P.EmployeeId = E.EmployeeId) AS TotalProductCount FROM Employee E";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDTO>(query);
                return values.ToList();
            }
        }
        //s
        public async Task<ResultEmployeeDTO> GetByIdEmployeeAsync(int id)
        {
            string query = "SELECT * FROM Employee WHERE EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultEmployeeDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO)
        {
            string query = "UPDATE Employee SET EmployeeNameSurname=@employeeNameSurname, EmployeeTitle=@employeeTitle, EmployeeEmail=@employeeEmail, EmployeePhoneNumber=@employeePhoneNumber, EmployeeStatus=@employeeStatus, EmployeeImageUrl=@employeeImageUrl WHERE EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", updateEmployeeDTO.EmployeeId);
            parameters.Add("@employeeNameSurname", updateEmployeeDTO.EmployeeNameSurname);
            parameters.Add("@employeeTitle", updateEmployeeDTO.EmployeeTitle);
            parameters.Add("@employeeEmail", updateEmployeeDTO.EmployeeEmail);
            parameters.Add("@employeePhoneNumber", updateEmployeeDTO.EmployeePhoneNumber);
            parameters.Add("@employeeStatus", updateEmployeeDTO.EmployeeStatus);
            parameters.Add("@employeeImageUrl", updateEmployeeDTO.EmployeeImageUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
