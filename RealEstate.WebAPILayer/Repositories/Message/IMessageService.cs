using RealEstate.WebAPILayer.DTOs.MessageDTOs;

namespace RealEstate.WebAPILayer.Repositories.Message
{
    public interface IMessageService
    {
        Task<List<ResultMessageDTO>> GetAllMessageAsync();
        Task CreateMessageAsync(CreateMessageDTO createMessageDTO);
        Task UpdateMessageAsync(UpdateMessageDTO updateMessageDTO);
        Task DeleteMessageAsync(int id);
        Task<ResultMessageDTO> GetByIdMessageAsync(int id);
    }
}
