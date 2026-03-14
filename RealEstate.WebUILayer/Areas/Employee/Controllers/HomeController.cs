using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.EmployeeDTOs;

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
            var employeeIdRaw = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId")?.Value;
            if (!int.TryParse(employeeIdRaw, out var employeeId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var responseMessage = await client.GetAsync($"api/Employees/{employeeId}");
            if (!responseMessage.IsSuccessStatusCode)
                return View();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultEmployeeDTO>(jsonData);
            return View(values);
        }
    }
}
