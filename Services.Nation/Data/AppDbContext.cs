using Microsoft.EntityFrameworkCore;
using Services.NationAPI.Models;
namespace Services.NationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Nation> Nations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Nation>().HasData(new Nation
            {
                Id = 1,
                Name = "Việt Nam",
                Status = true,
            });

            modelBuilder.Entity<Nation>().HasData(new Nation
            {
                Id = 2,
                Name = "Trung Quốc",
                Status = true,
            });
        }
    }
}
