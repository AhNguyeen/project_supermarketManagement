using DAL_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_ChucVu
    {
        DAL_ChucVu chucVu = new DAL_ChucVu();

        public DataTable LayThongTinChucVu()
        {
            return chucVu.LayThongTinChucVu();
        }
    }
}
