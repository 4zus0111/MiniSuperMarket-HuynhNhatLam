# 🛒 Hệ thống quản lý siêu thị mini (MiniSupermarket)

Ứng dụng quản lý siêu thị mini theo mô hình **Client – Server – Database**:

- **MiniSupermarket.API** – Backend ASP.NET Core Web API (.NET 8), Entity Framework Core + **MySQL**, xác thực JWT, phân quyền theo vai trò.
- **MiniSupermarket.WinForms** – Ứng dụng Windows Forms (.NET 8) gọi API qua `HttpClient`.

| | |
|---|---|
| Môn học | Lập trình Ứng dụng .NET Core (mã môn 229162) |
| Sinh viên | Huynh Nhat Lam – MSSV 2124110334 – Lớp CCQ2411C |

---

## 1. Tính năng

### Theo vai trò

| Chức năng | Admin | Warehouse (Thủ kho) | Cashier (Thu ngân) |
|---|:--:|:--:|:--:|
| Bán hàng POS (quét mã vạch, thanh toán, đổi điểm) | ✔ | | ✔ |
| Khách hàng thân thiết (xem/thêm/sửa; **xóa chỉ Admin**) | ✔ | | ✔ |
| Sản phẩm (xem, thêm, sửa, xóa, xuất tồn kho CSV) | ✔ | ✔ | xem (qua POS) |
| Nhóm hàng (xem; thêm/sửa/xóa chỉ Admin) | ✔ | xem | |
| Thương hiệu | ✔ | ✔ | |
| Đơn hàng (duyệt/hủy/xóa), Báo cáo, Dashboard | ✔ | | |
| Người dùng (tạo, đặt lại mật khẩu, khóa/mở khóa), Vai trò | ✔ | | |
| Doanh thu ca của tôi (POS) | ✔ | | ✔ |

Menu bên trái tự ẩn/hiện theo vai trò; màn hình mặc định: Admin → Dashboard, Thủ kho → Nhóm hàng, Thu ngân → POS.

### Chức năng nổi bật

- **Đăng nhập JWT**: token lưu trong `SessionManager` và được `ApiClient` tự gắn vào header `Authorization: Bearer ...`.
- **Sản phẩm**: tìm kiếm (`Contains`) theo tên/mã vạch, lọc theo nhóm hàng, **phân trang phía server** (`Skip/Take`), CRUD, tô màu dòng **tồn kho ≤ 10** (đỏ ≤ 5, cam ≤ 10) bằng `CellFormatting`.
- **POS**: quét mã vạch, cộng dồn số lượng, tính tổng/tiền thừa, thông báo lỗi tiếng Việt; tra cứu khách theo số điện thoại.
- **Thanh toán trong một transaction (ACID)**: lưu đơn, trừ kho, đổi/tích điểm. Thiếu hàng hoặc thiếu điểm → **rollback toàn bộ**.
- **Điểm thưởng**: mỗi 10.000đ được 1 điểm; **đổi 50 điểm = giảm 50.000đ** (bội số của 50, không vượt quá giá trị giỏ hàng). Hủy/xóa đơn sẽ trả lại điểm đã đổi và thu hồi điểm đã tích.
- **Dashboard** (Admin): 4 thẻ KPI (doanh thu hôm nay, đơn hàng hôm nay, khách hàng mới hôm nay, sản phẩm sắp hết hàng), **biểu đồ cột doanh thu theo ngày** có chọn khoảng ngày (7 ngày / 30 ngày / tháng này / tùy chọn), **top 5 sản phẩm bán chạy**.
- **Xuất CSV**: tồn kho sản phẩm và doanh thu theo ngày (UTF-8 có BOM, mở bằng Excel đọc đúng tiếng Việt).
- **Doanh thu ca**: stored procedure `sp_GetShiftRevenue` tính số đơn và doanh thu của thu ngân đang đăng nhập từ 00:00 đến hiện tại.

---

## 2. Công nghệ

| Công nghệ | Mục đích |
|---|---|
| C# 12 / .NET 8 | Ngôn ngữ và nền tảng |
| ASP.NET Core Web API, Swagger (OpenAPI) | Backend và tài liệu/kiểm thử API |
| Entity Framework Core 8 + Pomelo MySQL | Truy cập dữ liệu chính, Migrations, seed data |
| **MySqlConnector (ADO.NET)** | Tra cứu mã vạch và gọi stored procedure báo cáo |
| MySQL 8 / MariaDB (XAMPP) | Cơ sở dữ liệu |
| JWT Bearer, BCrypt.Net | Xác thực và băm mật khẩu |
| Windows Forms, `System.Windows.Forms.DataVisualization` | Giao diện desktop và biểu đồ cột |
| HttpClient + `System.Net.Http.Json` (có sẵn trong .NET) | Gọi API |

