using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class Insert_Form_Of_Payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormOfPayment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Datetime", "FormOfPayment" },
                values: new object[] { new DateTime(2024, 11, 23, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1603), null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Datetime", "FormOfPayment" },
                values: new object[] { new DateTime(2024, 11, 28, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1625), null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Datetime", "FormOfPayment" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 58, 22, 158, DateTimeKind.Local).AddTicks(1628), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormOfPayment",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                column: "Datetime",
                value: new DateTime(2024, 11, 11, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                column: "Datetime",
                value: new DateTime(2024, 11, 16, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8421));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                column: "Datetime",
                value: new DateTime(2024, 11, 21, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8423));
        }
    }
}
