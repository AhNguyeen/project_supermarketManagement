using DAL_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_HoaDon
    {
        DAL_HoaDon dal_hoaDon = new DAL_HoaDon();

        public DataTable LayDanhSach()
        {
            return dal_hoaDon.LayDanhSachHoaDon();
        }
        public DataTable LayDanhSachTheoHoaDon(string maHD = null)
        {
            return dal_hoaDon.LayDanhSachHoaDonTheoHoaDon(maHD);
        }

        public bool ThemHoaDon(ET_HoaDon hoaDon)
        {
            return dal_hoaDon.ThemHoaDon(hoaDon);
        }
        public bool SuaHoaDon(ET_HoaDon hoaDon)
        {
            return dal_hoaDon.SuaHoaDon(hoaDon);
        }
        public bool XoaHoaDon(int maHD)
        {
            return dal_hoaDon.XoaHoaDon(maHD);
        }

        public bool KiemTraTrungHoaDon(int maKhachHang, int maNhanVien, DateTime ngayLap)
        {
            return dal_hoaDon.KiemTraTrungThongTinHoaDon(maKhachHang, maNhanVien, ngayLap);
        }
    }
}
