using DBService.Models.Enums;

namespace DBService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
