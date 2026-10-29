using System.Net;
using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormPOS : Form
{
    private const string WalkInCustomerName = "Khách vãng lai";
    private const int PointsPerRedemption = 50;
    private const decimal DiscountPerRedemption = 50_000m;
    private const decimal DiscountPerPoint = DiscountPerRedemption / PointsPerRedemption;

    private readonly List<CartItemDto> _cart = new();
    private bool _formattingCash;
    private CustomerDto? _customer;

    private readonly Label lblCustomerPoints = new()
    {
        AutoSize = true,
        ForeColor = Color.DarkGoldenrod,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
    };

    private readonly Label lblRedeemTitle = new()
    {
        AutoSize = true,
        Text = $"Đổi điểm ({PointsPerRedemption} điểm = {DiscountPerRedemption:N0} đ):"
    };

    private readonly NumericUpDown nudRedeem = new() { Minimum = 0, Maximum = 0, Increment = PointsPerRedemption, Enabled = false };

    private readonly Label lblDiscountInfo = new() { AutoSize = false, Height = 22, ForeColor = Color.DarkGreen };

    private readonly Button btnShiftRevenue = new() { Text = "Doanh thu ca của tôi", FlatStyle = FlatStyle.Flat };

    public FormPOS()
    {
        InitializeComponent();
        SetupCartGrid();

        KeyPreview = true;
        KeyDown += FormPOS_KeyDown;

        pnlRight.AutoScroll = true;
        Load += (_, _) => ApplyResponsiveLayout();
        Resize += (_, _) => ApplyResponsiveLayout();
        Shown += (_, _) => txtBarcode.Focus();

        txtCashReceived.BringToFront();
        txtCashReceived.KeyPress += (_, e) =>
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        };
        txtCashReceived.Enter += (_, _) => txtCashReceived.SelectAll();

        pnlRight.Controls.AddRange(new Control[] { lblCustomerPoints, lblRedeemTitle, nudRedeem, lblDiscountInfo, btnShiftRevenue });
        nudRedeem.ValueChanged += (_, _) => RefreshTotals();
        btnShiftRevenue.Click += btnShiftRevenue_Click;

        txtCustomerPhone.Leave += async (_, _) => await LookupCustomerAsync();
        txtCustomerPhone.TextChanged += (_, _) =>
        {
            if (_customer != null && txtCustomerPhone.Text.Trim() != _customer.PhoneNumber) SetCustomer(null);
        };
        txtCustomerPhone.KeyDown += async (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            await LookupCustomerAsync();
        };
    }

    private decimal CartTotal => _cart.Sum(x => x.TotalPrice);

    private int RedeemPoints => (int)nudRedeem.Value;

    private decimal RedeemDiscount => RedeemPoints * DiscountPerPoint;

    private decimal Payable => CartTotal - RedeemDiscount;

    private void ApplyResponsiveLayout()
    {
        pnlLeft.Width = Math.Max(500, (int)(ClientSize.Width * 0.58));
        var width = Math.Max(200, pnlRight.ClientSize.Width - 50);

        int[] tops = { 10, 55, 80, 122, 145, 172, 198, 222, 262, 285, 340, 366, 390, 436, 458, 515, 570, 622 };
        Control[] controls =
        {
            lblPaymentTitle, lblCustomerPhone, txtCustomerPhone, lblCustomerNameTitle, lblCustomerName,
            lblCustomerPoints, lblRedeemTitle, nudRedeem,
            lblTotalTitle, lblTotalAmount, lblDiscountInfo, lblCashReceived, txtCashReceived, lblChangeTitle, lblChange,
            btnCheckout, btnClearCart, btnShiftRevenue
        };

        for (var i = 0; i < controls.Length; i++)
        {
            var control = controls[i];
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            control.Top = tops[i];
            control.Left = 25;

            if (control is TextBox or Button or NumericUpDown || control == lblTotalAmount || control == lblChange || control == lblDiscountInfo)
                control.Width = width;
        }

        lblPaymentTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        lblTotalAmount.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        lblTotalAmount.Height = 50;
        lblChange.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        lblChange.Height = 45;
        btnCheckout.Height = 50;
        btnClearCart.Height = 42;
        btnShiftRevenue.Height = 38;
        pnlRight.AutoScrollMinSize = new Size(0, 680);
    }

    private void SetupCartGrid()
    {
        dgvCart.AutoGenerateColumns = false;
        dgvCart.Columns.Clear();

        dgvCart.Columns.Add(TextColumn(nameof(CartItemDto.ProductId), "Mã SP", width: 80));
        dgvCart.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CartItemDto.ProductName),
            HeaderText = "Tên Sản Phẩm",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dgvCart.Columns.Add(TextColumn(nameof(CartItemDto.UnitPrice), "Đơn Giá", width: 110, format: "N0"));
        dgvCart.Columns.Add(TextColumn(nameof(CartItemDto.Quantity), "SL", width: 70));
        dgvCart.Columns.Add(TextColumn(nameof(CartItemDto.TotalPrice), "Thành Tiền", width: 120, format: "N0"));
    }

    private static DataGridViewTextBoxColumn TextColumn(string property, string header, int width, string? format = null) => new()
    {
        DataPropertyName = property,
        HeaderText = header,
        Width = width,
        DefaultCellStyle = new DataGridViewCellStyle { Format = format }
    };

    private async void txtBarcode_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;

        e.SuppressKeyPress = true;

        var barcode = txtBarcode.Text.Trim();
        if (barcode.Length == 0) return;

        txtBarcode.Clear();
        await AddProductToCartAsync(barcode);
    }

    private async Task AddProductToCartAsync(string barcode)
    {
        var product = await FindProductAsync(barcode);
        if (product != null && TryAddToCart(product))
            UpdateCartDisplay();

        txtBarcode.Focus();
    }

    private async Task<ProductDto?> FindProductAsync(string barcode)
    {
        try
        {
            var response = await ApiClient.Http.GetAsync($"products/barcode/{Uri.EscapeDataString(barcode)}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Ui.Warn("Không tìm thấy sản phẩm có mã vạch này!");
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                await Ui.ShowApiErrorAsync(response, "Tra cứu sản phẩm");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }
        catch (HttpRequestException ex)
        {
            Ui.ShowException(ex, "Tra cứu sản phẩm");
            return null;
        }
    }

    private bool TryAddToCart(ProductDto product)
    {
        var existing = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);

        if (existing != null)
        {
            if (existing.Quantity >= product.StockQuantity)
            {
                Ui.Warn("Số lượng sản phẩm trong kho không đủ!");
                return false;
            }

            existing.Quantity++;
            return true;
        }

        if (product.StockQuantity <= 0)
        {
            Ui.Warn("Sản phẩm đã hết hàng!");
            return false;
        }

        _cart.Add(new CartItemDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            UnitPrice = product.Price,
            Quantity = 1
        });
        return true;
    }

    private void UpdateCartDisplay()
    {
        dgvCart.DataSource = null;
        dgvCart.DataSource = _cart;

        UpdateRedeemLimit();
        RefreshTotals();
    }

    /// <summary>Số điểm đổi tối đa = theo điểm khách đang có và không vượt quá giá trị giỏ hàng.</summary>
    private void UpdateRedeemLimit()
    {
        var stepsByPoints = (_customer?.RewardPoints ?? 0) / PointsPerRedemption;
        var stepsBySubtotal = (int)(CartTotal / DiscountPerRedemption);
        var steps = Math.Min(stepsByPoints, stepsBySubtotal);

        nudRedeem.Maximum = steps * PointsPerRedemption;
        nudRedeem.Enabled = steps > 0;
    }

    private void RefreshTotals()
    {
        lblTotalAmount.Text = $"{Payable:N0} đ";
        lblDiscountInfo.Text = RedeemPoints > 0
            ? $"Tạm tính {CartTotal:N0} đ − giảm {RedeemDiscount:N0} đ ({RedeemPoints} điểm)"
            : string.Empty;
        CalculateChange();
    }

    private bool TryGetCash(out decimal cash)
    {
        var digits = new string(txtCashReceived.Text.Where(char.IsDigit).ToArray());
        return decimal.TryParse(digits, out cash);
    }

    private void txtCashReceived_TextChanged(object? sender, EventArgs e)
    {
        if (!_formattingCash && TryGetCash(out var cash))
        {
            _formattingCash = true;
            txtCashReceived.Text = cash.ToString("N0");
            txtCashReceived.SelectionStart = txtCashReceived.Text.Length;
            _formattingCash = false;
        }

        CalculateChange();
    }

    private void CalculateChange()
    {
        if (!TryGetCash(out var cashReceived))
        {
            lblChange.Text = "0 đ";
            lblChange.ForeColor = Color.Black;
            return;
        }

        var change = cashReceived - Payable;
        if (change >= 0)
        {
            lblChange.Text = $"{change:N0} đ";
            lblChange.ForeColor = Color.DarkGreen;
        }
        else
        {
            lblChange.Text = "Chưa đủ tiền!";
            lblChange.ForeColor = Color.Red;
        }
    }

    private async void btnCheckout_Click(object? sender, EventArgs e)
    {
        if (_cart.Count == 0)
        {
            Ui.Warn("Giỏ hàng đang trống!");
            txtBarcode.Focus();
            return;
        }

        if (!TryGetCash(out var cashReceived))
        {
            Ui.Warn("Vui lòng nhập số tiền khách đưa!");
            txtCashReceived.Focus();
            return;
        }

        if (RedeemPoints % PointsPerRedemption != 0)
        {
            Ui.Warn($"Số điểm đổi phải là bội số của {PointsPerRedemption}!");
            nudRedeem.Focus();
            return;
        }

        if (cashReceived < Payable)
        {
            Ui.Warn("Số tiền khách đưa chưa đủ!");
            txtCashReceived.Focus();
            return;
        }

        var request = new
        {
            CustomerPhone = txtCustomerPhone.Text.Trim(),
            RedeemPoints,
            CashReceived = cashReceived,
            Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
        };

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("orders/checkout", request),
            "Thanh toán",
            "Thanh toán thành công và đã in hóa đơn!",
            () =>
            {
                ResetSale();
                return Task.CompletedTask;
            });
    }

    private void btnClearCart_Click(object? sender, EventArgs e)
    {
        if (_cart.Count == 0) return;
        if (!Ui.Confirm("Bạn có chắc muốn hủy toàn bộ giỏ hàng?")) return;

        ResetSale();
    }

    private void ResetSale()
    {
        _cart.Clear();
        UpdateCartDisplay();

        txtCashReceived.Clear();
        txtCustomerPhone.Clear();
        SetCustomer(null);
        txtBarcode.Focus();
    }

    // ---------------------------------------------------------------
    // Khách hàng thân thiết và điểm thưởng
    // ---------------------------------------------------------------

    private async Task LookupCustomerAsync()
    {
        var phone = txtCustomerPhone.Text.Trim();
        if (phone.Length == 0)
        {
            SetCustomer(null);
            return;
        }

        if (_customer?.PhoneNumber == phone) return;

        try
        {
            var response = await ApiClient.Http.GetAsync($"customers/phone/{Uri.EscapeDataString(phone)}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                SetCustomer(null);
                lblCustomerName.Text = "Chưa đăng ký thành viên";
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                SetCustomer(null);
                await Ui.ShowApiErrorAsync(response, "Tra cứu khách hàng");
                return;
            }

            SetCustomer(await response.Content.ReadFromJsonAsync<CustomerDto>());
        }
        catch (HttpRequestException ex)
        {
            Ui.ShowException(ex, "Tra cứu khách hàng");
        }
    }

    private void SetCustomer(CustomerDto? customer)
    {
        _customer = customer;
        lblCustomerName.Text = customer?.CustomerName ?? WalkInCustomerName;
        lblCustomerPoints.Text = customer != null
            ? $"Điểm tích lũy: {customer.RewardPoints:N0} ({customer.MembershipRank})"
            : string.Empty;

        nudRedeem.Value = 0;
        UpdateRedeemLimit();
        RefreshTotals();
    }

    private async void btnShiftRevenue_Click(object? sender, EventArgs e)
    {
        var report = await Ui.FetchAsync<ShiftRevenueDto>("reports/shift-revenue", "Xem doanh thu ca");
        if (report == null) return;

        Ui.Info(
            $"Doanh thu ca của {report.Cashier}\n" +
            $"Từ {report.From:HH:mm dd/MM/yyyy} đến {report.To:HH:mm dd/MM/yyyy}\n\n" +
            $"Số đơn đã bán: {report.TotalOrders}\n" +
            $"Doanh thu: {report.Revenue:N0} đ");
    }

    private void FormPOS_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.F9) return;

        e.SuppressKeyPress = true;
        btnCheckout.PerformClick();
    }
}