---

## 3. Cấu trúc solution

```
MiniSupermarket/
├── MiniSupermarket.API/
│   ├── Constants/        # AppRoles, OrderStatus, LoyaltyRules, StockRules
│   ├── Controllers/      # ApiControllerBase + Auth, Categories, Products, Brands,
│   │                     #   Customers, Orders, Users, Roles, Reports
│   ├── Data/             # SupermarketDbContext, SeedData (15 sản phẩm, 15 nhóm hàng, ...)
│   │   └── AdoNet/       # ProductAdoRepository, ReportAdoRepository (MySqlConnector)
│   ├── DTOs/             # Request/Response DTO cho mọi controller, PagedResult<T>
│   ├── Migrations/       # InitialCreate, AddCustomerCreatedAt, AddReportStoredProcedures
│   ├── Models/           # Entity (User, Role, Category, Brand, Product, Customer, Order, OrderItem)
│   ├── Security/         # PasswordHasher (BCrypt, tự nâng cấp hash cũ)
│   ├── appsettings.json  # Chuỗi kết nối MySQL
│   └── Program.cs        # JWT, CORS, DbContext, Swagger, đăng ký repository ADO.NET
└── MiniSupermarket.WinForms/
    ├── Helpers/          # Ui (hộp thoại + xử lý lỗi API), CsvExporter, DataGridViewExtensions
    ├── Models/           # DTO nhận từ API
    ├── Services/         # ApiClient (BaseUrl, HttpClient, token), SessionManager
    ├── FormMainShell     # Khung chính: menu trái, tiêu đề, vùng làm việc (form con TopLevel=false, Dock=Fill)
    └── Form*.cs          # Login, POS, Product, Category, Brand, Customer, Order, Report, User, Role, Dashboard
```

---

## 4. Cài đặt và chạy

### 4.1. Yêu cầu

- **.NET 8 SDK** và Visual Studio 2022 (workload *.NET desktop development* và *ASP.NET and web development*) – hoặc dùng `dotnet` CLI.
- **MySQL 8 / MariaDB** (ví dụ XAMPP, cổng 3306), tài khoản có quyền tạo database và **CREATE ROUTINE** (tài khoản `root` có sẵn quyền này).
- Windows (WinForms). API có thể chạy trên bất kỳ hệ điều hành nào.
- Lần đầu build cần Internet để khôi phục NuGet (kể cả gói biểu đồ bản *prerelease* `System.Windows.Forms.DataVisualization 1.0.0-prerelease.20110.1`).

### 4.2. Cấu hình

1. **Chuỗi kết nối** – `MiniSupermarket.API/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Port=3306;Database=Lam_DB;Uid=root;Pwd=;"
   }
   ```

   Sửa `Uid`/`Pwd`/`Database` cho phù hợp máy của bạn. Chuỗi này dùng chung cho EF Core và ADO.NET.

2. **JWT secret** (bắt buộc, tối thiểu 32 ký tự): đã có giá trị phát triển trong `appsettings.Development.json`. Khi triển khai thật, đặt bằng user-secrets hoặc biến môi trường thay vì commit:

   ```bash
   dotnet user-secrets init --project MiniSupermarket.API
   dotnet user-secrets set "JwtSettings:Secret" "<chuoi-ngau-nhien-it-nhat-32-ky-tu>" --project MiniSupermarket.API
   ```

### 4.3. Tạo/cập nhật database (EF Core Migrations)

Chạy migration sẽ tạo bảng, **dữ liệu mẫu** và hai **stored procedure** báo cáo.

- Visual Studio – *Tools → NuGet Package Manager → Package Manager Console* (Default project: `MiniSupermarket.API`):

  ```powershell
  Update-Database
  ```

- Hoặc dòng lệnh:

  ```bash
  dotnet tool install --global dotnet-ef      # chỉ cần một lần
  dotnet ef database update --project MiniSupermarket.API
  ```

Khi sửa model: `Add-Migration <Ten>` rồi `Update-Database`.

