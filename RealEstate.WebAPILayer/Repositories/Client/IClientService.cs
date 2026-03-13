using RealEstate.WebAPILayer.DTOs.ClientDTOs;

namespace RealEstate.WebAPILayer.Repositories.Client
{
    public interface IClientService
    {
        Task<List<ResultClientDTO>> GetAllClientAsync();
        Task CreateClientAsync(CreateClientDTO createClientDTO);
        Task UpdateClientAsync(UpdateClientDTO updateClientDTO);
        Task DeleteClientAsync(int id);
        Task<ResultClientDTO> GetByIdClientAsync(int id);
    }
}
