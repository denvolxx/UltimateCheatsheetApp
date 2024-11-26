using DBModels;
using Microsoft.EntityFrameworkCore;


namespace DBService.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
