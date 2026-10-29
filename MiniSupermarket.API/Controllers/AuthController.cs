using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Security;

namespace MiniSupermarket.API.Controllers;

public class AuthController : ApiControllerBase
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(2);

    private readonly SymmetricSecurityKey _signingKey;
    private readonly SupermarketDbContext _db;

    public AuthController(SymmetricSecurityKey signingKey, SupermarketDbContext db)
    {
        _signingKey = signingKey;
        _db = db;
    }

    // POST: /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _db.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null || !user.IsActive || !PasswordHasher.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new LoginResponseDto { Success = false, Message = "Sai tài khoản hoặc mật khẩu!" });

        if (PasswordHasher.NeedsUpgrade(user.PasswordHash))
        {
            user.PasswordHash = PasswordHasher.Hash(request.Password);
            await _db.SaveChangesAsync();
        }

        var role = AppRoles.Normalize(user.Role?.RoleName);
        return Ok(new LoginResponseDto { Success = true, Token = GenerateJwtToken(user.Username, role), Role = role });
    }

    private string GenerateJwtToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.Add(TokenLifetime),
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(descriptor));
    }
}
