using RealEstate.WebAPILayer.DTOs.ServiceDTOs;

namespace RealEstate.WebAPILayer.Repositories.Service
{
    public interface IServiceService
    {
        Task<List<ResultServiceDTO>> GetAllServiceAsync();
        Task CreateServiceAsync(CreateServiceDTO createServiceDTO);
        Task UpdateServiceAsync(UpdateServiceDTO updateServiceDTO);
        Task DeleteServiceAsync(int id);
        Task<ResultServiceDTO> GetByIdServiceAsync(int id);
    }
}
