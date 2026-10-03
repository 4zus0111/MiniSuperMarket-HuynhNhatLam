using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Bánh kẹo nhập khẩu",
                    Description = "Các loại bánh quy, chocolate, kẹo và đồ ngọt nhập khẩu cao cấp"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Chocolate",
                    Description = "Chocolate đen, chocolate sữa và chocolate cao cấp từ các thương hiệu quốc tế"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Phô mai & Bơ",
                    Description = "Phô mai, bơ và các sản phẩm từ sữa nhập khẩu"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Thịt nguội & Xúc xích",
                    Description = "Thịt nguội, xúc xích, salami và các sản phẩm thịt nhập khẩu"
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Thực phẩm đóng hộp",
                    Description = "Cá hộp, thịt hộp, rau củ đóng hộp và thực phẩm chế biến sẵn"
                },
                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Mì & Pasta",
                    Description = "Mì Ý, pasta, mì ramen và các sản phẩm mì nhập khẩu"
                },
                new Category
                {
                    CategoryId = 7,
                    CategoryName = "Sốt & Gia vị",
                    Description = "Dầu olive, nước sốt, tương ớt, tiêu, muối và gia vị nhập khẩu"
                },
                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Đồ uống nhập khẩu",
                    Description = "Nước trái cây, nước khoáng, nước ngọt và các loại đồ uống quốc tế"
                },
                new Category
                {
                    CategoryId = 9,
                    CategoryName = "Cà phê & Trà",
                    Description = "Cà phê hạt, cà phê rang xay, trà túi lọc và trà cao cấp nhập khẩu"
                },
                new Category
                {
                    CategoryId = 10,
                    CategoryName = "Ngũ cốc & Thực phẩm ăn sáng",
                    Description = "Ngũ cốc, yến mạch, granola và các sản phẩm ăn sáng nhập khẩu"
                },
                new Category
                {
                    CategoryId = 11,
                    CategoryName = "Đồ ăn nhẹ",
                    Description = "Snack, khoai tây chiên, hạt dinh dưỡng và đồ ăn nhẹ nhập khẩu"
                },
                new Category
                {
                    CategoryId = 12,
                    CategoryName = "Thực phẩm đông lạnh",
                    Description = "Pizza, hải sản, thịt và các sản phẩm đông lạnh nhập khẩu"
                },
                new Category
                {
                    CategoryId = 13,
                    CategoryName = "Thực phẩm hữu cơ",
                    Description = "Các sản phẩm thực phẩm hữu cơ và sản phẩm tự nhiên nhập khẩu"
                },
                new Category
                {
                    CategoryId = 14,
                    CategoryName = "Trái cây nhập khẩu",
                    Description = "Các loại trái cây cao cấp nhập khẩu từ Mỹ, Úc, New Zealand và châu Âu"
                },
                new Category
                {
                    CategoryId = 15,
                    CategoryName = "Thực phẩm cao cấp Gourmet",
                    Description = "Các sản phẩm đặc sản và thực phẩm cao cấp phục vụ khách hàng Gourmet"
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn An",
                    PhoneNumber = "0901122334",
                    Address = "25 Nguyễn Huệ, Quận 1, TP.HCM",
                    RewardPoints = 1500,
                    MembershipRank = "Vàng"
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0918877665",
                    Address = "118 Võ Văn Tần, Quận 3, TP.HCM",
                    RewardPoints = 500,
                    MembershipRank = "Bạc"
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0983344556",
                    Address = "72 Nguyễn Trãi, Quận 5, TP.HCM",
                    RewardPoints = 100,
                    MembershipRank = "Chuẩn"
                },
                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0905678123",
                    Address = "156 Thành Thái, Quận 10, TP.HCM",
                    RewardPoints = 2300,
                    MembershipRank = "Vàng"
                },
                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn Đức",
                    PhoneNumber = "0912345678",
                    Address = "43 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM",
                    RewardPoints = 800,
                    MembershipRank = "Bạc"
                },
                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hà",
                    PhoneNumber = "0987654321",
                    Address = "89 Phạm Văn Đồng, Quận Gò Vấp, TP.HCM",
                    RewardPoints = 3200,
                    MembershipRank = "Kim Cương"
                },
                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Văn Giang",
                    PhoneNumber = "0909876543",
                    Address = "215 Cộng Hòa, Quận Tân Bình, TP.HCM",
                    RewardPoints = 250,
                    MembershipRank = "Chuẩn"
                },
                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị Hương",
                    PhoneNumber = "0913456789",
                    Address = "36 Phan Đình Phùng, Quận Phú Nhuận, TP.HCM",
                    RewardPoints = 1200,
                    MembershipRank = "Bạc"
                },
                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Đỗ Văn Khải",
                    PhoneNumber = "0981234567",
                    Address = "102 Nguyễn Thị Thập, Quận 7, TP.HCM",
                    RewardPoints = 4500,
                    MembershipRank = "Kim Cương"
                },
                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Nguyễn Thị Lan",
                    PhoneNumber = "0903456789",
                    Address = "68 Hậu Giang, Quận 6, TP.HCM",
                    RewardPoints = 150,
                    MembershipRank = "Chuẩn"
                },
                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Trương Văn Minh",
                    PhoneNumber = "0915678901",
                    Address = "145 Võ Văn Ngân, TP. Thủ Đức, TP.HCM",
                    RewardPoints = 1800,
                    MembershipRank = "Vàng"
                },
                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Phan Thị Mai",
                    PhoneNumber = "0986789012",
                    Address = "234 Lê Văn Khương, Quận 12, TP.HCM",
                    RewardPoints = 700,
                    MembershipRank = "Bạc"
                },
                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Lý Văn Nam",
                    PhoneNumber = "0907890123",
                    Address = "57 Lũy Bán Bích, Quận Tân Phú, TP.HCM",
                    RewardPoints = 5000,
                    MembershipRank = "Kim Cương"
                },
                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Huỳnh Thị Phương",
                    PhoneNumber = "0918901234",
                    Address = "321 Tỉnh Lộ 10, Quận Bình Tân, TP.HCM",
                    RewardPoints = 350,
                    MembershipRank = "Chuẩn"
                },
                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Mai Văn Quân",
                    PhoneNumber = "0989012345",
                    Address = "78 Nguyễn Hữu Trí, Huyện Bình Chánh, TP.HCM",
                    RewardPoints = 2600,
                    MembershipRank = "Vàng"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Barcode = "GOU000001",
                    ProductName = "Bánh quy bơ Walkers Shortbread",
                    Price = 145000m,
                    StockQuantity = 80,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Barcode = "GOU000002",
                    ProductName = "Bánh wafer Loacker Chocolate",
                    Price = 125000m,
                    StockQuantity = 65,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 3,
                    Barcode = "GOU000003",
                    ProductName = "Chocolate Lindt Excellence Dark 70%",
                    Price = 185000m,
                    StockQuantity = 50,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 4,
                    Barcode = "GOU000004",
                    ProductName = "Chocolate Ferrero Rocher",
                    Price = 210000m,
                    StockQuantity = 70,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 5,
                    Barcode = "GOU000005",
                    ProductName = "Chocolate Bỉ Godiva",
                    Price = 350000m,
                    StockQuantity = 35,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 6,
                    Barcode = "GOU000006",
                    ProductName = "Phô mai Cheddar nhập khẩu Anh",
                    Price = 320000m,
                    StockQuantity = 40,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 7,
                    Barcode = "GOU000007",
                    ProductName = "Bơ lạt President Pháp",
                    Price = 185000m,
                    StockQuantity = 55,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 8,
                    Barcode = "GOU000008",
                    ProductName = "Xúc xích Đức Bratwurst",
                    Price = 285000m,
                    StockQuantity = 40,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 9,
                    Barcode = "GOU000009",
                    ProductName = "Salami Ý Milano",
                    Price = 395000m,
                    StockQuantity = 30,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 10,
                    Barcode = "GOU000010",
                    ProductName = "Thịt nguội Prosciutto Ý",
                    Price = 520000m,
                    StockQuantity = 25,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 11,
                    Barcode = "GOU000011",
                    ProductName = "Cá ngừ ngâm dầu Rio Mare",
                    Price = 125000m,
                    StockQuantity = 100,
                    CategoryId = 5
                },
                new Product
                {
                    ProductId = 12,
                    Barcode = "GOU000012",
                    ProductName = "Đậu Hà Lan đóng hộp Bonduelle",
                    Price = 85000m,
                    StockQuantity = 90,
                    CategoryId = 5
                },
                new Product
                {
                    ProductId = 13,
                    Barcode = "GOU000013",
                    ProductName = "Pasta Barilla Spaghetti No.5",
                    Price = 78000m,
                    StockQuantity = 150,
                    CategoryId = 6
                },
                new Product
                {
                    ProductId = 14,
                    Barcode = "GOU000014",
                    ProductName = "Pasta Penne Rigate De Cecco",
                    Price = 95000m,
                    StockQuantity = 120,
                    CategoryId = 6
                },
                new Product
                {
                    ProductId = 15,
                    Barcode = "GOU000015",
                    ProductName = "Dầu Olive Extra Virgin Bertolli",
                    Price = 265000m,
                    StockQuantity = 60,
                    CategoryId = 7
                },
                new Product
                {
                    ProductId = 16,
                    Barcode = "GOU000016",
                    ProductName = "Sốt cà chua Ý Mutti",
                    Price = 135000m,
                    StockQuantity = 75,
                    CategoryId = 7
                },
                new Product
                {
                    ProductId = 17,
                    Barcode = "GOU000017",
                    ProductName = "Tiêu đen xay McCormick",
                    Price = 110000m,
                    StockQuantity = 80,
                    CategoryId = 7
                },
                new Product
                {
                    ProductId = 18,
                    Barcode = "GOU000018",
                    ProductName = "Nước ép táo Martinelli's",
                    Price = 175000m,
                    StockQuantity = 45,
                    CategoryId = 8
                },
                new Product
                {
                    ProductId = 19,
                    Barcode = "GOU000019",
                    ProductName = "Nước khoáng Perrier Pháp",
                    Price = 95000m,
                    StockQuantity = 100,
                    CategoryId = 8
                },
                new Product
                {
                    ProductId = 20,
                    Barcode = "GOU000020",
                    ProductName = "Cà phê Lavazza Qualità Oro",
                    Price = 395000m,
                    StockQuantity = 55,
                    CategoryId = 9
                },
                new Product
                {
                    ProductId = 21,
                    Barcode = "GOU000021",
                    ProductName = "Trà Earl Grey Twinings",
                    Price = 185000m,
                    StockQuantity = 70,
                    CategoryId = 9
                },
                new Product
                {
                    ProductId = 22,
                    Barcode = "GOU000022",
                    ProductName = "Ngũ cốc Kellogg's Corn Flakes",
                    Price = 165000m,
                    StockQuantity = 70,
                    CategoryId = 10
                },
                new Product
                {
                    ProductId = 23,
                    Barcode = "GOU000023",
                    ProductName = "Granola trái cây Nature Valley",
                    Price = 225000m,
                    StockQuantity = 50,
                    CategoryId = 10
                },
                new Product
                {
                    ProductId = 24,
                    Barcode = "GOU000024",
                    ProductName = "Hạt hạnh nhân rang Kirkland",
                    Price = 295000m,
                    StockQuantity = 65,
                    CategoryId = 11
                },
                new Product
                {
                    ProductId = 25,
                    Barcode = "GOU000025",
                    ProductName = "Khoai tây chiên Pringles Original",
                    Price = 85000m,
                    StockQuantity = 100,
                    CategoryId = 11
                },
                new Product
                {
                    ProductId = 26,
                    Barcode = "GOU000026",
                    ProductName = "Pizza đông lạnh Dr. Oetker",
                    Price = 185000m,
                    StockQuantity = 30,
                    CategoryId = 12
                },
                new Product
                {
                    ProductId = 27,
                    Barcode = "GOU000027",
                    ProductName = "Mật ong hữu cơ Manuka",
                    Price = 850000m,
                    StockQuantity = 25,
                    CategoryId = 13
                },
                new Product
                {
                    ProductId = 28,
                    Barcode = "GOU000028",
                    ProductName = "Nho xanh không hạt nhập khẩu Mỹ",
                    Price = 220000m,
                    StockQuantity = 40,
                    CategoryId = 14
                },
                new Product
                {
                    ProductId = 29,
                    Barcode = "GOU000029",
                    ProductName = "Táo Envy nhập khẩu New Zealand",
                    Price = 180000m,
                    StockQuantity = 50,
                    CategoryId = 14
                },
                new Product
                {
                    ProductId = 30,
                    Barcode = "GOU000030",
                    ProductName = "Gan ngỗng Foie Gras Gourmet",
                    Price = 1250000m,
                    StockQuantity = 15,
                    CategoryId = 15
                }
            );

        }
    }
}