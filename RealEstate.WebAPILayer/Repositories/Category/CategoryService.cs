using Dapper;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.CategoryDTOs;

namespace RealEstate.WebAPILayer.Repositories.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly DapperContext _context;

        public CategoryService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            string query = "INSERT INTO Categories (CategoryName, CategoryStatus) VALUES (@categoryName, @categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", createCategoryDTO.CategoryName);
            parameters.Add("@categoryStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            string query = "DELETE FROM ProductDetails WHERE ProductId IN (SELECT ProductId FROM Products WHERE ProductCategory=@categoryId); DELETE FROM Products WHERE ProductCategory=@categoryId; DELETE FROM Categories WHERE CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            string query = "SELECT * FROM Categories";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultCategoryDTO> GetByIdCategoryAsync(int id)
        {
            string query = "SELECT * FROM Categories WHERE CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("categoryId", id);
            using(var connection = _context.CreateConnection())
            {
               var values = await connection.QueryFirstOrDefaultAsync<ResultCategoryDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            string query = "UPDATE Categories SET CategoryName=@categoryName, CategoryStatus=@categoryStatus WHERE CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", updateCategoryDTO.CategoryName);
            parameters.Add("@categoryStatus", updateCategoryDTO.CategoryStatus);
            parameters.Add("@categoryId", updateCategoryDTO.CategoryId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
