using ApplicationDTO.MSSQL.Users.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApplicationDTO.MSSQL.Users
{
    public class RegisterUserDTO
    {
        [MaxLength(16)]
        public required string Name { get; set; }

        [MinLength(4)]
        public required string Password { get; set; }

        public required GenderEnum Gender { get; set; }
    }
}
