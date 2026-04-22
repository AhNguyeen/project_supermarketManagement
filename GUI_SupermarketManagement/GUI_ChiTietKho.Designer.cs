namespace GUI_SupermarketManagement
{
    partial class GUI_ChiTietKho
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvChiTietKho = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.cboMaSanPham = new System.Windows.Forms.ComboBox();
            this.cboMaKho = new System.Windows.Forms.ComboBox();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtMaChiTiet = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietKho)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1091, 61);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chi Tiết Kho";
            // 
            // dgvChiTietKho
            // 
            this.dgvChiTietKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietKho.Location = new System.Drawing.Point(0, 61);
            this.dgvChiTietKho.MultiSelect = false;
            this.dgvChiTietKho.Name = "dgvChiTietKho";
            this.dgvChiTietKho.ReadOnly = true;
            this.dgvChiTietKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietKho.Size = new System.Drawing.Size(703, 691);
            this.dgvChiTietKho.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnLamMoi);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.btnXoa);
            this.panel2.Controls.Add(this.btnReport);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Controls.Add(this.cboMaSanPham);
            this.panel2.Controls.Add(this.cboMaKho);
            this.panel2.Controls.Add(this.txtThanhTien);
            this.panel2.Controls.Add(this.txtGiaNhap);
            this.panel2.Controls.Add(this.txtSoLuong);
            this.panel2.Controls.Add(this.txtMaChiTiet);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(703, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 691);
            this.panel2.TabIndex = 2;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(258, 253);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(110, 38);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(18, 253);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 38);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(258, 209);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 38);
            this.btnSua.TabIndex = 3;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(138, 209);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 38);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(138, 253);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(110, 38);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Báo cáo";
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(18, 209);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 38);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // cboMaSanPham
            // 
            this.cboMaSanPham.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboMaSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaSanPham.FormattingEnabled = true;
            this.cboMaSanPham.Location = new System.Drawing.Point(106, 83);
            this.cboMaSanPham.Name = "cboMaSanPham";
            this.cboMaSanPham.Size = new System.Drawing.Size(261, 21);
            this.cboMaSanPham.TabIndex = 2;
            // 
            // cboMaKho
            // 
            this.cboMaKho.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboMaKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaKho.FormattingEnabled = true;
            this.cboMaKho.Location = new System.Drawing.Point(106, 53);
            this.cboMaKho.Name = "cboMaKho";
            this.cboMaKho.Size = new System.Drawing.Size(261, 21);
            this.cboMaKho.TabIndex = 2;
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtThanhTien.Location = new System.Drawing.Point(106, 173);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(261, 20);
            this.txtThanhTien.TabIndex = 1;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtGiaNhap.Location = new System.Drawing.Point(106, 143);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(261, 20);
            this.txtGiaNhap.TabIndex = 1;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSoLuong.Location = new System.Drawing.Point(106, 113);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(261, 20);
            this.txtSoLuong.TabIndex = 1;
            // 
            // txtMaChiTiet
            // 
            this.txtMaChiTiet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaChiTiet.Location = new System.Drawing.Point(106, 23);
            this.txtMaChiTiet.Name = "txtMaChiTiet";
            this.txtMaChiTiet.ReadOnly = true;
            this.txtMaChiTiet.Size = new System.Drawing.Size(261, 20);
            this.txtMaChiTiet.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Thành tiền:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tên sản phẩm:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Giá nhập:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số lượng nhập:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên kho:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã chi tiết:";
            // 
            // GUI_ChiTietKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 752);
            this.Controls.Add(this.dgvChiTietKho);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GUI_ChiTietKho";
            this.Text = "Chi Tiết Kho";
            this.Load += new System.EventHandler(this.GUI_ChiTietKho_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietKho)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvChiTietKho;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMaSanPham;
        private System.Windows.Forms.ComboBox cboMaKho;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtMaChiTiet;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnThem;
    }
}