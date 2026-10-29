using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using ChartCtl = System.Windows.Forms.DataVisualization.Charting;

namespace MiniSupermarket.WinForms;

/// <summary>Dashboard: 4 chỉ số KPI, biểu đồ cột doanh thu theo ngày và top sản phẩm bán chạy.</summary>
public class FormDashboard : Form
{
    private const int TopProductCount = 5;
    private const int MaxLabeledBars = 14;

    private readonly Label _lblRevenue = CreateValueLabel();
    private readonly Label _lblOrders = CreateValueLabel();
    private readonly Label _lblNewCustomers = CreateValueLabel();
    private readonly Label _lblLowStock = CreateValueLabel();

    private readonly DateTimePicker _dtpFrom = new() { Format = DateTimePickerFormat.Short, Width = 110 };
    private readonly DateTimePicker _dtpTo = new() { Format = DateTimePickerFormat.Short, Width = 110 };
    private readonly ChartCtl.Chart _chart = new() { Dock = DockStyle.Fill };
    private readonly DataGridView _dgvTopProducts = new();

    private List<DailyRevenueDto> _dailyRevenue = new();

    public FormDashboard()
    {
        Text = "DASHBOARD";
        BackColor = Color.White;

        // Form chưa hiển thị mặc định chỉ cao 300px, nhỏ hơn tổng chiều cao các panel dock (360px),
        // khiến Chart (Dock = Fill) có chiều cao <= 0 và ném ArgumentException.
        ClientSize = new Size(1100, 700);

        BuildChart();
        BuildTopProductsGrid();

        var topPanel = new GroupBox
        {
            Text = $"TOP {TopProductCount} SẢN PHẨM BÁN CHẠY",
            Dock = DockStyle.Bottom,
            Height = 200,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        topPanel.Controls.Add(_dgvTopProducts);

        // Thứ tự Add: control Fill trước, các panel Top/Bottom sau để Fill chiếm phần còn lại
        Controls.Add(_chart);
        Controls.Add(topPanel);
        Controls.Add(BuildFilterPanel());
        Controls.Add(BuildKpiPanel());

        Load += async (_, _) =>
        {
            SetRange(days: 7);
            await RefreshAllAsync();
        };
    }

    // ---------------------------------------------------------------
    // Dựng giao diện
    // ---------------------------------------------------------------

    private FlowLayoutPanel BuildKpiPanel()
    {
        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Padding = new Padding(10, 10, 0, 0),
            BackColor = Color.WhiteSmoke
        };

        panel.Controls.Add(CreateCard("DOANH THU HÔM NAY", _lblRevenue, Color.FromArgb(40, 167, 69)));
        panel.Controls.Add(CreateCard("ĐƠN HÀNG HÔM NAY", _lblOrders, Color.FromArgb(0, 123, 255)));
        panel.Controls.Add(CreateCard("KHÁCH HÀNG MỚI HÔM NAY", _lblNewCustomers, Color.FromArgb(111, 66, 193)));
        panel.Controls.Add(CreateCard("SẢN PHẨM SẮP HẾT HÀNG", _lblLowStock, Color.FromArgb(220, 53, 69)));
        return panel;
    }

    private FlowLayoutPanel BuildFilterPanel()
    {
        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 50,
            Padding = new Padding(10, 10, 0, 0)
        };

        var btnWeek = CreateButton("7 ngày", 80);
        var btnMonth = CreateButton("30 ngày", 80);
        var btnThisMonth = CreateButton("Tháng này", 90);
        var btnView = CreateButton("Xem", 80, Color.FromArgb(0, 120, 215));
        var btnExport = CreateButton("Xuất CSV", 100, Color.FromArgb(40, 167, 69));

        btnWeek.Click += async (_, _) => { SetRange(days: 7); await RefreshChartAndTopAsync(); };
        btnMonth.Click += async (_, _) => { SetRange(days: 30); await RefreshChartAndTopAsync(); };
        btnThisMonth.Click += async (_, _) =>
        {
            _dtpTo.Value = DateTime.Today;
            _dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            await RefreshChartAndTopAsync();
        };
        btnView.Click += async (_, _) => await RefreshAllAsync();
        btnExport.Click += (_, _) => ExportRevenue();

