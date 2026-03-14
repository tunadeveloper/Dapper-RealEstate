using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ClientDTOs;
using RealEstate.WebAPILayer.Repositories.Client;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> ClientList()
        {
            var values = await _clientService.GetAllClientAsync();
            return Ok(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateClient(CreateClientDTO createClientDTO)
        {
            await _clientService.CreateClientAsync(createClientDTO);
            return Ok("Müşteri Kısmı Başarılı Bir Şekilde Eklendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return Ok("Müşteri Kısmı Başarılı Bir Şekilde Silindi");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClient(UpdateClientDTO updateClientDTO)
        {
            await _clientService.UpdateClientAsync(updateClientDTO);
            return Ok("Müşteri Kısmı Başarıyla Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var value = await _clientService.GetByIdClientAsync(id);
            return Ok(value);
        }
    }
}
