using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_Id = table.Column<int>(type: "int", nullable: false),
                    Nat_Id = table.Column<int>(type: "int", nullable: false),
                    Bra_Id = table.Column<int>(type: "int", nullable: false),
                    Sup_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Col_Id = table.Column<int>(type: "int", nullable: false),
                    Siz_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImportPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductVariations",
                columns: new[] { "Id", "Col_Id", "Desc", "Discount", "ImportPrice", "Pic", "Price", "Quantity", "Siz_Id", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Áo thun cao cấp, siêu bền đẹp.", 10, 40000m, "https://res.cloudinary.com/dt46dvdeu/image/upload/v1730974763/demowebHKH/aothun/atpe0pdkpyekfcmb981n.jpg", 50000m, 200, 1, true },
                    { 2, 1, "Áo thun hoá trang DonalTrump cao cấp, siêu bền đẹp.", 8, 800000m, "https://res.cloudinary.com/dt46dvdeu/image/upload/v1730974762/demowebHKH/aothun/x3gsg9qmgtmrqxq4cnqn.jpg", 990000m, 100, 1, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Bra_Id", "Cat_Id", "Name", "Nat_Id", "Status", "Sup_Id" },
                values: new object[,]
                {
                    { 1, 1, 1, "Áo thun co giãn", 1, true, 1 },
                    { 2, 1, 2, "Áo thun Halloween", 1, true, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductVariations");
        }
    }
}
