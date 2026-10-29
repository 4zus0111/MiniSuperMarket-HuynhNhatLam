using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Controllers;

/// <summary>Dữ liệu lưu tạm trong bộ nhớ; buổi 2 sẽ chuyển sang MySQL bằng EF Core.</summary>
public class ProductsController : ApiControllerBase
{
    private static readonly List<ProductItemDto> Products = new()
    {
        new() { ProductId = 1, Barcode = "8934001000011", ProductName = "Sữa tươi 1L", Price = 32000, StockQuantity = 40, CategoryId = 1, CategoryName = "Sữa" },
        new() { ProductId = 2, Barcode = "8934001000028", ProductName = "Bánh quy bơ", Price = 18000, StockQuantity = 60, CategoryId = 2, CategoryName = "Bánh kẹo" },
        new() { ProductId = 3, Barcode = "8934001000035", ProductName = "Nước suối 500ml", Price = 6000, StockQuantity = 120, CategoryId = 3, CategoryName = "Đồ uống" }
    };

    private static int _nextId = 4;

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Task.CompletedTask;
        return Ok(Products);
    }

    // GET: api/products/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        await Task.CompletedTask;
        var product = Products.FirstOrDefault(p => p.ProductId == id);
        return product == null ? NotFoundMessage("Không tìm thấy sản phẩm!") : Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductInput input)
    {
        await Task.CompletedTask;

        var error = Validate(input, excludeProductId: 0, out var category);
        if (error != null) return BadRequestMessage(error);

        var product = new ProductItemDto { ProductId = _nextId++ };
        Apply(product, input, category!);
        Products.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
    }

    // PUT: api/products/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductInput input)
    {
        await Task.CompletedTask;

        var product = Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null) return NotFoundMessage("Không tìm thấy sản phẩm!");

        var error = Validate(input, excludeProductId: id, out var category);
        if (error != null) return BadRequestMessage(error);

        Apply(product, input, category!);
        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Task.CompletedTask;
        var product = Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null) return NotFoundMessage("Không tìm thấy sản phẩm!");

        Products.Remove(product);
        return NoContent();
    }

    private static string? Validate(ProductInput input, int excludeProductId, out CategoryDto? category)
    {
        category = CategoriesController.Categories.FirstOrDefault(c => c.CategoryId == input.CategoryId);
        if (category == null) return "Nhóm hàng không hợp lệ!";

        var barcode = input.Barcode.Trim();
        return Products.Any(p => p.Barcode == barcode && p.ProductId != excludeProductId)
            ? "Mã vạch này đã tồn tại!"
            : null;
    }

    private static void Apply(ProductItemDto product, ProductInput input, CategoryDto category)
    {
        product.Barcode = input.Barcode.Trim();
        product.ProductName = input.ProductName.Trim();
        product.Price = input.Price;
        product.StockQuantity = input.StockQuantity;
        product.CategoryId = category.CategoryId;
        product.CategoryName = category.CategoryName;
    }
}
