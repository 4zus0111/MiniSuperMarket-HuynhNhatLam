using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormLogin : Form
{
    public FormLogin() => InitializeComponent();

    private async void btnLogin_Click(object? sender, EventArgs e)
    {
        var username = txtUser.Text.Trim();
        var password = txtPass.Text.Trim();

        if (username.Length == 0 || password.Length == 0)
        {
            Ui.Warn("Vui lòng nhập đầy đủ tài khoản và mật khẩu!");
            return;
        }

        LoginResponse? login;
        try
        {
            login = await ApiClient.LoginAsync(username, password);
        }
        catch (Exception ex)
        {
            Ui.ShowException(ex, "Đăng nhập");
            return;
        }

        if (login == null)
        {
            Ui.Error("Sai tài khoản hoặc mật khẩu!");
            return;
        }

        SessionManager.SignIn(username, login);
        Ui.Info($"Đăng nhập thành công với quyền: {login.Role}");

        Hide();
        using (var main = new FormMainShell())
        {
            main.ShowDialog();
        }
        Close();
    }
}
