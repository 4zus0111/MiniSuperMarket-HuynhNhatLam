using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormOrderManagement : Form
{
    private int _selectedOrderId;

    public FormOrderManagement() => InitializeComponent();

    private async void FormOrderManagement_Load(object? sender, EventArgs e) => await LoadOrdersAsync();

    // Click vào một đơn hàng -> hiển thị chi tiết đơn xuống lưới phía dưới
    private void dgvOrders_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvOrders.ItemAt<OrderDto>(e.RowIndex) is not { } order) return;

        _selectedOrderId = order.OrderId;
        dgvOrderDetails.DataSource = order.OrderDetails;
    }

    private async void btnApprove_Click(object? sender, EventArgs e) => await UpdateStatusAsync("Đã duyệt");

    private async void btnCancel_Click(object? sender, EventArgs e) => await UpdateStatusAsync("Đã hủy");

    private async Task UpdateStatusAsync(string status)
    {
        if (_selectedOrderId == 0)
        {
            Ui.Warn("Vui lòng click chọn một đơn hàng trên lưới!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"orders/{_selectedOrderId}/status", new { status }),
            "Cập nhật trạng thái đơn hàng",
            $"Đã chuyển trạng thái đơn hàng thành: {status}",
            LoadOrdersAsync);
    }

    private async Task LoadOrdersAsync()
    {
        dgvOrders.DataSource = await Ui.FetchAsync<List<OrderDto>>("orders", "Lấy danh sách đơn hàng");

        if (dgvOrders.Columns.Contains(nameof(OrderDto.OrderDetails)))
            dgvOrders.Columns[nameof(OrderDto.OrderDetails)]!.Visible = false;
    }
}
