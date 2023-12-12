using System.Data.SqlClient;
using System.Data;

namespace HRMIS_Dapper_API.Services.Dapper
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Constr");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
