using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealEstate.WebAPILayer.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponse GenerateToken(GetCheckAppUser model)
        {
            var claims = new List<Claim>();
            if (!string.IsNullOrEmpty(model.Role))
                claims.Add(new Claim(ClaimTypes.Role, model.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(model.Username))
                claims.Add(new Claim("Username", model.Username));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signinCreadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCreadentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponse(handler.WriteToken(token), expireDate);
        }
    }
}
