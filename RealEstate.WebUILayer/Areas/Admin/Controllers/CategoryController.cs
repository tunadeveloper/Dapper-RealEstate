using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.CategoryDTOs;
using RealEstate.WebUILayer.DTOs.ProductDTOs;
using System.Text;
using X.PagedList.Extensions;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync("api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsondata);
                return View(values.ToPagedList(page ?? 1, 5));
            }
            return View();
        }

        public IActionResult CreateCategory() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var json = JsonConvert.SerializeObject(createCategoryDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Categories", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Kategori Ba�ar�yla Eklendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Kategori Eklenmedi";
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.DeleteAsync($"api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Kategori Ba�ar�yla Silindi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Kategori Silinmedi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);

                ViewBag.StatusList = new SelectList(new[]
                {
                    new {Value = true, Text="Aktif"},
                    new{Value=false,Text="Pasif"}
                }, "Value", "Text", values.CategoryStatus);

                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var json = JsonConvert.SerializeObject(updateCategoryDTO);
            var content = new StringContent(json,Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("api/Categories", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Kategori Ba�ar�yla G�ncellendi";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Kategori G�ncellenmedi";
            return RedirectToAction("Index");
        }
    }
}
