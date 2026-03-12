using RealEstate.WebAPILayer.DTOs.MessageDTOs;

namespace RealEstate.WebAPILayer.Repositories.Message
{
    public class MessageService : IMessageService
    {
        public Task CreateMessageAsync(CreateMessageDTO createMessageDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultMessageDTO>> GetAllMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultMessageDTO> GetByIdMessageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessageAsync(UpdateMessageDTO updateMessageDTO)
        {
            throw new NotImplementedException();
        }
    }
}
