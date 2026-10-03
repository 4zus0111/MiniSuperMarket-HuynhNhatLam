using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class _22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chiếu sáng thông minh", "Bóng đèn thông minh, dây LED, đèn cảm ứng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "An ninh & Camera", "Camera giám sát trong/ngoài trời, chuông cửa màn hình" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Khóa cửa thông minh", "Khóa vân tay, khóa nhận diện khuôn mặt, thẻ từ" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Cảm biến thông minh", "Cảm biến chuyển động, nhiệt độ, cửa, khói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Công tắc & Ổ cắm", "Công tắc cảm ứng WiFi/Zigbee, ổ cắm điều khiển từ xa" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Loa & Trợ lý ảo", "Loa Google Nest, Amazon Echo, Apple HomePod" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nhà bếp thông minh", "Nồi chiên không dầu tự động, máy pha cà phê thông minh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Robot hút bụi & Lau nhà", "Robot dọn dẹp tự động, máy hút bụi cầm tay" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Xử lý không khí", "Máy lọc không khí, máy tạo ẩm, máy hút ẩm thông minh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Điều khiển trung tâm (Hub)", "Bộ điều khiển trung tâm Aqara, Tuya, Xiaomi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Động cơ & Rèm cửa", "Động cơ rèm cuốn, rèm vải thông minh tự động" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thiết bị vệ sinh thông minh", "Nắp bồn cầu tự động, máy sấy tay, gương thông minh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chăm sóc sức khỏe", "Cân sức khỏe thông minh, máy đo huyết áp kết nối app" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sân vườn tự động", "Van tưới cây tự động, máy cắt cỏ robot" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Phụ kiện Smart Home", "Pin, dây cáp, remote, bộ chuyển đổi tín hiệu" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "RewardPoints",
                value: 1500);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "RewardPoints",
                value: 500);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "RewardPoints",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "RewardPoints",
                value: 2300);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "RewardPoints",
                value: 800);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Kim Cương", 3200 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "RewardPoints",
                value: 250);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "RewardPoints",
                value: 1200);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Kim Cương", 4500 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "RewardPoints",
                value: 150);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Vàng", 1800 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "RewardPoints",
                value: 700);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Kim Cương", 5000 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "RewardPoints",
                value: 350);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "RewardPoints",
                value: 2600);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000001", 1290000m, "Bóng đèn thông minh Philips Hue Color", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000002", 550000m, "Camera WiFi xoay 360 Ezviz C6N", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000003", 4500000m, "Khóa cửa vân tay thông minh Xiaomi", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000004", 350000m, "Cảm biến chuyển động gắn tường Aqara", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000005", 150000m, "Ổ cắm điện WiFi đo công suất Tuya", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000006", 690000m, "Loa trợ lý ảo Google Nest Mini", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000007", 1490000m, "Nồi chiên không dầu Xiaomi Smart Air Fryer", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000008", 24990000m, "Robot hút bụi Roborock S8 Pro Ultra", 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000009", 3290000m, "Máy lọc không khí Xiaomi Mi Air Purifier 4", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000010", 1190000m, "Bộ điều khiển trung tâm Aqara Hub M2", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000011", 1250000m, "Động cơ rèm cuốn tự động Tuya WiFi", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000012", 8500000m, "Nắp bồn cầu sưởi ấm thông minh TOTO", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "SMH000013", 390000m, "Cân sức khỏe thông minh Xiaomi Body Composition" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000014", 850000m, "Van nước tưới cây tự động WiFi", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000015", 180000m, "Bộ Hub hồng ngoại điều khiển TV/Điều hòa", 150 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gạo & Ngũ cốc", "Gạo, yến mạch, ngũ cốc" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ hộp", "Cá hộp, thịt hộp, rau củ đóng hộp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm đông lạnh", "Thịt đông lạnh, hải sản, thực phẩm chế biến" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Rau củ quả", "Rau xanh, củ và trái cây" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thịt & Hải sản", "Thịt heo, thịt bò, gà và hải sản" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ dùng gia đình", "Khăn giấy, túi rác, dụng cụ nhà bếp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Hóa mỹ phẩm", "Dầu gội, sữa tắm, xà phòng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chăm sóc cá nhân", "Kem đánh răng, bàn chải, nước súc miệng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ uống dinh dưỡng", "Nước ép, sữa hạt, nước bổ sung vitamin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm khô", "Mộc nhĩ, nấm khô, đậu và thực phẩm khô" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "RewardPoints",
                value: 150);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "RewardPoints",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "RewardPoints",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "RewardPoints",
                value: 230);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "RewardPoints",
                value: 80);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Vàng", 320 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "RewardPoints",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "RewardPoints",
                value: 120);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Vàng", 450 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "RewardPoints",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Bạc", 180 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "RewardPoints",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                columns: new[] { "MembershipRank", "RewardPoints" },
                values: new object[] { "Vàng", 500 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "RewardPoints",
                value: 35);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "RewardPoints",
                value: 260);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450001", 15000m, "Bánh Oreo vị socola", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450002", 10000m, "Nước ngọt Coca Cola 330ml", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450003", 35000m, "Sữa tươi Vinamilk 1L", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450004", 5000m, "Mì Hảo Hảo tôm chua cay", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450005", 28000m, "Nước mắm Nam Ngư 500ml", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450006", 145000m, "Gạo ST25 5kg", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450007", 32000m, "Cá ngừ đóng hộp", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450008", 45000m, "Xúc xích tiệt trùng", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450009", 75000m, "Táo Mỹ", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450010", 180000m, "Thịt bò thăn", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450011", 25000m, "Khăn giấy Pulppy", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450012", 85000m, "Dầu gội Sunsilk 650g", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "893123450013", 38000m, "Kem đánh răng P/S 180g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450014", 18000m, "Nước ép cam Twister", 75 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "893123450015", 42000m, "Nấm hương khô 100g", 45 });
        }
    }
}
