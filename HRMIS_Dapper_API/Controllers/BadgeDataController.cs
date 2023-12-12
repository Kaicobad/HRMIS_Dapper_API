using Dapper;
using HRMIS_Dapper_API.DTO;
using HRMIS_Dapper_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace HRMIS_Dapper_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BadgeDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Commented Working Onece For Client
        //[Authorize]
        //[HttpGet("GetBadges")]
        //public async Task<ActionResult<List<BadgeDataModel>>> GetAllBadgedata()
        //{
        //    using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));
        //    var response = await connection.QueryAsync<BadgeDataModel>("select top 1 punch_date as PunchDate,card_no as PunchNo,terminal as DeviceNo from badge_data");
        //    return Ok(response);
        //}
        //[Authorize]
        //[HttpPost("DaterangeBadgeData")]
        //public async Task<ResponseDTo<BadgeDataModel>> GetByDateRange(string fromDate, string toDate)
        //{
        //    DateTime fromDateValue;
        //    DateTime toDateValue;

        //    CultureInfo culture = new CultureInfo("en-US");
        //    fromDateValue = Convert.ToDateTime(fromDate, culture);
        //    toDateValue = Convert.ToDateTime(toDate, culture);

        //    int daysDifference = (int)(toDateValue - fromDateValue).TotalDays;

        //    if (daysDifference <= 30)
        //    {
        //        try
        //        {
        //            var parameters = new { DateFrom = fromDate, DateTo = toDate };

        //            var sql = "SELECT punch_date as PunchDate,card_no as PunchNo,terminal as DeviceNo FROM badge_data WHERE punch_date BETWEEN @DateFrom AND @DateTo";
        //            using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

        //            var response = await connection.QueryAsync<BadgeDataModel>(sql, parameters);
        //            return new ResponseDTo<BadgeDataModel>
        //            {
        //                Success = true,
        //                Message = "Badge Datas Generated",
        //                Data = response.ToList(),
        //            };
        //        }
        //        catch (Exception ex)
        //        {

        //            return new ResponseDTo<BadgeDataModel>
        //            {
        //                Success = false,
        //                Message = ex.Message,
        //                Data = null,
        //            };
        //        }
        //    }
        //    else
        //    {
        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = false,
        //            Message = "Selected Dates Counting More Then 10 Days, Please Select Dates In Betwween or equal 30 days Approx!",
        //            Data = null,
        //        };
        //    }

        //}

        //[Authorize]
        //[HttpPost("BadgeDataByPunchId")]
        //public async Task<ResponseDTo<BadgeDataModel>> GetBadgeDataByPunchNo(string PunchNo)
        //{
        //    try
        //    {
        //        var parameters = new { PunchNo = PunchNo };

        //        var sql = "select punch_date as PunchDate,card_no as PunchNo,terminal as DeviceNo from badge_data where card_no = @PunchNo ";
        //        using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

        //        var response = await connection.QueryAsync<BadgeDataModel>(sql, parameters);
        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = true,
        //            Message = "Badge Datas Generated and Filterd By Punch No.",
        //            Data = response.ToList(),
        //        }; ;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = false,
        //            Message = ex.Message,
        //            Data = null,
        //        }; ;
        //    }

        //}

        //[HttpPost("GetByDateRangePagination")]
        //public async Task<ResponseDTo<BadgeDataModel>> GetByDateRange2(string fromDate, string toDate, int pageNumber, int pageSize)
        //{
        //    DateTime fromDateValue;
        //    DateTime toDateValue;

        //    CultureInfo culture = new CultureInfo("en-US");
        //    fromDateValue = Convert.ToDateTime(fromDate, culture);
        //    toDateValue = Convert.ToDateTime(toDate, culture);

        //    int daysDifference = (int)(toDateValue - fromDateValue).TotalDays;

        //    if (daysDifference <= 30)
        //    {
        //        try
        //        {
        //            var parameters = new
        //            {
        //                DateFrom = fromDate,
        //                DateTo = toDate,
        //                Offset = (pageNumber - 1) * pageSize,
        //                PageSize = pageSize
        //            };

        //            var sql = @"SELECT punch_date as PunchDate, card_no as PunchNo, terminal as DeviceNo FROM badge_data 
        //                              WHERE punch_date BETWEEN @DateFrom AND @DateTo ORDER BY punch_date OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        //            using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

        //            var response = await connection.QueryAsync<BadgeDataModel>(sql, parameters);
        //            return new ResponseDTo<BadgeDataModel>
        //            {
        //                Success = true,
        //                Message = "Badge Datas Generated",
        //                Data = response.ToList(),
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new ResponseDTo<BadgeDataModel>
        //            {
        //                Success = false,
        //                Message = ex.Message,
        //                Data = null,
        //            };
        //        }
        //    }
        //    else
        //    {
        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = false,
        //            Message = "Selected Dates Counting More Than 10 Days, Please Select Dates In Between or equal to 10 days Approx!",
        //            Data = null,
        //        };
        //    }
        //}
        //[Authorize]
        //[HttpGet("GetLastThreeDays")]
        //public async Task<ResponseDTo<BadgeDataModel>> GetlastThree()
        //{
        //    try
        //    {
        //        var sql = "SELECT punch_date as PunchDate,card_no as PunchNo,terminal as DeviceNo FROM badge_data WHERE punch_date BETWEEN DATEADD(DAY, - 3,GETDATE()) AND GETDATE()  ORDER BY punch_date";
        //        using var connection = new SqlConnection(_configuration.GetConnectionString("Constr"));

        //        var response = await connection.QueryAsync<BadgeDataModel>(sql);
        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = true,
        //            Message = "Badge Datas Generated",
        //            Data = response.ToList(),
        //        };
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ResponseDTo<BadgeDataModel>
        //        {
        //            Success = false,
        //            Message = ex.Message,
        //            Data = null,
        //        };
        //    }

        //}
        #endregion


    }
}
