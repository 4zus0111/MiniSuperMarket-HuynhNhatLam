using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // KHAI BÁO PANEL
        // =========================================================

        private Panel panelSidebar;
        private Panel panelTopHeader;
        private Panel panelMainContent;
        private Panel panelLogout;

        // =========================================================
        // KHAI BÁO LABEL
        // =========================================================

        private Label lblLogo;
        private Label lblTitle;
        private Label lblUserInfo;

        // =========================================================
        // KHAI BÁO BUTTON
        // =========================================================

        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReports;
        private Button btnUserManage;
        private Button btnLogout;

        // =========================================================
        // DISPOSE
        // =========================================================

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // =========================================================
        // INITIALIZE COMPONENT
        // =========================================================

        private void InitializeComponent()
        {
            // =====================================================
            // KHỞI TẠO CONTROL
            // =====================================================

            this.components =
                new System.ComponentModel.Container();

            this.panelSidebar =
                new Panel();

            this.panelTopHeader =
                new Panel();

            this.panelMainContent =
                new Panel();

            this.panelLogout =
                new Panel();

            this.lblLogo =
                new Label();

            this.lblTitle =
                new Label();

            this.lblUserInfo =
                new Label();

            this.btnPOS =
                new Button();

            this.btnCategory =
                new Button();

            this.btnProduct =
                new Button();

            this.btnCustomer =
                new Button();

            this.btnReports =
                new Button();

            this.btnUserManage =
                new Button();

            this.btnLogout =
                new Button();

            // =====================================================
            // SUSPEND LAYOUT
            // =====================================================

            this.panelSidebar.SuspendLayout();
            this.panelLogout.SuspendLayout();
            this.panelTopHeader.SuspendLayout();

            this.SuspendLayout();

            // =====================================================
            // PANEL SIDEBAR
            // =====================================================

            this.panelSidebar.BackColor =
                Color.FromArgb(24, 30, 48);

            this.panelSidebar.Dock =
                DockStyle.Left;

            this.panelSidebar.Location =
                new Point(0, 0);

            this.panelSidebar.Name =
                "panelSidebar";

            this.panelSidebar.Size =
                new Size(230, 720);

            this.panelSidebar.TabIndex =
                0;

            // =====================================================
            // LOGO
            // =====================================================

            this.lblLogo.BackColor =
                Color.FromArgb(24, 30, 48);

            this.lblLogo.Dock =
                DockStyle.Top;

            this.lblLogo.Font =
                new Font(
                    "Segoe UI",
                    14F,
                    FontStyle.Bold,
                    GraphicsUnit.Point
                );

            this.lblLogo.ForeColor =
                Color.White;

            this.lblLogo.Location =
                new Point(0, 0);

            this.lblLogo.Name =
                "lblLogo";

            this.lblLogo.Size =
                new Size(230, 75);

            this.lblLogo.TabIndex =
                0;

            this.lblLogo.Text =
                "🛒 MiniMart POS";

            this.lblLogo.TextAlign =
                ContentAlignment.MiddleCenter;

            // =====================================================
            // BUTTON POS
            // =====================================================

            ConfigureMenuButton(
                this.btnPOS,
                "🛒  Bán hàng (POS)"
            );

            this.btnPOS.Dock =
                DockStyle.Top;

            this.btnPOS.Location =
                new Point(0, 75);

            this.btnPOS.Name =
                "btnPOS";

            this.btnPOS.Size =
                new Size(230, 45);

            this.btnPOS.TabIndex =
                1;

            this.btnPOS.Click +=
                new System.EventHandler(
                    this.btnPOS_Click
                );

            // =====================================================
            // BUTTON CATEGORY
            // =====================================================

            ConfigureMenuButton(
                this.btnCategory,
                "📁  Quản lý Danh mục"
            );

            this.btnCategory.Dock =
                DockStyle.Top;

            this.btnCategory.Location =
                new Point(0, 120);

            this.btnCategory.Name =
                "btnCategory";

            this.btnCategory.Size =
                new Size(230, 45);

            this.btnCategory.TabIndex =
                2;

            this.btnCategory.Click +=
                new System.EventHandler(
                    this.btnCategory_Click
                );

            // =====================================================
            // BUTTON PRODUCT
            // =====================================================

            ConfigureMenuButton(
                this.btnProduct,
                "📦  Quản lý Sản phẩm"
            );

            this.btnProduct.Dock =
                DockStyle.Top;

            this.btnProduct.Location =
                new Point(0, 165);

            this.btnProduct.Name =
                "btnProduct";

            this.btnProduct.Size =
                new Size(230, 45);

            this.btnProduct.TabIndex =
                3;

            this.btnProduct.Click +=
                new System.EventHandler(
                    this.btnProduct_Click
                );

            // =====================================================
            // BUTTON CUSTOMER
            // =====================================================

            ConfigureMenuButton(
                this.btnCustomer,
                "👥  Quản lý Khách hàng"
            );

            this.btnCustomer.Dock =
                DockStyle.Top;

            this.btnCustomer.Location =
                new Point(0, 210);

            this.btnCustomer.Name =
                "btnCustomer";

            this.btnCustomer.Size =
                new Size(230, 45);

            this.btnCustomer.TabIndex =
                4;

            this.btnCustomer.Click +=
                new System.EventHandler(
                    this.btnCustomer_Click
                );

            // =====================================================
            // BUTTON REPORTS
            // =====================================================

            ConfigureMenuButton(
                this.btnReports,
                "📊  Báo cáo Doanh thu"
            );

            this.btnReports.Dock =
                DockStyle.Top;

            this.btnReports.Location =
                new Point(0, 255);

            this.btnReports.Name =
                "btnReports";

            this.btnReports.Size =
                new Size(230, 45);

            this.btnReports.TabIndex =
                5;

            this.btnReports.Click +=
                new System.EventHandler(
                    this.btnReports_Click
                );

            // =====================================================
            // BUTTON USER MANAGEMENT
            // =====================================================

            ConfigureMenuButton(
                this.btnUserManage,
                "🛡  Quản trị Tài khoản"
            );

            this.btnUserManage.Dock =
                DockStyle.Top;

            this.btnUserManage.Location =
                new Point(0, 300);

            this.btnUserManage.Name =
                "btnUserManage";

            this.btnUserManage.Size =
                new Size(230, 45);

            this.btnUserManage.TabIndex =
                6;

            this.btnUserManage.Click +=
                new System.EventHandler(
                    this.btnUserManage_Click
                );

            // =====================================================
            // PANEL LOGOUT
            // =====================================================

            this.panelLogout.BackColor =
                Color.FromArgb(24, 30, 48);

            this.panelLogout.Dock =
                DockStyle.Bottom;

            this.panelLogout.Location =
                new Point(0, 660);

            this.panelLogout.Name =
                "panelLogout";

            this.panelLogout.Padding =
                new Padding(0, 5, 0, 5);

            this.panelLogout.Size =
                new Size(230, 60);

            this.panelLogout.TabIndex =
                7;

            // =====================================================
            // BUTTON LOGOUT
            // =====================================================

            ConfigureMenuButton(
                this.btnLogout,
                "🚪  Đăng xuất"
            );

            this.btnLogout.Dock =
                DockStyle.Fill;

            this.btnLogout.Location =
                new Point(0, 5);

            this.btnLogout.Name =
                "btnLogout";

            this.btnLogout.Size =
                new Size(230, 50);

            this.btnLogout.TabIndex =
                0;

            this.btnLogout.Click +=
                new System.EventHandler(
                    this.btnLogout_Click
                );

            // Thêm Logout vào panel Logout
            this.panelLogout.Controls.Add(
                this.btnLogout
            );

            // =====================================================
            // ADD CONTROL VÀO SIDEBAR
            // =====================================================

            this.panelSidebar.Controls.Add(
                this.btnUserManage
            );

            this.panelSidebar.Controls.Add(
                this.btnReports
            );

            this.panelSidebar.Controls.Add(
                this.btnCustomer
            );

            this.panelSidebar.Controls.Add(
                this.btnProduct
            );

            this.panelSidebar.Controls.Add(
                this.btnCategory
            );

            this.panelSidebar.Controls.Add(
                this.btnPOS
            );

            this.panelSidebar.Controls.Add(
                this.lblLogo
            );

            this.panelSidebar.Controls.Add(
                this.panelLogout
            );

            // =====================================================
            // PANEL TOP HEADER
            // =====================================================

            this.panelTopHeader.BackColor =
                Color.White;

            this.panelTopHeader.Dock =
                DockStyle.Top;

            this.panelTopHeader.Location =
                new Point(230, 0);

            this.panelTopHeader.Name =
                "panelTopHeader";

            this.panelTopHeader.Size =
                new Size(1050, 60);

            this.panelTopHeader.TabIndex =
                1;

            // =====================================================
            // TITLE
            // =====================================================

            this.lblTitle.AutoSize =
                false;

            this.lblTitle.Dock =
                DockStyle.Left;

            this.lblTitle.Font =
                new Font(
                    "Segoe UI",
                    14F,
                    FontStyle.Bold,
                    GraphicsUnit.Point
                );

            this.lblTitle.ForeColor =
                Color.FromArgb(35, 40, 50);

            this.lblTitle.Location =
                new Point(0, 0);

            this.lblTitle.Name =
                "lblTitle";

            this.lblTitle.Padding =
                new Padding(20, 0, 0, 0);

            this.lblTitle.Size =
                new Size(500, 60);

            this.lblTitle.TabIndex =
                0;

            this.lblTitle.Text =
                "BÀN LÀM VIỆC HỆ THỐNG";

            this.lblTitle.TextAlign =
                ContentAlignment.MiddleLeft;

            // =====================================================
            // USER INFO
            // =====================================================

            this.lblUserInfo.AutoSize =
                false;

            this.lblUserInfo.Dock =
                DockStyle.Fill;

            this.lblUserInfo.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Regular,
                    GraphicsUnit.Point
                );

            this.lblUserInfo.ForeColor =
                Color.FromArgb(80, 85, 95);

            this.lblUserInfo.Location =
                new Point(500, 0);

            this.lblUserInfo.Name =
                "lblUserInfo";

            this.lblUserInfo.Padding =
                new Padding(0, 0, 20, 0);

            this.lblUserInfo.Size =
                new Size(550, 60);

            this.lblUserInfo.TabIndex =
                1;

            this.lblUserInfo.Text =
                "Xin chào: ....";

            this.lblUserInfo.TextAlign =
                ContentAlignment.MiddleRight;

            // Add Header Controls
            this.panelTopHeader.Controls.Add(
                this.lblUserInfo
            );

            this.panelTopHeader.Controls.Add(
                this.lblTitle
            );

            // =====================================================
            // PANEL MAIN CONTENT
            // =====================================================

            this.panelMainContent.BackColor =
                Color.FromArgb(244, 245, 247);

            this.panelMainContent.Dock =
                DockStyle.Fill;

            this.panelMainContent.Location =
                new Point(230, 60);

            this.panelMainContent.Name =
                "panelMainContent";

            this.panelMainContent.Padding =
                new Padding(0);

            this.panelMainContent.Size =
                new Size(1050, 660);

            this.panelMainContent.TabIndex =
                2;

            // =====================================================
            // FORM MAIN SHELL
            // =====================================================

            this.AutoScaleDimensions =
                new SizeF(7F, 15F);

            this.AutoScaleMode =
                AutoScaleMode.Font;

            this.BackColor =
                Color.FromArgb(244, 245, 247);

            this.ClientSize =
                new Size(1280, 720);

            // Thứ tự Add Control rất quan trọng
            this.Controls.Add(
                this.panelMainContent
            );

            this.Controls.Add(
                this.panelTopHeader
            );

            this.Controls.Add(
                this.panelSidebar
            );

            this.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Regular,
                    GraphicsUnit.Point
                );

            this.MinimumSize =
                new Size(1100, 650);

            this.Name =
                "FormMainShell";

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.Text =
                "MiniMart POS - Bàn làm việc hệ thống";

            this.WindowState =
                FormWindowState.Maximized;

            this.Load +=
                new System.EventHandler(
                    this.FormMainShell_Load
                );

            // =====================================================
            // RESUME LAYOUT
            // =====================================================

            this.panelSidebar.ResumeLayout(false);
            this.panelLogout.ResumeLayout(false);
            this.panelTopHeader.ResumeLayout(false);

            this.ResumeLayout(false);
        }

        // =========================================================
        // CẤU HÌNH BUTTON MENU
        // =========================================================

        private void ConfigureMenuButton(
            Button button,
            string text)
        {
            button.BackColor =
                Color.FromArgb(24, 30, 48);

            button.FlatAppearance.BorderSize =
                0;

            button.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);

            button.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(34, 42, 68);

            button.FlatStyle =
                FlatStyle.Flat;

            button.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Regular,
                    GraphicsUnit.Point
                );

            button.ForeColor =
                Color.White;

            button.Height =
                45;

            button.Margin =
                new Padding(0);

            button.Padding =
                new Padding(20, 0, 0, 0);

            button.Text =
                text;

            button.TextAlign =
                ContentAlignment.MiddleLeft;

            button.UseVisualStyleBackColor =
                false;
        }
    }
}