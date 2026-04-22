using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_ChiTietKho
    {
        // Khởi tạo đối tượng kết nối chung tới Database
        Connect.ConnectData db = new Connect.ConnectData();

        /// <summary>
        /// Lấy danh sách chi tiết các sản phẩm (số lượng, giá) nằm trong một kho cụ thể.
        /// Nếu không truyền tham số maKho (bằng null), hệ thống sẽ lấy toàn bộ chi tiết của tất cả các kho.
        /// </summary>
        /// <param name="maKho">Mã kho cần xem chi tiết (Mặc định là null)</param>
        /// <returns>DataTable chứa danh sách chi tiết kho. Trả về null nếu lỗi.</returns>
        public DataTable LayDanhSachChiTietKho(string maKho = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_ChiTietKho_Search";

                // ĐÃ FIX LỖI: Chuyển đổi null của C# thành DBNull.Value của SQL để tránh crash app
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKho", (object)maKho ?? DBNull.Value)
                };

                dt = db.ReadData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }
    }
}