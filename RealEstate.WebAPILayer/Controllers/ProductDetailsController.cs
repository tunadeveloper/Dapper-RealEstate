using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ProductDetailDTOs;
using RealEstate.WebAPILayer.Repositories.Product;
using RealEstate.WebAPILayer.Repositories.ProductDetail;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IProductService _productService;

        public ProductDetailsController(IProductDetailService productDetailService, IProductService productService)
        {
            _productDetailService = productDetailService;
            _productService = productService;
        }

        [HttpGet("GetList/{id}")]
        public async Task<IActionResult> ProductDetailList(int id)
        {
            var values = await _productDetailService.GetAllProductDetailAsync(id);
            return Ok(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTO createProductDetailDTO)
        {
            if (User.IsInRole("Employee"))
            {
                if (!await EmployeeOwnsProductAsync(createProductDetailDTO.ProductId))
                    return Forbid();
            }
            await _productDetailService.CreateProductDetailAsync(createProductDetailDTO);
            return Ok("İlan Detayları Başarılı Bir Şekilde Eklendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            if (User.IsInRole("Employee"))
            {
                var detail = await _productDetailService.GetByIdProductDetailAsync(id);
                if (detail == null)
                    return NotFound();
                if (!await EmployeeOwnsProductAsync(detail.ProductId))
                    return Forbid();
            }
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("İlan Detayları Başarılı Bir Şekilde Silindi");
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDTO updateProductDetailDTO)
        {
            if (User.IsInRole("Employee"))
            {
                if (!await EmployeeOwnsProductAsync(updateProductDetailDTO.ProductId))
                    return Forbid();
            }
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDTO);
            return Ok("İlan Detayları Başarıyla Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var value = await _productDetailService.GetByIdProductDetailAsync(id);
            return Ok(value);
        }

        private async Task<bool> EmployeeOwnsProductAsync(int productId)
        {
            var raw = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
            if (!int.TryParse(raw, out var eid))
                return false;
            var p = await _productService.GetByIdProductAsync(productId);
            return p != null && p.EmployeeId == eid;
        }
    }
}
