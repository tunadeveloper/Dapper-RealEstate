using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ContactDTOs;
using System.Text;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Contacts");
            if (!responseMessage.IsSuccessStatusCode)
                return View((UpdateContactDTO?)null);

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultContactDTO>>(jsonData);
            var first = list?.OrderBy(c => c.ContactId).FirstOrDefault();
            if (first == null)
                return View((UpdateContactDTO?)null);

            var oneResponse = await client.GetAsync($"api/Contacts/{first.ContactId}");
            if (!oneResponse.IsSuccessStatusCode)
                return View((UpdateContactDTO?)null);

            var oneJson = await oneResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(oneJson))
                return View((UpdateContactDTO?)null);

            var model = JsonConvert.DeserializeObject<UpdateContactDTO>(oneJson);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateContactDTO updateContactDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateContactDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Contacts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İletişim bilgileri başarıyla güncellendi";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "İletişim bilgileri güncellenemedi";
            return View(updateContactDTO);
        }
    }
}
