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
    public class BUS_Kho
    {
        DAL_Kho dal_Kho = new DAL_Kho();

        public DataTable InDanhSachKho()
        {
            return dal_Kho.InDanhSachKho();
        }
        public bool ThemKho(ET_Kho kho)
        {
            return dal_Kho.ThemNhaKho(kho);
        }
        public bool SuaKho(ET_Kho kho)
        {
            return dal_Kho.SuaNhaKho(kho);
        }
        public bool XoaKho(string maKho)
        {
            return dal_Kho.XoaNhaKho(maKho);
        }
        public bool KiemTraTrung(string tenKho, int maNhanVien, int maNCC, DateTime ngayNhap)
        {
            return dal_Kho.KiemTraTrungThongTinKho(tenKho, maNhanVien, maNCC, ngayNhap);
        }
    }
}
