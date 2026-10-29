using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormCustomerManagement : Form
{
    private const string DefaultRank = "Chuẩn";
    private const int MaxPhoneLength = 15;

    public FormCustomerManagement()
    {
        InitializeComponent();

        Load += FormCustomerManagement_Load;
        btnLoad.Click += btnLoad_Click;
        btnAdd.Click += btnAdd_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnSearch.Click += btnSearch_Click;
        dgvCustomers.CellClick += dgvCustomers_CellClick;
    }

    private async void FormCustomerManagement_Load(object? sender, EventArgs e)
    {
        txtCustomerId.ReadOnly = true;
        ResetRewardFields();
        await LoadCustomersAsync();
    }

    private async void btnLoad_Click(object? sender, EventArgs e) => await LoadCustomersAsync();

    // Ô "Tên khách hàng" dùng làm từ khóa tìm theo tên hoặc số điện thoại
    private async void btnSearch_Click(object? sender, EventArgs e)
    {
        var keyword = txtCustomerName.Text.Trim();
        if (keyword.Length == 0)
        {
            await LoadCustomersAsync();
            return;
        }

        BindCustomers(await Ui.FetchAsync<List<CustomerDto>>(
            $"customers/search?keyword={Uri.EscapeDataString(keyword)}", "Tìm kiếm khách hàng"));
    }

    private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvCustomers.ItemAt<CustomerDto>(e.RowIndex) is not { } customer) return;

        txtCustomerId.Text = customer.CustomerId.ToString();
        txtCustomerName.Text = customer.CustomerName;
        txtPhoneNumber.Text = customer.PhoneNumber;
        txtAddress.Text = customer.Address ?? string.Empty;
        txtRewardPoints.Text = customer.RewardPoints.ToString();
        txtMembershipRank.Text = customer.MembershipRank;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        var customer = BuildCustomer(id: 0, rewardPoints: 0, rank: DefaultRank);

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("customers", customer),
            "Thêm khách hàng", "Thêm khách hàng thành công!", ReloadAsync);
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtCustomerId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn khách hàng cần cập nhật!");
            return;
        }

        if (!ValidateInput()) return;

        if (!int.TryParse(txtRewardPoints.Text, out var rewardPoints))
        {
            Ui.Warn("Điểm tích lũy phải là số nguyên!");
            txtRewardPoints.Focus();
            return;
        }

        var customer = BuildCustomer(id, rewardPoints, txtMembershipRank.Text.Trim());

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"customers/{id}", customer),
            "Cập nhật khách hàng", "Cập nhật khách hàng thành công!", ReloadAsync);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtCustomerId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn khách hàng cần xóa!");
            return;
        }

        if (!Ui.Confirm("Bạn có chắc chắn muốn xóa khách hàng này?")) return;

        await Ui.SubmitAsync(
            ApiClient.Http.DeleteAsync($"customers/{id}"),
            "Xóa khách hàng", "Xóa khách hàng thành công!", ReloadAsync);
    }

    private CustomerDto BuildCustomer(int id, int rewardPoints, string rank) => new()
    {
        CustomerId = id,
        CustomerName = txtCustomerName.Text.Trim(),
        PhoneNumber = txtPhoneNumber.Text.Trim(),
        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
        RewardPoints = rewardPoints,
        MembershipRank = rank
    };

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            return Invalid("Vui lòng nhập tên khách hàng!", txtCustomerName);

        if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            return Invalid("Vui lòng nhập số điện thoại!", txtPhoneNumber);

        if (txtPhoneNumber.Text.Trim().Length > MaxPhoneLength)
            return Invalid($"Số điện thoại không được vượt quá {MaxPhoneLength} ký tự!", txtPhoneNumber);

        return true;
    }

    private static bool Invalid(string message, Control focusTarget)
    {
        Ui.Warn(message);
        focusTarget.Focus();
        return false;
    }

    private async Task LoadCustomersAsync() =>
        BindCustomers(await Ui.FetchAsync<List<CustomerDto>>("customers", "Tải danh sách khách hàng"));

    private async Task ReloadAsync()
    {
        await LoadCustomersAsync();
        ClearInput();
    }

    private void BindCustomers(List<CustomerDto>? customers)
    {
        dgvCustomers.DataSource = customers;
        ConfigureGrid();
    }

    private void ConfigureGrid()
    {
        if (dgvCustomers.Columns.Count == 0) return;

        dgvCustomers.SetHeaders(
            ("CustomerId", "Mã KH"),
            ("CustomerName", "Tên khách hàng"),
            ("PhoneNumber", "Số điện thoại"),
            ("Address", "Địa chỉ"),
            ("RewardPoints", "Điểm tích lũy"),
            ("MembershipRank", "Hạng thành viên"));

        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.MultiSelect = false;
        dgvCustomers.ReadOnly = true;
        dgvCustomers.AllowUserToAddRows = false;
    }

    private void ClearInput()
    {
        txtCustomerId.Clear();
        txtCustomerName.Clear();
        txtPhoneNumber.Clear();
        txtAddress.Clear();
        ResetRewardFields();
        txtCustomerName.Focus();
    }

    private void ResetRewardFields()
    {
        txtRewardPoints.Text = "0";
        txtMembershipRank.Text = DefaultRank;
    }
}
