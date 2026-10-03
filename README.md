🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)

Buổi thực hành: Buổi 3 (Tích hợp SQL Server & Entity Framework Core Code-First)

🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server - Database)
Dự án được nâng cấp từ lưu trữ tạm thời (In-Memory) lên cơ sở dữ liệu quan hệ thực tế, đảm bảo tính bền vững của dữ liệu:

Microsoft SQL Server (Database): Nơi lưu trữ vĩnh viễn dữ liệu của hệ thống (Danh mục, Sản phẩm, Khách hàng, Tài khoản...).

MiniSupermarket.API (Backend): Dự án ASP.NET Core Web API đóng vai trò trung gian. Sử dụng Entity Framework Core (EF Core) theo chuẩn Code-First để tự động sinh cơ sở dữ liệu từ mã C# và xử lý các thao tác CRUD bất đồng bộ (Async/Await) thông qua LINQ.

MiniSupermarket.WinForms (Frontend Client): Ứng dụng Windows Forms (máy trạm POS/Admin), gọi API qua HttpClient kèm theo JWT Token và hiển thị trực quan lên DataGridView.

🛠️ 2. Công nghệ Sử dụng
Ngôn ngữ: C# (.NET 8.0)

Cơ sở dữ liệu: Microsoft SQL Server

ORM (Object-Relational Mapping): Entity Framework Core 8.0 (Code-First, Migrations, Data Seeding)

Backend: ASP.NET Core Web API, Async/Await, LINQ to Entities, Dependency Injection

Frontend: Windows Forms (.NET 8.0), System.Net.Http.Json

Công cụ: Visual Studio 2022, SQL Server Management Studio (SSMS), Swagger UI

📂 3. Cấu trúc Solution Cập nhật (Buổi 3)
Plaintext
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Dự án Web API (Backend)
│   ├── Controllers/              # Bổ sung CustomersController xử lý logic Khách hàng
│   ├── Data/                     # Chứa SupermarketDbContext.cs (Cấu hình DB & Data Seeding)
│   ├── Migrations/               # Chứa các file lịch sử tạo bảng và cập nhật Database tự động
│   ├── Models/                   # Cập nhật Category.cs, thêm Product.cs và Customer.cs
│   ├── appsettings.json          # Cấu hình chuỗi kết nối (Connection String) tới SQL Server
│   └── Program.cs                # Đăng ký DbContext vào hệ thống Dependency Injection
│
└── MiniSupermarket.WinForms/     # Dự án Windows Forms (Frontend Client)
    ├── FormLogin.cs              # Giao diện đăng nhập hệ thống
    ├── FormCategoryManagement.cs # Quản lý Nhóm hàng (Gọi API thực tế từ CSDL)
    ├── FormCustomerManagement.cs # (MỚI) Quản lý Khách hàng thân thiết
    └── SessionManager.cs         # Lớp tĩnh lưu trữ Token và Vai trò
🚀 4. Hướng dẫn Cài đặt và Chạy Dự án (Mới nhất)
Bước 1: Cấu hình Cơ sở dữ liệu (Database)

Mở file appsettings.json trong project MiniSupermarket.API.

Đảm bảo DefaultConnection trỏ đúng vào SQL Server của bạn (ví dụ: Server=.;Database=Phat_DB;Trusted_Connection=True;...).

Mở Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console).

Chọn Default project là MiniSupermarket.API và chạy lệnh:

PowerShell
Update-Database
(Hệ thống sẽ tự động tạo Database, tạo bảng Categories, Products, Customers và nạp dữ liệu mẫu).

Bước 2: Khởi chạy Backend (Web API)

Nhấp chuột phải vào MiniSupermarket.API chọn Set as Startup Project.

Nhấn F5 để chạy. Trình duyệt mở Swagger UI.

Kiểm thử: Gọi các API GET/POST/PUT/DELETE. Mở SSMS để kiểm chứng dữ liệu đã được lưu thật xuống đĩa cứng (không còn bị mất khi tắt API như Buổi 1 & 2).

Bước 3: Chạy phía Frontend (WinForms Client)

Cập nhật BaseAddress trong các Form (Customer, Category, Login) khớp với Port HTTPS của Swagger.

Nhấp chuột phải vào MiniSupermarket.WinForms chọn Debug -> Start new instance.

Đăng nhập bằng tài khoản Admin.

Trải nghiệm thao tác Thêm/Sửa/Xóa Khách hàng và Nhóm hàng. Dữ liệu sẽ đồng bộ trực tiếp với SQL Server.

👨‍💻 5. Tác giả
Họ tên sinh viên: Huỳnh Nhật Lâm

Mã sinh viên: 2124110334

Lớp học phần: CCQ2411C
