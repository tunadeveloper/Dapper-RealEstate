using RealEstate.WebAPILayer.DTOs.ProductDetailDTOs;

namespace RealEstate.WebAPILayer.Repositories.ProductDetail
{
    public interface IProductDetailService
    {
        Task<ResultProductDetailDTO> GetAllProductDetailAsync(int id);
        Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO);
        Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO);
        Task DeleteProductDetailAsync(int id);
        Task<ResultProductDetailDTO> GetByIdProductDetailAsync(int id);
    }
}
