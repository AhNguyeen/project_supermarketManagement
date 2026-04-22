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
    public class DAL_TaiKhoan
    {
        //Tạo cổng kết nối Database
        Connect.ConnectData connect = new Connect.ConnectData();

        //Kiểm tra Username và Password để đăng nhập và ứng dụng
        public DataTable KiemTraTaiKhoanNhanVien(string userName, string passWord)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Username", userName),
            new SqlParameter("@Password", passWord)
            };

            // Trả thẳng DataTable lên cho giao diện (GUI)
            return connect.ReadData("sp_TaiKhoan_Login", parameters);
        }
    }
}
