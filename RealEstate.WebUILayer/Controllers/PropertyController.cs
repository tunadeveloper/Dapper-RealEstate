using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ProductDTOs;
using RealEstate.WebUILayer.DTOs.ProductDetailDTOs;

namespace RealEstate.WebUILayer.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var response = await client.GetAsync("api/Products");
            if (!response.IsSuccessStatusCode)
                return View(new List<ResultProductWithCategoryDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(json) ?? new List<ResultProductWithCategoryDTO>();
            return View(list);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var productResponse = await client.GetAsync($"api/Products/{id}");
            if (!productResponse.IsSuccessStatusCode)
                return NotFound();

            var productJson = await productResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ResultProductDTO>(productJson);

            ResultProductDetailDTO? detail = null;
            var detailResponse = await client.GetAsync($"api/ProductDetails/GetList/{id}");
            if (detailResponse.IsSuccessStatusCode)
            {
                var detailJson = await detailResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(detailJson))
                    detail = JsonConvert.DeserializeObject<ResultProductDetailDTO>(detailJson);
            }

            ViewBag.Detail = detail;
            return View(product);
        }
    }
}
