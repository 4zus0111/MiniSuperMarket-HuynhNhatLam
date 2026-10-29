using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormUserManagement : Form
{
    private static readonly string[] Roles = { AppRoles.Admin, AppRoles.Cashier, AppRoles.Warehouse };
    private const int DefaultRoleIndex = 1; // Cashier

    public FormUserManagement()
    {
        InitializeComponent();

        cboRole.Items.AddRange(Roles);
        cboRole.SelectedIndex = DefaultRoleIndex;
    }

    private UserDto? SelectedUser => dgvUsers.CurrentRow?.DataBoundItem as UserDto;

    private async void FormUserManagement_Load(object? sender, EventArgs e) => await LoadUsersAsync();

    private async void btnAddUser_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            Ui.Warn("Tên đăng nhập và mật khẩu không được trống!");
            return;
        }

        var newUser = new
        {
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text.Trim(),
            FullName = txtFullName.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Role = cboRole.SelectedItem?.ToString() ?? AppRoles.Cashier
        };

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("users", newUser),
            "Tạo tài khoản", "Tạo tài khoản mới thành công!",
            async () =>
            {
                await LoadUsersAsync();
                ClearInput();
            });
    }

    private async void btnResetPassword_Click(object? sender, EventArgs e)
    {
        if (SelectedUser is not { } user)
        {
            Ui.Warn("Vui lòng chọn tài khoản cần đặt lại mật khẩu!");
            return;
        }

        var newPassword = txtPassword.Text.Trim();
        if (newPassword.Length == 0)
        {
            Ui.Warn("Vui lòng nhập mật khẩu mới!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"users/{user.UserId}/reset-password", new { Password = newPassword }),
            "Đặt lại mật khẩu", "Đặt lại mật khẩu thành công!",
            () =>
            {
                txtPassword.Clear();
                return Task.CompletedTask;
            });
    }

    private async void btnToggleLock_Click(object? sender, EventArgs e)
    {
        if (SelectedUser is not { } user)
        {
            Ui.Warn("Vui lòng chọn tài khoản!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsync($"users/{user.UserId}/toggle-status", null),
            "Đổi trạng thái tài khoản",
            user.IsActive ? "Đã khóa tài khoản!" : "Đã mở khóa tài khoản!",
            LoadUsersAsync);
    }

    private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvUsers.ItemAt<UserDto>(e.RowIndex) is not { } user) return;

        txtUsername.Text = user.Username;
        txtFullName.Text = user.FullName;
        txtEmail.Text = user.Email ?? string.Empty;
        txtPassword.Clear();

        // API trả vai trò dạng chữ hoa (ADMIN), danh sách chọn dùng dạng Admin
        var index = Array.FindIndex(Roles, r => r.Equals(user.Role, StringComparison.OrdinalIgnoreCase));
        if (index >= 0) cboRole.SelectedIndex = index;
    }

    private async Task LoadUsersAsync()
    {
        dgvUsers.DataSource = null;
        dgvUsers.DataSource = await Ui.FetchAsync<List<UserDto>>("users", "Lấy danh sách tài khoản");

        if (dgvUsers.Columns.Count == 0) return;

        dgvUsers.Columns[nameof(UserDto.UserId)]!.Visible = false;
        dgvUsers.SetHeaders(
            (nameof(UserDto.Username), "Tên đăng nhập"),
            (nameof(UserDto.FullName), "Họ tên"),
            (nameof(UserDto.Email), "Email"),
            (nameof(UserDto.Role), "Vai trò"),
            (nameof(UserDto.IsActive), "Trạng thái"));
    }

    private void ClearInput()
    {
        txtUsername.Clear();
        txtFullName.Clear();
        txtEmail.Clear();
        txtPassword.Clear();
        cboRole.SelectedIndex = DefaultRoleIndex;
    }
}
