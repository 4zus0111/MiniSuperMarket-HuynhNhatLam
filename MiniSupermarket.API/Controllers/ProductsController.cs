using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

public class ProductsController : ApiControllerBase
{
    private const int DefaultPageSize = 5;
    private const int MaxPageSize = 50;

    private static readonly Expression<Func<Product, ProductItemDto>> ToItemDto = p => new ProductItemDto
    {
        ProductId = p.ProductId,
        Barcode = p.Barcode,
        ProductName = p.ProductName,
        Price = p.Price,
        StockQuantity = p.StockQuantity,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
        BrandId = p.BrandId
    };

    private readonly SupermarketDbContext _db;

    public ProductsController(SupermarketDbContext db) => _db = db;

    // GET: api/products/paged?keyword=sua&categoryId=1&pageNumber=1&pageSize=5
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(
        [FromQuery] string? keyword,
        [FromQuery] int? categoryId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1 || pageSize > MaxPageSize) pageSize = DefaultPageSize;

        var query = Filter(keyword, categoryId);
        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(p => p.ProductId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(ToItemDto)
            .ToListAsync();

        return Ok(PagedResult<ProductItemDto>.Create(items, totalItems, pageNumber, pageSize));
    }

    // GET: api/products?keyword=sua&categoryId=1
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? keyword, [FromQuery] int? categoryId)
    {
        var items = await Filter(keyword, categoryId)
            .OrderBy(p => p.ProductId)
            .Select(ToItemDto)
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/products/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _db.Products.AsNoTracking()
            .Where(p => p.ProductId == id)
            .Select(ToItemDto)
            .FirstOrDefaultAsync();

        return product == null ? NotFoundMessage("Không tìm thấy sản phẩm!") : Ok(product);
    }

    // GET: api/products/barcode/893...
    [HttpGet("barcode/{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode)
    {
        var code = barcode.Trim();
        var product = await _db.Products.AsNoTracking()
            .Where(p => p.Barcode == code)
            .Select(ToItemDto)
            .FirstOrDefaultAsync();

        return product == null ? NotFoundMessage("Không tìm thấy sản phẩm!") : Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductInput input)
    {
        var error = await ValidateAsync(input, excludeProductId: 0);
        if (error != null) return BadRequestMessage(error);

        var brandId = await ResolveBrandIdAsync(input.BrandId);
        if (brandId <= 0) return BadRequestMessage("Không có thương hiệu hợp lệ!");

        var product = new Product
        {
            Barcode = input.Barcode.Trim(),
            ProductName = input.ProductName.Trim(),
            Price = input.Price,
            StockQuantity = input.StockQuantity,
            CategoryId = input.CategoryId,
            BrandId = brandId
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        var created = await _db.Products.AsNoTracking()
            .Where(p => p.ProductId == product.ProductId)
            .Select(ToItemDto)
            .FirstAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, created);
    }

    // PUT: api/products/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductInput input)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFoundMessage("Không tìm thấy sản phẩm!");

        var error = await ValidateAsync(input, excludeProductId: id);
        if (error != null) return BadRequestMessage(error);

        product.Barcode = input.Barcode.Trim();
        product.ProductName = input.ProductName.Trim();
        product.Price = input.Price;
        product.StockQuantity = input.StockQuantity;
        product.CategoryId = input.CategoryId;

        if (input.BrandId > 0 && await _db.Brands.AnyAsync(b => b.BrandId == input.BrandId))
            product.BrandId = input.BrandId;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFoundMessage("Không tìm thấy sản phẩm!");

        if (await _db.OrderItems.AnyAsync(o => o.ProductId == id))
            return BadRequestMessage("Sản phẩm đã có trong hóa đơn, không thể xóa!");

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private IQueryable<Product> Filter(string? keyword, int? categoryId)
    {
        var query = _db.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var search = keyword.Trim();
            query = query.Where(p => p.ProductName.Contains(search) || p.Barcode.Contains(search));
        }

        if (categoryId > 0)
            query = query.Where(p => p.CategoryId == categoryId);

        return query;
    }

    /// <summary>Trả về thông báo lỗi, hoặc null nếu dữ liệu hợp lệ.</summary>
    private async Task<string?> ValidateAsync(ProductInput input, int excludeProductId)
    {
        var barcode = input.Barcode.Trim();
        if (await _db.Products.AnyAsync(p => p.Barcode == barcode && p.ProductId != excludeProductId))
            return "Mã vạch này đã tồn tại!";

        if (!await _db.Categories.AnyAsync(c => c.CategoryId == input.CategoryId))
            return "Nhóm hàng không hợp lệ!";

        return null;
    }

    /// <summary>Dùng thương hiệu được chọn; nếu không hợp lệ thì lấy thương hiệu đầu tiên (0 nếu chưa có).</summary>
    private async Task<int> ResolveBrandIdAsync(int requestedBrandId)
    {
        if (requestedBrandId > 0 && await _db.Brands.AnyAsync(b => b.BrandId == requestedBrandId))
            return requestedBrandId;

        return await _db.Brands
            .OrderBy(b => b.BrandId)
            .Select(b => b.BrandId)
            .FirstOrDefaultAsync();
    }
}
