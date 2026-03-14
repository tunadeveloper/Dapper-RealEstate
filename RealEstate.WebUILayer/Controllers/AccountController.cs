using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var client = _httpClientFactory.CreateClient();
            StringContent content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7175/api/Accounts", content);
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
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Product", new { area = "Admin" });
                    }
                }
            }
            return View();
        }
    }
}
