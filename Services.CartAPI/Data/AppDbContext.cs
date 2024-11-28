using Microsoft.EntityFrameworkCore;
using Services.CartItemAPI.Models;
using Services.CartItemAPI.Models.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Reflection.Emit;

namespace Services.CartItemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Định nghĩa khóa chính phức hợp từ Item_Id và Cus_Id
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.Item_Id, ci.Cus_Id });

            // Các cấu hình khác (nếu có)
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CartItem>().HasData(new CartItem
            //{
            //    Item_Id = 1,
            //    Cus_Id = '1',
            //    Price = 20000,
            //    Quantity = 2,
            //});

            //modelBuilder.Entity<CartItem>().HasData(new CartItem
            //{
            //    Item_Id = 2,
            //    Cus_Id = 1,
            //    Price = 90000,
            //    Quantity = 20,
            //});
        }
    }
}
