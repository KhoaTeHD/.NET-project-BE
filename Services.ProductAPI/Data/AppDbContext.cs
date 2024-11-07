using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Models;

namespace Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Cat_Id = 1,
                Nat_Id = 1,
                Bra_Id = 1,
                Sup_Id = 1,
                Name = "Áo thun co giãn",
                Status = true,
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Cat_Id = 2,
                Nat_Id = 1,
                Bra_Id = 1,
                Sup_Id = 2,
                Name = "Áo thun Halloween",
                Status = true,
            });

            modelBuilder.Entity<ProductVariation>().HasData(new ProductVariation
            {
                Id = 1,
                Col_Id = 1,
                Siz_Id = 1,
                Price = 50000,
                ImportPrice = 40000,
                Pic = "https://res.cloudinary.com/dt46dvdeu/image/upload/v1730974763/demowebHKH/aothun/atpe0pdkpyekfcmb981n.jpg",
                Quantity = 200,
                Desc = "Áo thun cao cấp, siêu bền đẹp.",
                Discount = 10,
                Status = true,
            });

            modelBuilder.Entity<ProductVariation>().HasData(new ProductVariation
            {
                Id = 2,
                Col_Id = 1,
                Siz_Id = 1,
                Price = 990000,
                ImportPrice = 800000,
                Pic = "https://res.cloudinary.com/dt46dvdeu/image/upload/v1730974762/demowebHKH/aothun/x3gsg9qmgtmrqxq4cnqn.jpg",
                Quantity = 100,
                Desc = "Áo thun hoá trang DonalTrump cao cấp, siêu bền đẹp.",
                Discount = 8,
                Status = true,
            });
        }
    }
}
