using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        public FormCategoryManagement()
        {
            InitializeComponent();

            // Gắn sự kiện Shown để tải dữ liệu an toàn sau khi form đã hiện lên
            this.Shown += FormCategoryManagement_Shown;
        }

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7158/api/")
            };

            // Đính kèm Token vào Header theo chuẩn Bearer Authentication
            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }
            return client;
        }








        // Sự kiện Form vừa bật lên: Tự động tải dữ liệu từ API lên bảng
        private async void FormCategoryManagement_Shown(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền khóa nút ngay khi hiển thị form
            if (SessionManager.CurrentRole != "Admin")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }

            await LoadDataAsync();
        }

        // Hàm dùng chung: Gọi API GET lấy danh sách và đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient(); // Sử dụng client đã gắn token
                var categories = await client.GetFromJsonAsync<List<CategoryDto>>("categories");
                dgvCategories.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quyền truy cập hoặc mất kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Nút Tải lại dữ liệu (Refresh)
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Sự kiện khi click vào một dòng trên DataGridView: Đưa dữ liệu lên các ô nhập (TextBox) để chuẩn bị Sửa/Xóa
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];
                txtId.Text = row.Cells["CategoryId"].Value.ToString();
                txtCategoryName.Text = row.Cells["CategoryName"].Value.ToString();
                txtDescription.Text = row.Cells["Description"]?.Value?.ToString() ?? string.Empty;
            }
        }

        // Nút THÊM MỚI (CREATE): Gửi dữ liệu POST lên Web API
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newCat = new
            {
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text
            };

            // Gửi request POST kèm theo đối tượng dạng JSON
            using var client = GetAuthenticatedClient(); // Sử dụng client đã gắn token
            var response = await client.PostAsJsonAsync("categories", newCat);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync(); // Tải lại danh sách mới
                ClearInputs();         // Xóa sạch ô nhập
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút CẬP NHẬT (UPDATE): Gửi dữ liệu PUT lên Web API theo ID
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var updateCat = new
            {
                CategoryId = id,
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text
            };

            // Gửi request PUT kèm ID trên đường dẫn URI
            using var client = GetAuthenticatedClient(); // Sử dụng client đã gắn token

            var response = await client.PutAsJsonAsync($"categories/{id}", updateCat);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút XÓA (DELETE): Gửi request DELETE lên Web API theo ID
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa nhóm hàng ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using var client = GetAuthenticatedClient(); // Sử dụng client đã gắn token

                var response = await client.DeleteAsync($"categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút TÌM KIẾM (SEARCH): Gọi API lọc danh mục theo từ khóa Query String
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync(); // Nếu ô tìm kiếm trống thì tải lại toàn bộ
                return;
            }

            try
            {
                using var client = GetAuthenticatedClient(); // Sử dụng client đã gắn token

                // Gọi API dạng: GET /api/categories/search?keyword=abc
                var result = await client.GetFromJsonAsync<List<CategoryDto>>($"categories/search?keyword={keyword}");
                dgvCategories.DataSource = result;
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Hàm phụ trợ: Xóa trắng các ô nhập liệu sau khi thao tác xong
        private void ClearInputs()
        {
            txtId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";
        }
    }

    // Lớp DTO trung gian tại Client hứng dữ liệu JSON trả về từ Server
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
