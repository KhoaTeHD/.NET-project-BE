using Microsoft.EntityFrameworkCore;
using Services.OrderAPI.Models;

namespace Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<DetailOrder> DetailOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Thiết lập khóa chính kết hợp
            modelBuilder.Entity<DetailOrder>()
                .HasKey(dgr => new { dgr.Order_ID, dgr.Product_ID });

            //modelBuilder.Entity<DetailOrder>()
            //    .HasOne(dgr => dgr.Order)
            //    .WithMany(gr => gr.DetailOrders)
            //    .HasForeignKey(dgr => dgr.Order_ID)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Order_ID = 1,
                    Customer_ID = "101",
                    Coupon_Code = null,
                    Address = "123 Main St",
                    Datetime = DateTime.Now.AddDays(-10),
                    Discount_amount = 5,
                    Total = 150,
                    OrderStatus = "Completed"
                },
                new Order
                {
                    Order_ID = 2,
                    Customer_ID = "102",
                    Coupon_Code = 2001,
                    Address = "456 Elm St",
                    Datetime = DateTime.Now.AddDays(-5),
                    Discount_amount = 10,
                    Total = 200,
                    OrderStatus = "Pending"
                },
                new Order
                {
                    Order_ID = 3,
                    Customer_ID = "103",
                    Coupon_Code = null,
                    Address = "789 Oak St",
                    Datetime = DateTime.Now,
                    Discount_amount = 0,
                    Total = 250,
                    OrderStatus = "Shipped"
                }
            );

            modelBuilder.Entity<DetailOrder>().HasData(
                // Order 1 - Details
                new DetailOrder { Order_ID = 1, Product_ID = 1, Quantity = 2, Unit_Price = 20 },
                new DetailOrder { Order_ID = 1, Product_ID = 2, Quantity = 1, Unit_Price = 50 },
                new DetailOrder { Order_ID = 1, Product_ID = 3, Quantity = 3, Unit_Price = 10 },

                // Order 2 - Details
                new DetailOrder { Order_ID = 2, Product_ID = 1, Quantity = 1, Unit_Price = 30 },
                new DetailOrder { Order_ID = 2, Product_ID = 2, Quantity = 2, Unit_Price = 40 },
                new DetailOrder { Order_ID = 2, Product_ID = 3, Quantity = 1, Unit_Price = 60 },

                // Order 3 - Details
                new DetailOrder { Order_ID = 3, Product_ID = 1, Quantity = 4, Unit_Price = 15 },
                new DetailOrder { Order_ID = 3, Product_ID = 2, Quantity = 1, Unit_Price = 70 },
                new DetailOrder { Order_ID = 3, Product_ID = 3, Quantity = 2, Unit_Price = 30 }
            );
        }
    }
}
