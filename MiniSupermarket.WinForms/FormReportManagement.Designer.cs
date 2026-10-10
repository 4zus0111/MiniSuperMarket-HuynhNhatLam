namespace MiniSupermarket.WinForms
{
    partial class FormReport
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Button btnViewReport;

        private Panel panelOrders;
        private Panel panelRevenue;
        private Panel panelBestSeller;

        private Label lblTotalOrdersTitle;
        private Label lblTotalOrdersValue;

        private Label lblTotalRevenueTitle;
        private Label lblTotalRevenueValue;

        private Label lblBestSellerTitle;
        private Label lblBestSellerValue;

        private Label lblRecentOrders;
        private DataGridView dgvRecentOrders;

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
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            btnViewReport = new Button();

            panelOrders = new Panel();
            panelRevenue = new Panel();
            panelBestSeller = new Panel();

            lblTotalOrdersTitle = new Label();
            lblTotalOrdersValue = new Label();

            lblTotalRevenueTitle = new Label();
            lblTotalRevenueValue = new Label();

            lblBestSellerTitle = new Label();
            lblBestSellerValue = new Label();

            lblRecentOrders = new Label();
            dgvRecentOrders = new DataGridView();

            panelOrders.SuspendLayout();
            panelRevenue.SuspendLayout();
            panelBestSeller.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).BeginInit();

            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 37);
            lblTitle.Text = "BÁO CÁO DOANH THU";

            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.Location = new Point(30, 85);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(65, 19);
            lblDate.Text = "Chọn ngày:";

            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpDate.Location = new Point(105, 82);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(150, 27);

            btnViewReport.Location = new Point(270, 80);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(150, 32);
            btnViewReport.Text = "Xem báo cáo";
            btnViewReport.UseVisualStyleBackColor = true;
            btnViewReport.Click += btnViewReport_Click;

            panelOrders.BorderStyle = BorderStyle.FixedSingle;
            panelOrders.Location = new Point(30, 140);
            panelOrders.Name = "panelOrders";
            panelOrders.Size = new Size(330, 130);

            lblTotalOrdersTitle.AutoSize = true;
            lblTotalOrdersTitle.Font = new Font("Segoe UI", 11F);
            lblTotalOrdersTitle.Location = new Point(20, 20);
            lblTotalOrdersTitle.Text = "TỔNG SỐ ĐƠN HÀNG";

            lblTotalOrdersValue.AutoSize = true;
            lblTotalOrdersValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalOrdersValue.Location = new Point(20, 55);
            lblTotalOrdersValue.Text = "0";

            panelOrders.Controls.Add(lblTotalOrdersTitle);
            panelOrders.Controls.Add(lblTotalOrdersValue);

            panelRevenue.BorderStyle = BorderStyle.FixedSingle;
            panelRevenue.Location = new Point(390, 140);
            panelRevenue.Name = "panelRevenue";
            panelRevenue.Size = new Size(330, 130);

            lblTotalRevenueTitle.AutoSize = true;
            lblTotalRevenueTitle.Font = new Font("Segoe UI", 11F);
            lblTotalRevenueTitle.Location = new Point(20, 20);
            lblTotalRevenueTitle.Text = "TỔNG DOANH THU";

            lblTotalRevenueValue.AutoSize = true;
            lblTotalRevenueValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenueValue.Location = new Point(20, 58);
            lblTotalRevenueValue.Text = "0 VNĐ";

            panelRevenue.Controls.Add(lblTotalRevenueTitle);
            panelRevenue.Controls.Add(lblTotalRevenueValue);

            panelBestSeller.BorderStyle = BorderStyle.FixedSingle;
            panelBestSeller.Location = new Point(750, 140);
            panelBestSeller.Name = "panelBestSeller";
            panelBestSeller.Size = new Size(400, 130);

            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font("Segoe UI", 11F);
            lblBestSellerTitle.Location = new Point(20, 20);
            lblBestSellerTitle.Text = "SẢN PHẨM BÁN CHẠY NHẤT";

            lblBestSellerValue.AutoSize = true;
            lblBestSellerValue.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblBestSellerValue.Location = new Point(20, 58);
            lblBestSellerValue.MaximumSize = new Size(350, 50);
            lblBestSellerValue.Text = "Chưa có";

            panelBestSeller.Controls.Add(lblBestSellerTitle);
            panelBestSeller.Controls.Add(lblBestSellerValue);

            lblRecentOrders.AutoSize = true;
            lblRecentOrders.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblRecentOrders.Location = new Point(30, 300);
            lblRecentOrders.Text = "ĐƠN HÀNG TRONG NGÀY";

            dgvRecentOrders.AllowUserToAddRows = false;
            dgvRecentOrders.AllowUserToDeleteRows = false;
            dgvRecentOrders.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentOrders.BackgroundColor = Color.White;
            dgvRecentOrders.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentOrders.Location = new Point(30, 340);
            dgvRecentOrders.MultiSelect = false;
            dgvRecentOrders.ReadOnly = true;
            dgvRecentOrders.RowHeadersVisible = false;
            dgvRecentOrders.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvRecentOrders.Size = new Size(1120, 330);

            ClientSize = new Size(1180, 710);
            Controls.Add(lblTitle);
            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(btnViewReport);

            Controls.Add(panelOrders);
            Controls.Add(panelRevenue);
            Controls.Add(panelBestSeller);

            Controls.Add(lblRecentOrders);
            Controls.Add(dgvRecentOrders);

            Name = "FormReport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo doanh thu";

            Load += FormReport_Load;

            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).EndInit();

            panelOrders.ResumeLayout(false);
            panelOrders.PerformLayout();

            panelRevenue.ResumeLayout(false);
            panelRevenue.PerformLayout();

            panelBestSeller.ResumeLayout(false);
            panelBestSeller.PerformLayout();

            ResumeLayout(false);
            PerformLayout();
        }
    }

}