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
                    EnsureNormalizedClaim(claims, ClaimTypes.Role, "role");
                    EnsureNormalizedClaim(claims, "RoleId", "roleid");
                    EnsureNormalizedClaim(claims, "EmployeeId", "employeeid");

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
            EnsureNormalizedClaim(claims, ClaimTypes.Role, "role");
            EnsureNormalizedClaim(claims, "RoleId", "roleid");
            EnsureNormalizedClaim(claims, "EmployeeId", "employeeid");
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
            var roleIdRaw = GetClaimValue(claims, "RoleId", "roleid");
            if (int.TryParse(roleIdRaw, out var roleId))
            {
                if (roleId == 1)
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                if (roleId == 2)
                    return RedirectToAction("Index", "Home", new { area = "Employee" });
            }

            var role = GetClaimValue(claims, ClaimTypes.Role, "role");
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

        private static void EnsureNormalizedClaim(List<Claim> claims, string targetType, params string[] alternativeTypes)
        {
            if (claims.Any(x => string.Equals(x.Type, targetType, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(x.Value)))
                return;

            var normalized = claims.FirstOrDefault(x =>
                alternativeTypes.Any(t => string.Equals(x.Type, t, StringComparison.OrdinalIgnoreCase)) ||
                x.Type.EndsWith($"/{targetType}", StringComparison.OrdinalIgnoreCase));

            if (normalized is not null && !string.IsNullOrWhiteSpace(normalized.Value))
                claims.Add(new Claim(targetType, normalized.Value));
        }

        private static string? GetClaimValue(IReadOnlyList<Claim> claims, string primaryType, params string[] alternativeTypes)
        {
            var claim = claims.FirstOrDefault(x =>
                string.Equals(x.Type, primaryType, StringComparison.OrdinalIgnoreCase) ||
                alternativeTypes.Any(t => string.Equals(x.Type, t, StringComparison.OrdinalIgnoreCase)) ||
                x.Type.EndsWith($"/{primaryType}", StringComparison.OrdinalIgnoreCase));

            return claim?.Value;
        }
    }
}
