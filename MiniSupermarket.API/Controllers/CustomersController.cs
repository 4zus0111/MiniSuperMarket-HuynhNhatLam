using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Authorize(Roles = AppRoles.AdminOrCashier)]
public class CustomersController : ApiControllerBase
{
    private readonly SupermarketDbContext _db;

    public CustomersController(SupermarketDbContext db) => _db = db;

    // GET: /api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Customers.AsNoTracking().Select(CustomerDto.Projection).ToListAsync());

    // GET: /api/customers/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _db.Customers.AsNoTracking()
            .Where(c => c.CustomerId == id)
            .Select(CustomerDto.Projection)
            .FirstOrDefaultAsync();

        return customer == null ? NotFoundMessage("Không tìm thấy khách hàng!") : Ok(customer);
    }

    // GET: /api/customers/phone/0901122334 (POS tra cứu khách hàng thân thiết)
    [HttpGet("phone/{phone}")]
    public async Task<IActionResult> GetByPhone(string phone)
    {
        var customer = await _db.Customers.AsNoTracking()
            .Where(c => c.PhoneNumber == phone.Trim())
            .Select(CustomerDto.Projection)
            .FirstOrDefaultAsync();

        return customer == null ? NotFoundMessage("Không tìm thấy khách hàng!") : Ok(customer);
    }

    // GET: /api/customers/search?keyword=Nguyen (tìm theo tên hoặc số điện thoại)
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequestMessage("Vui lòng nhập từ khóa tìm kiếm!");

        var customers = await _db.Customers
            .AsNoTracking()
            .Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword))
            .Select(CustomerDto.Projection)
            .ToListAsync();

        return Ok(customers);
    }

    // POST: /api/customers
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerRequest input)
    {
        var phone = input.PhoneNumber.Trim();
        if (await _db.Customers.AnyAsync(c => c.PhoneNumber == phone))
            return ConflictMessage("Số điện thoại đã được đăng ký!");

        var customer = new Customer
        {
            CustomerName = input.CustomerName.Trim(),
            PhoneNumber = phone,
            Address = input.Address,
            RewardPoints = 0,
            MembershipRank = "Chuẩn",
            CreatedAt = DateTime.Now
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, CustomerDto.From(customer));
    }

    // PUT: /api/customers/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest input)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return NotFoundMessage("Không tìm thấy khách hàng cần cập nhật!");

        var phone = input.PhoneNumber.Trim();
        if (await _db.Customers.AnyAsync(c => c.PhoneNumber == phone && c.CustomerId != id))
            return ConflictMessage("Số điện thoại đã được sử dụng bởi khách hàng khác!");

        customer.CustomerName = input.CustomerName.Trim();
        customer.PhoneNumber = phone;
        customer.Address = input.Address;
        customer.MembershipRank = input.MembershipRank;
        customer.RewardPoints = input.RewardPoints;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: /api/customers/1 (chỉ Admin)
    [HttpDelete("{id}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return NotFoundMessage("Không tìm thấy khách hàng cần xóa!");

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
