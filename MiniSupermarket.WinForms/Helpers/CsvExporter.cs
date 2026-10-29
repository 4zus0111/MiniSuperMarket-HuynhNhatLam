using System.Globalization;
using System.Text;

namespace MiniSupermarket.WinForms.Helpers;

public static class CsvExporter
{
    /// <summary>Hỏi nơi lưu rồi ghi file CSV (UTF-8 có BOM để Excel hiển thị đúng tiếng Việt).</summary>
    public static bool Export(string defaultFileName, IReadOnlyList<string> headers, IEnumerable<IEnumerable<object?>> rows)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = defaultFileName,
            DefaultExt = "csv",
            AddExtension = true
        };

        if (dialog.ShowDialog() != DialogResult.OK) return false;

        try
        {
            File.WriteAllText(dialog.FileName, BuildCsv(headers, rows), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
        }
        catch (IOException ex)
        {
            Ui.Error($"Không thể ghi file (file có đang mở trong Excel không?).\n\n{ex.Message}");
            return false;
        }
        catch (UnauthorizedAccessException ex)
        {
            Ui.Error($"Không có quyền ghi vào vị trí đã chọn.\n\n{ex.Message}");
            return false;
        }

        Ui.Info($"Đã xuất báo cáo:\n{dialog.FileName}");
        return true;
    }

    private static string BuildCsv(IReadOnlyList<string> headers, IEnumerable<IEnumerable<object?>> rows)
    {
        var builder = new StringBuilder();
        builder.AppendLine(string.Join(",", headers.Select(Escape)));

        foreach (var row in rows)
            builder.AppendLine(string.Join(",", row.Select(value => Escape(Format(value)))));

        return builder.ToString();
    }

    private static string Format(object? value) => value switch
    {
        null => string.Empty,
        DateTime date => date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty
    };

    private static string Escape(string value) =>
        value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0
            ? "\"" + value.Replace("\"", "\"\"") + "\""
            : value;
}
