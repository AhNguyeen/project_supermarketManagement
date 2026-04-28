using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_ChiTietHoaDon
    {
        Connect.ConnectData db = new Connect.ConnectData();
        public DAL_ChiTietHoaDon()
        {

        }

        public DataTable LayDachSachHoaDon(ET_ChiTietHoaDon chiTietHoaDon = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_HienThiChiTietHoaDon";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaHoaDon", chiTietHoaDon.MaHD)
                };

                dt = db.ReadData(strSQL, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hóa đơn", ex);
                dt = null;
            }
            return dt;
        }
    }
}
