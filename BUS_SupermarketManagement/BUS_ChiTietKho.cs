using DAL_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_ChiTietKho
    {
        DAL_ChiTietKho chiTietKho = new DAL_ChiTietKho();

        // Lấy danh sách chi tiết kho. Nếu maKho là null, sẽ lấy toàn bộ chi tiết của tất cả các kho.   
        public DataTable LayDanhSachChiTietKho(string maKho = null)
        {
            return chiTietKho.LayDanhSachChiTietKho(maKho);
        }

        // Thêm một chi tiết kho mới vào một phiếu nhập kho cụ thể
        public bool ThemChiTietKho(string maKho, string maSanPham, int soLuong, decimal gia)
        {
            return chiTietKho.ThemChiTietKho(maKho, maSanPham, soLuong, gia);
        }

        // Kiểm tra xem chi tiết kho này đã tồn tại trước đó chưa (Chống nhập đúp dữ liệu)
        public bool KiemTraTrungThongTinChiTietKho(string maKho, string maSanPham)
        {
            return chiTietKho.KiemTraTrungThongTinChiTietKho(maKho, maSanPham);
        }

        // Xóa toàn bộ chi tiết của một kho khi phiếu nhập kho đó bị xóa (Dùng để duy trì tính toàn vẹn dữ liệu)
        public bool XoaChiTietKhoTheoMaKho(int maKho, int maSanPham)
        {
            return chiTietKho.XoaSanPhamKhoiChiTietKho(maKho, maSanPham);
        }

        // Sửa thông tin chi tiết kho (Ví dụ: Cập nhật số lượng hoặc giá của một sản phẩm trong kho)
        public bool SuaChiTietKho(int maChiTietKho, string maKho, string maSanPham, int soLuong, decimal gia)
        {
            return chiTietKho.SuaChiTietKho(maChiTietKho, maKho, maSanPham, soLuong, gia);
        }
    }
}
