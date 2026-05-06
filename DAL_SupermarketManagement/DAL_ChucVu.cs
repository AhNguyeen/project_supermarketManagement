using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_ChucVu
    {
        Connect.ConnectData db = new Connect.ConnectData();

        /// <summary>
        /// Lấy thông tin các chức vụ trong siêu thị
        /// </summary>
        /// <returns>DataTable chứa thông tin các chức vụ</returns>
        public DataTable LayThongTinChucVu()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_ChucVu_Search";
                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }
    }
}