        panel.Controls.Add(CreateInlineLabel("Từ ngày:"));
        panel.Controls.Add(_dtpFrom);
        panel.Controls.Add(CreateInlineLabel("Đến ngày:"));
        panel.Controls.Add(_dtpTo);
        panel.Controls.AddRange(new Control[] { btnWeek, btnMonth, btnThisMonth, btnView, btnExport });
        return panel;
    }

    private void BuildChart()
    {
        _chart.MinimumSize = new Size(200, 150); // không bao giờ để Chart bị ép về chiều cao 0

        var area = new ChartCtl.ChartArea("Revenue");
        area.AxisX.Title = "Ngày";
        area.AxisY.Title = "Doanh thu (VNĐ)";
        area.AxisY.LabelStyle.Format = "N0";
        area.AxisX.MajorGrid.Enabled = false;
        _chart.ChartAreas.Add(area);

        _chart.Series.Add(new ChartCtl.Series("Doanh thu")
        {
            ChartType = ChartCtl.SeriesChartType.Column,
            ChartArea = "Revenue",
            Color = Color.FromArgb(0, 123, 255),
            LabelFormat = "N0",
            ToolTip = "#VALX: #VALY{N0} đ"
        });

        _chart.Titles.Add("DOANH THU THEO NGÀY");
    }

    private void BuildTopProductsGrid()
    {
        _dgvTopProducts.Dock = DockStyle.Fill;
        _dgvTopProducts.ReadOnly = true;
        _dgvTopProducts.AllowUserToAddRows = false;
        _dgvTopProducts.RowHeadersVisible = false;
        _dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvTopProducts.BackgroundColor = Color.White;
    }

    private static Panel CreateCard(string title, Label valueLabel, Color color)
    {
        var card = new Panel { Width = 250, Height = 90, BackColor = color, Margin = new Padding(0, 0, 10, 0) };
        var titleLabel = new Label
        {
            Text = title,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Dock = DockStyle.Top,
            Height = 30,
            Padding = new Padding(10, 8, 0, 0)
        };

        valueLabel.Dock = DockStyle.Fill;
        card.Controls.Add(valueLabel);
        card.Controls.Add(titleLabel);
        return card;
    }

    private static Label CreateValueLabel() => new()
    {
        Text = "-",
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 20, FontStyle.Bold),
        TextAlign = ContentAlignment.MiddleLeft,
        Padding = new Padding(10, 0, 0, 0)
    };

    private static Label CreateInlineLabel(string text) => new()
    {
        Text = text,
        AutoSize = true,
        Margin = new Padding(10, 6, 3, 0)
    };

    private static Button CreateButton(string text, int width, Color? backColor = null)
    {
        var button = new Button { Text = text, Width = width, Height = 28, FlatStyle = FlatStyle.Flat, Margin = new Padding(10, 0, 0, 0) };
        if (backColor != null)
        {
            button.BackColor = backColor.Value;
            button.ForeColor = Color.White;
        }

        return button;
    }

    // ---------------------------------------------------------------
    // Tải dữ liệu
    // ---------------------------------------------------------------

    private void SetRange(int days)
    {
        _dtpTo.Value = DateTime.Today;
        _dtpFrom.Value = DateTime.Today.AddDays(-(days - 1));
    }

    private async Task RefreshAllAsync()
    {
        await LoadSummaryAsync();
        await RefreshChartAndTopAsync();
    }

    private async Task RefreshChartAndTopAsync()
    {
        if (_dtpFrom.Value.Date > _dtpTo.Value.Date)
        {
            Ui.Warn("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!");
            return;
        }

        await LoadRevenueChartAsync();
        await LoadTopProductsAsync();
    }

    private async Task LoadSummaryAsync()
    {
        var summary = await Ui.FetchAsync<DashboardSummaryDto>("reports/dashboard-summary", "Tải chỉ số tổng quan");
        if (summary == null) return;

        _lblRevenue.Text = $"{summary.TodayRevenue:N0} đ";
        _lblOrders.Text = summary.TotalOrders.ToString();
        _lblNewCustomers.Text = summary.NewCustomers.ToString();
        _lblLowStock.Text = $"{summary.LowStockProducts} (≤ {summary.LowStockThreshold})";
    }

    private async Task LoadRevenueChartAsync()
    {
        var data = await Ui.FetchAsync<List<DailyRevenueDto>>($"reports/daily-revenue?{RangeQuery()}", "Tải doanh thu theo ngày");
        if (data == null) return;

        _dailyRevenue = data;

        var series = _chart.Series[0];
        series.Points.Clear();
        foreach (var day in data)
            series.Points.AddXY(day.Date.ToString("dd/MM"), (double)day.Revenue);

        series.IsValueShownAsLabel = data.Count <= MaxLabeledBars;
        _chart.ChartAreas[0].AxisX.Interval = Math.Max(1, Math.Ceiling(data.Count / 15.0));
    }

    private async Task LoadTopProductsAsync()
    {
        _dgvTopProducts.DataSource = await Ui.FetchAsync<List<TopProductDto>>(
            $"reports/top-products?count={TopProductCount}&{RangeQuery()}", "Tải top sản phẩm bán chạy");

        if (_dgvTopProducts.Columns.Count == 0) return;

        _dgvTopProducts.Columns[nameof(TopProductDto.ProductId)]!.Visible = false;
        _dgvTopProducts.Columns[nameof(TopProductDto.Revenue)]!.DefaultCellStyle.Format = "N0";
        _dgvTopProducts.SetHeaders(
            (nameof(TopProductDto.ProductName), "Sản phẩm"),
            (nameof(TopProductDto.TotalQuantity), "Số lượng đã bán"),
            (nameof(TopProductDto.Revenue), "Doanh thu (đ)"));
    }

    private string RangeQuery() =>
        $"from={_dtpFrom.Value:yyyy-MM-dd}&to={_dtpTo.Value:yyyy-MM-dd}";

    // ---------------------------------------------------------------
    // Xuất báo cáo
    // ---------------------------------------------------------------

    private void ExportRevenue()
    {
        if (_dailyRevenue.Count == 0)
        {
            Ui.Warn("Chưa có dữ liệu doanh thu để xuất!");
            return;
        }

        CsvExporter.Export(
            $"doanh-thu_{_dtpFrom.Value:yyyyMMdd}_{_dtpTo.Value:yyyyMMdd}.csv",
            new[] { "Ngày", "Số đơn", "Doanh thu (VNĐ)" },
            _dailyRevenue.Select(d => (IEnumerable<object?>)new object?[] { d.Date.ToString("dd/MM/yyyy"), d.TotalOrders, d.Revenue }));
    }
}
