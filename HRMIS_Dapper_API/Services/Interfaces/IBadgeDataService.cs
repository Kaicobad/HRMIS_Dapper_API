using HRMIS_Dapper_API.Models;

namespace HRMIS_Dapper_API.Services.Interfaces
{
    public interface IBadgeDataService
    {
        public Task<IEnumerable<BadgeDataModel>> GetPunchdatLastThreeDays();
    }
}
