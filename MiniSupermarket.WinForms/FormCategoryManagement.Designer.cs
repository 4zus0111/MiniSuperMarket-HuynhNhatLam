namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();

            this.grpSearch = new GroupBox();
            this.lblKeyword = new Label();
            this.txtKeyword = new TextBox();
            this.btnSearch = new Button();
            this.btnLoad = new Button();

            this.grpCategoryList = new GroupBox();
            this.dgvCategories = new DataGridView();

            this.colCategoryId = new DataGridViewTextBoxColumn();
            this.colCategoryName = new DataGridViewTextBoxColumn();
            this.colDescription = new DataGridViewTextBoxColumn();

            this.grpCategoryInfo = new GroupBox();
            this.lblId = new Label();
            this.txtId = new TextBox();

            this.lblCategoryName = new Label();
            this.txtCategoryName = new TextBox();

            this.lblDescription = new Label();
            this.txtDescription = new TextBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();

            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();

            this.grpSearch.SuspendLayout();
            this.grpCategoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.grpCategoryInfo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.lblKeyword);
            this.grpSearch.Controls.Add(this.txtKeyword);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.btnLoad);
            this.grpSearch.Location = new Point(12, 12);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new Size(590, 58);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tìm kiếm";

            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new Point(10, 25);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new Size(0, 15);
            this.lblKeyword.TabIndex = 0;

            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new Point(12, 22);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PlaceholderText = "Nhập từ khóa...";
            this.txtKeyword.Size = new Size(235, 23);
            this.txtKeyword.TabIndex = 0;

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new Point(258, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(75, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            // 
            // btnLoad
            // 
            this.btnLoad.Location = new Point(346, 21);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(75, 25);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Tải lại";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new EventHandler(this.btnLoad_Click);

            // 
            // grpCategoryList
            // 
            this.grpCategoryList.Controls.Add(this.dgvCategories);
            this.grpCategoryList.Location = new Point(12, 82);
            this.grpCategoryList.Name = "grpCategoryList";
            this.grpCategoryList.Size = new Size(590, 285);
            this.grpCategoryList.TabIndex = 1;
            this.grpCategoryList.TabStop = false;
            this.grpCategoryList.Text = "Danh sách Nhóm hàng";

            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.AllowUserToResizeRows = false;
            this.dgvCategories.AutoGenerateColumns = false;
            this.dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundColor = SystemColors.Window;
            this.dgvCategories.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvCategories.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colCategoryId,
                this.colCategoryName,
                this.colDescription
            });

            this.dgvCategories.Location = new Point(9, 21);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersWidth = 30;
            this.dgvCategories.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new Size(570, 253);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.CellClick +=
                new DataGridViewCellEventHandler(this.dgvCategories_CellClick);

            // 
            // colCategoryId
            // 
            this.colCategoryId.DataPropertyName = "CategoryId";
            this.colCategoryId.HeaderText = "Mã ID";
            this.colCategoryId.Name = "CategoryId";
            this.colCategoryId.ReadOnly = true;
            this.colCategoryId.FillWeight = 25F;

            // 
            // colCategoryName
            // 
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "Tên Nhóm hàng";
            this.colCategoryName.Name = "CategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.FillWeight = 40F;

            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Mô Tả";
            this.colDescription.Name = "Description";
            this.colDescription.ReadOnly = true;
            this.colDescription.FillWeight = 55F;

            // 
            // grpCategoryInfo
            // 
            this.grpCategoryInfo.Controls.Add(this.lblId);
            this.grpCategoryInfo.Controls.Add(this.txtId);
            this.grpCategoryInfo.Controls.Add(this.lblCategoryName);
            this.grpCategoryInfo.Controls.Add(this.txtCategoryName);
            this.grpCategoryInfo.Controls.Add(this.lblDescription);
            this.grpCategoryInfo.Controls.Add(this.txtDescription);
            this.grpCategoryInfo.Controls.Add(this.btnAdd);
            this.grpCategoryInfo.Controls.Add(this.btnUpdate);
            this.grpCategoryInfo.Controls.Add(this.btnDelete);

            this.grpCategoryInfo.Location = new Point(612, 82);
            this.grpCategoryInfo.Name = "grpCategoryInfo";
            this.grpCategoryInfo.Size = new Size(275, 285);
            this.grpCategoryInfo.TabIndex = 2;
            this.grpCategoryInfo.TabStop = false;
            this.grpCategoryInfo.Text = "Thông tin Nhóm hàng";

            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new Point(10, 27);
            this.lblId.Name = "lblId";
            this.lblId.Size = new Size(31, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Mã ID";

            // 
            // txtId
            // 
            this.txtId.Location = new Point(10, 47);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new Size(255, 23);
            this.txtId.TabIndex = 1;

            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new Point(10, 80);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new Size(112, 15);
            this.lblCategoryName.TabIndex = 2;
            this.lblCategoryName.Text = "Tên Nhóm hàng Ví dụ:";

            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new Point(10, 100);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new Size(255, 23);
            this.txtCategoryName.TabIndex = 3;

            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(10, 133);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(116, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Mô Tả mô tả chi tiết...";

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Point(10, 153);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Size = new Size(255, 65);
            this.txtDescription.TabIndex = 5;

            // 
            // btnAdd
            // 
            this.btnAdd.Location = new Point(10, 233);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(57, 27);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new Point(78, 233);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(72, 27);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(160, 233);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(65, 27);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new ToolStripItem[]
            {
                this.toolStripStatusLabel1,
                this.toolStripStatusLabel2
            });

            this.statusStrip1.Location = new Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(900, 22);
            this.statusStrip1.TabIndex = 3;

            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";

            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(210, 17);
            this.toolStripStatusLabel2.Text = "https://localhost:7123/api/categories";

            // 
            // FormCategoryManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 412);

            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpCategoryInfo);
            this.Controls.Add(this.grpCategoryList);
            this.Controls.Add(this.grpSearch);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "FormCategoryManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý Danh mục Nhóm hàng - FormCategoryManagement";


            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();

            this.grpCategoryList.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();

            this.grpCategoryInfo.ResumeLayout(false);
            this.grpCategoryInfo.PerformLayout();

            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ============================
        // GROUPBOX
        // ============================

        private GroupBox grpSearch;
        private GroupBox grpCategoryList;
        private GroupBox grpCategoryInfo;

        // ============================
        // SEARCH
        // ============================

        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        // ============================
        // DATAGRIDVIEW
        // ============================

        private DataGridView dgvCategories;

        private DataGridViewTextBoxColumn colCategoryId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;

        // ============================
        // CATEGORY INFORMATION
        // ============================

        private Label lblId;
        private TextBox txtId;

        private Label lblCategoryName;
        private TextBox txtCategoryName;

        private Label lblDescription;
        private TextBox txtDescription;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        // ============================
        // STATUS BAR
        // ============================

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}