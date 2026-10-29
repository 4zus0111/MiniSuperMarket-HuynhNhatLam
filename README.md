# MiniSupermarket – Buổi 3 – LINQ, phân trang và tối ưu truy vấn

Nội dung buổi học (mục 24–27 của đề bài): Tìm kiếm sản phẩm bằng `Contains`, lọc theo nhóm hàng, phân trang phía server (`Skip/Take`), lấy tên nhóm hàng cùng sản phẩm bằng projection `Select`, `AsNoTracking()` cho truy vấn chỉ đọc; tìm kiếm nhóm hàng, thương hiệu, khách hàng.

## Chạy thử

```bash
dotnet run --project MiniSupermarket.API --launch-profile http
```

API chạy tại http://localhost:5000, Swagger tại http://localhost:5000/swagger.

Chuỗi kết nối MySQL trong `MiniSupermarket.API/appsettings.json`; tạo database bằng `Update-Database` (Package Manager Console) hoặc `dotnet ef database update --project MiniSupermarket.API`.


Ứng dụng WinForms hiện chỉ là khung; giao diện đầy đủ ở nhánh `buoi5` (README đầy đủ nằm ở nhánh đó).
