using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "LastUpdated", "MinAmount" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.0, new DateTime(2024, 5, 8, 13, 48, 10, 743, DateTimeKind.Local).AddTicks(53), 20 },
                    { 2, "20OFF", 20.0, new DateTime(2024, 5, 8, 13, 48, 10, 743, DateTimeKind.Local).AddTicks(173), 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);
        }
    }
}
