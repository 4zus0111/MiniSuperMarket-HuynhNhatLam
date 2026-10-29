using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormCategoryManagement : Form
{
    public FormCategoryManagement()
    {
        InitializeComponent();
        toolStripStatusLabel2.Text = ApiClient.BaseUrl + "categories";
        Shown += FormCategoryManagement_Shown;
    }

    private async void FormCategoryManagement_Shown(object? sender, EventArgs e)
    {
        // Chỉ Admin được thêm / sửa / xóa nhóm hàng
        var canEdit = SessionManager.IsInRole(AppRoles.Admin);
        btnAdd.Enabled = canEdit;
        btnUpdate.Enabled = canEdit;
        btnDelete.Enabled = canEdit;

        await LoadDataAsync();
    }

    private async void btnLoad_Click(object? sender, EventArgs e) => await LoadDataAsync();

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
        var keyword = txtKeyword.Text.Trim();
        if (keyword.Length == 0)
        {
            await LoadDataAsync();
            return;
        }

        dgvCategories.DataSource = await Ui.FetchAsync<List<CategoryDto>>(
            $"categories/search?keyword={Uri.EscapeDataString(keyword)}", "Tìm kiếm nhóm hàng");
    }

    private void dgvCategories_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvCategories.ItemAt<CategoryDto>(e.RowIndex) is not { } category) return;

        txtId.Text = category.CategoryId.ToString();
        txtCategoryName.Text = category.CategoryName;
        txtDescription.Text = category.Description ?? string.Empty;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        var category = new { CategoryName = txtCategoryName.Text, Description = txtDescription.Text };

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("categories", category),
            "Thêm nhóm hàng", "Thêm mới thành công!", ReloadAsync);
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn nhóm hàng cần sửa!");
            return;
        }

        var category = new { CategoryId = id, CategoryName = txtCategoryName.Text, Description = txtDescription.Text };

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"categories/{id}", category),
            "Cập nhật nhóm hàng", "Cập nhật thành công!", ReloadAsync);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out var id))
        {
            Ui.Warn("Vui lòng chọn nhóm hàng cần xóa!");
            return;
        }

        if (!Ui.Confirm($"Bạn có chắc muốn xóa nhóm hàng ID = {id}?")) return;

        await Ui.SubmitAsync(
            ApiClient.Http.DeleteAsync($"categories/{id}"),
            "Xóa nhóm hàng", "Xóa thành công!", ReloadAsync);
    }

    private async Task LoadDataAsync() =>
        dgvCategories.DataSource = await Ui.FetchAsync<List<CategoryDto>>("categories");

    private async Task ReloadAsync()
    {
        await LoadDataAsync();
        ClearInputs();
    }

    private void ClearInputs()
    {
        txtId.Clear();
        txtCategoryName.Clear();
        txtDescription.Clear();
    }
}
