using RealEstate.WebAPILayer.DTOs.ContactDTOs;

namespace RealEstate.WebAPILayer.Repositories.Contact
{
    public interface IContactService
    {
        Task<List<ResultContactDTO>> GetAllContactAsync();
        Task CreateContactAsync(CreateContactDTO createContactDTO);
        Task UpdateContactAsync(UpdateContactDTO updateContactDTO);
        Task DeleteContactAsync(int id);
        Task<ResultContactDTO> GetByIdContactAsync(int id);
    }
}
