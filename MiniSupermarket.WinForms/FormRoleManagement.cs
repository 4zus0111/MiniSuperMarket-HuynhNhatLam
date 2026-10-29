using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormRoleManagement : Form
{
    public FormRoleManagement() => InitializeComponent();

    private async void FormRoleManagement_Load(object? sender, EventArgs e) => await LoadRolesAsync();

    private async void btnLoad_Click(object? sender, EventArgs e) => await LoadRolesAsync();

    private void dgvRoles_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvRoles.ItemAt<RoleDto>(e.RowIndex) is not { } role) return;

        txtId.Text = role.RoleId.ToString();
        txtRoleName.Text = role.RoleName;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtRoleName.Text))
        {
            Ui.Warn("Vui lòng nhập tên vai trò!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("roles", new { RoleName = txtRoleName.Text.Trim() }),
            "Thêm vai trò", "Thêm vai trò mới thành công!", ReloadAsync);
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn vai trò cần sửa!");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtRoleName.Text))
        {
            Ui.Warn("Vui lòng nhập tên vai trò!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"roles/{id}", new { RoleId = id, RoleName = txtRoleName.Text.Trim() }),
            "Cập nhật vai trò", "Cập nhật vai trò thành công!", ReloadAsync);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn vai trò cần xóa!");
            return;
        }

        if (!Ui.Confirm($"Bạn có chắc muốn xóa vai trò ID = {id}?")) return;

        await Ui.SubmitAsync(
            ApiClient.Http.DeleteAsync($"roles/{id}"),
            "Xóa vai trò", "Xóa vai trò thành công!", ReloadAsync);
    }

    private async Task LoadRolesAsync() =>
        dgvRoles.DataSource = await Ui.FetchAsync<List<RoleDto>>("roles");

    private async Task ReloadAsync()
    {
        await LoadRolesAsync();
        ClearInputs();
    }

    private void ClearInputs()
    {
        txtId.Clear();
        txtRoleName.Clear();
    }
}
