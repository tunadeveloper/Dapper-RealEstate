using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.AboutDTOs;

namespace RealEstate.WebUILayer.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ResultAboutDTO? model = null;
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var response = await client.GetAsync("api/Abouts/1");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(json))
                    model = JsonConvert.DeserializeObject<ResultAboutDTO>(json);
            }
            return View(model);
        }
    }
}
