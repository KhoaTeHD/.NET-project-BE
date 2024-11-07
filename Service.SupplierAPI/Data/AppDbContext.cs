using Microsoft.EntityFrameworkCore;
using Services.SupplierAPI.Models;

namespace Service.SupplierAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>().HasData(new Supplier
            {
                Supplier_ID = 1,
                SupplierName = "ABC Supplies",
                Address = "123 Main St, New York, NY",
                PhoneNumber = "9876543210",
                Status = true
            });

            modelBuilder.Entity<Supplier>().HasData(new Supplier
            {
                Supplier_ID = 2,
                SupplierName = "Global Traders",
                Address = "456 Elm St, Los Angeles, CA",
                PhoneNumber = "0123456789",
                Status = false
            });
        }

    }
}
