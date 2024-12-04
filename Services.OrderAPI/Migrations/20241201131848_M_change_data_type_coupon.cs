using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class M_change_data_type_coupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Coupon_Code",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { null, new DateTime(2024, 11, 21, 20, 18, 47, 834, DateTimeKind.Local).AddTicks(6936) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { "2001", new DateTime(2024, 11, 26, 20, 18, 47, 834, DateTimeKind.Local).AddTicks(6952) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { null, new DateTime(2024, 12, 1, 20, 18, 47, 834, DateTimeKind.Local).AddTicks(6954) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Coupon_Code",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 1L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { null, new DateTime(2024, 11, 11, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 2L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { 2001, new DateTime(2024, 11, 16, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8421) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Order_ID",
                keyValue: 3L,
                columns: new[] { "Coupon_Code", "Datetime" },
                values: new object[] { null, new DateTime(2024, 11, 21, 19, 37, 5, 783, DateTimeKind.Local).AddTicks(8423) });
        }
    }
}
