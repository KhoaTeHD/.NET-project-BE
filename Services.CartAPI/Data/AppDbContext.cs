using Microsoft.EntityFrameworkCore;
using Services.CartItemAPI.Models;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>().HasData(new CartItem
            {
                Item_Id = 1,
                Cus_Id = 1,
                Price = 20000,
                Quantity = 2,
            });

            modelBuilder.Entity<CartItem>().HasData(new CartItem
            {
                Item_Id = 2,
                Cus_Id = 1,
                Price = 90000,
                Quantity = 20,
            });
        }
    }
}
