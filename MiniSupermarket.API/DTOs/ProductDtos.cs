using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs;

public class ProductItemDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int BrandId { get; set; }
}

public class ProductInput
{
    [Required(ErrorMessage = "Mã vạch không được để trống!")]
    [StringLength(50, ErrorMessage = "Mã vạch không vượt quá 50 ký tự")]
    public string Barcode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
    [StringLength(150, ErrorMessage = "Tên sản phẩm không vượt quá 150 ký tự")]
    public string ProductName { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999999", ErrorMessage = "Đơn giá phải lớn hơn 0!")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Tồn kho không được âm!")]
    public int StockQuantity { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Nhóm hàng không hợp lệ!")]
    public int CategoryId { get; set; }

    public int BrandId { get; set; }
}
