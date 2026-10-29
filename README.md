# MiniSupermarket – Buổi 2 – Entity, DbContext và Migrations (EF Core + MySQL)

Nội dung buổi học (mục 18–23 của đề bài): 8 entity (`User`, `Role`, `Category`, `Brand`, `Product`, `Customer`, `Order`, `OrderItem`); `DbContext` và khóa ngoại (Category–Product, Order–OrderItem...); validation (`[Required]`, giá > 0, độ dài chuỗi); `Add-Migration` / `Update-Database`; dữ liệu mẫu (15 sản phẩm, 15 nhóm hàng); CRUD bằng EF Core cho nhóm hàng, thương hiệu, sản phẩm, khách hàng.

## Chạy thử

```bash
dotnet run --project MiniSupermarket.API --launch-profile http
```

API chạy tại http://localhost:5000, Swagger tại http://localhost:5000/swagger.

Chuỗi kết nối MySQL trong `MiniSupermarket.API/appsettings.json`; tạo database bằng `Update-Database` (Package Manager Console) hoặc `dotnet ef database update --project MiniSupermarket.API`.


Ứng dụng WinForms hiện chỉ là khung; giao diện đầy đủ ở nhánh `buoi5` (README đầy đủ nằm ở nhánh đó).
