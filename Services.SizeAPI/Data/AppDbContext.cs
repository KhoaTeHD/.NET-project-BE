using Microsoft.EntityFrameworkCore;
using Services.SizeAPI.Models;

namespace Services.SizeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Size>().HasData(new Size
            {
                Id = 1,
                Name = "S",
                Desc = "Size S, đây là size nhỏ nhất. S nghĩa là Small.",
                Status = true,
            });

            modelBuilder.Entity<Size>().HasData(new Size
            {
                Id = 2,
                Name = "M",
                Desc = "Size M, đây là size trung bình. M nghĩa là Medium.",
                Status = true,
            });
        }
    }
}
