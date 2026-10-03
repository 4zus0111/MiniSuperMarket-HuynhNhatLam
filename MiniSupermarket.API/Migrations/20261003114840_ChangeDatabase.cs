using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo nhập khẩu", "Các loại bánh quy, chocolate, kẹo và đồ ngọt nhập khẩu cao cấp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chocolate", "Chocolate đen, chocolate sữa và chocolate cao cấp từ các thương hiệu quốc tế" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Phô mai & Bơ", "Phô mai, bơ và các sản phẩm từ sữa nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thịt nguội & Xúc xích", "Thịt nguội, xúc xích, salami và các sản phẩm thịt nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm đóng hộp", "Cá hộp, thịt hộp, rau củ đóng hộp và thực phẩm chế biến sẵn" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì & Pasta", "Mì Ý, pasta, mì ramen và các sản phẩm mì nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sốt & Gia vị", "Dầu olive, nước sốt, tương ớt, tiêu, muối và gia vị nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ uống nhập khẩu", "Nước trái cây, nước khoáng, nước ngọt và các loại đồ uống quốc tế" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Cà phê & Trà", "Cà phê hạt, cà phê rang xay, trà túi lọc và trà cao cấp nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Ngũ cốc & Thực phẩm ăn sáng", "Ngũ cốc, yến mạch, granola và các sản phẩm ăn sáng nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ ăn nhẹ", "Snack, khoai tây chiên, hạt dinh dưỡng và đồ ăn nhẹ nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm đông lạnh", "Pizza, hải sản, thịt và các sản phẩm đông lạnh nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm hữu cơ", "Các sản phẩm thực phẩm hữu cơ và sản phẩm tự nhiên nhập khẩu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Trái cây nhập khẩu", "Các loại trái cây cao cấp nhập khẩu từ Mỹ, Úc, New Zealand và châu Âu" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm cao cấp Gourmet", "Các sản phẩm đặc sản và thực phẩm cao cấp phục vụ khách hàng Gourmet" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "CustomerName",
                value: "Nguyễn Văn An");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "CustomerName",
                value: "Trần Thị Bình");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "CustomerName",
                value: "Lê Văn Cường");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "CustomerName",
                value: "Phạm Thị Dung");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "CustomerName",
                value: "Hoàng Văn Đức");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "CustomerName",
                value: "Võ Thị Hà");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "CustomerName",
                value: "Đặng Văn Giang");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "CustomerName",
                value: "Bùi Thị Hương");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "CustomerName",
                value: "Đỗ Văn Khải");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "CustomerName",
                value: "Nguyễn Thị Lan");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "CustomerName",
                value: "Trương Văn Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "CustomerName",
                value: "Phan Thị Mai");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "CustomerName",
                value: "Lý Văn Nam");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "CustomerName",
                value: "Huỳnh Thị Phương");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "CustomerName",
                value: "Mai Văn Quân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "GOU000001", 2, 185000m, "Chocolate Lindt Excellence Dark 70%" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000002", 1, 145000m, "Bánh quy bơ Walkers Shortbread", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000003", 320000m, "Phô mai Cheddar nhập khẩu Anh", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000004", 285000m, "Xúc xích Đức Bratwurst", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000005", 125000m, "Cá ngừ ngâm dầu Rio Mare", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000006", 78000m, "Pasta Barilla Spaghetti No.5", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000007", 265000m, "Dầu Olive Extra Virgin Bertolli", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000008", 175000m, "Nước ép táo nguyên chất Martinelli's", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000009", 395000m, "Cà phê Lavazza Qualità Oro", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000010", 165000m, "Ngũ cốc Kellogg's Corn Flakes", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000011", 295000m, "Hạt hạnh nhân rang Kirkland", 65 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000012", 185000m, "Pizza đông lạnh Dr. Oetker", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000013", 850000m, "Mật ong hữu cơ Manuka", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000014", 220000m, "Nho xanh không hạt nhập khẩu Mỹ", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "GOU000015", 1250000m, "Gan ngỗng Foie Gras Gourmet", 15 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                column: "CustomerName",
                value: "Nguyễn Văn A");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "CustomerName",
                value: "Trần Thị B");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "CustomerName",
                value: "Lê Văn C");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "CustomerName",
                value: "Phạm Thị D");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "CustomerName",
                value: "Hoàng Văn E");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "CustomerName",
                value: "Võ Thị F");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "CustomerName",
                value: "Đặng Văn G");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "CustomerName",
                value: "Bùi Thị H");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "CustomerName",
                value: "Đỗ Văn I");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "CustomerName",
                value: "Nguyễn Thị K");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "CustomerName",
                value: "Trương Văn L");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "CustomerName",
                value: "Phan Thị M");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "CustomerName",
                value: "Lý Văn N");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "CustomerName",
                value: "Huỳnh Thị P");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "CustomerName",
                value: "Mai Văn Q");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "SMH000001", 1, 1290000m, "Bóng đèn thông minh Philips Hue Color" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000002", 2, 550000m, "Camera WiFi xoay 360 Ezviz C6N", 120 });

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
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "SMH000013", 390000m, "Cân sức khỏe thông minh Xiaomi Body Composition", 100 });

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
    }
}
