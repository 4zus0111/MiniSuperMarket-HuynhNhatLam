using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormProductManagement : Form
{
    private const int DefaultPageSize = 5;
    private const int LowStockThreshold = 10;
    private const int CriticalStockThreshold = 5;

    private static readonly Color CriticalStockColor = Color.FromArgb(255, 160, 160);
    private static readonly Color LowStockColor = Color.FromArgb(255, 213, 153);

    private TextBox txtSearchBarcode = null!;
    private ComboBox cboFilterCategory = null!;
    private DataGridView dgvProducts = null!;

    private TextBox txtId = null!;
    private TextBox txtBarcode = null!;
    private TextBox txtProductName = null!;
    private NumericUpDown nudPrice = null!;
    private NumericUpDown nudStock = null!;
    private ComboBox cboCategory = null!;

    private Button btnPreviousPage = null!;
    private Button btnNextPage = null!;
    private Label lblPageInfo = null!;
    private ComboBox cboPageSize = null!;

    private int _currentPage = 1;
    private int _pageSize = DefaultPageSize;
    private int _totalPages;

    public FormProductManagement()
    {
        InitializeComponent();
        BuildUI();
    }

    // ---------------------------------------------------------------
    // Dựng giao diện
    // ---------------------------------------------------------------

    private void BuildUI()
    {
        Text = "QUẢN LÝ SẢN PHẨM & KHO HÀNG";
        Size = new Size(1100, 720);
        MinimumSize = new Size(1100, 720);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.White;

        Controls.Add(BuildSearchPanel());
        Controls.Add(BuildGrid());
        Controls.Add(BuildDetailsPanel());
        Controls.Add(BuildPagingPanel());

        Load += FormProductManagement_Load;
    }

    private Panel BuildSearchPanel()
    {
        var panel = new Panel { Location = new Point(20, 15), Size = new Size(1040, 60), BackColor = Color.White };

        txtSearchBarcode = new TextBox { Location = new Point(80, 7), Size = new Size(250, 30) };
        cboFilterCategory = new ComboBox
        {
            Location = new Point(440, 7),
            Size = new Size(220, 30),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        var btnSearch = CreateButton("Tìm kiếm", new Point(680, 5), new Size(120, 35), Color.FromArgb(0, 120, 215));

        btnSearch.Click += async (_, _) => await SearchAsync();
        cboFilterCategory.SelectionChangeCommitted += async (_, _) => await SearchAsync();
        txtSearchBarcode.KeyDown += async (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            await SearchAsync();
        };

        panel.Controls.Add(CreateLabel("Từ khóa:", new Point(0, 10), new Size(80, 25), 10));
        panel.Controls.Add(txtSearchBarcode);
        panel.Controls.Add(CreateLabel("Nhóm hàng:", new Point(350, 10), new Size(90, 25), 10));
        panel.Controls.Add(cboFilterCategory);
        panel.Controls.Add(btnSearch);

        var legend = CreateLabel($"Tồn kho ≤ {CriticalStockThreshold}: đỏ  |  ≤ {LowStockThreshold}: cam (sắp hết hàng)",
            new Point(810, 10), new Size(230, 40), 8);
        legend.ForeColor = Color.DarkRed;
        panel.Controls.Add(legend);
        return panel;
    }

    private DataGridView BuildGrid()
    {
        dgvProducts = new DataGridView
        {
            Location = new Point(20, 90),
            Size = new Size(700, 420),
            AutoGenerateColumns = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            RowHeadersVisible = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.ProductId), "Mã SP", fillWeight: 40));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.Barcode), "Mã vạch"));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.ProductName), "Tên sản phẩm"));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.Price), "Đơn giá", format: "N0"));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.StockQuantity), "Tồn kho"));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.CategoryName), "Nhóm hàng"));
        dgvProducts.Columns.Add(CreateColumn(nameof(ProductDto.CategoryId), string.Empty, visible: false));

        dgvProducts.CellClick += dgvProducts_CellClick;
        dgvProducts.CellFormatting += dgvProducts_CellFormatting;
        return dgvProducts;
    }

    private GroupBox BuildDetailsPanel()
    {
        var group = new GroupBox
        {
            Text = "THÔNG TIN SẢN PHẨM",
            Location = new Point(740, 90),
            Size = new Size(320, 490),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };

        txtId = new TextBox { ReadOnly = true };
        txtBarcode = new TextBox();
        txtProductName = new TextBox();
        nudPrice = new NumericUpDown { Minimum = 0, Maximum = 1000000000, DecimalPlaces = 0, ThousandsSeparator = true };
        nudStock = new NumericUpDown { Minimum = 0, Maximum = 1000000 };
        cboCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

        AddRow(group, "ProductId:", txtId, 35);
        AddRow(group, "Mã vạch:", txtBarcode, 75);
        AddRow(group, "Tên sản phẩm:", txtProductName, 115);
        AddRow(group, "Đơn giá:", nudPrice, 155);
        AddRow(group, "Tồn kho:", nudStock, 195);
        AddRow(group, "Nhóm hàng:", cboCategory, 235);

        var btnLoad = CreateButton("Tải lại", new Point(20, 280), new Size(130, 40));
        var btnAdd = CreateButton("Thêm", new Point(165, 280), new Size(130, 40), Color.FromArgb(40, 167, 69));
        var btnUpdate = CreateButton("Cập nhật", new Point(20, 335), new Size(130, 40), Color.FromArgb(0, 123, 255));
        var btnDelete = CreateButton("Xóa", new Point(165, 335), new Size(130, 40), Color.FromArgb(220, 53, 69));
        var btnExport = CreateButton("Xuất tồn kho (CSV)", new Point(20, 395), new Size(275, 40), Color.FromArgb(108, 117, 125));

        btnLoad.Click += async (_, _) => await LoadProductsAsync();
        btnAdd.Click += btnAdd_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnExport.Click += btnExport_Click;

        group.Controls.AddRange(new Control[] { btnLoad, btnAdd, btnUpdate, btnDelete, btnExport });
        return group;
    }

    private Panel BuildPagingPanel()
    {
        var panel = new Panel
        {
            Location = new Point(20, 520),
            Size = new Size(700, 40),
            BackColor = Color.White,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left
        };

        btnPreviousPage = CreateButton("◀ Trang trước", new Point(0, 3), new Size(115, 32));
        btnNextPage = CreateButton("Trang sau ▶", new Point(385, 3), new Size(105, 32));
        lblPageInfo = CreateLabel("Trang 1/1 | Tổng: 0 sản phẩm", new Point(120, 7), new Size(260, 25), 9);
        lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;

        cboPageSize = new ComboBox
        {
            Location = new Point(515, 5),
            Size = new Size(85, 28),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cboPageSize.Items.AddRange(new object[] { 5, 10, 20, 50 });
        cboPageSize.SelectedItem = DefaultPageSize;

        var lblPageSize = CreateLabel("Dòng/trang:", new Point(600, 7), new Size(90, 25), 9);
        lblPageSize.TextAlign = ContentAlignment.MiddleLeft;

        btnPreviousPage.Click += async (_, _) => await GoToPageAsync(_currentPage - 1);
        btnNextPage.Click += async (_, _) => await GoToPageAsync(_currentPage + 1);
        cboPageSize.SelectedIndexChanged += async (_, _) =>
        {
            if (cboPageSize.SelectedItem == null) return;

            _pageSize = Convert.ToInt32(cboPageSize.SelectedItem);
            await SearchAsync();
        };

        panel.Controls.AddRange(new Control[] { btnPreviousPage, lblPageInfo, btnNextPage, cboPageSize, lblPageSize });
        return panel;
    }

    private static void AddRow(GroupBox group, string labelText, Control input, int top)
    {
        input.Location = new Point(120, top - 3);
        input.Size = new Size(175, 28);

        group.Controls.Add(new Label { Text = labelText, Location = new Point(20, top), Size = new Size(100, 25) });
        group.Controls.Add(input);
    }

    private static Label CreateLabel(string text, Point location, Size size, float fontSize) => new()
    {
        Text = text,
        Location = location,
        Size = size,
        Font = new Font("Segoe UI", fontSize)
    };

    private static Button CreateButton(string text, Point location, Size size, Color? backColor = null)
    {
        var button = new Button { Text = text, Location = location, Size = size, FlatStyle = FlatStyle.Flat };

        if (backColor != null)
        {
            button.BackColor = backColor.Value;
            button.ForeColor = Color.White;
        }

        return button;
    }

    private static DataGridViewTextBoxColumn CreateColumn(
        string property, string header, string? format = null, float? fillWeight = null, bool visible = true)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = property,
            HeaderText = header,
            DataPropertyName = property,
            Visible = visible
        };

        if (format != null) column.DefaultCellStyle = new DataGridViewCellStyle { Format = format };
        if (fillWeight != null) column.FillWeight = fillWeight.Value;
        return column;
    }

    // ---------------------------------------------------------------
    // Tải dữ liệu
    // ---------------------------------------------------------------

    private async void FormProductManagement_Load(object? sender, EventArgs e)
    {
        await LoadCategoriesAsync();
        await LoadProductsAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await Ui.FetchAsync<List<CategoryDto>>("categories", "Nạp danh mục") ?? new List<CategoryDto>();

        cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
        cboCategory.ValueMember = nameof(CategoryDto.CategoryId);
        cboCategory.DataSource = new List<CategoryDto>(categories);

        var filter = new List<CategoryDto> { new() { CategoryId = 0, CategoryName = "-- Tất cả --" } };
        filter.AddRange(categories);
        cboFilterCategory.DisplayMember = nameof(CategoryDto.CategoryName);
        cboFilterCategory.ValueMember = nameof(CategoryDto.CategoryId);
        cboFilterCategory.DataSource = filter;
    }

    private async Task LoadProductsAsync()
    {
        var url = $"products/paged?keyword={Uri.EscapeDataString(txtSearchBarcode.Text.Trim())}" +
                  $"&pageNumber={_currentPage}&pageSize={_pageSize}";

        if (cboFilterCategory.SelectedValue is int categoryId && categoryId > 0)
            url += $"&categoryId={categoryId}";

        var result = await Ui.FetchAsync<PagedResult<ProductDto>>(url, "Nạp sản phẩm");
        if (result == null) return;

        _totalPages = result.TotalPages;

        // Trang hiện tại không còn hợp lệ (ví dụ sau khi xóa dữ liệu) -> lùi về trang cuối
        if (_totalPages > 0 && _currentPage > _totalPages)
        {
            _currentPage = _totalPages;
            await LoadProductsAsync();
            return;
        }

        if (_totalPages == 0) _currentPage = 1;

        dgvProducts.DataSource = null;
        dgvProducts.DataSource = result.Items;

        lblPageInfo.Text = $"Trang {_currentPage}/{Math.Max(1, _totalPages)} | Tổng: {result.TotalItems} sản phẩm";
        btnPreviousPage.Enabled = _currentPage > 1;
        btnNextPage.Enabled = _currentPage < _totalPages;
    }

    private async Task SearchAsync()
    {
        _currentPage = 1;
        await LoadProductsAsync();
    }

    private async Task GoToPageAsync(int page)
    {
        if (page < 1 || page > _totalPages) return;

        _currentPage = page;
        await LoadProductsAsync();
    }

    // ---------------------------------------------------------------
    // Thêm / sửa / xóa
    // ---------------------------------------------------------------

    // Tô màu dòng có tồn kho thấp: đỏ (<= 5) và cam (<= 10)
    private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.CellStyle == null) return;
        if (dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductDto product) return;
        if (product.StockQuantity > LowStockThreshold) return;

        e.CellStyle.BackColor = product.StockQuantity <= CriticalStockThreshold ? CriticalStockColor : LowStockColor;
        e.CellStyle.ForeColor = Color.Black;
    }

    private async void btnExport_Click(object? sender, EventArgs e)
    {
        var products = await Ui.FetchAsync<List<ProductDto>>("products", "Xuất tồn kho");
        if (products == null) return;

        if (products.Count == 0)
        {
            Ui.Warn("Chưa có sản phẩm để xuất!");
            return;
        }

        CsvExporter.Export(
            $"ton-kho_{DateTime.Now:yyyyMMdd_HHmm}.csv",
            new[] { "Mã SP", "Mã vạch", "Tên sản phẩm", "Nhóm hàng", "Đơn giá", "Tồn kho", "Trạng thái" },
            products.Select(p => (IEnumerable<object?>)new object?[]
            {
                p.ProductId, p.Barcode, p.ProductName, p.CategoryName, p.Price, p.StockQuantity,
                p.StockQuantity <= LowStockThreshold ? "Sắp hết hàng" : "Bình thường"
            }));
    }

    private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvProducts.ItemAt<ProductDto>(e.RowIndex) is not { } product) return;

        txtId.Text = product.ProductId.ToString();
        txtBarcode.Text = product.Barcode;
        txtProductName.Text = product.ProductName;
        nudPrice.Value = product.Price;
        nudStock.Value = product.StockQuantity;
        cboCategory.SelectedValue = product.CategoryId;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        if (!ValidateInputs()) return;

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("products", BuildProductRequest()),
            "Thêm sản phẩm", "Thêm mới sản phẩm thành công!",
            async () =>
            {
                _currentPage = 1;
                await LoadProductsAsync();
                ClearInputs();
            });
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id)) return;
        if (!ValidateInputs()) return;

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"products/{id}", BuildProductRequest()),
            "Cập nhật sản phẩm", "Cập nhật thành công!",
            LoadProductsAsync);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id)) return;
        if (!Ui.Confirm($"Xác nhận xóa sản phẩm ID = {id}?")) return;

        await Ui.SubmitAsync(
            ApiClient.Http.DeleteAsync($"products/{id}"),
            "Xóa sản phẩm", "Đã xóa sản phẩm!",
            async () =>
            {
                await LoadProductsAsync();
                ClearInputs();
            });
    }

    private object BuildProductRequest() => new
    {
        Barcode = txtBarcode.Text.Trim(),
        ProductName = txtProductName.Text.Trim(),
        Price = nudPrice.Value,
        StockQuantity = (int)nudStock.Value,
        CategoryId = (int)cboCategory.SelectedValue!
    };

    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
        {
            Ui.Warn("Mã vạch và tên sản phẩm không được trống!");
            return false;
        }

        if (nudPrice.Value <= 0)
        {
            Ui.Warn("Đơn giá phải lớn hơn 0!");
            return false;
        }

        if (cboCategory.SelectedValue == null)
        {
            Ui.Warn("Vui lòng chọn nhóm hàng!");
            return false;
        }

        return true;
    }

    private void ClearInputs()
    {
        txtId.Clear();
        txtBarcode.Clear();
        txtProductName.Clear();
        nudPrice.Value = 0;
        nudStock.Value = 0;
    }
}
