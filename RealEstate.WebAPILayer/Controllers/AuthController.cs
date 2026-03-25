using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.DTOs.AccountDTOs;
using RealEstate.WebAPILayer.Repositories.Auth;
using System.Security.Claims;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result.Token is not null)
                return Ok(result.Token);

            if (result.StatusCode == StatusCodes.Status401Unauthorized)
                return Unauthorized();

            return StatusCode(result.StatusCode, result.Error);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (result.Token is not null)
                return Ok(result.Token);

            return StatusCode(result.StatusCode, result.Error);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.Identity?.Name;
            var role = User.FindFirstValue(ClaimTypes.Role);
            var roleId = User.FindFirstValue("RoleId");
            var employeeId = User.FindFirstValue("EmployeeId");

            return Ok(new { userId, username, role, roleId, employeeId });
        }
    }
}
