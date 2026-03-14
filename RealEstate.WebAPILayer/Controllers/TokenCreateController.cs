using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.Tools;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenCreateController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateToken(GetCheckAppUser model)
        {
            var values = JwtTokenGenerator.GenerateToken(model);
            return Ok(values);
        }
    }
}
