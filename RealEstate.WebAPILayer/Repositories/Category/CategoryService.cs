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

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            string query = "SELECT * FROM Categories";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDTO>(query);
                return values.ToList();
            }
        }
    }
}
