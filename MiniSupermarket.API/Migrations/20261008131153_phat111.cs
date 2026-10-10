using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class phat111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrandName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderCode = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CashierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Orders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName", "Description", "LogoUrl" },
                values: new object[,]
                {
                    { 1, "Xiaomi", "Hệ sinh thái nhà thông minh Xiaomi Mijia", null },
                    { 2, "Tuya Smart", "Nền tảng IoT Tuya toàn cầu", null },
                    { 3, "Philips", "Chuyên các thiết bị chiếu sáng thông minh Philips Hue", null },
                    { 4, "Aqara", "Thương hiệu cao cấp thuộc hệ sinh thái Apple HomeKit", null },
                    { 5, "Amazon", "Thiết bị loa và màn hình thông minh Alexa", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Chiếu sáng thông minh", "Bóng đèn thông minh, dây LED, đèn cảm ứng" },
                    { 2, "An ninh & Camera", "Camera giám sát trong/ngoài trời, chuông cửa màn hình" },
                    { 3, "Khóa cửa thông minh", "Khóa vân tay, khóa nhận diện khuôn mặt, thẻ từ" },
                    { 4, "Cảm biến thông minh", "Cảm biến chuyển động, nhiệt độ, cửa, khói" },
                    { 5, "Công tắc & Ổ cắm", "Công tắc cảm ứng WiFi/Zigbee, ổ cắm điều khiển từ xa" },
                    { 6, "Loa & Trợ lý ảo", "Loa Google Nest, Amazon Echo, Apple HomePod" },
                    { 7, "Nhà bếp thông minh", "Nồi chiên không dầu tự động, máy pha cà phê thông minh" },
                    { 8, "Robot hút bụi & Lau nhà", "Robot dọn dẹp tự động, máy hút bụi cầm tay" },
                    { 9, "Xử lý không khí", "Máy lọc không khí, máy tạo ẩm, máy hút ẩm thông minh" },
                    { 10, "Điều khiển trung tâm (Hub)", "Bộ điều khiển trung tâm Aqara, Tuya, Xiaomi" },
                    { 11, "Động cơ & Rèm cửa", "Động cơ rèm cuốn, rèm vải thông minh tự động" },
                    { 12, "Thiết bị vệ sinh thông minh", "Nắp bồn cầu tự động, máy sấy tay, gương thông minh" },
                    { 13, "Chăm sóc sức khỏe", "Cân sức khỏe thông minh, máy đo huyết áp kết nối app" },
                    { 14, "Sân vườn tự động", "Van tưới cây tự động, máy cắt cỏ robot" },
                    { 15, "Phụ kiện Smart Home", "Pin, dây cáp, remote, bộ chuyển đổi tín hiệu" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "25 Nguyễn Huệ, Quận 1, TP.HCM", "Nguyễn Văn A", "Vàng", "0901122334", 1500 },
                    { 2, "118 Võ Văn Tần, Quận 3, TP.HCM", "Trần Thị B", "Bạc", "0918877665", 500 },
                    { 3, "72 Nguyễn Trãi, Quận 5, TP.HCM", "Lê Văn C", "Chuẩn", "0983344556", 100 },
                    { 4, "156 Thành Thái, Quận 10, TP.HCM", "Phạm Thị D", "Vàng", "0905678123", 2300 },
                    { 5, "43 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM", "Hoàng Văn E", "Bạc", "0912345678", 800 },
                    { 6, "89 Phạm Văn Đồng, Quận Gò Vấp, TP.HCM", "Võ Thị F", "Kim Cương", "0987654321", 3200 },
                    { 7, "215 Cộng Hòa, Quận Tân Bình, TP.HCM", "Đặng Văn G", "Chuẩn", "0909876543", 250 },
                    { 8, "36 Phan Đình Phùng, Quận Phú Nhuận, TP.HCM", "Bùi Thị H", "Bạc", "0913456789", 1200 },
                    { 9, "102 Nguyễn Thị Thập, Quận 7, TP.HCM", "Đỗ Văn I", "Kim Cương", "0981234567", 4500 },
                    { 10, "68 Hậu Giang, Quận 6, TP.HCM", "Nguyễn Thị K", "Chuẩn", "0903456789", 150 },
                    { 11, "145 Võ Văn Ngân, TP. Thủ Đức, TP.HCM", "Trương Văn L", "Vàng", "0915678901", 1800 },
                    { 12, "234 Lê Văn Khương, Quận 12, TP.HCM", "Phan Thị M", "Bạc", "0986789012", 700 },
                    { 13, "57 Lũy Bán Bích, Quận Tân Phú, TP.HCM", "Lý Văn N", "Kim Cương", "0907890123", 5000 },
                    { 14, "321 Tỉnh Lộ 10, Quận Bình Tân, TP.HCM", "Huỳnh Thị P", "Chuẩn", "0918901234", 350 },
                    { 15, "78 Nguyễn Hữu Trí, Huyện Bình Chánh, TP.HCM", "Mai Văn Q", "Vàng", "0989012345", 2600 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "WAREHOUSE" },
                    { 3, "CASHIER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "BrandId", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 16, "SMH000016", 3, 1, 450000m, "Dây LED thông minh RGB trang trí phòng", 100 },
                    { 17, "SMH000017", 5, 2, 2500000m, "Chuông cửa màn hình thông minh Ring", 30 },
                    { 18, "SMH000018", 2, 3, 1800000m, "Khóa cửa thẻ từ khách sạn Tuya", 50 },
                    { 19, "SMH000019", 1, 4, 400000m, "Cảm biến báo khói báo cháy thông minh", 70 },
                    { 20, "SMH000020", 2, 5, 250000m, "Công tắc cảm ứng âm tường mặt kính 3 nút", 150 },
                    { 21, "SMH000021", 5, 6, 2100000m, "Màn hình thông minh Amazon Echo Show 8", 40 },
                    { 22, "SMH000022", 1, 7, 3500000m, "Máy pha cà phê thông minh kết nối WiFi", 20 },
                    { 23, "SMH000023", 1, 8, 12000000m, "Máy hút bụi cầm tay không dây Dyson", 10 },
                    { 24, "SMH000024", 1, 9, 550000m, "Máy tạo độ ẩm thông minh Deerma", 85 },
                    { 25, "SMH000025", 4, 10, 650000m, "Bộ điều khiển trung tâm Zigbee 3.0", 100 },
                    { 26, "SMH000026", 1, 11, 1850000m, "Động cơ kéo rèm vải thông minh Xiaomi", 25 },
                    { 27, "SMH000027", 2, 12, 2200000m, "Gương thông minh tích hợp đèn LED phòng tắm", 15 },
                    { 28, "SMH000028", 1, 13, 1450000m, "Máy đo huyết áp bắp tay Bluetooth Omron", 60 },
                    { 29, "SMH000029", 2, 14, 18000000m, "Robot cắt cỏ tự động ngoài trời", 5 },
                    { 30, "SMH000030", 4, 15, 120000m, "Cảm biến nhiệt độ và độ ẩm phòng", 200 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "admin01@gmail.com", "Nguyễn Văn Admin", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000001", 1, "admin01" },
                    { 2, new DateTime(2026, 1, 2, 8, 0, 0, 0, DateTimeKind.Unspecified), "admin02@gmail.com", "Trần Thị Admin", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000002", 1, "admin02" },
                    { 3, new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Unspecified), "admin03@gmail.com", "Lê Văn Quản", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000003", 1, "admin03" },
                    { 4, new DateTime(2026, 1, 4, 8, 0, 0, 0, DateTimeKind.Unspecified), "warehouse01@gmail.com", "Phạm Văn Kho", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000004", 2, "warehouse01" },
                    { 5, new DateTime(2026, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "warehouse02@gmail.com", "Hoàng Thị Hương", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000005", 2, "warehouse02" },
                    { 6, new DateTime(2026, 1, 6, 8, 0, 0, 0, DateTimeKind.Unspecified), "warehouse03@gmail.com", "Võ Minh Đức", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000006", 2, "warehouse03" },
                    { 7, new DateTime(2026, 1, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), "warehouse04@gmail.com", "Đặng Quốc Anh", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000007", 2, "warehouse04" },
                    { 8, new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier01@gmail.com", "Nguyễn Thị Lan", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000008", 3, "cashier01" },
                    { 9, new DateTime(2026, 1, 9, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier02@gmail.com", "Trần Văn Nam", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000009", 3, "cashier02" },
                    { 10, new DateTime(2026, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier03@gmail.com", "Phan Thị Mai", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000010", 3, "cashier03" },
                    { 11, new DateTime(2026, 1, 11, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier04@gmail.com", "Lý Hoàng Long", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000011", 3, "cashier04" },
                    { 12, new DateTime(2026, 1, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier05@gmail.com", "Bùi Thị Ngọc", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000012", 3, "cashier05" },
                    { 13, new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier06@gmail.com", "Đỗ Minh Tâm", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000013", 3, "cashier06" },
                    { 14, new DateTime(2026, 1, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier07@gmail.com", "Huỳnh Văn Phúc", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000014", 3, "cashier07" },
                    { 15, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), "cashier08@gmail.com", "Mai Thị Thu", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "0901000015", 3, "cashier08" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierId", "CreatedAt", "CustomerId", "Discount", "OrderCode", "PaymentMethod", "Status", "Subtotal", "Total" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2026, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 0m, "HD00001", "CASH", "PAID", 2950000m, 2950000m },
                    { 2, 9, new DateTime(2026, 1, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 0m, "HD00002", "CARD", "PAID", 2300000m, 2300000m },
                    { 3, 10, new DateTime(2026, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 6, 0m, "HD00003", "BANK_TRANSFER", "PAID", 14100000m, 14100000m },
                    { 4, 11, new DateTime(2026, 1, 12, 16, 45, 0, 0, DateTimeKind.Unspecified), 9, 0m, "HD00004", "CASH", "CANCELLED", 650000m, 650000m },
                    { 5, 12, new DateTime(2026, 1, 15, 8, 20, 0, 0, DateTimeKind.Unspecified), 13, 0m, "HD00005", "MOMO", "PAID", 4300000m, 4300000m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 450000m, 1, 16, 1, 450000m },
                    { 2, 2500000m, 1, 17, 1, 2500000m },
                    { 3, 1800000m, 2, 18, 1, 1800000m },
                    { 4, 500000m, 2, 20, 2, 250000m },
                    { 5, 12000000m, 3, 23, 1, 12000000m },
                    { 6, 2100000m, 3, 21, 1, 2100000m },
                    { 7, 650000m, 4, 25, 1, 650000m },
                    { 8, 1850000m, 5, 26, 1, 1850000m },
                    { 9, 2200000m, 5, 27, 1, 2200000m },
                    { 10, 250000m, 5, 20, 1, 250000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
