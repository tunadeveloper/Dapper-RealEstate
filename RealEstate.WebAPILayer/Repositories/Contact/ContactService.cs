using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.ContactDTOs;

namespace RealEstate.WebAPILayer.Repositories.Contact
{
    public class ContactService : IContactService
    {
        private readonly DapperContext _context;

        public ContactService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateContactAsync(CreateContactDTO createContactDTO)
        {
            string query = "INSERT INTO Contacts (ContactPhoneNumber, ContactEmail, ContactAddress, ContactMapUrl) VALUES (@contactPhoneNumber, @contactEmail, @contactAddress, @contactMapUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@contactPhoneNumber", createContactDTO.ContactPhoneNumber);
            parameters.Add("@contactEmail", createContactDTO.ContactEmail);
            parameters.Add("@contactAddress", createContactDTO.ContactAddress);
            parameters.Add("@contactMapUrl", createContactDTO.ContactMapUrl);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            string query = "DELETE FROM Contacts WHERE ContactId=@contactId";
            var parameters = new DynamicParameters();
            parameters.Add("@contactId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultContactDTO>> GetAllContactAsync()
        {
            string query = "SELECT * FROM Contacts";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultContactDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultContactDTO> GetByIdContactAsync(int id)
        {
            string query = "SELECT * FROM Contacts WHERE ContactId=@contactId";
            var parameters = new DynamicParameters();
            parameters.Add("@contactId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultContactDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateContactAsync(UpdateContactDTO updateContactDTO)
        {
            string query = "UPDATE Contacts SET ContactPhoneNumber=@contactPhoneNumber, ContactEmail=@contactEmail, ContactAddress=@contactAddress, ContactMapUrl=@contactMapUrl WHERE ContactId=@contactId";
            var parameters = new DynamicParameters();
            parameters.Add("@contactPhoneNumber", updateContactDTO.ContactPhoneNumber);
            parameters.Add("@contactEmail", updateContactDTO.ContactEmail);
            parameters.Add("@contactAddress", updateContactDTO.ContactAddress);
            parameters.Add("@contactMapUrl", updateContactDTO.ContactMapUrl);
            parameters.Add("@contactId", updateContactDTO.ContactId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
