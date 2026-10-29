using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.DTOs;

public class RoleRequest
{
    [Required(ErrorMessage = "Tên vai trò không được để trống!")]
    [StringLength(50, ErrorMessage = "Tên vai trò không vượt quá 50 ký tự")]
    public string RoleName { get; set; } = string.Empty;
}

public class RoleDto
{
    public static readonly Expression<Func<Role, RoleDto>> Projection = r => new RoleDto
    {
        RoleId = r.RoleId,
        RoleName = r.RoleName
    };

    private static readonly Func<Role, RoleDto> Compiled = Projection.Compile();

    public static RoleDto From(Role role) => Compiled(role);

    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
