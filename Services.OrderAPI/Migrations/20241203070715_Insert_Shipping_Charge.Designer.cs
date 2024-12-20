﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.OrderAPI.Data;

#nullable disable

namespace Services.OrderAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241203070715_Insert_Shipping_Charge")]
    partial class Insert_Shipping_Charge
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Services.OrderAPI.Models.DetailOrder", b =>
                {
                    b.Property<long>("Order_ID")
                        .HasColumnType("bigint");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Unit_Price")
                        .HasColumnType("money");

                    b.HasKey("Order_ID", "Product_ID");

                    b.ToTable("DetailOrders");

                    b.HasData(
                        new
                        {
                            Order_ID = 1L,
                            Product_ID = 1,
                            Quantity = 2,
                            Unit_Price = 20m
                        },
                        new
                        {
                            Order_ID = 1L,
                            Product_ID = 2,
                            Quantity = 1,
                            Unit_Price = 50m
                        },
                        new
                        {
                            Order_ID = 1L,
                            Product_ID = 3,
                            Quantity = 3,
                            Unit_Price = 10m
                        },
                        new
                        {
                            Order_ID = 2L,
                            Product_ID = 1,
                            Quantity = 1,
                            Unit_Price = 30m
                        },
                        new
                        {
                            Order_ID = 2L,
                            Product_ID = 2,
                            Quantity = 2,
                            Unit_Price = 40m
                        },
                        new
                        {
                            Order_ID = 2L,
                            Product_ID = 3,
                            Quantity = 1,
                            Unit_Price = 60m
                        },
                        new
                        {
                            Order_ID = 3L,
                            Product_ID = 1,
                            Quantity = 4,
                            Unit_Price = 15m
                        },
                        new
                        {
                            Order_ID = 3L,
                            Product_ID = 2,
                            Quantity = 1,
                            Unit_Price = 70m
                        },
                        new
                        {
                            Order_ID = 3L,
                            Product_ID = 3,
                            Quantity = 2,
                            Unit_Price = 30m
                        });
                });

            modelBuilder.Entity("Services.OrderAPI.Models.Order", b =>
                {
                    b.Property<long>("Order_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Order_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Coupon_Code")
                        .HasColumnType("int");

                    b.Property<string>("Customer_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount_amount")
                        .HasColumnType("money");

                    b.Property<string>("FormOfPayment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Shipping_Charge")
                        .HasColumnType("money");

                    b.Property<decimal>("Total")
                        .HasColumnType("money");

                    b.HasKey("Order_ID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Order_ID = 1L,
                            Address = "123 Main St",
                            Customer_ID = "101",
                            Datetime = new DateTime(2024, 11, 23, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4598),
                            Discount_amount = 5m,
                            OrderStatus = "Completed",
                            Total = 150m
                        },
                        new
                        {
                            Order_ID = 2L,
                            Address = "456 Elm St",
                            Coupon_Code = 2001,
                            Customer_ID = "102",
                            Datetime = new DateTime(2024, 11, 28, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4622),
                            Discount_amount = 10m,
                            OrderStatus = "Pending",
                            Total = 200m
                        },
                        new
                        {
                            Order_ID = 3L,
                            Address = "789 Oak St",
                            Customer_ID = "103",
                            Datetime = new DateTime(2024, 12, 3, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4641),
                            Discount_amount = 0m,
                            OrderStatus = "Shipped",
                            Total = 250m
                        });
                });

            modelBuilder.Entity("Services.OrderAPI.Models.DetailOrder", b =>
                {
                    b.HasOne("Services.OrderAPI.Models.Order", "Order")
                        .WithMany("DetailOrders")
                        .HasForeignKey("Order_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Services.OrderAPI.Models.Order", b =>
                {
                    b.Navigation("DetailOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
