using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.AboutDTOs;
using System.Text;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Abouts/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var values = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData);
                    if (values != null)
                        return View(values);
                }
            }
            return View(new UpdateAboutDTO { AboutId = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateAboutDTO updateAboutDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateAboutDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Abouts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Hakkımızda başarıyla güncellendi";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hakkımızda güncellenemedi";
            return View(updateAboutDTO);
        }
    }
}
