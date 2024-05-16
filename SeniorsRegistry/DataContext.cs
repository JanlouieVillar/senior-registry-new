using Microsoft.EntityFrameworkCore;

namespace SeniorsRegistry
{
    public class DataContext : DbContext
    {
        // overide a method found in DBContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = SeniorDB.db");
            // the parameter is the connection string
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Senior> Seniors { get; set; }

    }
}