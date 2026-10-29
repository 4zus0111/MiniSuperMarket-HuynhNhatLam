using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms.Helpers;

/// <summary>Hộp thoại thông báo và xử lý lỗi gọi API dùng chung cho các form.</summary>
public static class Ui
{
    public static void Info(string message) =>
        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void Warn(string message) =>
        MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public static void Error(string message) =>
        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static bool Confirm(string message) =>
        MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    /// <summary>GET và đọc JSON; hiển thị lỗi và trả về null nếu thất bại.</summary>
    public static async Task<T?> FetchAsync<T>(string url, string action = "Tải dữ liệu") where T : class
    {
        try
        {
            return await ApiClient.Http.GetFromJsonAsync<T>(url);
        }
        catch (HttpRequestException ex)
        {
            ShowException(ex, action);
            return null;
        }
    }

    /// <summary>
    /// Gửi một request ghi dữ liệu. Thành công: hiện successMessage (nếu có), chạy onSuccess và trả về true.
    /// Thất bại: hiện lỗi và trả về false.
    /// </summary>
    public static async Task<bool> SubmitAsync(
        Task<HttpResponseMessage> request,
        string action,
        string? successMessage = null,
        Func<Task>? onSuccess = null)
    {
        try
        {
            var response = await request;
            if (!response.IsSuccessStatusCode)
            {
                await ShowApiErrorAsync(response, action);
                return false;
            }

            if (successMessage != null) Info(successMessage);
            if (onSuccess != null) await onSuccess();
            return true;
        }
        catch (HttpRequestException ex)
        {
            ShowException(ex, action);
            return false;
        }
    }

    public static void ShowException(Exception ex, string action) =>
        ReportFailure((ex as HttpRequestException)?.StatusCode, action, ex.Message);

    public static async Task ShowApiErrorAsync(HttpResponseMessage response, string action = "Thao tác") =>
        ReportFailure(response.StatusCode, action, ExtractMessage(await response.Content.ReadAsStringAsync()));

    private static void ReportFailure(HttpStatusCode? status, string action, string detail)
    {
        switch (status)
        {
            case HttpStatusCode.Unauthorized:
                Warn("Phiên làm việc đã hết hạn hoặc chưa đăng nhập. Vui lòng đăng nhập lại!");
                break;
            case HttpStatusCode.Forbidden:
                Warn($"Bạn không có quyền thực hiện {action.ToLower()}!\n(Vai trò hiện tại: {SessionManager.CurrentRole})");
                break;
            case null:
                Error($"Không thể kết nối đến máy chủ API ({ApiClient.BaseUrl})!\n\nChi tiết lỗi: {detail}");
                break;
            default:
                Error($"{action} không thành công!\nMã lỗi: {(int)status.Value} ({status})\n\n{detail}");
                break;
        }
    }

    /// <summary>
    /// Lấy nội dung lỗi dễ đọc từ phản hồi của API: { "message": "..." } hoặc lỗi validation
    /// (ProblemDetails có "errors"); các dạng khác giữ nguyên nội dung.
    /// </summary>
    private static string ExtractMessage(string content)
    {
        try
        {
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return content;

            if (root.TryGetProperty("message", out var message) && message.ValueKind == JsonValueKind.String)
                return message.GetString() ?? content;

            if (root.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
            {
                var lines = errors.EnumerateObject()
                    .SelectMany(e => e.Value.ValueKind == JsonValueKind.Array ? e.Value.EnumerateArray() : Enumerable.Empty<JsonElement>())
                    .Where(v => v.ValueKind == JsonValueKind.String)
                    .Select(v => v.GetString())
                    .Where(v => !string.IsNullOrWhiteSpace(v));

                var text = string.Join(Environment.NewLine, lines);
                if (text.Length > 0) return text;
            }
        }
        catch (JsonException)
        {
        }

        return content;
    }
}
