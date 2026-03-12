using RealEstate.WebAPILayer.DTOs.ContactDTOs;

namespace RealEstate.WebAPILayer.Repositories.Contact
{
    public class ContactService : IContactService
    {
        public Task CreateContactAsync(CreateContactDTO createContactDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultContactDTO>> GetAllContactAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultContactDTO> GetByIdContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(UpdateContactDTO updateContactDTO)
        {
            throw new NotImplementedException();
        }
    }
}
