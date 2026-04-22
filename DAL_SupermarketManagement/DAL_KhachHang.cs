using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_KhachHang
    {
        // Khởi tạo đối tượng kết nối chung tới Database
        Connect.ConnectData db = new Connect.ConnectData();

        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng đang có trong hệ thống
        /// Dùng để đổ dữ liệu vào ComboBox khi lập hóa đơn hoặc in danh sách quản lý
        /// </summary>
        /// <returns>DataTable chứa thông tin khách hàng. Trả về null nếu có lỗi truy vấn.</returns>
        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi Store Procedure lấy danh sách khách hàng
                string strSQL = "sp_KhachHang_All";
                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                // Nếu lỗi truy vấn, trả về null để tầng trên (BUS/GUI) dễ dàng xử lý
                return null;
            }
            return dt;
        }

        // =======================================================
        // GỢI Ý MỞ RỘNG SAU NÀY:
        // Nếu sau này bạn làm thêm chức năng Quản lý Khách hàng 
        // (Tích điểm, Thêm, Sửa, Xóa), bạn có thể viết thêm các 
        // hàm ThemKhachHang, SuaKhachHang tại đây tương tự như DAL_QLSP.
        // =======================================================
    }
}