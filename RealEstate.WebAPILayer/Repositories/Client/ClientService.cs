using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.ClientDTOs;

namespace RealEstate.WebAPILayer.Repositories.Client
{
    public class ClientService : IClientService
    {
        private readonly DapperContext _context;

        public ClientService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateClientAsync(CreateClientDTO createClientDTO)
        {
            string query = "INSERT INTO Client (ClientNameSurname, ClientTitle, ClientComment, ClientImageUrl) VALUES (@clientNameSurname, @clientTitle, @clientComment, @clientImageUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@clientNameSurname", createClientDTO.ClientNameSurname);
            parameters.Add("@clientTitle", createClientDTO.ClientTitle);
            parameters.Add("@clientComment", createClientDTO.ClientComment);
            parameters.Add("@clientImageUrl", createClientDTO.ClientImageUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            string query = "DELETE FROM Client WHERE ClientId=@clientId";
            var parameters = new DynamicParameters();
            parameters.Add("@clientId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultClientDTO>> GetAllClientAsync()
        {
            string query = "SELECT * FROM Client";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultClientDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultClientDTO> GetByIdClientAsync(int id)
        {
            string query = "SELECT * FROM Client WHERE ClientId=@clientId";
            var parameters = new DynamicParameters();
            parameters.Add("@clientId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultClientDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateClientAsync(UpdateClientDTO updateClientDTO)
        {
            string query = "UPDATE Client SET ClientNameSurname=@clientNameSurname, ClientTitle=@clientTitle, ClientComment=@clientComment, ClientImageUrl=@clientImageUrl WHERE ClientId=@clientId";
            var parameters = new DynamicParameters();
            parameters.Add("@clientId", updateClientDTO.ClientId);
            parameters.Add("@clientNameSurname", updateClientDTO.ClientNameSurname);
            parameters.Add("@clientTitle", updateClientDTO.ClientTitle);
            parameters.Add("@clientComment", updateClientDTO.ClientComment);
            parameters.Add("@clientImageUrl", updateClientDTO.ClientImageUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
