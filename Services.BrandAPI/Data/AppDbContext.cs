using Microsoft.EntityFrameworkCore;
using Services.BrandAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Services.BrandAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                Id = 1,
                Name = "Hades",
                Status = true,
            });

            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                Id = 2,
                Name = "Dirty Coins",
                Status = true,
            });
        }
    }
}
