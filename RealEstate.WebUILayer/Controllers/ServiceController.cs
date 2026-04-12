using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ServiceDTOs;

namespace RealEstate.WebUILayer.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var response = await client.GetAsync("api/Services");
            if (!response.IsSuccessStatusCode)
                return View(new List<ResultServiceDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultServiceDTO>>(json) ?? new List<ResultServiceDTO>();
            return View(list);
        }
    }
}
