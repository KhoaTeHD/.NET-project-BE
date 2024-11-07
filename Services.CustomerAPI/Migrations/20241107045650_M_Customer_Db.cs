using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.CustomerAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_Customer_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Cus_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cus_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cus_Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cus_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cus_Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Cus_Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cus_Gender = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Cus_Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cus_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Cus_Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Cus_Id", "Cus_Avatar", "Cus_Birthday", "Cus_Email", "Cus_Gender", "Cus_Name", "Cus_Password", "Cus_Phone", "Cus_Status" },
                values: new object[,]
                {
                    { 1, "https://example.com/avatar.jpg", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", "M", "John Doe", "password123", "1234567890", true },
                    { 2, "https://example.com/avatar.jpg", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", "M", "Trà My", "password123", "1234567890", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
