using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_SupermarketManagement
{
    public static class UserSession
    {
        // Lưu Mã nhân viên
        public static int MaNhanVien { get; set; }

        // Bạn có thể lưu thêm các thông tin khác nếu cần dùng sau này
        public static string TenNhanVien { get; set; }
        public static string PhanQuyen { get; set; } // Ví dụ: "Admin", "NhanVien"
    }
}
