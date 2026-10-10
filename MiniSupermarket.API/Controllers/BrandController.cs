using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        // Khởi tạo danh sách lưu tạm trên RAM (In-Memory)
        private static readonly List<Brand> _brands = new();

        // 1. READ: Lấy toàn bộ danh sách thương hiệu
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_brands);
        }

        // 2. READ: Lấy chi tiết một thương hiệu theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu!" });
            }
            return Ok(brand);
        }

        // 3. SEARCH: Tìm kiếm thương hiệu theo tên
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }

            var result = _brands
                .Where(b => b.BrandName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới thương hiệu
        [HttpPost]
        public IActionResult Create([FromBody] Brand newBrand)
        {
            if (string.IsNullOrWhiteSpace(newBrand.BrandName))
            {
                return BadRequest(new { message = "Tên thương hiệu không được để trống!" });
            }

            // Tự động tăng ID
            newBrand.BrandId = _brands.Count > 0 ? _brands.Max(b => b.BrandId) + 1 : 1;

            _brands.Add(newBrand);

            return CreatedAtAction(nameof(GetById), new { id = newBrand.BrandId }, newBrand);
        }

        // 5. UPDATE: Cập nhật thông tin thương hiệu
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Brand updateBrand)
        {
            var brand = _brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu cần sửa!" });
            }

            if (string.IsNullOrWhiteSpace(updateBrand.BrandName))
            {
                return BadRequest(new { message = "Tên thương hiệu không được để trống!" });
            }

            // Cập nhật dữ liệu
            brand.BrandName = updateBrand.BrandName;
            brand.Description = updateBrand.Description;
            brand.LogoUrl = updateBrand.LogoUrl;

            return NoContent();
        }

        // 6. DELETE: Xóa thương hiệu theo ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brand = _brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu cần xóa!" });
            }

            _brands.Remove(brand);
            return NoContent();
        }
    }
}