namespace MiniSupermarket.WinForms
{
    partial class FormOrderManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(12, 10); this.label1.Text = "Danh sách Đơn hàng (Click để xem chi tiết):";

            // dgvOrders
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(12, 30); this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(760, 200);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellClick);

            // label2
            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(12, 240); this.label2.Text = "Danh sách Sản phẩm trong Đơn Hàng:";

            // dgvOrderDetails
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Location = new System.Drawing.Point(12, 260); this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.Size = new System.Drawing.Size(760, 150);
            this.dgvOrderDetails.TabIndex = 1;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // Các nút Duyệt / Hủy
            this.btnApprove.Location = new System.Drawing.Point(550, 420); this.btnApprove.Name = "btnApprove"; this.btnApprove.Size = new System.Drawing.Size(100, 30); this.btnApprove.Text = "Duyệt Đơn"; this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            this.btnCancel.Location = new System.Drawing.Point(670, 420); this.btnCancel.Name = "btnCancel"; this.btnCancel.Size = new System.Drawing.Size(100, 30); this.btnCancel.Text = "Hủy Đơn"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // FormOrderManagement
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnCancel); this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dgvOrderDetails); this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvOrders); this.Controls.Add(this.label1);
            this.Name = "FormOrderManagement";
            this.Text = "Quản lý Đơn Hàng (Order & OrderDetail)";
            this.Load += new System.EventHandler(this.FormOrderManagement_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}