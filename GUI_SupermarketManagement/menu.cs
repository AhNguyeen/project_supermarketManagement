using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI_SupermarketManagement
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        /// Xử lý sự kiện khi bấm nút Thoát trên Menu.
        /// Gọi lệnh Close() để kích hoạt sự kiện FrmMenu_FormClosing kiểm tra xác nhận.
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 1. Khai báo một cái "cờ hiệu" (Biến toàn cục trên cùng của class FrmMenu)
        bool isLogout = false;

        // 2. Chỉnh lại Nút Đăng Xuất
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            isLogout = true;                         // Bật cờ hiệu Đăng xuất lên
            this.DialogResult = DialogResult.Yes;    // Bắn tín hiệu "Đăng xuất" về cho GUI_Login
            this.Close();                            // Đóng form Menu
        }

        // 3. Chỉnh lại hàm FormClosing của Menu
        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // LỚP BẢO VỆ: Nếu đang là Đăng Xuất thì KHÔNG HIỂN THỊ THÔNG BÁO GÌ CẢ, CHO ĐÓNG LUÔN
            if (isLogout == true)
            {
                return;
            }

            // Nếu không phải Đăng xuất (ví dụ bấm dấu X góc phải), thì mới hiện thông báo hỏi
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        // Biến toàn cục lưu trữ vết của Form con đang được mở hiện tại trên màn hình
        private Form activeForm = null;

        /// <summary>
        /// Hàm xử lý phân quyền: Admin dùng hết, User bị ẩn các mục nhạy cảm
        /// </summary>
        private void PhanQuyenMenu()
        {
            if (UserSession.PhanQuyen == "User")
            {
                // Khóa hoặc ẩn các chức năng chỉ dành cho Admin
                btnThongKe.Enabled = false;   // Không cho xem doanh thu tổng
                btnNhanVien.Visible = false; // Ẩn luôn nút Quản lý nhân viên để bảo mật

                // Bạn có thể khóa thêm các nút khác tùy vào yêu cầu nghiệp vụ
            }
        }

        /// <summary>
        /// Mở một Form con bên trong không gian của Form cha (MDI Parent).
        /// </summary>
        /// <param name="formType">Kiểu (Type) của Form cần mở. Ví dụ: typeof(GUI_SanPham)</param>
        private void OpenChildForm(Type formType)
        {
            // BƯỚC 1: KHỞI TẠO VÀ CÀI ĐẶT FORM MỚI TRƯỚC
            Form newForm = (Form)Activator.CreateInstance(formType);
            newForm.MdiParent = this;
            newForm.ControlBox = false;
            newForm.ShowIcon = false;
            newForm.Text = "";
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            // BƯỚC 2: HIỂN THỊ FORM MỚI ĐỂ CHE ĐI FORM CŨ
            newForm.Show();

            // BƯỚC 3: SAU KHI FORM MỚI ĐÃ LÊN HÌNH, MỚI ĐÓNG CÁC FORM CŨ ĐI
            foreach (Form childForm in this.MdiChildren)
            {
                // Kiểm tra: Nếu không phải là cái form vừa tạo thì mới Close
                if (childForm != newForm)
                {
                    childForm.Close();
                    childForm.Dispose(); // Ép dọn rác RAM ngay lập tức
                }
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_ThongKe));
            lblGUI.Text = "Thống Kê";
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            // 1. Load thông tin người dùng lên các Label
            lblEmployee.Text = "Xin chào: " + UserSession.TenNhanVien + " | Quyền: " + UserSession.PhanQuyen;

            // 2. Thực hiện phân quyền các chức năng
            PhanQuyenMenu();

            // 3. Mở mặc định trang Thống kê (Nếu là Admin) hoặc Thanh toán (Nếu là User)
            if (UserSession.PhanQuyen == "Admin")
            {
                OpenChildForm(typeof(GUI_ThongKe));
                lblGUI.Text = "Thống Kê";
            }
            else
            {
                OpenChildForm(typeof(GUI_ThanhToan));
                lblGUI.Text = "Thanh Toán";
            }
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_DanhMuc));
            lblGUI.Text = "Danh Mục";
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_NhaCungCap));
            lblGUI.Text = "Nhà Cung Cấp";
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QLSP));
            lblGUI.Text = "Quản Lý Sản Phẩm";
        }

        private void btnQuanLyKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QuanLyKho));
            lblGUI.Text = "Quản Lý Kho";
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QuanLyHoaDon));
            lblGUI.Text = "Quản Lý Hóa Đơn";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_ThanhToan));
            lblGUI.Text = "Thanh Toán";
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_NhanVien));
            lblGUI.Text = "Quản Lý Nhân Viên";
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_KhachHang));
            lblGUI.Text = "Quản Lý Khách Hàng";
        }
    }
}
