using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.AboutDTOs;

namespace RealEstate.WebAPILayer.Repositories.About
{
    public class AboutService : IAboutService
    {
        private readonly DapperContext _context;

        public AboutService(DapperContext context)
        {
            _context = context;
        }

        public Task CreateAboutAsync(CreateAboutDTO createAboutDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAboutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultAboutDTO>> GetAllAboutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultAboutDTO> GetByIdAboutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO)
        {
            throw new NotImplementedException();
        }
    }
}
