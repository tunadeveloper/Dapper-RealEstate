using RealEstate.WebAPILayer.DTOs.CategoryDTOs;

namespace RealEstate.WebAPILayer.Repositories.Category
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync();
    }
}
