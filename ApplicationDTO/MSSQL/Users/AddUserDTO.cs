using ApplicationDTO.MSSQL.Users.Enums;
using System.Reflection;

namespace ApplicationDTO.MSSQL.Users
{
    public class AddUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
