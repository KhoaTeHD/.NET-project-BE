﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.CouponAPI.Data;

#nullable disable

namespace Services.CouponAPI.Migrations
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

            modelBuilder.Entity("Services.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<string>("Coupon_Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CouponName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Coupon_Code");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Coupon_Code = "NEWYEAR2025",
                            CouponName = "SUMMER2024",
                            Discount = 15.5f,
                            ExpirationDate = new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = true,
                            Unit = "%"
                        },
                        new
                        {
                            Coupon_Code = "20/11",
                            CouponName = "WINTER2024",
                            Discount = 20000f,
                            ExpirationDate = new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = false,
                            Unit = "nghìn đồng"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
