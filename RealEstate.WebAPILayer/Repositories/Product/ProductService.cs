using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.ProductDTOs;

namespace RealEstate.WebAPILayer.Repositories.Product
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _context;

        public ProductService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            string query = "INSERT INTO Products (ProductTitle, ProductPrice,ProductCoverImage,ProductCity,ProductDistrict,ProductAddress,ProductDescription,ProductIsPopular,ProductCategory,EmployeeId) VALUES (@productTitle, @productPrice,@productCoverImage,@productCity,@productDistrict,@productAddress,@productDescription,@productIsPopular,@productCategory,@employeeId)";
            var parameters = new DynamicParameters();
            parameters.Add("@productTitle", createProductDTO.ProductTitle);
            parameters.Add("@productPrice", createProductDTO.ProductPrice);
            parameters.Add("@productCoverImage", createProductDTO.ProductCoverImage);
            parameters.Add("@productCity", createProductDTO.ProductCity);
            parameters.Add("@productDistrict", createProductDTO.ProductDistrict);
            parameters.Add("@productAddress", createProductDTO.ProductAddress);
            parameters.Add("@productDescription", createProductDTO.ProductDescription);
            parameters.Add("@productIsPopular", createProductDTO.ProductIsPopular);
            parameters.Add("@productCategory", createProductDTO.ProductCategory);
            parameters.Add("@employeeId", createProductDTO.EmployeeId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "DELETE FROM ProductDetails WHERE ProductId=@productId; DELETE FROM Products WHERE ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            string query = "SELECT * FROM Products";
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDTO>(query);
                return values.ToList();
            }
            }

        public async Task<List<ResultProductWithCategoryDTO>> GetAllProductWithCategoryAsync()
        {
            string query = "SELECT ProductId, ProductTitle,ProductPrice,ProductCoverImage,ProductCity,ProductDistrict,ProductAddress,ProductDescription,ProductIsPopular,CategoryName FROM Products INNER JOIN Categories ON Products.ProductCategory=Categories.CategoryId";
            using( var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDTO>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithEmployeeAndCategoryDTO>> GetAllProductWithEmployeeAndCategoryAsync()
        {
            string query = "SELECT ProductId, ProductTitle,ProductPrice,ProductCoverImage,ProductCity,ProductDistrict,ProductAddress,ProductDescription,ProductIsPopular,EmployeeNameSurname,CategoryName FROM Products INNER JOIN Employee ON Products.EmployeeId=Employee.EmployeeId INNER JOIN Categories ON Products.ProductCategory=Categories.CategoryId";
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithEmployeeAndCategoryDTO>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithEmployeeDTO>> GetAllProductWithEmployeeAsync()
        {
            string query = "SELECT ProductId,ProductTitle,ProductPrice,ProductCoverImage,ProductCity,ProductDistrict,ProductAddress,ProductDescription,ProductIsPopular,EmployeeNameSurname FROM Products INNER JOIN Employee ON Products.EmployeeId=Employee.EmployeeId";
            using(var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultProductWithEmployeeDTO>(query);
                return values.ToList();
            }
            
        }

        public async Task<ResultProductDTO> GetByIdProductAsync(int id)
        {
            string query = "SELECT * FROM Products WHERE ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultProductDTO>(query);
                return values;
            }
        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            string query = "UPDATE Products SET ProductTitle=@productTitle, ProductPrice=@productPrice,ProductCoverImage=@productCoverImage,ProductCity=@productCity,ProductDistrict=@productDistrict,ProductAddress=@productAddress,ProductDescription=@productDescription,ProductIsPopular=@productIsPopular,ProductCategory=@productCategory,EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@productTitle", updateProductDTO.ProductTitle);
            parameters.Add("@productPrice", updateProductDTO.ProductPrice);
            parameters.Add("@productCoverImage", updateProductDTO.ProductCoverImage);
            parameters.Add("@productCity", updateProductDTO.ProductCity);
            parameters.Add("@productDistrict", updateProductDTO.ProductDistrict);
            parameters.Add("@productAddress", updateProductDTO.ProductAddress);
            parameters.Add("@productDescription", updateProductDTO.ProductDescription);
            parameters.Add("@productIsPopular", updateProductDTO.ProductIsPopular);
            parameters.Add("@productCategory", updateProductDTO.ProductCategory);
            parameters.Add("@employeeId", updateProductDTO.EmployeeId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
