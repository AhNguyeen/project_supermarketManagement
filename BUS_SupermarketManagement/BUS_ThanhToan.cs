using DAL_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_ThanhToan
    {
        DAL_ThanhToan dal_ThanhToan = new DAL_ThanhToan();

        public DataTable HienThiSanPham(string tenSP = null)
        {
            return dal_ThanhToan.HienThiDanhSachSanPham(tenSP);
        }
        public int ThanhToan(int maKH, int maNV, double thanhToan)
        {
            return dal_ThanhToan.ThanhToan(maKH, maNV, thanhToan);
        }
        public bool ThemSanPhamVaoHoaDon(int maHoaDon, int maSanPham, int soLuong, int donGia)
        {
            return dal_ThanhToan.ThemSanPhamVaoHoaDon(maHoaDon, maSanPham, soLuong, donGia);
        }
    }
}
