using DBService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace DBService.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
