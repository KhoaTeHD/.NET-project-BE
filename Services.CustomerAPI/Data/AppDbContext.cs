using Microsoft.EntityFrameworkCore;
using Services.CustomerAPI.Models;

namespace Services.CustomerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Cus_Id = 1,
                Cus_Name = "John Doe",
                Cus_Avatar = "https://example.com/avatar.jpg",
                Cus_Email = "johndoe@example.com",
                Cus_Phone = "1234567890",
                Cus_Password = "password123",
                Cus_Gender = "M",
                Cus_Birthday = new DateTime(1990, 1, 1),
                Cus_Status = true
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Cus_Id = 2,
                Cus_Name = "Trà My",
                Cus_Avatar = "https://example.com/avatar.jpg",
                Cus_Email = "johndoe@example.com",
                Cus_Phone = "1234567890",
                Cus_Password = "password123",
                Cus_Gender = "M",
                Cus_Birthday = new DateTime(1990, 1, 1),
                Cus_Status = true
            });
        }
    }
}
