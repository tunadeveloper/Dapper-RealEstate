using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.SubscriberDTOs;

namespace RealEstate.WebAPILayer.Repositories.Subscriber
{
    public class SubscriberService : ISubscriberService
    {
        private readonly DapperContext _context;

        public SubscriberService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateSubscriberAsync(CreateSubscriberDTO createSubscriberDTO)
        {
            string query = "INSERT INTO Subscriber (SubscriberEmail) VALUES (@subscriberEmail)";
            var parameters = new DynamicParameters();
            parameters.Add("@subscriberEmail", createSubscriberDTO.SubscriberEmail);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteSubscriberAsync(int id)
        {
            string query = "DELETE FROM Subscriber WHERE SubscriberId=@subscriberId";
            var parameters = new DynamicParameters();
            parameters.Add("@subscriberId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultSubscriberDTO>> GetAllSubscriberAsync()
        {
            string query = "SELECT * FROM Subscriber";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSubscriberDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultSubscriberDTO> GetByIdSubscriberAsync(int id)
        {
            string query = "SELECT * FROM Subscriber WHERE SubscriberId=@subscriberId";
            var parameters = new DynamicParameters();
            parameters.Add("@subscriberId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultSubscriberDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateSubscriberAsync(UpdateSubscriberDTO updateSubscriberDTO)
        {
            string query = "UPDATE Subscriber SET SubscriberEmail=@subscriberEmail WHERE SubscriberId=@subscriberId";
            var parameters = new DynamicParameters();
            parameters.Add("@subscriberEmail", updateSubscriberDTO.SubscriberEmail);
            parameters.Add("@subscriberId", updateSubscriberDTO.SubscriberId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
