using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.EmployeeDTOs;
using RealEstate.WebAPILayer.Repositories.Employee;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeService.GetAllEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            await _employeeService.CreateEmployeeAsync(createEmployeeDTO);
            return Ok("Danışman Başarılı Bir Şekilde Eklendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok("Danışman Başarılı Bir Şekilde Silindi");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
        {
            await _employeeService.UpdateEmployeeAsync(updateEmployeeDTO);
            return Ok("Danışman Başarıyla Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var value = await _employeeService.GetByIdEmployeeAsync(id);
            return Ok(value);
        }
    }
}
