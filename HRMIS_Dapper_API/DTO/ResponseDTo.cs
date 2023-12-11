using HRMIS_Dapper_API.Models;

namespace HRMIS_Dapper_API.DTO
{
    public class ResponseDTo<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<T> Data { get; set; }
    }
}
