using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Gạo & Ngũ cốc", "Gạo, yến mạch, ngũ cốc" },
                    { 7, "Đồ hộp", "Cá hộp, thịt hộp, rau củ đóng hộp" },
                    { 8, "Thực phẩm đông lạnh", "Thịt đông lạnh, hải sản, thực phẩm chế biến" },
                    { 9, "Rau củ quả", "Rau xanh, củ và trái cây" },
                    { 10, "Thịt & Hải sản", "Thịt heo, thịt bò, gà và hải sản" },
                    { 11, "Đồ dùng gia đình", "Khăn giấy, túi rác, dụng cụ nhà bếp" },
                    { 12, "Hóa mỹ phẩm", "Dầu gội, sữa tắm, xà phòng" },
                    { 13, "Chăm sóc cá nhân", "Kem đánh răng, bàn chải, nước súc miệng" },
                    { 14, "Đồ uống dinh dưỡng", "Nước ép, sữa hạt, nước bổ sung vitamin" },
                    { 15, "Thực phẩm khô", "Mộc nhĩ, nấm khô, đậu và các loại thực phẩm khô" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "Quận 5, TP.HCM");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 4, "Quận 10, TP.HCM", "Phạm Thị D", "Vàng", "0905678123", 230 },
                    { 5, "Quận Bình Thạnh, TP.HCM", "Hoàng Văn E", "Bạc", "0912345678", 80 },
                    { 6, "Quận Gò Vấp, TP.HCM", "Võ Thị F", "Vàng", "0987654321", 320 },
                    { 7, "Quận Tân Bình, TP.HCM", "Đặng Văn G", "Chuẩn", "0909876543", 25 },
                    { 8, "Quận Phú Nhuận, TP.HCM", "Bùi Thị H", "Bạc", "0913456789", 120 },
                    { 9, "Quận 7, TP.HCM", "Đỗ Văn I", "Vàng", "0981234567", 450 },
                    { 10, "Quận 6, TP.HCM", "Nguyễn Thị K", "Chuẩn", "0903456789", 15 },
                    { 11, "TP. Thủ Đức, TP.HCM", "Trương Văn L", "Bạc", "0915678901", 180 },
                    { 12, "Quận 12, TP.HCM", "Phan Thị M", "Bạc", "0986789012", 70 },
                    { 13, "Quận Tân Phú, TP.HCM", "Lý Văn N", "Vàng", "0907890123", 500 },
                    { 14, "Quận Bình Tân, TP.HCM", "Huỳnh Thị P", "Chuẩn", "0918901234", 35 },
                    { 15, "Huyện Bình Chánh, TP.HCM", "Mai Văn Q", "Vàng", "0989012345", 260 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893123450001", 1, 15000m, "Bánh Oreo vị socola", 100 },
                    { 2, "893123450002", 2, 10000m, "Nước ngọt Coca Cola 330ml", 200 },
                    { 3, "893123450003", 3, 35000m, "Sữa tươi Vinamilk 1L", 80 },
                    { 4, "893123450004", 4, 5000m, "Mì Hảo Hảo tôm chua cay", 300 },
                    { 5, "893123450005", 5, 28000m, "Nước mắm Nam Ngư 500ml", 70 },
                    { 6, "893123450006", 6, 145000m, "Gạo ST25 5kg", 50 },
                    { 7, "893123450007", 7, 32000m, "Cá ngừ đóng hộp", 60 },
                    { 8, "893123450008", 8, 45000m, "Xúc xích tiệt trùng", 90 },
                    { 9, "893123450009", 9, 75000m, "Táo Mỹ", 40 },
                    { 10, "893123450010", 10, 180000m, "Thịt bò thăn", 30 },
                    { 11, "893123450011", 11, 25000m, "Khăn giấy Pulppy", 120 },
                    { 12, "893123450012", 12, 85000m, "Dầu gội Sunsilk 650g", 55 },
                    { 13, "893123450013", 13, 38000m, "Kem đánh răng P/S 180g", 100 },
                    { 14, "893123450014", 14, 18000m, "Nước ép cam Twister", 75 },
                    { 15, "893123450015", 15, 42000m, "Nấm hương khô 100g", 45 }
                });
        }
    }
}
