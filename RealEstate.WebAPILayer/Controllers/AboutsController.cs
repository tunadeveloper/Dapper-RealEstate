using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.AboutDTOs;
using RealEstate.WebAPILayer.Repositories.About;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAbout()
        {
            var values = await _aboutService.GetAllAboutAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            await _aboutService.CreateAboutAsync(createAboutDTO);
            return Ok("Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return Ok("Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDTO);
            return Ok("Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(int id)
        {
            var values = await _aboutService.GetByIdAboutAsync(id);
            return Ok(values);
        }
    }
}
