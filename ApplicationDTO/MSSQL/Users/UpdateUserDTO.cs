using ApplicationDTO.MSSQL.Users.Enums;

namespace ApplicationDTO.MSSQL.Users
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
