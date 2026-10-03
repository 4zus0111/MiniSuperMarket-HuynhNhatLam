using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 125000m, "Bánh wafer Loacker Chocolate", 65 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 185000m, "Chocolate Lindt Excellence Dark 70%", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 210000m, "Chocolate Ferrero Rocher", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 350000m, "Chocolate Bỉ Godiva", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 320000m, "Phô mai Cheddar nhập khẩu Anh", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 185000m, "Bơ lạt President Pháp", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 285000m, "Xúc xích Đức Bratwurst", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "CategoryId", "ProductName", "StockQuantity" },
                values: new object[] { 4, "Salami Ý Milano", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 520000m, "Thịt nguội Prosciutto Ý", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 125000m, "Cá ngừ ngâm dầu Rio Mare", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 85000m, "Đậu Hà Lan đóng hộp Bonduelle", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 6, 78000m, "Pasta Barilla Spaghetti No.5", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 6, 95000m, "Pasta Penne Rigate De Cecco", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 7, 265000m, "Dầu Olive Extra Virgin Bertolli", 60 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 16, "GOU000016", 7, 135000m, "Sốt cà chua Ý Mutti", 75 },
                    { 17, "GOU000017", 7, 110000m, "Tiêu đen xay McCormick", 80 },
                    { 18, "GOU000018", 8, 175000m, "Nước ép táo Martinelli's", 45 },
                    { 19, "GOU000019", 8, 95000m, "Nước khoáng Perrier Pháp", 100 },
                    { 20, "GOU000020", 9, 395000m, "Cà phê Lavazza Qualità Oro", 55 },
                    { 21, "GOU000021", 9, 185000m, "Trà Earl Grey Twinings", 70 },
                    { 22, "GOU000022", 10, 165000m, "Ngũ cốc Kellogg's Corn Flakes", 70 },
                    { 23, "GOU000023", 10, 225000m, "Granola trái cây Nature Valley", 50 },
                    { 24, "GOU000024", 11, 295000m, "Hạt hạnh nhân rang Kirkland", 65 },
                    { 25, "GOU000025", 11, 85000m, "Khoai tây chiên Pringles Original", 100 },
                    { 26, "GOU000026", 12, 185000m, "Pizza đông lạnh Dr. Oetker", 30 },
                    { 27, "GOU000027", 13, 850000m, "Mật ong hữu cơ Manuka", 25 },
                    { 28, "GOU000028", 14, 220000m, "Nho xanh không hạt nhập khẩu Mỹ", 40 },
                    { 29, "GOU000029", 14, 180000m, "Táo Envy nhập khẩu New Zealand", 50 },
                    { 30, "GOU000030", 15, 1250000m, "Gan ngỗng Foie Gras Gourmet", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 185000m, "Chocolate Lindt Excellence Dark 70%", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 320000m, "Phô mai Cheddar nhập khẩu Anh", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 285000m, "Xúc xích Đức Bratwurst", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 125000m, "Cá ngừ ngâm dầu Rio Mare", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 6, 78000m, "Pasta Barilla Spaghetti No.5", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 7, 265000m, "Dầu Olive Extra Virgin Bertolli", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 8, 175000m, "Nước ép táo nguyên chất Martinelli's", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "CategoryId", "ProductName", "StockQuantity" },
                values: new object[] { 9, "Cà phê Lavazza Qualità Oro", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 10, 165000m, "Ngũ cốc Kellogg's Corn Flakes", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 11, 295000m, "Hạt hạnh nhân rang Kirkland", 65 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 12, 185000m, "Pizza đông lạnh Dr. Oetker", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 13, 850000m, "Mật ong hữu cơ Manuka", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 14, 220000m, "Nho xanh không hạt nhập khẩu Mỹ", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15, 1250000m, "Gan ngỗng Foie Gras Gourmet", 15 });
        }
    }
}
