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
        public DataTable LayDanhSachChiTietKho(string maKho = null)
        {
            return chiTietKho.LayDanhSachChiTietKho(maKho);
        }
    }
}
