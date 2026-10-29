using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs;

public class CategoryRequest
{
    [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
    [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "Mô tả không vượt quá 255 ký tự")]
    public string? Description { get; set; }
}

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
