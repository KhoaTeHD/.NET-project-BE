using Microsoft.EntityFrameworkCore;
using Services.AddressAPI.Models;

namespace Services.AddressAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(new Address
            {
                Address_ID = 1,
                Customer_ID = "CU0001",
                AddressLine = "123 Main St, District 1",
                Province = "Hồ Chí Minh",
                Ward = "Ward 1",
                District = "District 1",
                Phone = "0123456789",
                Name = "John Doe",
                IsDefault = true
            });

            modelBuilder.Entity<Address>().HasData(new Address
            {
                Address_ID = 2,
                Customer_ID = "CU0002",
                AddressLine = "456 Second Ave, District 3",
                Province = "Hà Nội",
                Ward = "Ward 5",
                District = "District 3",
                Phone = "0987654321",
                Name = "Trà My",
                IsDefault = false
            });
        }

    }
}
