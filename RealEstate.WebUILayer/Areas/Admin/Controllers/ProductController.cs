using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ProductDetailDTOs;
using RealEstate.WebUILayer.DTOs.ProductDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                int pageNumber = page ?? 1;
                return View(values.ToPagedList(pageNumber, 10));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7175/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İlan Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İlan Eklenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7175/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İlan Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İlan Silinmedi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7175/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var values = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData);
                    if (values != null)
                    {
                        return View(values);
                    }
                }
            }
            TempData["Error"] = "İlan Bulunamadı veya Bir Hata Oluştu!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "İlan Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "İlan Güncellenmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7175/api/ProductDetails/GetList/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var values = JsonConvert.DeserializeObject<UpdateProductDetailDTO>(jsonData);
                    if (values != null && values.ProductDetailId != 0)
                    {
                        return View(values);
                    }
                }
            }
            return RedirectToAction("CreateProductDetail", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> ProductDetail(UpdateProductDetailDTO updateProductDetailDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult CreateProductDetail(int id)
        {
            ViewBag.productId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTO createProductDetailDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDetailDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7175/api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
