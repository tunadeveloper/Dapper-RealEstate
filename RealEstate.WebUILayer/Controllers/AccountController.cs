using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebUILayer.DTOs.AccountDTOs;
using RealEstate.WebUILayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RealEstate.WebUILayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            StringContent content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Auth/login", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("realestatetoken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProps
                        );

                        return RedirectByRoleId(claims);
                    }
                }
            }
            var errorBody = await responseMessage.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(errorBody) ? "Giriş başarısız." : errorBody);
            return View(dto);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");
            StringContent content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/Auth/register", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorBody = await responseMessage.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(errorBody) ? "Kayıt başarısız." : errorBody);
                return View(dto);
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (tokenModel is null || string.IsNullOrWhiteSpace(tokenModel.Token))
                return View(dto);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenModel.Token);
            var claims = token.Claims.ToList();
            claims.Add(new Claim("realestatetoken", tokenModel.Token));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties
            {
                ExpiresUtc = tokenModel.ExpireDate,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProps
            );

            return RedirectByRoleId(claims);
        }

        private IActionResult RedirectByRoleId(IReadOnlyList<Claim> claims)
        {
            var roleIdRaw = claims.FirstOrDefault(x => x.Type == "RoleId")?.Value;
            if (int.TryParse(roleIdRaw, out var roleId))
            {
                if (roleId == 1)
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                if (roleId == 2)
                    return RedirectToAction("Index", "Home", new { area = "Employee" });
            }

            var role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            if (string.Equals(role, "Employee", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Index", "Home", new { area = "Employee" });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
