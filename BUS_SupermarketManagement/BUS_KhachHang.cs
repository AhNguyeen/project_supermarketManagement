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
    public class BUS_KhachHang
    {
        DAL_KhachHang khachHang = new DAL_KhachHang();

        // Lấy toàn bộ danh sách khách hàng đang có trong hệ thống
        public DataTable layDanhSach()
        {
            return khachHang.LayDanhSach();
        }

        // Thêm khách hàng mới vào hệ thống
        public bool themKhachHang(ET_KhachHang kh)
        {
            return khachHang.ThemKhachHang(kh);
        }

        // Cập nhật thông tin khách hàng đã có trong hệ thống
        public bool capNhatKhachHang(ET_KhachHang kh)
        {
            return khachHang.SuaKhachHang(kh);
        }

        // Xóa khách hàng khỏi hệ thống
        public bool xoaKhachHang(int maKH)
        {
            return khachHang.XoaKhachHang(maKH);
        }
    }
}
