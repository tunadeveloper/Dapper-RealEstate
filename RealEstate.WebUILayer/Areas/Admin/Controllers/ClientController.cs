using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ClientDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Clients");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultClientDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO createClientDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createClientDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Clients", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Müşteri Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Müşteri Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Clients/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Müşteri Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Müşteri Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClient(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Clients/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateClientDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(UpdateClientDTO updateClientDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateClientDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Clients", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Müşteri Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Müşteri Güncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
