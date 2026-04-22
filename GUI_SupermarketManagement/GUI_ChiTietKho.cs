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

namespace GUI_SupermarketManagement
{
    public partial class GUI_ChiTietKho : Form
    {
        public GUI_ChiTietKho()
        {
            InitializeComponent();

            // Thủ thuật chống giật lag (Flickering) khi load Form chứa DataGridView
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        // Khởi tạo các đối tượng tầng BUS
        BUS_ChiTietKho bus_ChiTietKho = new BUS_ChiTietKho();
        BUS_Kho bus_kho = new BUS_Kho();
        BUS_QLSP bus_SanPham = new BUS_QLSP();

        /// <summary>
        /// Hàm này được gọi TỪ FORM CHA (GUI_QuanLyKho) trước khi ShowDialog().
        /// Nhiệm vụ: Đổ dữ liệu vào ComboBox và hiển thị các sản phẩm thuộc về một mã kho cụ thể.
        /// </summary>
        /// <param name="maKho">Mã kho được truyền từ Form Quản Lý Kho sang</param>
        public void LoadDanhSach(string maKho = null)
        {
            // 1. Đổ dữ liệu vào ComboBox Kho
            cboMaKho.DataSource = bus_kho.InDanhSachKho();
            cboMaKho.ValueMember = "MaKho";
            cboMaKho.DisplayMember = "TenKho";

            // 2. Đổ dữ liệu vào ComboBox Sản Phẩm
            cboMaSanPham.DataSource = bus_SanPham.LayDS();
            cboMaSanPham.ValueMember = "MaSanPham";
            cboMaSanPham.DisplayMember = "TenSanPham";

            // 3. LOGIC UX QUAN TRỌNG: 
            // Nếu có mã kho truyền sang, ép ComboBox cboMaKho phải hiển thị đúng tên kho đó
            if (!string.IsNullOrEmpty(maKho))
            {
                cboMaKho.SelectedValue = Convert.ToInt32(maKho);

                // (Tùy chọn) Khóa ComboBox Kho lại không cho người dùng sửa sang kho khác để tránh lỗi logic
                cboMaKho.Enabled = false;
            }

            // 4. Load danh sách chi tiết các sản phẩm nằm trong kho đó lên DataGridView
            // (Bạn có thể thêm dgvChiTietKho.AutoGenerateColumns = false; ở đây nếu muốn tự thiết kế cột)
            dgvChiTietKho.DataSource = bus_ChiTietKho.LayDanhSachChiTietKho(maKho);
        }

        /// <summary>
        /// Sự kiện Load Form (Hiện tại để trống vì ta đã dùng hàm LoadDanhSach làm nhiệm vụ khởi tạo dữ liệu rồi)
        /// </summary>
        private void GUI_ChiTietKho_Load(object sender, EventArgs e)
        {
            // Trống
        }

        /// <summary>
        /// Xử lý nút Thoát: Đóng Form con và quay lại Form Quản lý kho
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}