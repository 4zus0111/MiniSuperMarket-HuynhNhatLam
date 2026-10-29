using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập!")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? Role { get; set; }
    public string? Message { get; set; }
}
