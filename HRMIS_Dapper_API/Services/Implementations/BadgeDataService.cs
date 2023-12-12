using Dapper;
using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;
using HRMIS_Dapper_API.Services.Dapper;
using HRMIS_Dapper_API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace HRMIS_Dapper_API.Services.Implementations
{
    public class BadgeDataService : IBadgeDataService
    {
        private readonly DapperContext _context;

        public BadgeDataService(DapperContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<BadgeDataModel>> GetPunchdatLastThreeDays()
        {
            try
            {

                var sql = "SELECT punch_date as PunchDate,card_no as PunchNo,terminal as DeviceNo FROM badge_data WHERE punch_date BETWEEN DATEADD(DAY, - 3,GETDATE()) AND GETDATE()  ORDER BY punch_date";
                //using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

                using var connection = _context.CreateConnection();
                var hrmCompanyId = await connection.QueryAsync(sql);

                var response = await connection.QueryAsync<BadgeDataModel>(sql);
                return response.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
