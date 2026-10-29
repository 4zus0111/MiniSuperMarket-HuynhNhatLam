using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.DTOs;

public class BrandRequest
{
    [Required(ErrorMessage = "Tên thương hiệu không được để trống!")]
    [StringLength(100, ErrorMessage = "Tên thương hiệu không vượt quá 100 ký tự")]
    public string BrandName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Mô tả không vượt quá 500 ký tự")]
    public string? Description { get; set; }

    [StringLength(200, ErrorMessage = "Đường dẫn logo không vượt quá 200 ký tự")]
    public string? LogoUrl { get; set; }
}

public class BrandDto
{
    public static readonly Expression<Func<Brand, BrandDto>> Projection = b => new BrandDto
    {
        BrandId = b.BrandId,
        BrandName = b.BrandName,
        Description = b.Description,
        LogoUrl = b.LogoUrl
    };

    private static readonly Func<Brand, BrandDto> Compiled = Projection.Compile();

    public static BrandDto From(Brand brand) => Compiled(brand);

    public int BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}
