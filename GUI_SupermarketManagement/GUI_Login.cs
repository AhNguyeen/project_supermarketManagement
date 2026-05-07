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
            // BƯỚC BẢO VỆ: Nếu form đang đóng do lệnh Application.Exit() (Từ Form Menu truyền qua)
            // thì ta thoát luôn, không hiển thị lại cái bảng hỏi ngớ ngẩn này nữa.
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

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

                UserSession.MaNhanVien = Convert.ToInt32(dt.Rows[0]["MaNhanVien"]);
                UserSession.TenNhanVien = dt.Rows[0]["HoTen"].ToString();
                UserSession.PhanQuyen = dt.Rows[0]["PhanQuyen"].ToString(); // LƯU QUYỀN TẠI ĐÂY

                // Khởi tạo Form Menu
                FrmMenu formMenu = new FrmMenu();

                // Ẩn Form đăng nhập hiện tại đi
                this.Hide();

                // BẮT SỰ KIỆN KHI FORM MENU BỊ ĐÓNG
                formMenu.FormClosed += (s, args) =>
                {
                    // Ta quy ước: Nếu FrmMenu gửi về tín hiệu DialogResult.Yes -> Đây là bấm Đăng Xuất
                    if (formMenu.DialogResult == DialogResult.Yes)
                    {
                        this.Show(); // Hiện lại form đăng nhập
                        txtPassword.Clear(); // An toàn: Xóa mật khẩu đi để đăng nhập lại
                    }
                    else
                    {
                        // Còn lại (Bấm dấu X góc phải) -> Đây là Tắt phần mềm
                        Application.Exit();
                    }
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