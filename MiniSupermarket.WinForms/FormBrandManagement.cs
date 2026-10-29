using System.Net.Http.Json;
using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormBrandManagement : Form
{
    // Thương hiệu đang chọn trên lưới; giữ lại để cập nhật không làm mất LogoUrl
    private BrandDto? _selected;

    public FormBrandManagement() => InitializeComponent();

    private async void FormBrandManagement_Load(object? sender, EventArgs e) => await LoadBrandsAsync();

    private async void btnSearch_Click(object? sender, EventArgs e) => await LoadBrandsAsync(txtSearch.Text.Trim());

    private async void btnReload_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        await LoadBrandsAsync();
    }

    private void dgvBrands_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (dgvBrands.ItemAt<BrandDto>(e.RowIndex) is not { } brand) return;

        _selected = brand;
        txtId.Text = brand.BrandId.ToString();
        txtName.Text = brand.BrandName;
        txtDescription.Text = brand.Description ?? string.Empty;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            Ui.Warn("Tên thương hiệu không được để trống!");
            return;
        }

        await Ui.SubmitAsync(
            ApiClient.Http.PostAsJsonAsync("brands", BuildBrand(id: 0, logoUrl: null)),
            "Thêm thương hiệu", "Thêm thành công!", ReloadAsync);
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (_selected == null)
        {
            Ui.Warn("Vui lòng click chọn 1 thương hiệu trên lưới để sửa!");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            Ui.Warn("Tên thương hiệu không được để trống!");
            return;
        }

        var brand = BuildBrand(_selected.BrandId, _selected.LogoUrl);

        await Ui.SubmitAsync(
            ApiClient.Http.PutAsJsonAsync($"brands/{brand.BrandId}", brand),
            "Cập nhật thương hiệu", "Sửa thành công!", ReloadAsync);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;
        if (!Ui.Confirm("Bạn có chắc chắn muốn xóa?")) return;

        await Ui.SubmitAsync(
            ApiClient.Http.DeleteAsync($"brands/{_selected.BrandId}"),
            "Xóa thương hiệu", "Xóa thành công!", ReloadAsync);
    }

    private BrandDto BuildBrand(int id, string? logoUrl) => new()
    {
        BrandId = id,
        BrandName = txtName.Text.Trim(),
        Description = txtDescription.Text.Trim(),
        LogoUrl = logoUrl
    };

    private async Task LoadBrandsAsync(string keyword = "")
    {
        var url = keyword.Length == 0 ? "brands" : $"brands/search?keyword={Uri.EscapeDataString(keyword)}";
        dgvBrands.DataSource = await Ui.FetchAsync<List<BrandDto>>(url, "Tải thương hiệu");
        ClearInputs();
    }

    private async Task ReloadAsync() => await LoadBrandsAsync();

    private void ClearInputs()
    {
        _selected = null;
        txtId.Clear();
        txtName.Clear();
        txtDescription.Clear();
    }
}
