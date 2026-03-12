using RealEstate.WebAPILayer.DTOs.AboutDTOs;

namespace RealEstate.WebAPILayer.Repositories.About
{
    public interface IAboutService
    {
        Task<List<ResultAboutDTO>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDTO createAboutDTO);
        Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO);
        Task DeleteAboutAsync(int id);
        Task<ResultAboutDTO> GetByIdAboutAsync(int id);
    }
}
