using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Controllers;

/// <summary>Đăng nhập đơn giản với tài khoản cố định. JWT và phân quyền sẽ được bổ sung ở buổi 4.</summary>
public class AuthController : ApiControllerBase
{
    private static readonly Dictionary<string, string> DemoUsers = new(StringComparer.OrdinalIgnoreCase)
    {
        ["admin"] = "admin123",
        ["cashier"] = "cashier123"
    };

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        await Task.CompletedTask;

        if (!DemoUsers.TryGetValue(request.Username, out var password) || password != request.Password)
            return Unauthorized(new LoginResponseDto { Success = false, Message = "Sai tài khoản hoặc mật khẩu!" });

        return Ok(new LoginResponseDto { Success = true, Message = "Đăng nhập thành công!" });
    }
}
