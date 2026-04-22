using DAL_SupermarketManagement;
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

        public DataTable layDanhSach()
        {
            return khachHang.LayDanhSach();
        }
    }
}
