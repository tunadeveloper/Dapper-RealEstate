using RealEstate.WebAPILayer.DTOs.CategoryDTOs;

namespace RealEstate.WebAPILayer.Repositories.Category
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(int id);
        Task<ResultCategoryDTO> GetByIdCategoryAsync(int id);

    }
}
