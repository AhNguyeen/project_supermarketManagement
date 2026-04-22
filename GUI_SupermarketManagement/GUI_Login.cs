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
    public partial class GUI_Login : Form
    {
        public GUI_Login()
        {
            InitializeComponent();
        }
        BUS_TaiKhoan taiKhoan = new BUS_TaiKhoan();
        private void GUI_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát khỏi trang đăng nhập không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // 1. Nhận DataTable từ DAL gửi lên
            DataTable dt = taiKhoan.KiemTraTaiKhoan(txtUsername.Text, txtPassword.Text);

            // 2. Kiểm tra xem có dòng dữ liệu nào không (Tương đương với đăng nhập thành công)
            if (dt != null && dt.Rows.Count > 0)
            {
                // ĐĂNG NHẬP THÀNH CÔNG

                // 3. Lúc này dt đang cầm toàn bộ dữ liệu từ Store Procedure
                // GẮN THÔNG TIN VÀO BIẾN TOÀN CỤC
                UserSession.MaNhanVien = Convert.ToInt32(dt.Rows[0]["MaNhanVien"]);

                // Gọi đúng tên cột 'HoTen' như trong câu SELECT của Store Procedure
                UserSession.TenNhanVien = dt.Rows[0]["HoTen"].ToString();

                // Khởi tạo Form Menu
                FrmMenu formMenu = new FrmMenu();

                // Ẩn Form đăng nhập hiện tại đi
                this.Hide();

                // Đăng ký sự kiện tắt ứng dụng
                formMenu.FormClosed += (s, args) =>
                {
                    Application.Exit();
                };

                // Hiển thị Form Menu lên màn hình
                formMenu.Show();
            }
            else
            {
                // ĐĂNG NHẬP THẤT BẠI
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng kiểm tra lại!",
                                "Lỗi đăng nhập",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
