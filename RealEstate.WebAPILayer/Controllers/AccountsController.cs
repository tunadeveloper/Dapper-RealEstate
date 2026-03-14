using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.DTOs.AccountDTOs;
using RealEstate.WebAPILayer.Tools;

namespace RealEstate.WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DapperContext _context;

        public AccountsController(DapperContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(CreateLoginDTO dto)
        {
            string query = "SELECT * FROM Users WHERE Username=@username and Password=@password";
            var parameters = new DynamicParameters();
            parameters.Add("@username", dto.Username);
            parameters.Add("@password", dto.Password);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync(query, parameters);
                if (values != null)
                {
                    GetCheckAppUser model = new GetCheckAppUser();
                    model.Username = values.Username;
                    model.Id = values.UserId;
                    var token = JwtTokenGenerator.GenerateToken(model);
                    return Ok(token);
                }
                return Ok("Başarısız");
        }
    }
}
}