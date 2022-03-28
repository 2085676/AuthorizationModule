using Microsoft.EntityFrameworkCore;

namespace AuthorizationModule.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
