using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class Insert_Shipping_Charge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Shipping_Charge",
                table: "Orders",
                type: "money",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Datetime", "Shipping_Charge" },
                values: new object[] { new DateTime(2024, 11, 23, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4598), null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Datetime", "Shipping_Charge" },
                values: new object[] { new DateTime(2024, 11, 28, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4622), null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Datetime", "Shipping_Charge" },
                values: new object[] { new DateTime(2024, 12, 3, 14, 7, 15, 41, DateTimeKind.Local).AddTicks(4641), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping_Charge",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                column: "Datetime",
                value: new DateTime(2024, 11, 23, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1603));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                column: "Datetime",
                value: new DateTime(2024, 11, 28, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                column: "Datetime",
                value: new DateTime(2024, 12, 3, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1628));
        }
    }
}
