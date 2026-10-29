namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpLogin = new GroupBox();
            btnLogin = new Button();
            txtPass = new TextBox();
            lblPass = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            grpLogin.SuspendLayout();
            SuspendLayout();
            // 
            // grpLogin
            // 
            grpLogin.Controls.Add(btnLogin);
            grpLogin.Controls.Add(txtPass);
            grpLogin.Controls.Add(lblPass);
            grpLogin.Controls.Add(txtUser);
            grpLogin.Controls.Add(lblUser);
            grpLogin.Location = new Point(20, 20);
            grpLogin.Name = "grpLogin";
            grpLogin.Size = new Size(390, 220);
            grpLogin.TabIndex = 0;
            grpLogin.TabStop = false;
            grpLogin.Text = "Thông tin đăng nhập";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(30, 40);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(61, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "Tài khoản";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(30, 62);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Nhập tài khoản...";
            txtUser.Size = new Size(330, 23);
            txtUser.TabIndex = 1;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(30, 100);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(57, 15);
            lblPass.TabIndex = 2;
            lblPass.Text = "Mật khẩu";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(30, 122);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Nhập mật khẩu...";
            txtPass.Size = new Size(330, 23);
            txtPass.TabIndex = 3;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(30, 165);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(330, 35);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Đăng nhập hệ thống";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 260);
            Controls.Add(grpLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            grpLogin.ResumeLayout(false);
            grpLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;
    }
}