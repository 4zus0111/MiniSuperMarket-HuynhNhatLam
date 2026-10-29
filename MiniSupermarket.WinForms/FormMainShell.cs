using MiniSupermarket.WinForms.Helpers;
using MiniSupermarket.WinForms.Models;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms;

public partial class FormMainShell : Form
{
    private static readonly Color MenuColor = Color.FromArgb(24, 30, 48);
    private static readonly Color ActiveMenuColor = Color.FromArgb(41, 100, 180);

    private sealed record MenuEntry(Button Button, string Title, Func<Form> CreateForm, string[] Roles);

    private List<MenuEntry> _menu = new();
    private Button _btnDashboard = null!;
    private Form? _activeForm;

    public FormMainShell() => InitializeComponent();

    private void FormMainShell_Load(object? sender, EventArgs e)
    {
        lblUserInfo.Text = $"Nhân viên: {SessionManager.CurrentUsername} | Vai trò: [{SessionManager.CurrentRole}]";

        if (!SessionManager.IsInRole(AppRoles.Admin, AppRoles.Cashier, AppRoles.Warehouse))
        {
            Ui.Warn("Tài khoản chưa được cấp quyền hạn hợp lệ!");
            Close();
            return;
        }

        BuildMenu();

        // Màn hình mặc định: Thu ngân -> Bán hàng (POS); Thủ kho -> Nhóm hàng; Admin -> Dashboard
        var defaultButton =
            SessionManager.IsInRole(AppRoles.Cashier) ? btnPOS :
            SessionManager.IsInRole(AppRoles.Warehouse) ? btnCategory :
            _btnDashboard;
        OpenChildForm(_menu.First(m => m.Button == defaultButton));
    }

    private void BuildMenu()
    {
        const string admin = AppRoles.Admin;
        const string cashier = AppRoles.Cashier;
        const string warehouse = AppRoles.Warehouse;

        _btnDashboard = CreateMenuButton("📊  Dashboard");

        _menu = new List<MenuEntry>
        {
            new(btnPOS, "BÁN HÀNG (POS)", () => new FormPOS(), new[] { admin, cashier }),
            new(btnCategory, "QUẢN LÝ DANH MỤC NHÓM HÀNG", () => new FormCategoryManagement(), new[] { admin, warehouse }),
            new(btnProduct, "QUẢN LÝ SẢN PHẨM", () => new FormProductManagement(), new[] { admin, warehouse }),
            new(btnCustomer, "QUẢN LÝ KHÁCH HÀNG THÂN THIẾT", () => new FormCustomerManagement(), new[] { admin, cashier }),
            new(btnReports, "BÁO CÁO DOANH THU", () => new FormReportManagement(), new[] { admin }),
            new(btnUserManage, "QUẢN TRỊ TÀI KHOẢN", () => new FormUserManagement(), new[] { admin }),
            new(CreateMenuButton("🧾  Đơn hàng"), "QUẢN LÝ ĐƠN HÀNG", () => new FormOrderManagement(), new[] { admin }),
            new(CreateMenuButton("🏷  Thương hiệu"), "QUẢN LÝ THƯƠNG HIỆU", () => new FormBrandManagement(), new[] { admin, warehouse }),
            new(CreateMenuButton("🔑  Vai trò"), "QUẢN LÝ VAI TRÒ", () => new FormRoleManagement(), new[] { admin }),
            new(_btnDashboard, "DASHBOARD TỔNG QUAN", () => new FormDashboard(), new[] { admin }),
        };

        foreach (var entry in _menu)
        {
            entry.Button.Visible = SessionManager.IsInRole(entry.Roles);
            entry.Button.Click += (_, _) => OpenChildForm(entry);
        }
    }

    /// <summary>Tạo nút menu mới có cùng kiểu dáng với nút "Quản trị tài khoản".</summary>
    private Button CreateMenuButton(string text)
    {
        var button = new Button
        {
            Text = text,
            Dock = DockStyle.Top,
            Height = btnUserManage.Height,
            FlatStyle = FlatStyle.Flat,
            ForeColor = btnUserManage.ForeColor,
            Font = btnUserManage.Font,
            TextAlign = btnUserManage.TextAlign,
            Padding = btnUserManage.Padding,
            Cursor = Cursors.Hand,
            BackColor = MenuColor
        };
        button.FlatAppearance.BorderSize = 0;

        panelSidebar.Controls.Add(button);
        button.BringToFront();
        return button;
    }

    /// <summary>Nhúng form con vào panelMainContent, thay thế form đang mở.</summary>
    private void OpenChildForm(MenuEntry entry)
    {
        CloseActiveForm();
        HighlightActiveButton(entry.Button);

        var form = entry.CreateForm();
        _activeForm = form;

        form.TopLevel = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;

        panelMainContent.Controls.Clear();
        panelMainContent.Controls.Add(form);
        lblTitle.Text = entry.Title;

        form.BringToFront();
        form.Show();
    }

    private void CloseActiveForm()
    {
        _activeForm?.Close();
        _activeForm?.Dispose();
        _activeForm = null;
    }

    private void HighlightActiveButton(Button activeButton)
    {
        foreach (var button in panelSidebar.Controls.OfType<Button>().Where(b => b != btnLogout))
            button.BackColor = MenuColor;

        activeButton.BackColor = ActiveMenuColor;
    }

    private void btnLogout_Click(object? sender, EventArgs e)
    {
        if (!Ui.Confirm("Bạn có chắc chắn muốn đăng xuất phiên làm việc?")) return;

        SessionManager.SignOut();
        CloseActiveForm();

        Hide();
        using (var login = new FormLogin())
        {
            login.ShowDialog();
        }
        Close();
    }
}
