using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetAllProductWithCategoryAsync();
            return Ok(values);
        }
    }
}
