using RealEstate.WebAPILayer.DTOs.SubscriberDTOs;

namespace RealEstate.WebAPILayer.Repositories.Subscriber
{
    public interface ISubscriberService
    {
        Task<List<ResultSubscriberDTO>> GetAllSubscriberAsync();
        Task CreateSubscriberAsync(CreateSubscriberDTO createSubscriberDTO);
        Task UpdateSubscriberAsync(UpdateSubscriberDTO updateSubscriberDTO);
        Task DeleteSubscriberAsync(int id);
        Task<ResultSubscriberDTO> GetByIdSubscriberAsync(int id);
    }
}
