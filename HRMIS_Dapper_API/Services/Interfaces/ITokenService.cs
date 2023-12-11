using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;

namespace HRMIS_Dapper_API.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JwtTokenModel> Authenticate(UserDTO users);
    }
}
