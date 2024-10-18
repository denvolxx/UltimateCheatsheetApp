using ApplicationDTO.Users.Enums;
using System.Reflection;

namespace ApplicationDTO.Users
{
    public class AddUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
