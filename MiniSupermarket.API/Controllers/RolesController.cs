using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Authorize(Roles = AppRoles.Admin)]
public class RolesController : ApiControllerBase
{
    private readonly SupermarketDbContext _db;

    public RolesController(SupermarketDbContext db) => _db = db;

    // GET: /api/roles
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Roles.AsNoTracking().Select(RoleDto.Projection).ToListAsync());

    // GET: /api/roles/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var role = await _db.Roles.AsNoTracking()
            .Where(r => r.RoleId == id)
            .Select(RoleDto.Projection)
            .FirstOrDefaultAsync();

        return role == null ? NotFoundMessage("Không tìm thấy vai trò!") : Ok(role);
    }

    // POST: /api/roles
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleRequest input)
    {
        var role = new Role { RoleName = input.RoleName.Trim() };

        _db.Roles.Add(role);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, RoleDto.From(role));
    }

    // PUT: /api/roles/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleRequest input)
    {
        var role = await _db.Roles.FindAsync(id);
        if (role == null) return NotFoundMessage("Không tìm thấy vai trò cần sửa!");

        role.RoleName = input.RoleName.Trim();
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: /api/roles/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _db.Roles.FindAsync(id);
        if (role == null) return NotFoundMessage("Không tìm thấy vai trò cần xóa!");

        if (await _db.Users.AnyAsync(u => u.RoleId == id))
            return BadRequestMessage("Vai trò đang được gán cho người dùng, không thể xóa!");

        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
