namespace GUI_SupermarketManagement
{
    partial class GUI_QLSP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnThemHinhAnh = new System.Windows.Forms.Button();
            this.pbAnhSanPham = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbNhaCungCap = new System.Windows.Forms.ComboBox();
            this.cbDanhMuc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenSanPham = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtMoTa = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvsanpham = new System.Windows.Forms.DataGridView();
            this.quanLySieuThiDataSet = new GUI_SupermarketManagement.QuanLySieuThiDataSet();
            this.sanPhamBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sanPhamTableAdapter = new GUI_SupermarketManagement.QuanLySieuThiDataSetTableAdapters.SanPhamTableAdapter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnhSanPham)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsanpham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLySieuThiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sanPhamBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnChiTiet.Location = new System.Drawing.Point(806, 21);
            this.btnChiTiet.Margin = new System.Windows.Forms.Padding(2);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(92, 37);
            this.btnChiTiet.TabIndex = 21;
            this.btnChiTiet.Text = "Chi Tiết";
            this.btnChiTiet.UseVisualStyleBackColor = true;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa.Location = new System.Drawing.Point(586, 21);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(92, 37);
            this.btnXoa.TabIndex = 19;
            this.btnXoa.Text = "Xóa Sản phẩm";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSua.Location = new System.Drawing.Point(481, 21);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(92, 37);
            this.btnSua.TabIndex = 18;
            this.btnSua.Text = "Sửa Sản phẩm";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThem.Location = new System.Drawing.Point(375, 21);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(92, 37);
            this.btnThem.TabIndex = 17;
            this.btnThem.Text = "Thêm mới ";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(76, 195);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Mô tả:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(94, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Giá bán (VND) :";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(94, 56);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Giá nhập (VND) :";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLamMoi.Location = new System.Drawing.Point(695, 21);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(92, 37);
            this.btnLamMoi.TabIndex = 20;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnThemHinhAnh);
            this.groupBox2.Controls.Add(this.pbAnhSanPham);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtGiaBan);
            this.groupBox2.Controls.Add(this.txtGiaNhap);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(637, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(631, 349);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Giá & Tồn kho";
            // 
            // btnThemHinhAnh
            // 
            this.btnThemHinhAnh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThemHinhAnh.Location = new System.Drawing.Point(299, 351);
            this.btnThemHinhAnh.Name = "btnThemHinhAnh";
            this.btnThemHinhAnh.Size = new System.Drawing.Size(129, 31);
            this.btnThemHinhAnh.TabIndex = 7;
            this.btnThemHinhAnh.Text = "Thêm hình ảnh";
            this.btnThemHinhAnh.UseVisualStyleBackColor = true;
            // 
            // pbAnhSanPham
            // 
            this.pbAnhSanPham.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbAnhSanPham.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAnhSanPham.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbAnhSanPham.Location = new System.Drawing.Point(206, 125);
            this.pbAnhSanPham.Margin = new System.Windows.Forms.Padding(2);
            this.pbAnhSanPham.Name = "pbAnhSanPham";
            this.pbAnhSanPham.Size = new System.Drawing.Size(180, 180);
            this.pbAnhSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAnhSanPham.TabIndex = 6;
            this.pbAnhSanPham.TabStop = false;
            this.pbAnhSanPham.Click += new System.EventHandler(this.pbAnhSanPham_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(94, 125);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ảnh sản phẩm:";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtGiaBan.Location = new System.Drawing.Point(206, 84);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(316, 24);
            this.txtGiaBan.TabIndex = 3;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtGiaNhap.Location = new System.Drawing.Point(206, 51);
            this.txtGiaNhap.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(316, 24);
            this.txtGiaNhap.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 159);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nhà cung cấp:";
            // 
            // cbNhaCungCap
            // 
            this.cbNhaCungCap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhaCungCap.FormattingEnabled = true;
            this.cbNhaCungCap.Location = new System.Drawing.Point(166, 155);
            this.cbNhaCungCap.Margin = new System.Windows.Forms.Padding(2);
            this.cbNhaCungCap.Name = "cbNhaCungCap";
            this.cbNhaCungCap.Size = new System.Drawing.Size(317, 25);
            this.cbNhaCungCap.TabIndex = 6;
            // 
            // cbDanhMuc
            // 
            this.cbDanhMuc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDanhMuc.FormattingEnabled = true;
            this.cbDanhMuc.Location = new System.Drawing.Point(167, 118);
            this.cbDanhMuc.Margin = new System.Windows.Forms.Padding(2);
            this.cbDanhMuc.Name = "cbDanhMuc";
            this.cbDanhMuc.Size = new System.Drawing.Size(316, 25);
            this.cbDanhMuc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Danh mục :";
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTenSanPham.Location = new System.Drawing.Point(167, 81);
            this.txtTenSanPham.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Size = new System.Drawing.Size(316, 24);
            this.txtTenSanPham.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên sản phẩm :";
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaSanPham.Location = new System.Drawing.Point(167, 48);
            this.txtMaSanPham.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.ReadOnly = true;
            this.txtMaSanPham.Size = new System.Drawing.Size(316, 24);
            this.txtMaSanPham.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtMoTa);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbNhaCungCap);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbDanhMuc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTenSanPham);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaSanPham);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(631, 349);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung";
            // 
            // rtxtMoTa
            // 
            this.rtxtMoTa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rtxtMoTa.Location = new System.Drawing.Point(166, 195);
            this.rtxtMoTa.Name = "rtxtMoTa";
            this.rtxtMoTa.Size = new System.Drawing.Size(317, 110);
            this.rtxtMoTa.TabIndex = 8;
            this.rtxtMoTa.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sản phẩm :";
            // 
            // dgvsanpham
            // 
            this.dgvsanpham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvsanpham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsanpham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsanpham.Location = new System.Drawing.Point(0, 427);
            this.dgvsanpham.Margin = new System.Windows.Forms.Padding(2);
            this.dgvsanpham.Name = "dgvsanpham";
            this.dgvsanpham.RowHeadersWidth = 51;
            this.dgvsanpham.RowTemplate.Height = 24;
            this.dgvsanpham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvsanpham.Size = new System.Drawing.Size(1270, 265);
            this.dgvsanpham.TabIndex = 15;
            this.dgvsanpham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvsanpham_CellClick);
            this.dgvsanpham.Click += new System.EventHandler(this.dgvsanpham_Click);
            // 
            // quanLySieuThiDataSet
            // 
            this.quanLySieuThiDataSet.DataSetName = "QuanLySieuThiDataSet";
            this.quanLySieuThiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sanPhamBindingSource
            // 
            this.sanPhamBindingSource.DataMember = "SanPham";
            this.sanPhamBindingSource.DataSource = this.quanLySieuThiDataSet;
            // 
            // sanPhamTableAdapter
            // 
            this.sanPhamTableAdapter.ClearBeforeFill = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnXoa);
            this.panel1.Controls.Add(this.btnChiTiet);
            this.panel1.Controls.Add(this.btnLamMoi);
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.btnSua);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1270, 74);
            this.panel1.TabIndex = 22;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1270, 353);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // GUI_QLSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 692);
            this.Controls.Add(this.dgvsanpham);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GUI_QLSP";
            this.Text = "GUI_QLSP";
            this.Load += new System.EventHandler(this.GUI_QLSP_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnhSanPham)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsanpham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLySieuThiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sanPhamBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.PictureBox pbAnhSanPham;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbNhaCungCap;
        private System.Windows.Forms.ComboBox cbDanhMuc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenSanPham;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private QuanLySieuThiDataSet quanLySieuThiDataSet;
        private System.Windows.Forms.BindingSource sanPhamBindingSource;
        private QuanLySieuThiDataSetTableAdapters.SanPhamTableAdapter sanPhamTableAdapter;
        public System.Windows.Forms.DataGridView dgvsanpham;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.RichTextBox rtxtMoTa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnThemHinhAnh;
    }
}