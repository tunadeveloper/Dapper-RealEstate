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
            if (!int.TryParse(employeeIdRaw, out var employeeId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var client = _httpClientFactory.CreateClient("RealEstateApi");

            var totalProductCountTask = GetStatIntAsync(client, "api/Statistics/ProductCount");
            var myProductCountTask = GetStatIntAsync(client, $"api/Statistics/ProductCountByEmployee/{employeeId}");
            var categoryCountTask = GetStatIntAsync(client, "api/Statistics/CategoryCount");
            var differentCityCountTask = GetStatIntAsync(client, "api/Statistics/DifferentCityCount");

            await Task.WhenAll(totalProductCountTask, myProductCountTask, categoryCountTask, differentCityCountTask);

            var model = new EmployeeDashboardViewModel
            {
                TotalProductCount = await totalProductCountTask,
                MyProductCount = await myProductCountTask,
                CategoryCount = await categoryCountTask,
                DifferentCityCount = await differentCityCountTask,
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
    }
}