> **Nâng cấp database cũ**: nếu database đã tạo bằng `InitialCreate`, `Update-Database` chỉ áp dụng thêm hai migration mới (`AddCustomerCreatedAt`, `AddReportStoredProcedures`); dữ liệu hiện có được giữ nguyên.

### 4.4. Chạy Backend (API)

```bash
dotnet run --project MiniSupermarket.API --launch-profile http
```

Hoặc trong Visual Studio chọn `MiniSupermarket.API` → profile **http** → F5. API chạy tại **http://localhost:5000** (Swagger: http://localhost:5000/swagger). Dự án chỉ dùng HTTP, không chuyển hướng sang HTTPS.

**Dùng Swagger với JWT:**

1. `POST /api/auth/login` với `{ "username": "...", "password": "..." }` → sao chép `token`.
2. Nhấn nút **Authorize** (ổ khóa) ở góc trên → dán token (Swagger tự thêm tiền tố `Bearer`) → **Authorize**.
3. Gọi các API được bảo vệ bằng **Try it out → Execute**. Không có token sẽ nhận **401**, sai vai trò nhận **403**.

### 4.5. Chạy Frontend (WinForms)

```bash
dotnet run --project MiniSupermarket.WinForms
```

Hoặc trong Visual Studio: *Configure Startup Projects → Multiple startup projects* (API và WinForms đều **Start**), rồi F5. Địa chỉ API cấu hình một chỗ duy nhất tại `Services/ApiClient.BaseUrl` (`http://localhost:5000/api/`).

### 4.6. Tài khoản mẫu

Dữ liệu mẫu có các tài khoản `admin01…03`, `warehouse01…04`, `cashier01…08` (mật khẩu lưu dạng BCrypt trong `SeedData.cs`). Nếu quên mật khẩu, đăng nhập bằng một tài khoản Admin còn dùng được và đặt lại bằng màn hình **Quản trị tài khoản** (hoặc `PUT /api/users/{id}/reset-password`).

### 4.7. Xử lý sự cố thường gặp

| Hiện tượng | Cách xử lý |
|---|---|
| WinForms báo *Không thể kết nối đến máy chủ API* | Chưa chạy API hoặc cổng 5000 bị chiếm; kiểm tra http://localhost:5000/swagger |
| API không khởi động: *Thiếu JwtSettings:Secret* | Chạy ở môi trường Development hoặc đặt secret (mục 4.2) |
| `Update-Database` lỗi *Access denied* / *Unknown database* | Kiểm tra chuỗi kết nối và MySQL đang chạy |
| Lỗi tạo stored procedure (`CREATE ROUTINE`) | Dùng tài khoản MySQL có quyền `CREATE ROUTINE` (ví dụ `root`) |
| API báo *Procedure sp_GetDailyRevenue does not exist* | Chưa chạy `Update-Database` sau khi cập nhật code |
| Không khôi phục được gói biểu đồ | Bật *Include prerelease* trong NuGet hoặc chạy `dotnet restore` có Internet |

---

## 5. Tài liệu API

Tiền tố `http://localhost:5000/api`. Lỗi trả về dạng `{ "message": "..." }` hoặc ProblemDetails (lỗi validation). Mã trạng thái: `200`, `201` (tạo mới), `204` (cập nhật/xóa), `400`, `401`, `403`, `404`, `409`.

