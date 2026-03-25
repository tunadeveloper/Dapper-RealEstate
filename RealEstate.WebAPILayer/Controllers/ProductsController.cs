using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ProductDTOs;
using RealEstate.WebAPILayer.Repositories.Product;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var values = await _productService.GetAllProductWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("ByEmployee/{employeeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetProductsByEmployee(int employeeId)
        {
            if (User.IsInRole("Employee"))
            {
                var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
                if (!int.TryParse(raw, out var eid) || eid != employeeId)
                    return Forbid();
            }
            var values = await _productService.GetProductsByEmployeeIdAsync(employeeId);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            if (User.IsInRole("Employee"))
            {
                var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
                if (!int.TryParse(raw, out var eid))
                    return Forbid();
                createProductDTO.EmployeeId = eid;
            }
            await _productService.CreateProductAsync(createProductDTO);
            return Ok("Eklendi");
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            if (User.IsInRole("Employee"))
            {
                var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
                if (!int.TryParse(raw, out var eid))
                    return Forbid();
                var existing = await _productService.GetByIdProductAsync(updateProductDTO.ProductId);
                if (existing == null)
                    return NotFound();
                if (existing.EmployeeId != eid)
                    return Forbid();
                updateProductDTO.EmployeeId = eid;
            }
            await _productService.UpdateProductAsync(updateProductDTO);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (User.IsInRole("Employee"))
            {
                var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
                if (!int.TryParse(raw, out var eid))
                    return Forbid();
                var existing = await _productService.GetByIdProductAsync(id);
                if (existing == null)
                    return NotFound();
                if (existing.EmployeeId != eid)
                    return Forbid();
            }
            await _productService.DeleteProductAsync(id);
            return Ok("Silindi");
        }

        [HttpGet("GetAllProductWithCategory")]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var values = await _productService.GetAllProductWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetAllProductWithEmployee")]
        public async Task<IActionResult> GetAllProductWithEmployee()
        {
            var values = await _productService.GetAllProductWithEmployeeAsync();
            return Ok(values);
        }

        [HttpGet("GetAllProductWithEmployeeAndCategory")]
        public async Task<IActionResult> GetAllProductWithEmployeeAndCategory()
        {
            var values = await _productService.GetAllProductWithEmployeeAndCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetTop3ProductByIsPopular")]
        public async Task<IActionResult> GetTop3ProductByIsPopular()
        {
            var values = await _productService.GetTop3ProductByIsPopularAsync();
            return Ok(values);
        }

        [HttpGet("GetTopByPriceForDashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTopByPriceForDashboard([FromQuery] int take = 15)
        {
            if (take < 1) take = 15;
            if (take > 100) take = 100;
            var values = await _productService.GetTopProductsByPriceForDashboardAsync(take);
            return Ok(values);
        }

        [HttpGet("GetPopularByPriceForDashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPopularByPriceForDashboard([FromQuery] int take = 10)
        {
            if (take < 1) take = 10;
            if (take > 100) take = 100;
            var values = await _productService.GetPopularProductsByPriceForDashboardAsync(take);
            return Ok(values);
        }
    }
}
