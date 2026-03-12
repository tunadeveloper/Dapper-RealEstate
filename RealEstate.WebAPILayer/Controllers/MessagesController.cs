using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.MessageDTOs;
using RealEstate.WebAPILayer.Repositories.Message;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {
            var values = await _messageService.GetAllMessageAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO createMessageDTO)
        {
            await _messageService.CreateMessageAsync(createMessageDTO);
            return Ok("Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Ok("Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDTO updateMessageDTO)
        {
            await _messageService.UpdateMessageAsync(updateMessageDTO);
            return Ok("Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMessage(int id)
        {
            var values = await _messageService.GetByIdMessageAsync(id);
            return Ok(values);
        }
    }
}
