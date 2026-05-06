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

        /// Bắt sự kiện hệ thống khi Form chuẩn bị đóng.
        /// Hiển thị hộp thoại cảnh báo để tránh việc người dùng vô tình tắt nhầm phần mềm.
        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu người dùng chọn No, hủy lệnh đóng Form (giữ phần mềm tiếp tục chạy)
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc chắn thoát khỏi hệ thống không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        // Biến toàn cục lưu trữ vết của Form con đang được mở hiện tại trên màn hình
        private Form activeForm = null;

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

            // Tạm thời tắt vẽ giao diện để load nhanh hơn
            newForm.SuspendLayout();

            // BƯỚC 2: HIỂN THỊ FORM MỚI ĐỂ CHE ĐI FORM CŨ
            newForm.Show();
            newForm.ResumeLayout();

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
            OpenChildForm(typeof(GUI_ThongKe));
            lblGUI.Text = "Thống Kê";
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
    }
}
