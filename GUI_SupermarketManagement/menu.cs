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

        private void OpenChildForm(Type formType)
        {
            // Đóng form cũ trước khi mở form mới để giải phóng bộ nhớ
            if (activeForm != null)
            {
                activeForm.Close();
            }

            Form newForm = (Form)Activator.CreateInstance(formType);
            activeForm = newForm;

            // THỦ THUẬT ÉP FORM THÀNH CONTROL BÌNH THƯỜNG
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            // Nhét Form con vào trong Panel
            pnContainer.Controls.Add(newForm);
            pnContainer.Tag = newForm;
            newForm.BringToFront();
            newForm.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_ThongKe));
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_ThongKe));
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_DanhMuc));
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_NhaCungCap));
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QLSP));
        }

        private void btnQuanLyKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QuanLyKho));
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_QuanLyHoaDon));
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            OpenChildForm(typeof(GUI_ThanhToan));
        }
    }
}
