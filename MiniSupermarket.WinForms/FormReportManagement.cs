using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms;

public partial class FormReportManagement : Form
{
    public FormReportManagement()
    {
        InitializeComponent();
        dtpDate.Value = DateTime.Today;
    }

    private async void FormReportManagement_Load(object? sender, EventArgs e) => await LoadReportAsync();

    private async void btnViewReport_Click(object? sender, EventArgs e) => await LoadReportAsync();

    private async Task LoadReportAsync()
    {
        var date = dtpDate.Value.ToString("yyyy-MM-dd");
        var report = await Ui.FetchAsync<ReportDto>($"reports/daily?date={date}", "Lấy báo cáo");
        if (report == null) return;

        lblTotalOrdersValue.Text = report.TotalOrders.ToString();
        lblTotalRevenueValue.Text = report.TotalRevenue.ToString("N0") + " VNĐ";
        lblBestSellerValue.Text = string.IsNullOrWhiteSpace(report.BestSeller) ? "Chưa có" : report.BestSeller;

        dgvRecentOrders.DataSource = null;
        dgvRecentOrders.DataSource = report.RecentOrders;
        FormatGrid();
    }

    private void FormatGrid()
    {
        if (dgvRecentOrders.Columns.Count == 0) return;

        dgvRecentOrders.Columns[nameof(RecentOrderDto.OrderId)]!.Visible = false;
        dgvRecentOrders.Columns[nameof(RecentOrderDto.CreatedAt)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        dgvRecentOrders.Columns[nameof(RecentOrderDto.Total)]!.DefaultCellStyle.Format = "N0";

        dgvRecentOrders.SetHeaders(
            (nameof(RecentOrderDto.OrderCode), "Mã đơn"),
            (nameof(RecentOrderDto.CreatedAt), "Ngày tạo"),
            (nameof(RecentOrderDto.Total), "Tổng tiền"),
            (nameof(RecentOrderDto.Status), "Trạng thái"),
            (nameof(RecentOrderDto.CustomerName), "Khách hàng"));
    }
}
