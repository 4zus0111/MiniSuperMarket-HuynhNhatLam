using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;
using MiniSupermarket.API.Security;

namespace MiniSupermarket.API.Controllers;

[Authorize(Roles = AppRoles.Admin)]
public class UsersController : ApiControllerBase
{
    private const string DefaultRoleName = "CASHIER";
    private const string RootAdminUsername = "admin";

    private static readonly Expression<Func<User, UserResponseDto>> ToResponse = u => new UserResponseDto
    {
        UserId = u.UserId,
        Username = u.Username,
        FullName = u.FullName,
        Email = u.Email,
        Phone = u.Phone,
        IsActive = u.IsActive,
        RoleId = u.RoleId,
        Role = u.Role != null ? u.Role.RoleName : DefaultRoleName,
        CreatedAt = u.CreatedAt
    };

    private static readonly Func<User, UserResponseDto> ToResponseCompiled = ToResponse.Compile();

    private readonly SupermarketDbContext _db;

    public UsersController(SupermarketDbContext db) => _db = db;

    // GET: api/users?keyword=abc
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? keyword)
    {
        var query = _db.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(u =>
                u.Username.Contains(keyword) ||
                u.FullName.Contains(keyword) ||
                (u.Email != null && u.Email.Contains(keyword)));
        }

        return Ok(await query.OrderBy(u => u.UserId).Select(ToResponse).ToListAsync());
    }

    // GET: api/users/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Users.AsNoTracking()
            .Where(u => u.UserId == id)
            .Select(ToResponse)
            .FirstOrDefaultAsync();

        return user == null ? NotFoundMessage("Không tìm thấy người dùng") : Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequestMessage("Tên đăng nhập và mật khẩu không được để trống");

        if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
            return ConflictMessage("Tên đăng nhập đã tồn tại");

        var role = await FindRoleAsync(dto.Role);
        if (role == null) return BadRequestMessage($"Vai trò '{dto.Role}' không tồn tại");

        var user = new User
        {
            Username = dto.Username.Trim(),
            PasswordHash = PasswordHasher.Hash(dto.Password),
            FullName = string.IsNullOrWhiteSpace(dto.FullName) ? dto.Username : dto.FullName.Trim(),
            Email = dto.Email?.Trim(),
            Phone = dto.Phone?.Trim(),
            IsActive = dto.IsActive,
            RoleId = role.RoleId,
            Role = role,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.UserId }, ToResponseCompiled(user));
    }

    // PUT: api/users/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFoundMessage("Không tìm thấy người dùng");

        if (!string.IsNullOrWhiteSpace(dto.FullName))
            user.FullName = dto.FullName.Trim();

        user.Email = dto.Email?.Trim();
        user.Phone = dto.Phone?.Trim();
        user.IsActive = dto.IsActive;

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = PasswordHasher.Hash(dto.Password);

        if (!string.IsNullOrWhiteSpace(dto.Role))
        {
            var role = await FindRoleAsync(dto.Role);
            if (role == null) return BadRequestMessage($"Vai trò '{dto.Role}' không tồn tại");
            user.RoleId = role.RoleId;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // PUT: api/users/5/reset-password
    [HttpPut("{id:int}/reset-password")]
    public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Password))
            return BadRequestMessage("Mật khẩu mới không được để trống");

        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFoundMessage("Không tìm thấy người dùng");

        user.PasswordHash = PasswordHasher.Hash(dto.Password);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // PUT: api/users/5/toggle-status (khóa / mở khóa tài khoản)
    [HttpPut("{id:int}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFoundMessage("Không tìm thấy người dùng");

        if (user.Username.Equals(User.Identity?.Name, StringComparison.OrdinalIgnoreCase))
            return BadRequestMessage("Không thể khóa tài khoản đang đăng nhập");

        user.IsActive = !user.IsActive;
        await _db.SaveChangesAsync();

        return Ok(new { user.IsActive });
    }

    // DELETE: api/users/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFoundMessage("Không tìm thấy người dùng");

        if (user.Username.Equals(RootAdminUsername, StringComparison.OrdinalIgnoreCase))
            return BadRequestMessage("Không thể xóa tài khoản quản trị tối cao admin");

        // Đã từng lập hóa đơn thì chỉ vô hiệu hóa để giữ lịch sử
        if (await _db.Orders.AnyAsync(o => o.CashierId == id))
        {
            user.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok(new { message = "Người dùng đã có hóa đơn trong hệ thống, chỉ vô hiệu hóa tài khoản" });
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Tìm vai trò theo tên (không phân biệt hoa thường, mặc định CASHIER).</summary>
    private Task<Role?> FindRoleAsync(string? roleName)
    {
        var name = (roleName ?? DefaultRoleName).Trim().ToUpperInvariant();
        return _db.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
    }
}
