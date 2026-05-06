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
            // Bước 1: Đóng tất cả các form con MDI đang mở để giải phóng bộ nhớ
            // (Đảm bảo tại một thời điểm chỉ có 1 form duy nhất được hiển thị trên màn hình Menu)
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }

            // Bước 2: Khởi tạo instance của Form mới từ Type được truyền vào
            Form newForm = (Form)Activator.CreateInstance(formType);

            // Bước 3: THIẾT LẬP MDI PARENT QUAN TRỌNG
            // Chỉ định Form Menu hiện tại (this) chính là Form Cha của newForm
            newForm.MdiParent = this;

            newForm.ControlBox = false;     // Ẩn 3 nút Thu nhỏ/Phóng to/Đóng
            newForm.ShowIcon = false;       // Ẩn icon ở góc trái
            newForm.Text = "";              // Xóa tên Form để nó không bị ghép chữ lên thanh Menu của Form cha

            // Bước 4: Tối ưu UX giao diện (Tùy chọn)
            // Nếu bạn muốn Form con lấp đầy toàn bộ không gian MDI và ẩn đi thanh tiêu đề (giống như dùng Panel trước đây)
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            // Bước 5: Hiển thị Form con lên màn hình
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
