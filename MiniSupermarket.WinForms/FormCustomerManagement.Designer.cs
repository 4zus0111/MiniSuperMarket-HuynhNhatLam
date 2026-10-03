namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // KHAI BÁO CONTROL
        // =========================================================

        private System.Windows.Forms.DataGridView dgvCustomers;

        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtRewardPoints;
        private System.Windows.Forms.TextBox txtMembershipRank;

        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblRewardPoints;
        private System.Windows.Forms.Label lblMembershipRank;

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCustomers = new System.Windows.Forms.DataGridView();

            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtRewardPoints = new System.Windows.Forms.TextBox();
            this.txtMembershipRank = new System.Windows.Forms.TextBox();

            this.lblCustomerId = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblRewardPoints = new System.Windows.Forms.Label();
            this.lblMembershipRank = new System.Windows.Forms.Label();

            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();

            // =========================================================
            // FORM
            // =========================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1100, 650);

            this.Name =
                "FormCustomerManagement";

            this.Text =
                "Quản lý khách hàng";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.BackColor =
                System.Drawing.Color.White;

            // =========================================================
            // TITLE
            // =========================================================

            System.Windows.Forms.Label lblTitle =
                new System.Windows.Forms.Label();

            lblTitle.AutoSize = true;

            lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    20F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(0, 102, 204);

            lblTitle.Location =
                new System.Drawing.Point(350, 20);

            lblTitle.Name =
                "lblTitle";

            lblTitle.Size =
                new System.Drawing.Size(390, 37);

            lblTitle.Text =
                "QUẢN LÝ KHÁCH HÀNG";

            this.Controls.Add(lblTitle);

            // =========================================================
            // LABEL CUSTOMER ID
            // =========================================================

            this.lblCustomerId.AutoSize = true;

            this.lblCustomerId.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblCustomerId.Location =
                new System.Drawing.Point(30, 85);

            this.lblCustomerId.Name =
                "lblCustomerId";

            this.lblCustomerId.Text =
                "Mã khách hàng:";

            this.lblCustomerId.Size =
                new System.Drawing.Size(98, 19);

            this.Controls.Add(this.lblCustomerId);

            // =========================================================
            // TEXTBOX CUSTOMER ID
            // =========================================================

            this.txtCustomerId.Location =
                new System.Drawing.Point(150, 82);

            this.txtCustomerId.Name =
                "txtCustomerId";

            this.txtCustomerId.Size =
                new System.Drawing.Size(250, 23);

            this.txtCustomerId.ReadOnly = true;

            this.txtCustomerId.BackColor =
                System.Drawing.Color.LightGray;

            this.Controls.Add(this.txtCustomerId);

            // =========================================================
            // LABEL CUSTOMER NAME
            // =========================================================

            this.lblCustomerName.AutoSize = true;

            this.lblCustomerName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblCustomerName.Location =
                new System.Drawing.Point(30, 125);

            this.lblCustomerName.Name =
                "lblCustomerName";

            this.lblCustomerName.Text =
                "Tên khách hàng:";

            this.lblCustomerName.Size =
                new System.Drawing.Size(110, 19);

            this.Controls.Add(this.lblCustomerName);

            // =========================================================
            // TEXTBOX CUSTOMER NAME
            // =========================================================

            this.txtCustomerName.Location =
                new System.Drawing.Point(150, 122);

            this.txtCustomerName.Name =
                "txtCustomerName";

            this.txtCustomerName.Size =
                new System.Drawing.Size(250, 23);

            this.Controls.Add(this.txtCustomerName);

            // =========================================================
            // LABEL PHONE
            // =========================================================

            this.lblPhoneNumber.AutoSize = true;

            this.lblPhoneNumber.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblPhoneNumber.Location =
                new System.Drawing.Point(30, 165);

            this.lblPhoneNumber.Name =
                "lblPhoneNumber";

            this.lblPhoneNumber.Text =
                "Số điện thoại:";

            this.lblPhoneNumber.Size =
                new System.Drawing.Size(96, 19);

            this.Controls.Add(this.lblPhoneNumber);

            // =========================================================
            // TEXTBOX PHONE
            // =========================================================

            this.txtPhoneNumber.Location =
                new System.Drawing.Point(150, 162);

            this.txtPhoneNumber.Name =
                "txtPhoneNumber";

            this.txtPhoneNumber.Size =
                new System.Drawing.Size(250, 23);

            this.Controls.Add(this.txtPhoneNumber);

            // =========================================================
            // LABEL ADDRESS
            // =========================================================

            this.lblAddress.AutoSize = true;

            this.lblAddress.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblAddress.Location =
                new System.Drawing.Point(30, 205);

            this.lblAddress.Name =
                "lblAddress";

            this.lblAddress.Text =
                "Địa chỉ:";

            this.lblAddress.Size =
                new System.Drawing.Size(53, 19);

            this.Controls.Add(this.lblAddress);

            // =========================================================
            // TEXTBOX ADDRESS
            // =========================================================

            this.txtAddress.Location =
                new System.Drawing.Point(150, 202);

            this.txtAddress.Name =
                "txtAddress";

            this.txtAddress.Size =
                new System.Drawing.Size(250, 23);

            this.Controls.Add(this.txtAddress);

            // =========================================================
            // LABEL REWARD POINTS
            // =========================================================

            this.lblRewardPoints.AutoSize = true;

            this.lblRewardPoints.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblRewardPoints.Location =
                new System.Drawing.Point(30, 245);

            this.lblRewardPoints.Name =
                "lblRewardPoints";

            this.lblRewardPoints.Text =
                "Điểm tích lũy:";

            this.lblRewardPoints.Size =
                new System.Drawing.Size(91, 19);

            this.Controls.Add(this.lblRewardPoints);

            // =========================================================
            // TEXTBOX REWARD POINTS
            // =========================================================

            this.txtRewardPoints.Location =
                new System.Drawing.Point(150, 242);

            this.txtRewardPoints.Name =
                "txtRewardPoints";

            this.txtRewardPoints.Size =
                new System.Drawing.Size(250, 23);

            this.txtRewardPoints.Text =
                "0";

            this.Controls.Add(this.txtRewardPoints);

            // =========================================================
            // LABEL MEMBERSHIP RANK
            // =========================================================

            this.lblMembershipRank.AutoSize = true;

            this.lblMembershipRank.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblMembershipRank.Location =
                new System.Drawing.Point(30, 285);

            this.lblMembershipRank.Name =
                "lblMembershipRank";

            this.lblMembershipRank.Text =
                "Hạng thành viên:";

            this.lblMembershipRank.Size =
                new System.Drawing.Size(111, 19);

            this.Controls.Add(this.lblMembershipRank);

            // =========================================================
            // TEXTBOX MEMBERSHIP RANK
            // =========================================================

            this.txtMembershipRank.Location =
                new System.Drawing.Point(150, 282);

            this.txtMembershipRank.Name =
                "txtMembershipRank";

            this.txtMembershipRank.Size =
                new System.Drawing.Size(250, 23);

            this.txtMembershipRank.Text =
                "Chuẩn";

            this.Controls.Add(this.txtMembershipRank);

            // =========================================================
            // BUTTON LOAD
            // =========================================================

            this.btnLoad.BackColor =
                System.Drawing.Color.FromArgb(0, 123, 255);

            this.btnLoad.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnLoad.FlatAppearance.BorderSize = 0;

            this.btnLoad.ForeColor =
                System.Drawing.Color.White;

            this.btnLoad.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnLoad.Location =
                new System.Drawing.Point(30, 335);

            this.btnLoad.Name =
                "btnLoad";

            this.btnLoad.Size =
                new System.Drawing.Size(100, 38);

            this.btnLoad.Text =
                "Tải danh sách";

            this.btnLoad.UseVisualStyleBackColor = false;

            this.Controls.Add(this.btnLoad);

            // =========================================================
            // BUTTON ADD
            // =========================================================

            this.btnAdd.BackColor =
                System.Drawing.Color.FromArgb(40, 167, 69);

            this.btnAdd.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnAdd.FlatAppearance.BorderSize = 0;

            this.btnAdd.ForeColor =
                System.Drawing.Color.White;

            this.btnAdd.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnAdd.Location =
                new System.Drawing.Point(145, 335);

            this.btnAdd.Name =
                "btnAdd";

            this.btnAdd.Size =
                new System.Drawing.Size(100, 38);

            this.btnAdd.Text =
                "Thêm";

            this.btnAdd.UseVisualStyleBackColor = false;

            this.Controls.Add(this.btnAdd);

            // =========================================================
            // BUTTON UPDATE
            // =========================================================

            this.btnUpdate.BackColor =
                System.Drawing.Color.FromArgb(255, 193, 7);

            this.btnUpdate.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnUpdate.FlatAppearance.BorderSize = 0;

            this.btnUpdate.ForeColor =
                System.Drawing.Color.Black;

            this.btnUpdate.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnUpdate.Location =
                new System.Drawing.Point(260, 335);

            this.btnUpdate.Name =
                "btnUpdate";

            this.btnUpdate.Size =
                new System.Drawing.Size(100, 38);

            this.btnUpdate.Text =
                "Cập nhật";

            this.btnUpdate.UseVisualStyleBackColor = false;

            this.Controls.Add(this.btnUpdate);

            // =========================================================
            // BUTTON DELETE
            // =========================================================

            this.btnDelete.BackColor =
                System.Drawing.Color.FromArgb(220, 53, 69);

            this.btnDelete.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnDelete.FlatAppearance.BorderSize = 0;

            this.btnDelete.ForeColor =
                System.Drawing.Color.White;

            this.btnDelete.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnDelete.Location =
                new System.Drawing.Point(375, 335);

            this.btnDelete.Name =
                "btnDelete";

            this.btnDelete.Size =
                new System.Drawing.Size(100, 38);

            this.btnDelete.Text =
                "Xóa";

            this.btnDelete.UseVisualStyleBackColor = false;

            this.Controls.Add(this.btnDelete);

            // =========================================================
            // BUTTON SEARCH
            // =========================================================

            this.btnSearch.BackColor =
                System.Drawing.Color.FromArgb(108, 117, 125);

            this.btnSearch.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSearch.FlatAppearance.BorderSize = 0;

            this.btnSearch.ForeColor =
                System.Drawing.Color.White;

            this.btnSearch.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnSearch.Location =
                new System.Drawing.Point(490, 335);

            this.btnSearch.Name =
                "btnSearch";

            this.btnSearch.Size =
                new System.Drawing.Size(100, 38);

            this.btnSearch.Text =
                "Tìm kiếm";

            this.btnSearch.UseVisualStyleBackColor = false;

            this.Controls.Add(this.btnSearch);

            // =========================================================
            // DATAGRIDVIEW
            // =========================================================

            this.dgvCustomers.AllowUserToAddRows = false;

            this.dgvCustomers.AllowUserToDeleteRows = false;

            this.dgvCustomers.AllowUserToResizeRows = false;

            this.dgvCustomers.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvCustomers.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvCustomers.BorderStyle =
                System.Windows.Forms.BorderStyle.Fixed3D;

            this.dgvCustomers.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvCustomers.Location =
                new System.Drawing.Point(30, 400);

            this.dgvCustomers.MultiSelect = false;

            this.dgvCustomers.Name =
                "dgvCustomers";

            this.dgvCustomers.ReadOnly = true;

            this.dgvCustomers.RowHeadersVisible = false;

            this.dgvCustomers.RowTemplate.Height = 30;

            this.dgvCustomers.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvCustomers.Size =
                new System.Drawing.Size(1035, 220);

            this.dgvCustomers.TabIndex = 0;

            this.Controls.Add(this.dgvCustomers);

            // =========================================================
            // RESUME FORM
            // =========================================================

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvCustomers)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
