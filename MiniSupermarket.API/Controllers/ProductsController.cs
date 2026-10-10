using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    public class ProductInput
    {
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _db;
        public ProductsController(SupermarketDbContext db) => _db = db;

        private static object Map(Product p) => new
        {
            p.ProductId, p.Barcode, p.ProductName, p.Price, p.StockQuantity,
            p.CategoryId, p.BrandId,
            CategoryName = p.Category != null ? p.Category.CategoryName : "",
            Category = p.Category == null ? null : new { p.Category.CategoryId, p.Category.CategoryName },
            Unit = "cái", IsActive = true
        };

        // GET api/products?keyword=&categoryId=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var q = _db.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(p => p.ProductName.Contains(keyword) || p.Barcode.Contains(keyword));
            }
            if (categoryId > 0) q = q.Where(p => p.CategoryId == categoryId);
            var list = await q.OrderBy(p => p.ProductId).ToListAsync();
            return Ok(list.Select(Map));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == id);
            return p == null ? NotFound(new { message = "Không tìm thấy sản phẩm!" }) : Ok(Map(p));
        }

        // Dùng cho POS quét mã vạch
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var p = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Barcode == barcode);
            return p == null ? NotFound(new { message = "Không tìm thấy sản phẩm!" }) : Ok(Map(p));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductInput i)
        {
            if (string.IsNullOrWhiteSpace(i.Barcode) || string.IsNullOrWhiteSpace(i.ProductName))
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            if (await _db.Products.AnyAsync(p => p.Barcode == i.Barcode))
                return BadRequest(new { message = "Mã vạch này đã tồn tại!" });
            if (!await _db.Categories.AnyAsync(c => c.CategoryId == i.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không hợp lệ!" });

            int brandId = i.BrandId;
            if (!await _db.Brands.AnyAsync(b => b.BrandId == brandId))
                brandId = await _db.Brands.OrderBy(b => b.BrandId).Select(b => b.BrandId).FirstOrDefaultAsync();

            var p = new Product
            {
                Barcode = i.Barcode.Trim(), ProductName = i.ProductName.Trim(),
                Price = i.Price, StockQuantity = i.StockQuantity,
                CategoryId = i.CategoryId, BrandId = brandId
            };
            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            return Ok(new { p.ProductId });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductInput i)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            if (string.IsNullOrWhiteSpace(i.Barcode) || string.IsNullOrWhiteSpace(i.ProductName))
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            if (await _db.Products.AnyAsync(x => x.Barcode == i.Barcode && x.ProductId != id))
                return BadRequest(new { message = "Mã vạch bị trùng với sản phẩm khác!" });
            if (!await _db.Categories.AnyAsync(c => c.CategoryId == i.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không hợp lệ!" });

            p.Barcode = i.Barcode.Trim();
            p.ProductName = i.ProductName.Trim();
            p.Price = i.Price;
            p.StockQuantity = i.StockQuantity;
            p.CategoryId = i.CategoryId;
            if (i.BrandId > 0 && await _db.Brands.AnyAsync(b => b.BrandId == i.BrandId)) p.BrandId = i.BrandId;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            if (await _db.OrderItems.AnyAsync(o => o.ProductId == id))
                return BadRequest(new { message = "Sản phẩm đã có trong hóa đơn, không thể xóa!" });
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
