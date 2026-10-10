using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Yêu cầu đăng nhập bằng JWT
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // 1. GET: Lấy toàn bộ danh sách khách hàng
        // GET /api/customers
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return Ok(customers);
        }

        // =========================================================
        // 2. GET: Lấy chi tiết khách hàng theo ID
        // GET /api/customers/{id}
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            return Ok(customer);
        }

        // =========================================================
        // 3. SEARCH: Tìm kiếm theo tên hoặc số điện thoại
        // GET /api/customers/search?keyword=Nguyen
        // GET /api/customers/search?keyword=0901122334
        // =========================================================
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa tìm kiếm!"
                });
            }

            var customers = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .ToListAsync();

            return Ok(customers);
        }

        // =========================================================
        // 4. POST: Thêm mới khách hàng
        // POST /api/customers
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(newCustomer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(newCustomer.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            // Kiểm tra số điện thoại đã tồn tại
            var phoneExists = await _context.Customers
                .AnyAsync(c => c.PhoneNumber == newCustomer.PhoneNumber);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được đăng ký!"
                });
            }

            // Giá trị mặc định
            newCustomer.RewardPoints = 0;
            newCustomer.MembershipRank = "Chuẩn";

            _context.Customers.Add(newCustomer);

            // Lưu vào SQL Server
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCustomer.CustomerId },
                newCustomer
            );
        }

        // =========================================================
        // 5. PUT: Cập nhật thông tin khách hàng
        // PUT /api/customers/{id}
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần cập nhật!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            // Kiểm tra số điện thoại có bị trùng với khách hàng khác
            var phoneExists = await _context.Customers
                .AnyAsync(c =>
                    c.PhoneNumber == updateCustomer.PhoneNumber &&
                    c.CustomerId != id);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được sử dụng bởi khách hàng khác!"
                });
            }

            // Cập nhật thông tin
            customer.CustomerName = updateCustomer.CustomerName;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.Address = updateCustomer.Address;
            customer.MembershipRank = updateCustomer.MembershipRank;
            customer.RewardPoints = updateCustomer.RewardPoints;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================================================
        // 6. DELETE: Xóa khách hàng
        // DELETE /api/customers/{id}
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa!"
                });
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
