using Microsoft.EntityFrameworkCore;
using Services.GoodsReceiptAPI.Models;

namespace Services.GoodsReceiptAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<DetailGoodsReceipt> DetailGoodsReceipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Thiết lập khóa chính kết hợp
            modelBuilder.Entity<DetailGoodsReceipt>()
                .HasKey(dgr => new { dgr.Goo_ID, dgr.Product_ID });

            // Seed data for GoodsReceipt
            for (int i = 1; i <= 3; i++)
            {
                modelBuilder.Entity<GoodsReceipt>().HasData(new GoodsReceipt
                {
                    Goo_ID = i,
                    Supplier_ID = 100 + i,
                    Datetime = new DateTime(2024, 11, i),
                    Total = 5000.00m * i
                });

                // Seed data for DetailGoodsReceipt
                for (int j = 1; j <= 3; j++)
                {
                    modelBuilder.Entity<DetailGoodsReceipt>().HasData(new DetailGoodsReceipt
                    {
                        Goo_ID = i, // Liên kết với GoodsReceipt
                        Product_ID = 200 + j,
                        Quantity = 10 * j,
                        Unit_Price = 100.00m * j
                    });
                }
            }
        }

    }
}
