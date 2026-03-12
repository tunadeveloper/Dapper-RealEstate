using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.ServiceDTOs;

namespace RealEstate.WebAPILayer.Repositories.Service
{
    public class ServiceService : IServiceService
    {
        private readonly DapperContext _context;

        public ServiceService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateServiceAsync(CreateServiceDTO createServiceDTO)
        {
            string query = "INSERT INTO Services (ServiceDescription, ServiceIcon) VALUES (@serviceDescription, @serviceIcon)";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceDescription", createServiceDTO.ServiceDescription);
            parameters.Add("@serviceIcon", createServiceDTO.ServiceIcon);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteServiceAsync(int id)
        {
            string query = "DELETE FROM Services WHERE ServiceId=@serviceId";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultServiceDTO>> GetAllServiceAsync()
        {
            string query = "SELECT * FROM Services";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultServiceDTO> GetByIdServiceAsync(int id)
        {
            string query = "SELECT * FROM Services WHERE ServiceId=@serviceId";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultServiceDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateServiceAsync(UpdateServiceDTO updateServiceDTO)
        {
            string query = "UPDATE Services SET ServiceDescription=@serviceDescription, ServiceIcon=@serviceIcon WHERE ServiceId=@serviceId";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceDescription", updateServiceDTO.ServiceDescription);
            parameters.Add("@serviceIcon", updateServiceDTO.ServiceIcon);
            parameters.Add("@serviceId", updateServiceDTO.ServiceId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
