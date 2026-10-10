namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Panel panelInput;

        private Label lblUsername;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblRole;

        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPassword;

        private ComboBox cboRole;

        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnToggleLock;

        private DataGridView dgvUsers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitle = new Label();
            panelInput = new Panel();

            lblUsername = new Label();
            lblFullName = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblRole = new Label();

            txtUsername = new TextBox();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();

            cboRole = new ComboBox();

            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();

            dgvUsers = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panelInput.SuspendLayout();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(290, 37);
            lblTitle.Text = "QUẢN LÝ TÀI KHOẢN";

            panelInput.BorderStyle = BorderStyle.FixedSingle;
            panelInput.Location = new Point(30, 75);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(1120, 235);

            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(25, 20);
            lblUsername.Text = "Tên đăng nhập";

            txtUsername.Location = new Point(25, 45);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(210, 27);

            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(255, 20);
            lblFullName.Text = "Họ tên";

            txtFullName.Location = new Point(255, 45);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(210, 27);

            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(485, 20);
            lblEmail.Text = "Email";

            txtEmail.Location = new Point(485, 45);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(230, 27);

            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(735, 20);
            lblPassword.Text = "Mật khẩu";

            txtPassword.Location = new Point(735, 45);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(180, 27);
            txtPassword.UseSystemPasswordChar = true;

            lblRole.AutoSize = true;
            lblRole.Location = new Point(935, 20);
            lblRole.Text = "Vai trò";

            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.FormattingEnabled = true;
            cboRole.Location = new Point(935, 45);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(155, 28);

            btnAddUser.Location = new Point(25, 100);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(210, 45);
            btnAddUser.Text = "Thêm tài khoản";
            btnAddUser.UseVisualStyleBackColor = true;
            btnAddUser.Click += btnAddUser_Click;

            btnResetPassword.Location = new Point(255, 100);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(210, 45);
            btnResetPassword.Text = "Đặt lại mật khẩu";
            btnResetPassword.UseVisualStyleBackColor = true;
            btnResetPassword.Click += btnResetPassword_Click;

            btnToggleLock.Location = new Point(485, 100);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(230, 45);
            btnToggleLock.Text = "Khóa / Mở khóa";
            btnToggleLock.UseVisualStyleBackColor = true;
            btnToggleLock.Click += btnToggleLock_Click;

            panelInput.Controls.Add(lblUsername);
            panelInput.Controls.Add(txtUsername);

            panelInput.Controls.Add(lblFullName);
            panelInput.Controls.Add(txtFullName);

            panelInput.Controls.Add(lblEmail);
            panelInput.Controls.Add(txtEmail);

            panelInput.Controls.Add(lblPassword);
            panelInput.Controls.Add(txtPassword);

            panelInput.Controls.Add(lblRole);
            panelInput.Controls.Add(cboRole);

            panelInput.Controls.Add(btnAddUser);
            panelInput.Controls.Add(btnResetPassword);
            panelInput.Controls.Add(btnToggleLock);

            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(30, 330);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1120, 360);
            dgvUsers.CellClick += dgvUsers_CellClick;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(1180, 730);

            Controls.Add(lblTitle);
            Controls.Add(panelInput);
            Controls.Add(dgvUsers);

            Name = "FormUserManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài khoản";

            Load += FormUserManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }

}