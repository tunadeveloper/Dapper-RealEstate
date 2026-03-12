using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.SubscriberDTOs;
using RealEstate.WebAPILayer.Repositories.Subscriber;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriber()
        {
            var values = await _subscriberService.GetAllSubscriberAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDTO createSubscriberDTO)
        {
            await _subscriberService.CreateSubscriberAsync(createSubscriberDTO);
            return Ok("Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            await _subscriberService.DeleteSubscriberAsync(id);
            return Ok("Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberDTO updateSubscriberDTO)
        {
            await _subscriberService.UpdateSubscriberAsync(updateSubscriberDTO);
            return Ok("Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSubscriber(int id)
        {
            var values = await _subscriberService.GetByIdSubscriberAsync(id);
            return Ok(values);
        }
    }
}
