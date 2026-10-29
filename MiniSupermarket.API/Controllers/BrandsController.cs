using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

public class BrandsController : ApiControllerBase
{
    private readonly SupermarketDbContext _db;

    public BrandsController(SupermarketDbContext db) => _db = db;

    // GET: /api/brands
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Brands.Select(BrandDto.Projection).ToListAsync());

    // GET: /api/brands/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brand = await _db.Brands
            .Where(b => b.BrandId == id)
            .Select(BrandDto.Projection)
            .FirstOrDefaultAsync();

        return brand == null ? NotFoundMessage("Không tìm thấy thương hiệu!") : Ok(brand);
    }

    // POST: /api/brands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BrandRequest input)
    {
        var brand = new Brand
        {
            BrandName = input.BrandName.Trim(),
            Description = input.Description,
            LogoUrl = input.LogoUrl
        };

        _db.Brands.Add(brand);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = brand.BrandId }, BrandDto.From(brand));
    }

    // PUT: /api/brands/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BrandRequest input)
    {
        var brand = await _db.Brands.FindAsync(id);
        if (brand == null) return NotFoundMessage("Không tìm thấy thương hiệu cần sửa!");

        brand.BrandName = input.BrandName.Trim();
        brand.Description = input.Description;
        brand.LogoUrl = input.LogoUrl;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: /api/brands/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _db.Brands.FindAsync(id);
        if (brand == null) return NotFoundMessage("Không tìm thấy thương hiệu cần xóa!");

        if (await _db.Products.AnyAsync(p => p.BrandId == id))
            return BadRequestMessage("Thương hiệu đang có sản phẩm, không thể xóa!");

        _db.Brands.Remove(brand);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
