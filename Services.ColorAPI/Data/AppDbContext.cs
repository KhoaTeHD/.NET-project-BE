using Microsoft.EntityFrameworkCore;
using Services.ColorAPI.Models;

namespace Services.ColorAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Color>().HasData(new Color
            {
                Id = 1,
                Name = "Trắng",
                Status = true,
            });

            modelBuilder.Entity<Color>().HasData(new Color
            {
                Id = 2,
                Name = "Đen",
                Status = true,
            });
        }
    }
}
