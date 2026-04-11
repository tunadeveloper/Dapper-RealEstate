using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealEstate.WebAPILayer.Hubs;
using RealEstate.WebAPILayer.Repositories.Statistics;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("ActiveCategoryCount")]
        public IActionResult ActiveCategoryCount()
        {
            return Ok(_statisticsService.ActiveCategoryCount());
        }

        [HttpGet("ApartmentCount")]
        public IActionResult ApartmentCount()
        {
            return Ok(_statisticsService.ApartmentCount());
        }

        [HttpGet("AverageProductPrice")]
        public IActionResult AverageProductPrice()
        {
            return Ok(_statisticsService.AverageProductPrice());
        }

        [HttpGet("CategoryCount")]
        public IActionResult CategoryCount()
        {
            return Ok(_statisticsService.CategoryCount());
        }

        [HttpGet("CategoryNameByMaxProductCount")]
        public IActionResult CategoryNameByMaxProductCount()
        {
            return Ok(_statisticsService.CategoryNameByMaxProductCount());
        }

        [HttpGet("CityNameByMaxProductCount")]
        public IActionResult CityNameByMaxProductCount()
        {
            return Ok(_statisticsService.CityNameByMaxProductCount());
        }

        [HttpGet("CityNameByMaxProductPrice")]
        public IActionResult CityNameByMaxProductPrice()
        {
            return Ok(_statisticsService.CityNameByMaxProductPrice());
        }

        [HttpGet("CityNameByMinProductPrice")]
        public IActionResult CityNameByMinProductPrice()
        {
            return Ok(_statisticsService.CityNameByMinProductPrice());
        }

        [HttpGet("DifferentCityCount")]
        public IActionResult DifferentCityCount()
        {
            return Ok(_statisticsService.DifferentCityCount());
        }

        [HttpGet("DifferentDistrictCount")]
        public IActionResult DifferentDistrictCount()
        {
            return Ok(_statisticsService.DifferentDistrictCount());
        }

        [HttpGet("DistrictNameByMaxProductCount")]
        public IActionResult DistrictNameByMaxProductCount()
        {
            return Ok(_statisticsService.DistrictNameByMaxProductCount());
        }

        [HttpGet("EmployeeNameByMaxProductCount")]
        public IActionResult EmployeeNameByMaxProductCount()
        {
            return Ok(_statisticsService.EmployeeNameByMaxProductCount());
        }

        [HttpGet("EmployeeNameByMinProductCount")]
        public IActionResult EmployeeNameByMinProductCount()
        {
            return Ok(_statisticsService.EmployeeNameByMinProductCount());
        }

        [HttpGet("LastProductPrice")]
        public IActionResult LastProductPrice()
        {
            return Ok(_statisticsService.LastProductPrice());
        }

        [HttpGet("MaxProductPrice")]
        public IActionResult MaxProductPrice()
        {
            return Ok(_statisticsService.MaxProductPrice());
        }

        [HttpGet("MinProductPrice")]
        public IActionResult MinProductPrice()
        {
            return Ok(_statisticsService.MinProductPrice());
        }

        [HttpGet("PassiveCategoryCount")]
        public IActionResult PassiveCategoryCount()
        {
            return Ok(_statisticsService.PassiveCategoryCount());
        }

        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_statisticsService.ProductCount());
        }

        [HttpGet("ProductNameByMaxProductPrice")]
        public IActionResult ProductNameByMaxProductPrice()
        {
            return Ok(_statisticsService.ProductNameByMaxProductPrice());
        }

        [HttpGet("ProductNameByMinProductPrice")]
        public IActionResult ProductNameByMinProductPrice()
        {
            return Ok(_statisticsService.ProductNameByMinProductPrice());
        }

        [HttpGet("ProductCountByEmployee/{employeeId}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Employee")]
        public IActionResult ProductCountByEmployee(int employeeId)
        {
            if (User.IsInRole("Employee"))
            {
                var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
                if (!int.TryParse(raw, out var eid) || eid != employeeId)
                    return Forbid();
            }
            return Ok(_statisticsService.ProductCountByEmployee(employeeId));
        }
    }
}
