using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Controllers;

/// <summary>Dữ liệu lưu tạm trong bộ nhớ; buổi 2 sẽ chuyển sang MySQL bằng EF Core.</summary>
public class CategoriesController : ApiControllerBase
{
    internal static readonly List<CategoryDto> Categories = new()
    {
        new() { CategoryId = 1, CategoryName = "Sữa", Description = "Sữa và sản phẩm từ sữa" },
        new() { CategoryId = 2, CategoryName = "Bánh kẹo", Description = "Bánh, kẹo, snack" },
        new() { CategoryId = 3, CategoryName = "Đồ uống", Description = "Nước ngọt, nước suối, trà" }
    };

    private static int _nextId = 4;

    // GET: api/categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Task.CompletedTask;
        return Ok(Categories);
    }

    // GET: api/categories/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        await Task.CompletedTask;
        var category = Categories.FirstOrDefault(c => c.CategoryId == id);
        return category == null ? NotFoundMessage("Không tìm thấy nhóm hàng!") : Ok(category);
    }

    // POST: api/categories
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryRequest input)
    {
        await Task.CompletedTask;
        var category = new CategoryDto
        {
            CategoryId = _nextId++,
            CategoryName = input.CategoryName.Trim(),
            Description = input.Description
        };

        Categories.Add(category);
        return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
    }

    // PUT: api/categories/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest input)
    {
        await Task.CompletedTask;
        var category = Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category == null) return NotFoundMessage("Không tìm thấy nhóm hàng cần sửa!");

        category.CategoryName = input.CategoryName.Trim();
        category.Description = input.Description;
        return NoContent();
    }

    // DELETE: api/categories/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Task.CompletedTask;
        var category = Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category == null) return NotFoundMessage("Không tìm thấy nhóm hàng cần xóa!");

        Categories.Remove(category);
        return NoContent();
    }
}
