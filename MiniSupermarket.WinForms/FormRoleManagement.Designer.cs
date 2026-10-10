namespace MiniSupermarket.WinForms
{
    partial class FormRoleManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.Label lblDescription;

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.TextBox txtDescription;

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.DataGridView dgvRoles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitle = new Label();
            lblId = new Label();
            lblRoleName = new Label();
            lblDescription = new Label();

            txtId = new TextBox();
            txtRoleName = new TextBox();
            txtDescription = new TextBox();

            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            dgvRoles = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(280, 32);
            lblTitle.Text = "QUẢN LÝ VAI TRÒ";

            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 10F);
            lblId.Location = new Point(30, 80);
            lblId.Name = "lblId";
            lblId.Size = new Size(25, 19);
            lblId.Text = "ID:";

            // 
            // txtId
            // 
            txtId.Location = new Point(130, 77);
            txtId.Name = "txtId";
            txtId.Size = new Size(200, 27);
            txtId.ReadOnly = true;
            txtId.TabIndex = 0;

            // 
            // lblRoleName
            // 
            lblRoleName.AutoSize = true;
            lblRoleName.Font = new Font("Segoe UI", 10F);
            lblRoleName.Location = new Point(30, 125);
            lblRoleName.Name = "lblRoleName";
            lblRoleName.Size = new Size(76, 19);
            lblRoleName.Text = "Tên vai trò:";

            // 
            // txtRoleName
            // 
            txtRoleName.Location = new Point(130, 122);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new Size(300, 27);
            txtRoleName.TabIndex = 1;

            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.Location = new Point(30, 170);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(66, 19);
            lblDescription.Text = "Mô tả:";

            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(130, 167);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(300, 70);
            txtDescription.TabIndex = 2;

            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Segoe UI", 10F);
            btnLoad.Location = new Point(470, 77);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(110, 40);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Tải danh sách";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.Location = new Point(470, 125);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 10F);
            btnUpdate.Location = new Point(600, 125);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(110, 40);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10F);
            btnDelete.Location = new Point(730, 125);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 40);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Location = new Point(30, 270);
            dgvRoles.MultiSelect = false;
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.RowHeadersWidth = 51;
            dgvRoles.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(810, 300);
            dgvRoles.TabIndex = 7;
            dgvRoles.CellClick += dgvRoles_CellClick;

            // 
            // FormRoleManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 610);

            Controls.Add(lblTitle);

            Controls.Add(lblId);
            Controls.Add(txtId);

            Controls.Add(lblRoleName);
            Controls.Add(txtRoleName);

            Controls.Add(lblDescription);
            Controls.Add(txtDescription);

            Controls.Add(btnLoad);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);

            Controls.Add(dgvRoles);

            Name = "FormRoleManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý vai trò";

            Load += FormRoleManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}