using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.MessageDTOs;

namespace RealEstate.WebAPILayer.Repositories.Message
{
    public class MessageService : IMessageService
    {
        private readonly DapperContext _context;

        public MessageService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateMessageAsync(CreateMessageDTO createMessageDTO)
        {
            string query = "INSERT INTO Messages (MessageNameSurname, MessageEmail, MessageSubject, MessageDetail) VALUES (@messageNameSurname, @messageEmail, @messageSubject, @messageDetail)";
            var parameters = new DynamicParameters();
            parameters.Add("@messageNameSurname", createMessageDTO.MessageNameSurname);
            parameters.Add("@messageEmail", createMessageDTO.MessageEmail);
            parameters.Add("@messageSubject", createMessageDTO.MessageSubject);
            parameters.Add("@messageDetail", createMessageDTO.MessageDetail);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            string query = "DELETE FROM Messages WHERE MessageId=@messageId";
            var parameters = new DynamicParameters();
            parameters.Add("@messageId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultMessageDTO>> GetAllMessageAsync()
        {
            string query = "SELECT * FROM Messages";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultMessageDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultMessageDTO> GetByIdMessageAsync(int id)
        {
            string query = "SELECT * FROM Messages WHERE MessageId=@messageId";
            var parameters = new DynamicParameters();
            parameters.Add("@messageId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultMessageDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateMessageAsync(UpdateMessageDTO updateMessageDTO)
        {
            string query = "UPDATE Messages SET MessageNameSurname=@messageNameSurname, MessageEmail=@messageEmail, MessageSubject=@messageSubject, MessageDetail=@messageDetail WHERE MessageId=@messageId";
            var parameters = new DynamicParameters();
            parameters.Add("@messageNameSurname", updateMessageDTO.MessageNameSurname);
            parameters.Add("@messageEmail", updateMessageDTO.MessageEmail);
            parameters.Add("@messageSubject", updateMessageDTO.MessageSubject);
            parameters.Add("@messageDetail", updateMessageDTO.MessageDetail);
            parameters.Add("@messageId", updateMessageDTO.MessageId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
