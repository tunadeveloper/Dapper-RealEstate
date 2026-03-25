using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.MessageDTOs;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Messages");
            var pageNumber = page ?? 1;
            if (!responseMessage.IsSuccessStatusCode)
            {
                var empty = new List<ResultMessageDTO>();
                return View(empty.ToPagedList(pageNumber, 10));
            }
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageDTO>>(jsonData) ?? new List<ResultMessageDTO>();
            return View(values.ToPagedList(pageNumber, 10));
        }
    }
}