| Method | URL | Vai trò | Mô tả |
|---|---|---|---|
| POST | `/auth/login` | Công khai | Đăng nhập, trả JWT |
| GET | `/categories`, `/categories/{id}`, `/categories/search?keyword=` | Đăng nhập | Danh sách / chi tiết / tìm nhóm hàng |
| POST, PUT, DELETE | `/categories`, `/categories/{id}` | Admin | Thêm / sửa / xóa nhóm hàng |
| GET | `/products/paged?keyword=&categoryId=&pageNumber=&pageSize=` | Đăng nhập | Phân trang, tìm kiếm, lọc |
| GET | `/products`, `/products/{id}` | Đăng nhập | Danh sách / chi tiết |
| GET | `/products/barcode/{barcode}` | Đăng nhập | Tra cứu mã vạch (ADO.NET) |
| POST (201), PUT, DELETE | `/products`, `/products/{id}` | Admin, Warehouse | Thêm / sửa / xóa sản phẩm |
| GET, POST, PUT, DELETE | `/brands`, `/brands/{id}`, `/brands/search?keyword=` | Đăng nhập | Thương hiệu |
| GET | `/customers`, `/customers/{id}`, `/customers/phone/{phone}`, `/customers/search?keyword=` | Admin, Cashier | Khách hàng |
| POST (201), PUT | `/customers`, `/customers/{id}` | Admin, Cashier | Thêm / sửa khách hàng |
| DELETE | `/customers/{id}` | Admin | Xóa khách hàng |
| POST | `/orders/checkout` | Admin, Cashier | Thanh toán (transaction, đổi điểm) |
| GET | `/orders`, `/orders?page=`, `/orders/{id}` | Admin | Danh sách / chi tiết đơn |
| PUT, DELETE | `/orders/{id}/status`, `/orders/{id}` | Admin | Duyệt-hủy / xóa đơn (hoàn kho, hoàn điểm) |
| GET | `/reports/daily?date=` | Admin | Báo cáo một ngày |
| GET | `/reports/dashboard-summary` | Admin | 4 chỉ số KPI |
| GET | `/reports/daily-revenue?from=&to=` | Admin | Doanh thu từng ngày (stored procedure) |
| GET | `/reports/top-products?count=5&from=&to=` | Admin | Top sản phẩm bán chạy |
| GET | `/reports/shift-revenue?from=&to=` | Admin, Cashier | Doanh thu ca của người đăng nhập (stored procedure) |
| GET, POST, PUT, DELETE | `/users`, `/users/{id}` | Admin | Người dùng |
| PUT | `/users/{id}/reset-password`, `/users/{id}/toggle-status` | Admin | Đặt lại mật khẩu / khóa-mở khóa |
| GET, POST, PUT, DELETE | `/roles`, `/roles/{id}` | Admin | Vai trò |

### Ví dụ JSON

**Đăng nhập** – `POST /auth/login`

```json
{ "username": "admin01", "password": "******" }
```
```json
{ "success": true, "token": "eyJhbGciOi...", "role": "Admin", "message": null }
```

**Thanh toán có đổi điểm** – `POST /orders/checkout`

```json
{
  "customerPhone": "0901234567",
  "redeemPoints": 50,
  "cashReceived": 200000,
  "items": [ { "productId": 16, "quantity": 3, "unitPrice": 25000 } ]
}
```
```json
{
  "orderId": 12, "orderCode": "HD00012",
  "subtotal": 75000, "discount": 50000, "total": 25000, "change": 175000,
  "rewardPointsRedeemed": 50, "rewardPointsEarned": 2
}
```

**Thêm sản phẩm** – `POST /products` → `201 Created`

```json
{ "barcode": "8934001000123", "productName": "Sữa tươi 1L", "price": 32000, "stockQuantity": 40, "categoryId": 1 }
```

**Dashboard** – `GET /reports/dashboard-summary`

```json
{ "date": "2026-10-24", "todayRevenue": 1250000, "totalOrders": 14, "newCustomers": 2, "lowStockProducts": 5, "lowStockThreshold": 10 }
```

**Doanh thu theo ngày** – `GET /reports/daily-revenue?from=2026-10-22&to=2026-10-24`

```json
[
  { "date": "2026-10-22T00:00:00", "totalOrders": 9,  "revenue": 830000 },
  { "date": "2026-10-23T00:00:00", "totalOrders": 0,  "revenue": 0 },
  { "date": "2026-10-24T00:00:00", "totalOrders": 14, "revenue": 1250000 }
]
```

**Lỗi validation** – `POST /products` với giá âm → `400`

```json
{ "title": "One or more validation errors occurred.", "status": 400,
  "errors": { "Price": [ "Đơn giá phải lớn hơn 0!" ] } }
```

---

## 6. Cơ sở dữ liệu

- **Bảng**: `Roles`, `Users`, `Categories`, `Brands`, `Products`, `Customers`, `Orders`, `OrderItems` (quan hệ 1-N: Category–Product, Brand–Product, Role–User, Customer–Order, User–Order, Order–OrderItem, Product–OrderItem).
- **Dữ liệu mẫu** (tự tạo khi migrate): 3 vai trò, 15 người dùng, 15 nhóm hàng, 5 thương hiệu, 15 sản phẩm, 15 khách hàng và 5 đơn hàng mẫu (10 dòng chi tiết).
- **Stored procedure** (tạo trong migration `AddReportStoredProcedures`, cú pháp MySQL):
  - `sp_GetDailyRevenue(p_from DATE, p_to DATE)` – doanh thu theo ngày, `GROUP BY CAST(CreatedAt AS DATE)`.
  - `sp_GetShiftRevenue(p_cashier_id INT, p_from DATETIME, p_to DATETIME)` – số đơn và doanh thu của một thu ngân trong ca.
