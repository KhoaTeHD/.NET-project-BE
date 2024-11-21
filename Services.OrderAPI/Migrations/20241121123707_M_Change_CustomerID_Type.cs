using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_Change_CustomerID_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Customer_ID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { "101", new DateTime(2024, 11, 11, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { "102", new DateTime(2024, 11, 16, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8421) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { "103", new DateTime(2024, 11, 21, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8423) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Customer_ID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { 101, new DateTime(2024, 10, 28, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(33) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { 102, new DateTime(2024, 11, 2, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(52) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Customer_ID", "Datetime" },
                values: new object[] { 103, new DateTime(2024, 11, 7, 21, 45, 11, 156, DateTimeKind.Local).AddTicks(54) });
        }
    }
}
