using System.ComponentModel.DataAnnotations;

namespace HRMIS_Dapper_API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
