using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.Areas.Employee.Models;
using System.Security.Claims;

namespace RealEstate.WebUILayer.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var employeeIdRaw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
            var hasEmployeeId = int.TryParse(employeeIdRaw, out var employeeId);

            var client = _httpClientFactory.CreateClient("RealEstateApi");

            var totalProductCountTask = GetStatIntAsync(client, "api/Statistics/ProductCount");
            var myProductCountTask = hasEmployeeId
                ? GetStatIntAsync(client, $"api/Statistics/ProductCountByEmployee/{employeeId}")
                : Task.FromResult(0);
            var categoryCountTask = GetStatIntAsync(client, "api/Statistics/CategoryCount");
            var differentCityCountTask = GetStatIntAsync(client, "api/Statistics/DifferentCityCount");
            var differentDistrictCountTask = GetStatIntAsync(client, "api/Statistics/DifferentDistrictCount");
            var apartmentCountTask = GetStatIntAsync(client, "api/Statistics/ApartmentCount");
            var avgPriceTask = GetStatDecimalAsync(client, "api/Statistics/AverageProductPrice");
            var maxPriceTask = GetStatDecimalAsync(client, "api/Statistics/MaxProductPrice");
            var minPriceTask = GetStatDecimalAsync(client, "api/Statistics/MinProductPrice");

            if (!hasEmployeeId)
                TempData["Warning"] = "Hesabınız bir çalışan kaydıyla eşleşmediği için kişisel ilan istatistikleri gösterilemiyor.";

            await Task.WhenAll(
                totalProductCountTask, myProductCountTask,
                categoryCountTask, differentCityCountTask,
                differentDistrictCountTask, apartmentCountTask,
                avgPriceTask, maxPriceTask, minPriceTask);

            var model = new EmployeeDashboardViewModel
            {
                TotalProductCount = await totalProductCountTask,
                MyProductCount = await myProductCountTask,
                CategoryCount = await categoryCountTask,
                DifferentCityCount = await differentCityCountTask,
                DifferentDistrictCount = await differentDistrictCountTask,
                ApartmentCount = await apartmentCountTask,
                AverageProductPrice = await avgPriceTask,
                MaxProductPrice = await maxPriceTask,
                MinProductPrice = await minPriceTask,
                EmployeeName = User.Identity?.Name ?? ""
            };

            return View(model);
        }

        private static async Task<int> GetStatIntAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return 0;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return 0;
            return JsonConvert.DeserializeObject<int>(json);
        }

        private static async Task<decimal> GetStatDecimalAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return 0;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return 0;
            return JsonConvert.DeserializeObject<decimal>(json);
        }
    }
}
