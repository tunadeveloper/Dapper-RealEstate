using RealEstate.WebAPILayer.DTOs.EmployeeDTOs;

namespace RealEstate.WebAPILayer.Repositories.Employee
{
    public interface IEmployeeService
    {
        Task<List<ResultEmployeeDTO>> GetAllEmployeeAsync();
        Task CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO);
        Task UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO);
        Task DeleteEmployeeAsync(int id);
        Task<ResultEmployeeDTO> GetByIdEmployeeAsync(int id);
    }
}
