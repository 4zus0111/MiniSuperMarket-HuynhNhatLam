using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class x : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 145000m, "Bánh quy bơ Walkers Shortbread", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 185000m, "Chocolate Lindt Excellence Dark 70%", 50 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 185000m, "Chocolate Lindt Excellence Dark 70%", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 145000m, "Bánh quy bơ Walkers Shortbread", 80 });
        }
    }
}
