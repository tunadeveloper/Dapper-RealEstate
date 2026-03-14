using RealEstate.WebAPILayer.Tools;

namespace RealEstate.WebAPILayer.Repositories.Auth
{
    public class AuthResult
    {
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public TokenResponse? Token { get; set; }
    }
}
