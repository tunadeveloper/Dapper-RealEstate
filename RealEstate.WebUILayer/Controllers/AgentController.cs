using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.DTOs.EmployeeDTOs;

namespace RealEstate.WebUILayer.Controllers
{
    public class AgentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AgentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            var response = await client.GetAsync("api/Employees");
            if (!response.IsSuccessStatusCode)
                return View(new List<ResultEmployeeDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultEmployeeDTO>>(json) ?? new List<ResultEmployeeDTO>();
            return View(list);
        }
    }
}
