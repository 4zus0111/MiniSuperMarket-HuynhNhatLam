using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms.Services;

public static class SessionManager
{
    public static string CurrentUsername { get; private set; } = string.Empty;
    public static string CurrentRole { get; private set; } = string.Empty;

    public static void SignIn(string username, LoginResponse login)
    {
        CurrentUsername = username;
        CurrentRole = login.Role;
        ApiClient.SetToken(login.Token);
    }

    public static void SignOut()
    {
        CurrentUsername = string.Empty;
        CurrentRole = string.Empty;
        ApiClient.SetToken(null);
    }

    public static bool IsInRole(params string[] roles) =>
        roles.Any(r => r.Equals(CurrentRole, StringComparison.OrdinalIgnoreCase));
}
