using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MiniSupermarket.WinForms.SessionManager;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        public FormUserManagement()
        {
            InitializeComponent();

            cboRole.Items.AddRange(new string[]
            {
            "Admin",
            "Cashier",
            "Warehouse"
            });

            cboRole.SelectedIndex = 1;
        }

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");

                dgvUsers.DataSource = null;
                dgvUsers.DataSource = users;

                if (dgvUsers.Columns["Id"] != null)
                    dgvUsers.Columns["Id"].Visible = false;

                if (dgvUsers.Columns["Username"] != null)
                    dgvUsers.Columns["Username"].HeaderText = "Tên đăng nhập";

                if (dgvUsers.Columns["FullName"] != null)
                    dgvUsers.Columns["FullName"].HeaderText = "Họ tên";

                if (dgvUsers.Columns["Email"] != null)
                    dgvUsers.Columns["Email"].HeaderText = "Email";

                if (dgvUsers.Columns["Role"] != null)
                    dgvUsers.Columns["Role"].HeaderText = "Vai trò";

                if (dgvUsers.Columns["IsActive"] != null)
                    dgvUsers.Columns["IsActive"].HeaderText = "Trạng thái";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Tên đăng nhập và mật khẩu không được trống!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier"
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync(
                    "users",
                    newUser);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Tạo tài khoản mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                    ClearInput();
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Tạo tài khoản thất bại!\n" + message,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần đặt lại mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var userId = Convert.ToInt32(
                dgvUsers.CurrentRow.Cells["Id"].Value);

            string newPassword = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu mới!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var data = new
            {
                Password = newPassword
            };

            try
            {
                var res = await ApiClientService.Client.PutAsJsonAsync(
                    $"users/{userId}/reset-password",
                    data);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể đặt lại mật khẩu!",
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnToggleLock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var userId = Convert.ToInt32(
                dgvUsers.CurrentRow.Cells["Id"].Value);

            bool isActive = Convert.ToBoolean(
                dgvUsers.CurrentRow.Cells["IsActive"].Value);

            try
            {
                var data = new
                {
                    IsActive = !isActive
                };

                var res = await ApiClientService.Client.PutAsJsonAsync(
                    $"users/{userId}/toggle-status",
                    data);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        !isActive
                            ? "Đã mở khóa tài khoản!"
                            : "Đã khóa tài khoản!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể thay đổi trạng thái tài khoản!",
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvUsers.Rows[e.RowIndex];

            txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
            txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtPassword.Clear();

            string role = row.Cells["Role"].Value?.ToString() ?? "Cashier";

            if (cboRole.Items.Contains(role))
                cboRole.SelectedItem = role;
        }

        private void ClearInput()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 1;
        }
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

}