# MiniSupermarket – Buổi 1 – Khởi tạo solution và Web API

Nội dung buổi học (mục 11–17 của đề bài): Solution gồm 2 project (API + WinForms); Program.cs cấu hình Swagger và CORS; chuỗi kết nối nằm trong `appsettings.json`; `AuthController`, `CategoriesController`, `ProductsController` với route RESTful (`api/categories`, `api/products`); đúng phương thức HTTP và mã trạng thái (200, 201, 204, 400, 404); dùng DTO; mọi action là `async Task<IActionResult>`; kiểm thử bằng Swagger UI. Dữ liệu tạm lưu trong bộ nhớ.

## Chạy thử

```bash
dotnet run --project MiniSupermarket.API --launch-profile http
```

API chạy tại http://localhost:5000, Swagger tại http://localhost:5000/swagger.

Chưa cần database: dữ liệu lưu tạm trong bộ nhớ.


Ứng dụng WinForms hiện chỉ là khung; giao diện đầy đủ ở nhánh `buoi5` (README đầy đủ nằm ở nhánh đó).
