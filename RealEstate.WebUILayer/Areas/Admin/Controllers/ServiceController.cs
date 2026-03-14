using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ServiceDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Services");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDTO createServiceDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createServiceDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Services", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hizmet Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hizmet Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Services/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hizmet Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hizmet Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Services/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateServiceDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDTO updateServiceDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateServiceDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Services", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hizmet Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hizmet Güncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
