using System.Net.Http.Headers;
using System.Net.Http.Json;
using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms.Services;

public static class ApiClient
{
    public const string BaseUrl = "http://localhost:5000/api/";

    public static HttpClient Http { get; } = new() { BaseAddress = new Uri(BaseUrl) };

    public static void SetToken(string? token) =>
        Http.DefaultRequestHeaders.Authorization =
            string.IsNullOrEmpty(token) ? null : new AuthenticationHeaderValue("Bearer", token);

    /// <summary>Trả về thông tin đăng nhập, hoặc null nếu sai tài khoản / mật khẩu.</summary>
    public static async Task<LoginResponse?> LoginAsync(string username, string password)
    {
        var response = await Http.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<LoginResponse>()
            : null;
    }
}
