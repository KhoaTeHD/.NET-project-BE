using Microsoft.EntityFrameworkCore;
using Services.CategoryAPI.Models;

namespace Services.CategoryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Gucci",
                Description = "This is Gucci",
                Status = true,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                Name = "Fendi",
                Description = "This is Fendi",
                Status = true,
            });
        }
    }
}
