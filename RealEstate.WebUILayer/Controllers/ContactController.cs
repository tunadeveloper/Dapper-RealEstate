using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ContactDTOs;
using RealEstate.WebUILayer.DTOs.MessageDTOs;
using System.Text;

namespace RealEstate.WebUILayer.Controllers
{
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
            return View(await GetFirstContactAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CreateMessageDTO dto)
        {
            var contact = await GetFirstContactAsync();

            if (string.IsNullOrWhiteSpace(dto.MessageNameSurname) ||
                string.IsNullOrWhiteSpace(dto.MessageEmail) ||
                string.IsNullOrWhiteSpace(dto.MessageSubject) ||
                string.IsNullOrWhiteSpace(dto.MessageDetail))
            {
                TempData["Error"] = "Lütfen tüm alanları doldurun.";
                return View(contact);
            }

            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Messages", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Mesajınız alındı. En kısa sürede size dönüş yapacağız.";
                return View(contact);
            }

            TempData["Error"] = "Mesaj gönderilemedi. Lütfen daha sonra tekrar deneyin.";
            return View(contact);
        }

        private async Task<ResultContactDTO?> GetFirstContactAsync()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var response = await client.GetAsync("api/Contacts");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultContactDTO>>(json);
            return list?.FirstOrDefault();
        }
    }
}
