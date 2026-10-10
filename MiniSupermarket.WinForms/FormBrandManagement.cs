using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MiniSupermarket.WinForms
{
    public partial class FormBrandManagement : Form
    {
        private readonly HttpClient _http;
        private const string API_URL = "https://localhost:7158/api/brands"; // Nhớ kiểm tra lại Port của API

        public FormBrandManagement()
        {
            InitializeComponent();
            _http = new HttpClient();
        }

        private async void FormBrandManagement_Load(object sender, EventArgs e)
        {
            await LoadBrands();
        }

        private async Task LoadBrands(string keyword = "")
        {
            try
            {
                string url = string.IsNullOrWhiteSpace(keyword)
                    ? API_URL
                    : $"{API_URL}/search?keyword={keyword}";

                var brands = await _http.GetFromJsonAsync<List<BrandDto>>(url);
                dgvBrands.DataSource = brands;

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối API: " + ex.Message);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadBrands(txtSearch.Text.Trim());
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadBrands();
        }

        private void dgvBrands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvBrands.Rows[e.RowIndex];
                txtId.Text = row.Cells["BrandId"].Value?.ToString();
                txtName.Text = row.Cells["BrandName"].Value?.ToString();
                txtDescription.Text = row.Cells["Description"].Value?.ToString();
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên thương hiệu không được để trống!");
                return;
            }

            var brand = new BrandDto { BrandName = txtName.Text.Trim(), Description = txtDescription.Text.Trim() };
            var response = await _http.PostAsJsonAsync(API_URL, brand);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm thành công!");
                await LoadBrands();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng click chọn 1 thương hiệu trên lưới để sửa!");
                return;
            }

            int id = int.Parse(txtId.Text);
            var brand = new BrandDto { BrandId = id, BrandName = txtName.Text.Trim(), Description = txtDescription.Text.Trim() };

            var response = await _http.PutAsJsonAsync($"{API_URL}/{id}", brand);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Sửa thành công!");
                await LoadBrands();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = int.Parse(txtId.Text);
                var response = await _http.DeleteAsync($"{API_URL}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa thành công!");
                    await LoadBrands();
                }
            }
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
        }
    }

    public class BrandDto
    {
        public int BrandId { get; set; }

        // Thêm dấu ? để không bị cảnh báo Non-nullable
        public string? BrandName { get; set; }
        public string? Description { get; set; }
    }
}