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
    public class BUS_ChiTietHoaDon
    {
        DAL_ChiTietHoaDon dal_ChiTietHoaDon = new DAL_ChiTietHoaDon();

        public DataTable LayDachSachHoaDon(ET_ChiTietHoaDon chiTietHoaDon = null)
        {
            return dal_ChiTietHoaDon.LayDachSachHoaDon(chiTietHoaDon);

        }
    }
}
