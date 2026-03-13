using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.ProductDetailDTOs;

namespace RealEstate.WebAPILayer.Repositories.ProductDetail
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly DapperContext _context;

        public ProductDetailService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO)
        {
            string query = "INSERT INTO ProductDetails (ProductId, ProductSize, ProductBedRoomCount, ProductBathCount, ProductRoomCount, ProductGarageSize, ProductBuildYear, ProductPrice, ProductLocation) VALUES (@productId, @productSize, @productBedRoomCount, @productBathCount, @productRoomCount, @productGarageSize, @productBuildYear, @productPrice, @productLocation)";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", createProductDetailDTO.ProductId);
            parameters.Add("@productSize", createProductDetailDTO.ProductSize);
            parameters.Add("@productBedRoomCount", createProductDetailDTO.ProductBedRoomCount);
            parameters.Add("@productBathCount", createProductDetailDTO.ProductBathCount);
            parameters.Add("@productRoomCount", createProductDetailDTO.ProductRoomCount);
            parameters.Add("@productGarageSize", createProductDetailDTO.ProductGarageSize);
            parameters.Add("@productBuildYear", createProductDetailDTO.ProductBuildYear);
            parameters.Add("@productPrice", createProductDetailDTO.ProductPrice);
            parameters.Add("@productLocation", createProductDetailDTO.ProductLocation);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteProductDetailAsync(int id)
        {
            string query = "DELETE FROM ProductDetails WHERE ProductDetailId=@productDetailId";
            var parameters = new DynamicParameters();
            parameters.Add("@productDetailId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<ResultProductDetailDTO> GetAllProductDetailAsync(int id)
        {
            string query = "SELECT * FROM ProductDetails INNER JOIN Products ON ProductDetails.ProductId=Products.ProductId WHERE ProductDetails.ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultProductDetailDTO>(query, parameters);
                return values;
            }
        }

        public async Task<ResultProductDetailDTO> GetByIdProductDetailAsync(int id)
        {
            string query = "SELECT * FROM ProductDetails WHERE ProductDetailId=@productDetailId";
            var parameters = new DynamicParameters();
            parameters.Add("@productDetailId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultProductDetailDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO)
        {
            string query = "UPDATE ProductDetails SET ProductId=@productId, ProductSize=@productSize, ProductBedRoomCount=@productBedRoomCount, ProductBathCount=@productBathCount, ProductRoomCount=@productRoomCount, ProductGarageSize=@productGarageSize, ProductBuildYear=@productBuildYear, ProductPrice=@productPrice, ProductLocation=@productLocation WHERE ProductDetailId=@productDetailId";
            var parameters = new DynamicParameters();
            parameters.Add("@productDetailId", updateProductDetailDTO.ProductDetailId);
            parameters.Add("@productId", updateProductDetailDTO.ProductId);
            parameters.Add("@productSize", updateProductDetailDTO.ProductSize);
            parameters.Add("@productBedRoomCount", updateProductDetailDTO.ProductBedRoomCount);
            parameters.Add("@productBathCount", updateProductDetailDTO.ProductBathCount);
            parameters.Add("@productRoomCount", updateProductDetailDTO.ProductRoomCount);
            parameters.Add("@productGarageSize", updateProductDetailDTO.ProductGarageSize);
            parameters.Add("@productBuildYear", updateProductDetailDTO.ProductBuildYear);
            parameters.Add("@productPrice", updateProductDetailDTO.ProductPrice);
            parameters.Add("@productLocation", updateProductDetailDTO.ProductLocation);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
