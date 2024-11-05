using Microsoft.EntityFrameworkCore;
using Services.BrandAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Services.BrandAPI.Data
{
    public class AppDbContextcs : DbContext
    {
        public AppDbContextcs(DbContextOptions<AppDbContextcs> options) : base(options)
        {
        }
        public DbSet<Brand> Categories { get; set; }

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
