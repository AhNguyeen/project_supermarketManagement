using BUS_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI_SupermarketManagement
{
    public partial class GUI_ThongKe : Form
    {
        public GUI_ThongKe()
        {
            InitializeComponent();

            //Thủ thật chống giật lag của GEMINI
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
        BUS_ThongKe thongKe_BUS = new BUS_ThongKe();

        private void GUI_ThongKe_Load(object sender, EventArgs e)
        {
            labelTongDoanhThu.Text = thongKe_BUS.TongDoanhThu().ToString("#,##0") + "đ";
            labelTongLoiNhuan.Text = thongKe_BUS.LoiNhuan().ToString("#,##0") + "đ";
            labelSoDonHang.Text = thongKe_BUS.SoDonHang().ToString("#,##0");
            labelSanPhamTonKho.Text = thongKe_BUS.SanPhamTonKho().ToString("#,##0");
            LoadSanPham();
            VeBieuDoDoanhThu();
        }

        private void VeBieuDoDoanhThu()
        {
            // 1. LẤY DỮ LIỆU TỪ DATABASE
            DataTable dtDoanhThu = thongKe_BUS.VeBieuDoDoanhThu();

            // 2. DỌN DẸP VÀ CHỈNH NỀN BIỂU ĐỒ TRẮNG TINH KHÔI
            chartDoanhThu.Datasets.Clear();
            chartDoanhThu.Legend.Display = false; // Ẩn chú giải mặc định

            // Tắt hoàn toàn lưới dọc, làm mờ lưới ngang cho giống web
            chartDoanhThu.XAxes.GridLines.Display = false;
            chartDoanhThu.YAxes.GridLines.Display = true;
            chartDoanhThu.YAxes.GridLines.Color = Color.FromArgb(245, 245, 245);
            chartDoanhThu.YAxes.GridLines.DrawTicks = false;

            // =========================================================
            // 3. KHỞI TẠO ĐƯỜNG CONG DOANH THU (CÓ PHỦ NỀN MỜ)
            // =========================================================
            Guna.Charts.WinForms.GunaSplineAreaDataset dataDoanhThu = new Guna.Charts.WinForms.GunaSplineAreaDataset();
            dataDoanhThu.Label = "Doanh Thu";
            dataDoanhThu.BorderColor = Color.FromArgb(0, 118, 255); // Màu viền xanh dương
            dataDoanhThu.FillColor = Color.FromArgb(40, 0, 118, 255); // Đổ nền xanh mờ (Alpha = 40)

            // Giấu chấm tròn, chỉ hiện khi rê chuột
            dataDoanhThu.PointRadius = 0;
            //dataDoanhThu.PointHoverRadius = 6;

            // =========================================================
            // 4. ĐỔ DỮ LIỆU THẬT TỪ DATATABLE VÀO ĐƯỜNG CONG
            // =========================================================
            foreach (DataRow row in dtDoanhThu.Rows)
            {
                // Định dạng ngày thành dạng dd/MM
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                string ngayFormat = ngay.ToString("dd/MM");

                double doanhThu = Convert.ToDouble(row["DoanhThu"]);

                // Nạp dữ liệu vào biểu đồ
                dataDoanhThu.DataPoints.Add(ngayFormat, doanhThu);
            }

            // 5. THÊM VÀO BIỂU ĐỒ VÀ RENDER
            chartDoanhThu.Datasets.Add(dataDoanhThu);

            // 6. LÀM ĐẸP TOOLTIP (Bảng nhỏ hiện ra khi rê chuột)
            chartDoanhThu.Tooltips.BackgroundColor = Color.White;
            chartDoanhThu.Tooltips.TitleForeColor = Color.Black;
            chartDoanhThu.Tooltips.BodyForeColor = Color.DimGray;
            chartDoanhThu.Tooltips.BorderColor = Color.FromArgb(220, 220, 220);
            chartDoanhThu.Tooltips.BorderWidth = 1;

            // Định dạng số tiền có dấu phẩy trên trục Y (Nếu phiên bản Guna của bạn hỗ trợ)
            // chartDoanhThu.YAxes.Format = "N0"; 

            // Bắt buộc phải có lệnh Update() để GunaChart vẽ lại màn hình
            chartDoanhThu.Update();
        }

        public void LoadSanPham()
        {
            dgvCacSanPhamBanChay.DataSource = thongKe_BUS.LaySanPhamBanChay();
            FormatDataGridView(dgvCacSanPhamBanChay);
            LoadCanhBaoTonKho_HienDai();
        }

        private void FormatDataGridView(DataGridView dgv)
        {
            // 1. Tắt định dạng mặc định của Windows để tự custom màu
            dgv.EnableHeadersVisualStyles = false;

            // 2. Tắt các đường viền thô kệch
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ giữ lại đường kẻ ngang
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false; // Tắt cột mũi tên vô duyên ở bên trái
            dgv.AllowUserToAddRows = false; // Tắt dòng trắng dưới cùng

            // 3. Chỉnh màu sắc TÔNG SÁNG (Light Theme)
            dgv.BackgroundColor = Color.White; // Nền trắng
            dgv.GridColor = Color.FromArgb(235, 235, 235); // Đường kẻ ngang màu xám cực nhạt

            // -- Tiêu đề (Header)
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 100, 100); // Chữ xám thanh lịch
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            // -- Dòng dữ liệu (Rows)
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255); // Khi chọn dòng thì nổi màu xanh nhạt cực sang
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 45; // Chiều cao dòng rộng rãi (Cực kỳ quan trọng để nhìn giống web)

            // 4. Canh lề cho đẹp (Tùy chọn dựa theo tên cột của bạn)
            if (dgv.Columns.Contains("Xếp Hạng"))
                dgv.Columns["Xếp Hạng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains("Số Lượng"))
                dgv.Columns["Số Lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains("Doanh Thu"))
            {
                dgv.Columns["Doanh Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["Doanh Thu"].DefaultCellStyle.Format = "#,##0 đ"; // Ép định dạng tiền tệ có chữ đ
            }
        }

        private void LoadCanhBaoTonKho_HienDai()
        {

            // 1. Xóa sạch các thẻ cũ (nếu có) để load lại từ đầu
            flpCuaToi.Controls.Clear();

            // 2. Lấy dữ liệu tồn kho (Giả sử bạn dùng Store sp_ThongKeTonKho)
            // Ở đây mình ví dụ dùng DataTable dt, bạn tự gọi lớp BUS của bạn vào nhé:
            DataTable dtTonKho = thongKe_BUS.ThongKeTonKho();

            // ---- THÊM LÁ CHẮN BẢO VỆ Ở ĐÂY ----
            if (dtTonKho == null || dtTonKho.Rows.Count == 0)
            {
                MessageBox.Show("Không thể kết nối đến Database hoặc không có dữ liệu tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại ngay, không chạy xuống vòng lặp foreach nữa
            }

            foreach (DataRow row in dtTonKho.Rows)
            {
                int soLuongTon = Convert.ToInt32(row["Số Lượng Tồn"]);

                // Chỉ hiện những cảnh báo cho sản phẩm có tồn kho dưới 50
                if (soLuongTon > 50) continue;

                // --- BẮT ĐẦU VẼ 1 CÁI CARD (THẺ) ---

                // 1. Tạo Khung Thẻ (Panel bo góc)
                Guna.UI2.WinForms.Guna2Panel pnlCard = new Guna.UI2.WinForms.Guna2Panel();
                pnlCard.Size = new Size(flpCuaToi.Width - 30, 80); // Rộng vừa bằng khay chứa, cao 80
                pnlCard.BorderRadius = 10;
                pnlCard.FillColor = Color.FromArgb(255, 245, 235); // Màu nền cam cực nhạt (giống ảnh)
                pnlCard.BorderColor = Color.FromArgb(255, 150, 50); // Viền cam
                pnlCard.BorderThickness = 1;
                pnlCard.Margin = new Padding(5, 5, 5, 10); // Cách đều nhau ra

                // 2. Tạo Tên Sản Phẩm (Label)
                Label lblTen = new Label();
                lblTen.Text = row["Tên Sản Phẩm"].ToString();
                lblTen.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lblTen.ForeColor = Color.Black;
                lblTen.Location = new Point(15, 10);
                lblTen.AutoSize = true;
                lblTen.BackColor = Color.Transparent;

                // 3. Tạo chữ hiển thị Số Lượng Tồn
                Label lblTon = new Label();
                lblTon.Text = "Tồn: " + soLuongTon.ToString();
                lblTon.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                lblTon.ForeColor = Color.DimGray;
                lblTon.Location = new Point(15, 35);
                lblTon.AutoSize = true;
                lblTon.BackColor = Color.Transparent;

                // 4. Tạo thanh Progress Bar màu cam
                Guna.UI2.WinForms.Guna2ProgressBar pBar = new Guna.UI2.WinForms.Guna2ProgressBar();
                pBar.Location = new Point(15, 58);
                pBar.Size = new Size(pnlCard.Width - 70, 8); // Chiều dài thanh bar
                pBar.BorderRadius = 4;
                pBar.Maximum = 50; // Đặt mốc 50 là đầy thanh
                pBar.Value = soLuongTon; // Lượng màu cam hiển thị
                pBar.ProgressColor = Color.FromArgb(255, 100, 0); // Màu cam đậm
                pBar.ProgressColor2 = Color.FromArgb(255, 100, 0);
                pBar.FillColor = Color.FromArgb(255, 220, 190); // Màu nền thanh bar

                // 5. Gắn các chữ và thanh bar vào Khung Thẻ
                pnlCard.Controls.Add(lblTen);
                pnlCard.Controls.Add(lblTon);
                pnlCard.Controls.Add(pBar);

                // 6. Ném Khung Thẻ hoàn thiện vào FlowLayoutPanel để hiển thị lên màn hình
                flpCuaToi.Controls.Add(pnlCard);
            }
        }

    }
}
