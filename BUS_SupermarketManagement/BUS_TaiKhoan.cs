using DAL_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_TaiKhoan
    {
        // Khởi tạo đối tượng tầng DAL để truy xuất dữ liệu từ cơ sở dữ liệu
        DAL_TaiKhoan dal_TaiKhoan = new DAL_TaiKhoan();

        /// <summary>
        /// Kiểm tra thông tin đăng nhập của nhân viên vào hệ thống.
        /// Nhận dữ liệu từ Form Đăng Nhập và truyền xuống tầng DAL để đối chiếu với Database.
        /// </summary>
        /// <param name="userName">Tên đăng nhập do người dùng nhập</param>
        /// <param name="passWord">Mật khẩu do người dùng nhập</param>
        /// <returns>True nếu tài khoản và mật khẩu khớp, False nếu sai thông tin</returns>
        public DataTable KiemTraTaiKhoan(string userName, string passWord)
        {
            return dal_TaiKhoan.KiemTraTaiKhoanNhanVien(userName, passWord);
        }

        // =======================================================
        // GỢI Ý MỞ RỘNG SAU NÀY:
        // Nếu phần mềm của bạn có phân quyền (Quản lý / Nhân viên), 
        // bạn có thể viết thêm hàm LayQuyenTaiKhoan(userName) ở đây.
        // =======================================================
    }
}