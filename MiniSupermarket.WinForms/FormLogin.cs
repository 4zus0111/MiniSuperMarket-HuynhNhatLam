using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {

        // Khởi tạo HttpClient trỏ đến địa chỉ của Web API Backend
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7158/api/")
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        // Sự kiện khi người dùng bấm nút Đăng nhập
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // Kiểm tra ràng buộc cơ bản phía Client
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đóng gói dữ liệu gửi lên endpoint POST /api/auth/login
                var loginData = new { Username = username, Password = password };
                var response = await _client.PostAsJsonAsync("auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc chuỗi JSON trả về từ Server khi đăng nhập thành công
                    var jsonString = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonString);

                    // Trích xuất Token và Role lưu vào lớp tĩnh SessionManager dùng chung toàn ứng dụng
                    SessionManager.JwtToken = doc.RootElement.GetProperty("token").GetString() ?? string.Empty;
                    SessionManager.CurrentRole = doc.RootElement.GetProperty("role").GetString() ?? string.Empty;

                    MessageBox.Show($"Đăng nhập thành công với quyền: {SessionManager.CurrentRole}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở Form quản lý Khách hàng và ẩn Form đăng nhập đi
                    FormCustomerManagement frmCustomer = new FormCustomerManagement();

                    this.Hide(); // Ẩn màn hình đăng nhập
                    frmCustomer.Show(); // Hiển thị màn hình Khách hàng (ShowDialog để tạm dừng code ở đây chờ người dùng dùng xong)

                    //this.Close(); // Đóng hẳn ứng dụng (tắt luôn FormLogin đang ẩn) khi người dùng tắt màn hình Khách hàng

                    // Mở Form quản lý Khách hàng và ẩn Form đăng nhập đi
                    FormCategoryManagement frmCategory = new FormCategoryManagement();

                    //this.Hide(); // Ẩn màn hình đăng nhập
                    frmCategory.Show(); // Hiển thị màn hình Khách hàng (ShowDialog để tạm dừng code ở đây chờ người dùng dùng xong)

                    //this.Close(); // Đóng hẳn ứng dụng (tắt luôn FormLogin đang ẩn) khi người dùng tắt màn hình Khách hàng


                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
