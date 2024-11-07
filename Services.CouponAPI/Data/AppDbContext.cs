using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.Models;

namespace Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Coupon_Code = 1,
                StartDate = new DateTime(2024, 11, 1),
                ExpirationDate = new DateTime(2024, 12, 1),
                CouponName = "SUMMER2024",
                Discount = 15.5f,
                Unit = "%",
                Status = true
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Coupon_Code = 2,
                StartDate = new DateTime(2024, 11, 15),
                ExpirationDate = new DateTime(2025, 1, 15),
                CouponName = "WINTER2024",
                Discount = 20000,
                Unit = "nghìn đồng",
                Status = false
            });
        }

    }
}
