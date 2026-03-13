using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.ProductDetailDTOs;
using RealEstate.WebAPILayer.Repositories.ProductDetail;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet("GetList/{id}")]
        public async Task<IActionResult> ProductDetailList(int id)
        {
            var values = await _productDetailService.GetAllProductDetailAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTO createProductDetailDTO)
        {
            await _productDetailService.CreateProductDetailAsync(createProductDetailDTO);
            return Ok("İlan Detayları Başarılı Bir Şekilde Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("İlan Detayları Başarılı Bir Şekilde Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDTO updateProductDetailDTO)
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDTO);
            return Ok("İlan Detayları Başarıyla Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var value = await _productDetailService.GetByIdProductDetailAsync(id);
            return Ok(value);
        }
    }
}
