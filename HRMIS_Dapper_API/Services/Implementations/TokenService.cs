using Dapper;
using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;
using HRMIS_Dapper_API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMIS_Dapper_API.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<JwtTokenModel> Authenticate(UserDTO users)
        {
            try
            {
                var parameters = new { UserEmail = users.Email, UserPasseord = users.Password };

                var sql = "SELECT Id, Name, Password, Email FROM Users WHERE Email = @UserEmail And Password = @UserPasseord";
                using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

                var response = await connection.QueryAsync<UserModel>(sql, parameters);

                if (response != null && response.Any())
                {
                    var user = response.First();
                    if (user.Password == users.Password)
                    {
                        var tokenhandler = new JwtSecurityTokenHandler();
                        var tkey = Encoding.UTF8.GetBytes(_configuration["JWTToken:key"]);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                            {
                        new Claim(ClaimTypes.Email, users.Email),
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(30),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenhandler.CreateToken(tokenDescriptor);

                        return new JwtTokenModel { Token = tokenhandler.WriteToken(token) };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Rethrow the exception if needed
            }
        }
    }
}
