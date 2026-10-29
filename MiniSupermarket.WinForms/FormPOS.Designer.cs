using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlLeft;
        private Panel pnlRight;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private DataGridView dgvCart;
        private Label lblCustomerPhone;
        private TextBox txtCustomerPhone;
        private Label lblCustomerNameTitle;
        private Label lblCustomerName;
        private Label lblTotalTitle;
        private Label lblTotalAmount;
        private Label lblCashReceived;
        private TextBox txtCashReceived;
        private Label lblChangeTitle;
        private Label lblChange;
        private Button btnCheckout;
        private Button btnClearCart;
        private Label lblCartTitle;
        private Label lblPaymentTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            pnlLeft = new Panel();
            pnlRight = new Panel();

            lblCartTitle = new Label();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            dgvCart = new DataGridView();

            lblPaymentTitle = new Label();
            lblCustomerPhone = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerNameTitle = new Label();
            lblCustomerName = new Label();
            lblTotalTitle = new Label();
            lblTotalAmount = new Label();
            lblCashReceived = new Label();
            txtCashReceived = new TextBox();
            lblChangeTitle = new Label();
            lblChange = new Label();
            btnCheckout = new Button();
            btnClearCart = new Button();

            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1100, 650);
            Name = "FormPOS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mini Supermarket - Bán hàng";
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Width = 910;
            pnlLeft.Padding = new Padding(20);
            pnlLeft.BackColor = Color.White;

            lblCartTitle.AutoSize = true;
            lblCartTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblCartTitle.Location = new Point(20, 18);
            lblCartTitle.Text = "Giỏ hàng";

            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBarcode.Location = new Point(20, 70);
            lblBarcode.Text = "Quét mã vạch";

            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Font = new Font("Segoe UI", 14F);
            txtBarcode.Location = new Point(20, 95);
            txtBarcode.Size = new Size(850, 32);
            txtBarcode.PlaceholderText = "Nhập hoặc quét mã vạch rồi nhấn Enter";
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AllowUserToResizeRows = false;
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.FixedSingle;
            dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };
            dgvCart.ColumnHeadersHeight = 40;
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.Location = new Point(20, 145);
            dgvCart.RowHeadersVisible = false;
            dgvCart.RowTemplate.Height = 38;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(850, 615);
            dgvCart.TabIndex = 0;

            pnlLeft.Controls.Add(lblCartTitle);
            pnlLeft.Controls.Add(lblBarcode);
            pnlLeft.Controls.Add(txtBarcode);
            pnlLeft.Controls.Add(dgvCart);

            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Padding = new Padding(25);
            pnlRight.BackColor = Color.FromArgb(248, 249, 250);

            lblPaymentTitle.AutoSize = true;
            lblPaymentTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblPaymentTitle.Location = new Point(25, 20);
            lblPaymentTitle.Text = "Thanh toán";

            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCustomerPhone.Location = new Point(25, 82);
            lblCustomerPhone.Text = "SĐT khách hàng";

            txtCustomerPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerPhone.Font = new Font("Segoe UI", 13F);
            txtCustomerPhone.Location = new Point(25, 108);
            txtCustomerPhone.Size = new Size(390, 31);
            txtCustomerPhone.PlaceholderText = "Nhập số điện thoại";

            lblCustomerNameTitle.AutoSize = true;
            lblCustomerNameTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCustomerNameTitle.Location = new Point(25, 160);
            lblCustomerNameTitle.Text = "Khách hàng";

            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 11F);
            lblCustomerName.Location = new Point(25, 188);
            lblCustomerName.Text = "Khách vãng lai";

            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTotalTitle.Location = new Point(25, 245);
            lblTotalTitle.Text = "Tổng tiền";

            lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalAmount.Font = new Font("Segoe UI Semibold", 28F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.DarkBlue;
            lblTotalAmount.Location = new Point(25, 275);
            lblTotalAmount.Size = new Size(390, 55);
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleLeft;

            lblCashReceived.AutoSize = true;
            lblCashReceived.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCashReceived.Location = new Point(25, 355);
            lblCashReceived.Text = "Tiền khách đưa";

            txtCashReceived.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCashReceived.Font = new Font("Segoe UI", 15F);
            txtCashReceived.Location = new Point(25, 382);
            txtCashReceived.Size = new Size(390, 34);
            txtCashReceived.TextAlign = HorizontalAlignment.Right;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;

            lblChangeTitle.AutoSize = true;
            lblChangeTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblChangeTitle.Location = new Point(25, 440);
            lblChangeTitle.Text = "Tiền thừa";

            lblChange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblChange.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblChange.Location = new Point(25, 468);
            lblChange.Size = new Size(390, 45);
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;

            btnCheckout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCheckout.BackColor = Color.DarkGreen;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(25, 620);
            btnCheckout.Size = new Size(390, 55);
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;

            btnClearCart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClearCart.BackColor = Color.Firebrick;
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(25, 690);
            btnClearCart.Size = new Size(390, 50);
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;

            pnlRight.Controls.Add(lblPaymentTitle);
            pnlRight.Controls.Add(lblCustomerPhone);
            pnlRight.Controls.Add(txtCustomerPhone);
            pnlRight.Controls.Add(lblCustomerNameTitle);
            pnlRight.Controls.Add(lblCustomerName);
            pnlRight.Controls.Add(lblTotalTitle);
            pnlRight.Controls.Add(lblTotalAmount);
            pnlRight.Controls.Add(lblCashReceived);
            pnlRight.Controls.Add(txtCashReceived);
            pnlRight.Controls.Add(lblChangeTitle);
            pnlRight.Controls.Add(lblChange);
            pnlRight.Controls.Add(btnCheckout);
            pnlRight.Controls.Add(btnClearCart);

            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);

            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();

            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();

            ResumeLayout(false);
        }
    }
}