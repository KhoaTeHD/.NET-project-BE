using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.AddressAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_Change_CustomerID_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Customer_ID",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Address_ID",
                keyValue: 1,
                column: "Customer_ID",
                value: "CU0001");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Address_ID",
                keyValue: 2,
                column: "Customer_ID",
                value: "CU0002");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Customer_ID",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Address_ID",
                keyValue: 1,
                column: "Customer_ID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Address_ID",
                keyValue: 2,
                column: "Customer_ID",
                value: 2);
        }
    }
}
