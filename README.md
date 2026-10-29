# MiniSupermarket – Buổi 4 – Bảo mật JWT, ADO.NET và giao dịch

Nội dung buổi học (mục 28–35 của đề bài): Đăng nhập trả JWT (BCrypt), `[Authorize]` (401), `[Authorize(Roles=...)]` (Admin / Warehouse / Cashier), Swagger Authorize; tra cứu mã vạch bằng ADO.NET (`MySqlCommand`, `MySqlDataReader`, tham số hóa); stored procedure `sp_GetShiftRevenue` gọi bằng `CommandType.StoredProcedure`; thanh toán trong một transaction (lưu đơn, trừ kho, tích điểm, rollback khi thiếu hàng); quản lý người dùng và vai trò.

## Chạy thử

```bash
dotnet run --project MiniSupermarket.API --launch-profile http
```

API chạy tại http://localhost:5000, Swagger tại http://localhost:5000/swagger.

Chuỗi kết nối MySQL trong `MiniSupermarket.API/appsettings.json`; tạo database bằng `Update-Database` (Package Manager Console) hoặc `dotnet ef database update --project MiniSupermarket.API`.

JWT secret nằm trong `appsettings.Development.json`; đăng nhập qua `POST /api/auth/login`, rồi bấm **Authorize** trong Swagger và dán token.

Ứng dụng WinForms hiện chỉ là khung; giao diện đầy đủ ở nhánh `buoi5` (README đầy đủ nằm ở nhánh đó).
