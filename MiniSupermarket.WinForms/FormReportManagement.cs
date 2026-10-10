using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MiniSupermarket.WinForms.SessionManager;

namespace MiniSupermarket.WinForms
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();

            dtpDate.Value = DateTime.Today;
        }

        private async void FormReport_Load(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnViewReport_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async Task LoadReportAsync()
        {
            try
            {
                string date = dtpDate.Value.ToString("yyyy-MM-dd");

                var report = await ApiClientService.Client
                    .GetFromJsonAsync<ReportDto>($"reports/daily?date={date}");

                if (report == null)
                {
                    MessageBox.Show(
                        "Không có dữ liệu báo cáo!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                lblTotalOrdersValue.Text = report.TotalOrders.ToString();

                lblTotalRevenueValue.Text =
                    report.TotalRevenue.ToString("N0") + " VNĐ";

                lblBestSellerValue.Text =
                    string.IsNullOrWhiteSpace(report.BestSeller)
                        ? "Chưa có"
                        : report.BestSeller;

                dgvRecentOrders.DataSource = null;
                dgvRecentOrders.DataSource = report.RecentOrders;

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy báo cáo: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvRecentOrders.Columns["OrderId"] != null)
                dgvRecentOrders.Columns["OrderId"].Visible = false;

            if (dgvRecentOrders.Columns["OrderCode"] != null)
                dgvRecentOrders.Columns["OrderCode"].HeaderText = "Mã đơn";

            if (dgvRecentOrders.Columns["CreatedAt"] != null)
            {
                dgvRecentOrders.Columns["CreatedAt"].HeaderText = "Ngày tạo";
                dgvRecentOrders.Columns["CreatedAt"].DefaultCellStyle.Format =
                    "dd/MM/yyyy HH:mm";
            }

            if (dgvRecentOrders.Columns["Total"] != null)
            {
                dgvRecentOrders.Columns["Total"].HeaderText = "Tổng tiền";
                dgvRecentOrders.Columns["Total"].DefaultCellStyle.Format =
                    "N0";
            }

            if (dgvRecentOrders.Columns["Status"] != null)
                dgvRecentOrders.Columns["Status"].HeaderText = "Trạng thái";

            if (dgvRecentOrders.Columns["CustomerName"] != null)
                dgvRecentOrders.Columns["CustomerName"].HeaderText =
                    "Khách hàng";
        }
    }

    public class ReportDto
    {
        public string Date { get; set; } = string.Empty;

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public string BestSeller { get; set; } = string.Empty;

        public List<RecentOrderDto> RecentOrders { get; set; } = new();
    }

    public class RecentOrderDto
    {
        public int OrderId { get; set; }

        public string OrderCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;
    }

}