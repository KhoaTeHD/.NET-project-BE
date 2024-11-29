﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.CartItemAPI.Data;

#nullable disable

namespace Services.CartItemAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241129113642_updateCart")]
    partial class updateCart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Services.CartItemAPI.Models.CartItem", b =>
                {
                    b.Property<int>("Item_Id")
                        .HasColumnType("int");

                    b.Property<string>("Cus_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Item_Id", "Cus_Id");

                    b.ToTable("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}