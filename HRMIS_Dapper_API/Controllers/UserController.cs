using Dapper;
using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;
using HRMIS_Dapper_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace HRMIS_Dapper_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserController(ITokenService tokenService,IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult> Authenticate(UserDTO users)
        {
            var token = await _tokenService.Authenticate(users);

            if (token == null)
            {
                return Ok(new { Message = "Unauthorized" });
            }

            return Ok(token);
        }
        [Authorize]
        [HttpPost("AddUser")]
        public async Task<ActionResult> InsertUser(UserDTOInsert user)
        {
            try
            {
                var parameters = new { UserName = user.Name, UserEmail = user.Email, UserPassword = user.Password };

                var sql = "INSERT INTO Users (Name, Password, Email) VALUES (@UserName, @UserPassword, @UserEmail)";
                using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

                var response = await connection.QueryAsync<BadgeDataModel>(sql, parameters);

                var CustomResponse = new
                {
                    Message = "User Inserted"
                };

                return Ok(CustomResponse);
            }
            catch (Exception ex)
            {
                var CustomResponse = new
                {
                    Message = ex.Message
                };
                return BadRequest(CustomResponse);
            }
        }
    }
}
