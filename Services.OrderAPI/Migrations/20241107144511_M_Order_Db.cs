using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_Order_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    Coupon_Code = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount_amount = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_ID);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrders",
                columns: table => new
                {
                    Order_ID = table.Column<long>(type: "bigint", nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit_Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrders", x => new { x.Order_ID, x.Product_ID });
                    table.ForeignKey(
                        name: "FK_DetailOrders_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Order_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Order_ID", "Address", "Coupon_Code", "Customer_ID", "Datetime", "Discount_amount", "OrderStatus", "Total" },
                values: new object[,]
                {
                    { 1L, "123 Main St", null, 101, new DateTime(2024, 10, 28, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(33), 5m, "Completed", 150m },
                    { 2L, "456 Elm St", 2001, 102, new DateTime(2024, 11, 2, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(52), 10m, "Pending", 200m },
                    { 3L, "789 Oak St", null, 103, new DateTime(2024, 11, 7, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(54), 0m, "Shipped", 250m }
                });

            migrationBuilder.InsertData(
                table: "DetailOrders",
                columns: new[] { "Order_ID", "Product_ID", "Quantity", "Unit_Price" },
                values: new object[,]
                {
                    { 1L, 1, 2, 20m },
                    { 1L, 2, 1, 50m },
                    { 1L, 3, 3, 10m },
                    { 2L, 1, 1, 30m },
                    { 2L, 2, 2, 40m },
                    { 2L, 3, 1, 60m },
                    { 3L, 1, 4, 15m },
                    { 3L, 2, 1, 70m },
                    { 3L, 3, 2, 30m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailOrders");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