- **Khóa ngoại** dùng `Cascade`; vì vậy API **chặn** xóa Category/Brand/Role đang có dữ liệu liên quan và sản phẩm đã có trong hóa đơn để tránh mất dữ liệu dây chuyền.

---

## 7. Ghi chú kỹ thuật

Một số điểm khác với mô tả đề bài (SQL Server) do dự án dùng **MySQL + EF Core** làm công nghệ chính:

| Nội dung | Cách thực hiện trong dự án |
|---|---|
| `SqlConnection/SqlCommand/SqlDataReader` | `MySqlConnection/MySqlCommand/MySqlDataReader` (MySqlConnector) trong `using` block, tham số hóa bằng `Parameters.AddWithValue`, không nối chuỗi SQL |
| Stored procedure SQL Server | Stored procedure MySQL, gọi bằng `CommandType.StoredProcedure` |
| `SqlTransaction` khi thanh toán | Transaction của EF Core (`BeginTransactionAsync`): lưu đơn, trừ kho, đổi/tích điểm cùng commit hoặc cùng rollback |
| `.Include(p => p.Category)` | Dùng projection `Select` lấy luôn `CategoryName` – cùng kết quả, truy vấn gọn hơn |
| `ApiClientService` | Lớp `ApiClient` + `SessionManager` (HttpClient dùng chung, tự gắn Bearer token) |
| Thực thể `OrderDetail` | Thực thể `OrderItem` (bảng `OrderItems`), client gọi là `OrderDetails` trong JSON |
| Excel/CSV | Xuất CSV (UTF-8 BOM) không cần thư viện ngoài |
| CORS | Cấu hình policy `AllowClient` trong `Program.cs` (cho phép mọi origin để học tập; nên giới hạn khi triển khai thật) |

---

## 8. Kiểm thử thủ công

| # | Kịch bản | Kết quả mong đợi |
|---|---|---|
| 1 | Gọi API được bảo vệ khi chưa có token | `401 Unauthorized` |
| 2 | Cashier gọi `POST /products` | `403 Forbidden` |
| 3 | Đăng nhập sai mật khẩu | `401`, thông báo "Sai tài khoản hoặc mật khẩu!" |
| 4 | Thêm sản phẩm giá 0 hoặc âm | `400`, "Đơn giá phải lớn hơn 0!" |
| 5 | Thêm sản phẩm hợp lệ | `201 Created` |
| 6 | Tìm kiếm "sua", lọc nhóm hàng, chuyển trang | Danh sách đúng trang, đúng tổng số |
| 7 | Quét mã vạch không tồn tại ở POS | Thông báo "Không tìm thấy sản phẩm có mã vạch này!" |
| 8 | Thanh toán số lượng vượt tồn kho | `400`, toàn bộ giao dịch rollback (tồn kho và điểm không đổi) |
| 9 | Thanh toán thành công | Đơn `HDxxxxx`, tồn kho giảm, điểm khách tăng |
| 10 | Đổi 50 điểm | Giảm 50.000đ, điểm khách giảm 50 (cộng điểm theo số tiền thực trả) |
| 11 | Hủy đơn đã đổi điểm | Tồn kho hoàn lại, điểm đã đổi được trả lại |
| 12 | Sản phẩm tồn ≤ 10 | Dòng tô cam/đỏ trong lưới sản phẩm |
| 13 | Dashboard | 4 thẻ KPI, biểu đồ cột theo khoảng ngày, top 5 |
| 14 | Xuất CSV tồn kho / doanh thu | File `.csv` mở được bằng Excel, tiếng Việt đúng dấu |
| 15 | Xóa nhóm hàng đang có sản phẩm | `400`, thông báo không thể xóa |

---

## 9. Git

Nhánh làm việc hiện tại: `buoi5`. Mỗi mốc chức năng là một commit riêng; đẩy lên GitHub bằng:

```bash
git push origin buoi5
```

`.gitignore` đã loại trừ `bin/`, `obj/`, `.vs/`, `*.user` và `node_modules/`.
