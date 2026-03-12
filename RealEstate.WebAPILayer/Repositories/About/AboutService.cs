using Dapper;
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

        public async Task CreateAboutAsync(CreateAboutDTO createAboutDTO)
        {
            string query = "INSERT INTO Abouts (AboutTitle, AboutDescription) VALUES (@aboutTitle, @aboutDescription)";
            var parameters = new DynamicParameters();
            parameters.Add("@aboutTitle", createAboutDTO.AboutTitle);
            parameters.Add("@aboutDescription", createAboutDTO.AboutDescription);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAboutAsync(int id)
        {
            string query = "DELETE FROM Abouts WHERE AboutId=@aboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@aboutId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultAboutDTO>> GetAllAboutAsync()
        {
            string query = "SELECT * FROM Abouts";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAboutDTO>(query);
                return values.ToList();
            }
        }

        public async Task<ResultAboutDTO> GetByIdAboutAsync(int id)
        {
            string query = "SELECT * FROM Abouts WHERE AboutId=@aboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@aboutId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultAboutDTO>(query, parameters);
                return values;
            }
        }

        public async Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO)
        {
            string query = "UPDATE Abouts SET AboutTitle=@aboutTitle, AboutDescription=@aboutDescription WHERE AboutId=@aboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@aboutTitle", updateAboutDTO.AboutTitle);
            parameters.Add("@aboutDescription", updateAboutDTO.AboutDescription);
            parameters.Add("@aboutId", updateAboutDTO.AboutId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
