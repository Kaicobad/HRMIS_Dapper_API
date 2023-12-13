using HRMIS_Dapper_API.ApiAttribute;
using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;
using HRMIS_Dapper_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMIS_Dapper_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunchDatasController : ControllerBase
    {
        private readonly IBadgeDataService _service;

        public PunchDatasController(IBadgeDataService service)
        {
            this._service = service;
        }
        [ApiKey]
        [HttpGet("GetLastThreeDaysPunchData")]
        public async Task<ResponseDTo<BadgeDataModel>> GetLaastThreeDaysPunchByDate(string date)
        {
            try
            {
                var response = await _service.GetPunchdataLastThreeDaysDate(date);
                return new ResponseDTo<BadgeDataModel>
                {
                    Success = true,
                    Message = "Punch Data Generated !",
                    Data = response.ToList(),
                };
            }
            catch (Exception ex)
            {
                //log error
                return new ResponseDTo<BadgeDataModel>
                {
                    Success = false,
                    Message = "Punch Data Generated !",
                    Data = null,
                };
            }
        }

    }
}
