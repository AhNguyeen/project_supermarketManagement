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
using Guna.Charts.WinForms;


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
            LoadBieuDoDoanhThuDanhMuc();
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

        // Nhớ using System.Windows.Forms.DataVisualization.Charting; ở trên đầu file nhé!

        private void LoadBieuDoDoanhThuDanhMuc()
        {
            try
            {
                // 1. Lấy dữ liệu từ BUS
                DataTable dt = thongKe_BUS.LayDoanhThuTheoDanhMuc();

                // 2. Làm sạch dữ liệu cũ trên GunaChart (nếu có)
                chartDanhThuDanhMuc.Datasets.Clear();

                // Setup tiêu đề (Tùy chọn, bạn có thể chỉnh trực tiếp trong Properties của GunaChart cho khỏe)
                chartDanhThuDanhMuc.Title.Text = "Cơ cấu doanh thu theo Danh mục";

                // 3. Tạo một Dataset định dạng Doughnut (Biểu đồ tròn khuyết)
                GunaDoughnutDataset dataset = new GunaDoughnutDataset();
                dataset.Label = "Doanh thu (VNĐ)"; // Tên hiển thị khi hover chuột

                // 4. Lặp qua từng dòng trong DataTable để nạp dữ liệu vào Dataset
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string tenDanhMuc = dr["TenDanhMuc"].ToString();
                        double doanhThu = Convert.ToDouble(dr["TongDoanhThu"]);

                        // Thêm từng điểm dữ liệu: DataPoints.Add("Tên Nhãn", GiáTrị)
                        dataset.DataPoints.Add(tenDanhMuc, doanhThu);
                    }
                }

                // 5. Thêm Dataset đã có dữ liệu vào biểu đồ
                chartDanhThuDanhMuc.Datasets.Add(dataset);

                // 6. Cập nhật lại biểu đồ để hiển thị
                chartDanhThuDanhMuc.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load biểu đồ: " + ex.Message);
            }
        }
    }
}
