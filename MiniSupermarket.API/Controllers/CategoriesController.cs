using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Authorize]
public class CategoriesController : ApiControllerBase
{
    private readonly SupermarketDbContext _db;

    public CategoriesController(SupermarketDbContext db) => _db = db;

    // GET: /api/categories
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Categories.AsNoTracking().Select(CategoryDto.Projection).ToListAsync());

    // GET: /api/categories/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _db.Categories.AsNoTracking()
            .Where(c => c.CategoryId == id)
            .Select(CategoryDto.Projection)
            .FirstOrDefaultAsync();

        return category == null ? NotFoundMessage("Không tìm thấy nhóm hàng trong CSDL!") : Ok(category);
    }

    // GET: /api/categories/search?keyword=bánh
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequestMessage("Vui lòng nhập từ khóa tìm kiếm!");

        var result = await _db.Categories
            .AsNoTracking()
            .Where(c => c.CategoryName.Contains(keyword))
            .Select(CategoryDto.Projection)
            .ToListAsync();

        return Ok(result);
    }

    // POST: /api/categories
    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CategoryRequest input)
    {
        var category = new Category { CategoryName = input.CategoryName.Trim(), Description = input.Description };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, CategoryDto.From(category));
    }

    // PUT: /api/categories/1
    [HttpPut("{id}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest input)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null) return NotFoundMessage("Không tìm thấy nhóm hàng cần sửa!");

        category.CategoryName = input.CategoryName.Trim();
        category.Description = input.Description;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: /api/categories/1
    [HttpDelete("{id}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null) return NotFoundMessage("Không tìm thấy nhóm hàng cần xóa!");

        if (await _db.Products.AnyAsync(p => p.CategoryId == id))
            return BadRequestMessage("Nhóm hàng đang có sản phẩm, không thể xóa!");

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
