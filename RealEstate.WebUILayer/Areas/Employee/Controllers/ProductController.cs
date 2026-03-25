using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.ProductDetailDTOs;
using RealEstate.WebUILayer.DTOs.ProductDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var raw = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId")?.Value;
            var pageNumber = page ?? 1;
            if (!int.TryParse(raw, out var employeeId))
            {
                var empty = new List<ResultProductDTO>();
                return View(empty.ToPagedList(pageNumber, 10));
            }

            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Products/ByEmployee/{employeeId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                var empty = new List<ResultProductDTO>();
                return View(empty.ToPagedList(pageNumber, 10));
            }
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData) ?? new List<ResultProductDTO>();
            return View(values.ToPagedList(pageNumber, 10));
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Products", stringContent);
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Products/{id}");
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Products/{id}");
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Products", stringContent);
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/ProductDetails/GetList/{id}");
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/ProductDetails", stringContent);
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
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var jsonData = JsonConvert.SerializeObject(createProductDetailDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
