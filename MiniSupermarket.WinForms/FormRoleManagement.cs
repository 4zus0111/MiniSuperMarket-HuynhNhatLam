using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormRoleManagement : Form
    {
        // Kết nối đến API /api/roles (Đảm bảo Port khớp với Backend của bạn)
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7158/api/")
        };

        public FormRoleManagement()
        {
            InitializeComponent();
        }

        private async void FormRoleManagement_Load(object sender, EventArgs e)
        {
            await LoadRolesAsync();
        }

        private async Task LoadRolesAsync()
        {
            try
            {
                var roles = await _client.GetFromJsonAsync<List<RoleDto>>("roles");
                dgvRoles.DataSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRoles.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtRoleName.Text = row.Cells["RoleName"].Value.ToString();
                txtDescription.Text = row.Cells["Description"]?.Value?.ToString() ?? string.Empty;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newRole = new
            {
                RoleName = txtRoleName.Text,
                Description = txtDescription.Text
            };

            var response = await _client.PostAsJsonAsync("roles", newRole);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm vai trò mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadRolesAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn vai trò cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var updateRole = new
            {
                Id = id,
                RoleName = txtRoleName.Text,
                Description = txtDescription.Text
            };

            var response = await _client.PutAsJsonAsync($"roles/{id}", updateRole);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật vai trò thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadRolesAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn vai trò cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa vai trò ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                var response = await _client.DeleteAsync($"roles/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa vai trò thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadRolesAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadRolesAsync();
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtRoleName.Text = "";
            txtDescription.Text = "";
        }
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
