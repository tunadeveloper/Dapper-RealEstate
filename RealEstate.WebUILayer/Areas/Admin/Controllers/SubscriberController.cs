using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.SubscriberDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriberController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Subscribers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSubscriberDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateSubscriber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDTO createSubscriberDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSubscriberDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7175/api/Subscribers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Abone Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Abone Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7175/api/Subscribers/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Abone Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Abone Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubscriber(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7175/api/Subscribers/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateSubscriberDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberDTO updateSubscriberDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSubscriberDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Subscribers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Abone Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Abone Güncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
