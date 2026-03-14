using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.AboutDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Abouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createAboutDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Abouts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hakkımızda Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hakkımızda Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hakkımızda Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hakkımızda Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateAboutDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Abouts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hakkımızda Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Hakkımızda Güncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
