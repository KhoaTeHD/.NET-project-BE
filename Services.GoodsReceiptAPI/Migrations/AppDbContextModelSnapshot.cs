﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.GoodsReceiptAPI.Data;

#nullable disable

namespace Services.GoodsReceiptAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Services.GoodsReceiptAPI.Models.DetailGoodsReceipt", b =>
                {
                    b.Property<int>("Goo_ID")
                        .HasColumnType("int");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Unit_Price")
                        .HasColumnType("money");

                    b.HasKey("Goo_ID", "Product_ID");

                    b.ToTable("DetailGoodsReceipts");

                    b.HasData(
                        new
                        {
                            Goo_ID = 1,
                            Product_ID = 201,
                            Quantity = 10,
                            Unit_Price = 100.00m
                        },
                        new
                        {
                            Goo_ID = 1,
                            Product_ID = 202,
                            Quantity = 20,
                            Unit_Price = 200.00m
                        },
                        new
                        {
                            Goo_ID = 1,
                            Product_ID = 203,
                            Quantity = 30,
                            Unit_Price = 300.00m
                        },
                        new
                        {
                            Goo_ID = 2,
                            Product_ID = 201,
                            Quantity = 10,
                            Unit_Price = 100.00m
                        },
                        new
                        {
                            Goo_ID = 2,
                            Product_ID = 202,
                            Quantity = 20,
                            Unit_Price = 200.00m
                        },
                        new
                        {
                            Goo_ID = 2,
                            Product_ID = 203,
                            Quantity = 30,
                            Unit_Price = 300.00m
                        },
                        new
                        {
                            Goo_ID = 3,
                            Product_ID = 201,
                            Quantity = 10,
                            Unit_Price = 100.00m
                        },
                        new
                        {
                            Goo_ID = 3,
                            Product_ID = 202,
                            Quantity = 20,
                            Unit_Price = 200.00m
                        },
                        new
                        {
                            Goo_ID = 3,
                            Product_ID = 203,
                            Quantity = 30,
                            Unit_Price = 300.00m
                        });
                });

            modelBuilder.Entity("Services.GoodsReceiptAPI.Models.GoodsReceipt", b =>
                {
                    b.Property<int>("Goo_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Goo_ID"));

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Supplier_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("money");

                    b.HasKey("Goo_ID");

                    b.ToTable("GoodsReceipts");

                    b.HasData(
                        new
                        {
                            Goo_ID = 1,
                            Datetime = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Supplier_ID = 101,
                            Total = 5000.00m
                        },
                        new
                        {
                            Goo_ID = 2,
                            Datetime = new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Supplier_ID = 102,
                            Total = 10000.00m
                        },
                        new
                        {
                            Goo_ID = 3,
                            Datetime = new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Supplier_ID = 103,
                            Total = 15000.00m
                        });
                });

            modelBuilder.Entity("Services.GoodsReceiptAPI.Models.DetailGoodsReceipt", b =>
                {
                    b.HasOne("Services.GoodsReceiptAPI.Models.GoodsReceipt", "GoodsReceipt")
                        .WithMany("DetailGoodsReceipts")
                        .HasForeignKey("Goo_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoodsReceipt");
                });

            modelBuilder.Entity("Services.GoodsReceiptAPI.Models.GoodsReceipt", b =>
                {
                    b.Navigation("DetailGoodsReceipts");
                });
#pragma warning restore 612, 618
        }
    }
}