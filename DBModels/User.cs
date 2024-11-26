using DBModels.Enums;

namespace DBModels
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? City { get; set; }
        public string? Country { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
    }
}
