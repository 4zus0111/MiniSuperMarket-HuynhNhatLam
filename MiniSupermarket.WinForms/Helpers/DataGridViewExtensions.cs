namespace MiniSupermarket.WinForms.Helpers;

public static class DataGridViewExtensions
{
    public static void SetHeaders(this DataGridView grid, params (string Column, string Header)[] headers)
    {
        foreach (var (column, header) in headers)
        {
            if (grid.Columns.Contains(column))
                grid.Columns[column].HeaderText = header;
        }
    }

    /// <summary>Lấy đối tượng dữ liệu của dòng được click (null nếu click vào header).</summary>
    public static T? ItemAt<T>(this DataGridView grid, int rowIndex) where T : class =>
        rowIndex >= 0 ? grid.Rows[rowIndex].DataBoundItem as T : null;
}
