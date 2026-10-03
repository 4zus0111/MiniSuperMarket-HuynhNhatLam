using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // =========================================================
        // HTTP CLIENT
        // =========================================================

        private readonly HttpClient _httpClient;

        // Sửa port theo MiniSupermarket.API của bạn
        private const string ApiUrl =
            "https://localhost:7158/api/customers";

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public FormCustomerManagement()
        {
            InitializeComponent();

            _httpClient = new HttpClient();

            // =========================================================
            // GẮN TOKEN VÀO HTTPCLIENT TỰ ĐỘNG TỪ SESSION
            // =========================================================
            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            // Đăng ký sự kiện
            this.Load += FormCustomerManagement_Load;

            btnLoad.Click += btnLoad_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;

            dgvCustomers.CellClick += dgvCustomers_CellClick;
        }

        // =========================================================
        // MODEL CUSTOMER
        // =========================================================

        public class Customer
        {
            public int CustomerId { get; set; }

            public string CustomerName { get; set; } = string.Empty;

            public string PhoneNumber { get; set; } = string.Empty;

            public string? Address { get; set; }

            public int RewardPoints { get; set; }

            public string MembershipRank { get; set; } = "Chuẩn";
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private async void FormCustomerManagement_Load(
            object? sender,
            EventArgs e)
        {
            txtCustomerId.ReadOnly = true;

            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";

            await LoadCustomers();
        }

        // =========================================================
        // 1. GET ALL CUSTOMERS
        // GET /api/customers
        // =========================================================

        private async Task LoadCustomers()
        {
            try
            {
                var customers =
                    await _httpClient.GetFromJsonAsync<List<Customer>>(
                        ApiUrl);

                if (customers == null)
                {
                    dgvCustomers.DataSource = null;
                    return;
                }

                dgvCustomers.DataSource = customers;

                ConfigureDataGridView();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể kết nối đến API!\n\n" +
                    "Kiểm tra các thông tin sau:\n" +
                    "- MiniSupermarket.API đã chạy chưa?\n" +
                    "- Port API có đúng không?\n" +
                    "- URL API có chính xác không?\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẤU HÌNH DATAGRIDVIEW
        // =========================================================

        private void ConfigureDataGridView()
        {
            if (dgvCustomers.Columns.Count == 0)
                return;

            if (dgvCustomers.Columns["CustomerId"] != null)
                dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";

            if (dgvCustomers.Columns["CustomerName"] != null)
                dgvCustomers.Columns["CustomerName"].HeaderText =
                    "Tên khách hàng";

            if (dgvCustomers.Columns["PhoneNumber"] != null)
                dgvCustomers.Columns["PhoneNumber"].HeaderText =
                    "Số điện thoại";

            if (dgvCustomers.Columns["Address"] != null)
                dgvCustomers.Columns["Address"].HeaderText =
                    "Địa chỉ";

            if (dgvCustomers.Columns["RewardPoints"] != null)
                dgvCustomers.Columns["RewardPoints"].HeaderText =
                    "Điểm tích lũy";

            if (dgvCustomers.Columns["MembershipRank"] != null)
                dgvCustomers.Columns["MembershipRank"].HeaderText =
                    "Hạng thành viên";

            dgvCustomers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCustomers.MultiSelect = false;

            dgvCustomers.ReadOnly = true;

            dgvCustomers.AllowUserToAddRows = false;
        }

        // =========================================================
        // BUTTON LOAD
        // =========================================================

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            await LoadCustomers();
        }

        // =========================================================
        // 2. ADD CUSTOMER
        // POST /api/customers
        // =========================================================

        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!ValidateCustomerInput())
                    return;

                var customer = new Customer
                {
                    CustomerName =
                        txtCustomerName.Text.Trim(),

                    PhoneNumber =
                        txtPhoneNumber.Text.Trim(),

                    Address =
                        string.IsNullOrWhiteSpace(txtAddress.Text)
                        ? null
                        : txtAddress.Text.Trim(),

                    RewardPoints = 0,

                    MembershipRank = "Chuẩn"
                };

                var response =
                    await _httpClient.PostAsJsonAsync(
                        ApiUrl,
                        customer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomers();

                    ClearInput();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm khách hàng thất bại!\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi thêm khách hàng!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 3. UPDATE CUSTOMER
        // PUT /api/customers/{id}
        // =========================================================

        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!int.TryParse(
                    txtCustomerId.Text,
                    out int customerId))
                {
                    MessageBox.Show(
                        "Vui lòng chọn khách hàng cần cập nhật!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (!ValidateCustomerInput())
                    return;

                if (!int.TryParse(
                    txtRewardPoints.Text,
                    out int rewardPoints))
                {
                    MessageBox.Show(
                        "Điểm tích lũy phải là số nguyên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtRewardPoints.Focus();

                    return;
                }

                var customer = new Customer
                {
                    CustomerId = customerId,

                    CustomerName =
                        txtCustomerName.Text.Trim(),

                    PhoneNumber =
                        txtPhoneNumber.Text.Trim(),

                    Address =
                        string.IsNullOrWhiteSpace(txtAddress.Text)
                        ? null
                        : txtAddress.Text.Trim(),

                    RewardPoints = rewardPoints,

                    MembershipRank =
                        txtMembershipRank.Text.Trim()
                };

                string url =
                    $"{ApiUrl}/{customerId}";

                var response =
                    await _httpClient.PutAsJsonAsync(
                        url,
                        customer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomers();

                    ClearInput();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật khách hàng thất bại!\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi cập nhật khách hàng!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 4. DELETE CUSTOMER
        // DELETE /api/customers/{id}
        // =========================================================

        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!int.TryParse(
                    txtCustomerId.Text,
                    out int customerId))
                {
                    MessageBox.Show(
                        "Vui lòng chọn khách hàng cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa khách hàng này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                string url =
                    $"{ApiUrl}/{customerId}";

                var response =
                    await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomers();

                    ClearInput();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa khách hàng thất bại!\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi xóa khách hàng!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 5. SEARCH CUSTOMER
        // GET /api/customers/search?keyword=...
        // =========================================================

        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                string keyword =
                    txtCustomerName.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    await LoadCustomers();
                    return;
                }

                string url =
                    $"{ApiUrl}/search?keyword=" +
                    Uri.EscapeDataString(keyword);

                var customers =
                    await _httpClient
                        .GetFromJsonAsync<List<Customer>>(url);

                dgvCustomers.DataSource = customers;

                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Tìm kiếm khách hàng thất bại!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 6. CLICK DATA GRIDVIEW
        // =========================================================

        private void dgvCustomers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvCustomers.Rows[e.RowIndex];

            txtCustomerId.Text =
                row.Cells["CustomerId"]
                    .Value?.ToString() ?? "";

            txtCustomerName.Text =
                row.Cells["CustomerName"]
                    .Value?.ToString() ?? "";

            txtPhoneNumber.Text =
                row.Cells["PhoneNumber"]
                    .Value?.ToString() ?? "";

            txtAddress.Text =
                row.Cells["Address"]
                    .Value?.ToString() ?? "";

            txtRewardPoints.Text =
                row.Cells["RewardPoints"]
                    .Value?.ToString() ?? "0";

            txtMembershipRank.Text =
                row.Cells["MembershipRank"]
                    .Value?.ToString() ?? "Chuẩn";
        }

        // =========================================================
        // VALIDATE INPUT
        // =========================================================

        private bool ValidateCustomerInput()
        {
            if (string.IsNullOrWhiteSpace(
                txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên khách hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCustomerName.Focus();

                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtPhoneNumber.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPhoneNumber.Focus();

                return false;
            }

            if (txtPhoneNumber.Text.Trim().Length > 15)
            {
                MessageBox.Show(
                    "Số điện thoại không được vượt quá 15 ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPhoneNumber.Focus();

                return false;
            }

            return true;
        }

        // =========================================================
        // CLEAR INPUT
        // =========================================================

        private void ClearInput()
        {
            txtCustomerId.Clear();

            txtCustomerName.Clear();

            txtPhoneNumber.Clear();

            txtAddress.Clear();

            txtRewardPoints.Text = "0";

            txtMembershipRank.Text = "Chuẩn";

            txtCustomerName.Focus();
        }

        // =========================================================
        // GẮN JWT TOKEN
        // Gọi hàm này sau khi đăng nhập thành công
        // =========================================================
        // (Đã được chuyển lên Constructor, giữ lại phòng hờ nếu cần gán lại Token)
        public void SetJwtToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        // =========================================================
        // FORM CLOSED
        // =========================================================

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            _httpClient.Dispose();

            base.OnFormClosed(e);
        }
    }
}