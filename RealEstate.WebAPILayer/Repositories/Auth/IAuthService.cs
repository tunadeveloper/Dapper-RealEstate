using RealEstate.WebAPILayer.DTOs.AccountDTOs;

namespace RealEstate.WebAPILayer.Repositories.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(CreateLoginDTO dto);
        Task<AuthResult> RegisterAsync(RegisterDTO dto);
    }
}
