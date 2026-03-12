using RealEstate.WebAPILayer.DTOs.ProductDTOs;

namespace RealEstate.WebAPILayer.Repositories.Product
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDTO createProductDTO);
        Task UpdateProductAsync(UpdateProductDTO updateProductDTO);
        Task DeleteProductAsync(int id);
        Task<ResultProductDTO> GetByIdProductAsync(int id);
        Task<List<ResultProductWithCategoryDTO>> GetAllProductWithCategoryAsync();
        Task<List<ResultProductWithEmployeeDTO>> GetAllProductWithEmployeeAsync();
        Task<List<ResultProductWithEmployeeAndCategoryDTO>> GetAllProductWithEmployeeAndCategoryAsync();
    }
}
