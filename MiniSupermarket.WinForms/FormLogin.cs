using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7158/api/")
        };


    public FormLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                var loginData = new
                {
                    Username = username,
                    Password = password
                };

                var response = await _client.PostAsJsonAsync(
                    "auth/login",
                    loginData);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Sai tài khoản hoặc mật khẩu!",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(jsonString);

                SessionManager.JwtToken =
                    doc.RootElement.GetProperty("token").GetString()
                    ?? string.Empty;

                SessionManager.CurrentRole =
                    doc.RootElement.GetProperty("role").GetString()
                    ?? string.Empty;

                MessageBox.Show(
                    $"Đăng nhập thành công với quyền: {SessionManager.CurrentRole}",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                FormMainShell frmMain = new FormMainShell();

                this.Hide();

                frmMain.ShowDialog();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối đến Server: " + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

}
