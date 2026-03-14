using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ContactDTOs;
using System.Text;
using X.PagedList.Extensions;

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

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Contacts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createContactDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Contacts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İletişim Mesajı Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İletişim Mesajı Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Contacts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İletişim Mesajı Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İletişim Mesajı Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Contacts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateContactDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDTO updateContactDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateContactDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Contacts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İletişim Mesajı Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İletişim Mesajı Güncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
