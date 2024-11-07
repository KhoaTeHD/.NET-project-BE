using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.GoodsReceiptAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_GoodsReceipt_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Goo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_ID = table.Column<int>(type: "int", nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Goo_ID);
                });

            migrationBuilder.CreateTable(
                name: "DetailGoodsReceipts",
                columns: table => new
                {
                    Goo_ID = table.Column<int>(type: "int", nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit_Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailGoodsReceipts", x => new { x.Goo_ID, x.Product_ID });
                    table.ForeignKey(
                        name: "FK_DetailGoodsReceipts_GoodsReceipts_Goo_ID",
                        column: x => x.Goo_ID,
                        principalTable: "GoodsReceipts",
                        principalColumn: "Goo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GoodsReceipts",
                columns: new[] { "Goo_ID", "Datetime", "Supplier_ID", "Total" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 101, 5000.00m },
                    { 2, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 102, 10000.00m },
                    { 3, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 103, 15000.00m }
                });

            migrationBuilder.InsertData(
                table: "DetailGoodsReceipts",
                columns: new[] { "Goo_ID", "Product_ID", "Quantity", "Unit_Price" },
                values: new object[,]
                {
                    { 1, 201, 10, 100.00m },
                    { 1, 202, 20, 200.00m },
                    { 1, 203, 30, 300.00m },
                    { 2, 201, 10, 100.00m },
                    { 2, 202, 20, 200.00m },
                    { 2, 203, 30, 300.00m },
                    { 3, 201, 10, 100.00m },
                    { 3, 202, 20, 200.00m },
                    { 3, 203, 30, 300.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailGoodsReceipts");

            migrationBuilder.DropTable(
                name: "GoodsReceipts");
        }
    }
}
