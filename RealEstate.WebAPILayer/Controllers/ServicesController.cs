using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ServiceDTOs;
using RealEstate.WebAPILayer.Repositories.Service;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetService()
        {
            var values = await _serviceService.GetAllServiceAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDTO createServiceDTO)
        {
            await _serviceService.CreateServiceAsync(createServiceDTO);
            return Ok("Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return Ok("Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(UpdateServiceDTO updateServiceDTO)
        {
            await _serviceService.UpdateServiceAsync(updateServiceDTO);
            return Ok("Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdService(int id)
        {
            var values = await _serviceService.GetByIdServiceAsync(id);
            return Ok(values);
        }
    }
}
