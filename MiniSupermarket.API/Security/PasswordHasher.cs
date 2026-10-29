using System.Security.Cryptography;
using System.Text;

namespace MiniSupermarket.API.Security;

public static class PasswordHasher
{
    public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static bool Verify(string password, string hash)
    {
        if (string.IsNullOrEmpty(hash)) return false;

        if (IsBCrypt(hash))
        {
            try { return BCrypt.Net.BCrypt.Verify(password, hash); }
            catch { return false; }
        }

        return LegacySha256(password) == hash;
    }

    /// <summary>Hash cũ (SHA256-Base64) cần được nâng cấp lên BCrypt sau khi đăng nhập thành công.</summary>
    public static bool NeedsUpgrade(string hash) => !IsBCrypt(hash);

    private static bool IsBCrypt(string hash) => hash.StartsWith("$2");

    private static string LegacySha256(string password) =>
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
