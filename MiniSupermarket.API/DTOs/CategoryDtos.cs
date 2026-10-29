using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MiniSupermarket.API.Models;

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
    public static readonly Expression<Func<Category, CategoryDto>> Projection = c => new CategoryDto
    {
        CategoryId = c.CategoryId,
        CategoryName = c.CategoryName,
        Description = c.Description
    };

    private static readonly Func<Category, CategoryDto> Compiled = Projection.Compile();

    public static CategoryDto From(Category category) => Compiled(category);

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
