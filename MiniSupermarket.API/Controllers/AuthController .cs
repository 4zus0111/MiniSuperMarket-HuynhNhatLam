using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _db;

        public AuthController(IConfiguration configuration, SupermarketDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        // Kiểm tra mật khẩu: hỗ trợ BCrypt (dữ liệu seed) và SHA256-Base64 (tạo từ form Quản trị tài khoản)
        private static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(hash)) return false;
            if (hash.StartsWith("$2"))
            {
                try { return BCrypt.Net.BCrypt.Verify(password, hash); }
                catch { return false; }
            }
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var h = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return h == hash;
        }

        private static string NormalizeRole(string? roleName) => (roleName ?? "").ToUpper() switch
        {
            "ADMIN" => "Admin",
            "CASHIER" => "Cashier",
            "WAREHOUSE" => "Warehouse",
            var r => r
        };

        // Endpoint Đăng nhập: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user != null && user.IsActive && VerifyPassword(request.Password, user.PasswordHash))
            {
                var role = NormalizeRole(user.Role?.RoleName);
                return Ok(new { success = true, token = GenerateJwtToken(user.Username, role), role });
            }

            // Tài khoản mẫu dự phòng
            if (request.Username == "admin" && request.Password == "123456")
                return Ok(new { success = true, token = GenerateJwtToken("admin", "Admin"), role = "Admin" });
            if (request.Username == "cashier" && request.Password == "123456")
                return Ok(new { success = true, token = GenerateJwtToken("cashier", "Cashier"), role = "Cashier" });

            return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Lấy khóa bí mật từ appsettings.json
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Thời hạn token là 2 tiếng
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}