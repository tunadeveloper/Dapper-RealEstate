using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ContactDTOs;
using RealEstate.WebAPILayer.Repositories.Contact;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            var values = await _contactService.GetAllContactAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            await _contactService.CreateContactAsync(createContactDTO);
            return Ok("Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok("Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDTO updateContactDTO)
        {
            await _contactService.UpdateContactAsync(updateContactDTO);
            return Ok("Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            var values = await _contactService.GetByIdContactAsync(id);
            return Ok(values);
        }
    }
}
