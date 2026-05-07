using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        /// <summary>
        /// Xử lí thêm khách hàng khi đăng ký thành viên ở siêu thị
        /// </summary>
        /// <return>Trả về true nếu thêm thông tin khách hàng mới thành công.</return>
        public bool ThemKhachHang(ET_KhachHang khachHang)
        {
            try
            {
                string strSQL = "sp_KhachHang_Insert";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@HoTen", khachHang.HoTen),
                    new SqlParameter("@SoDienThoai", khachHang.SDT),
                    new SqlParameter("@Email", khachHang.EMail),
                    new SqlParameter("@DiaChi", khachHang.DiaChi)
                };

                return db.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không thêm khách hàng mới vào Database: "+ ex.Message);
            }
        }

        /// <summary>
        /// Xử lý sửa thông tin khách hàng
        /// </summary>
        /// <return>Trả về True nếu sửa thông tin khách hàng thành công.</return>
        public bool SuaKhachHang(ET_KhachHang khachHang)
        {
            try
            {
                string strSQL = "sp_KhachHang_Update";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhachHang", khachHang.MaKH),
                    new SqlParameter("@HoTen", khachHang.HoTen),
                    new SqlParameter("@SoDienThoai", khachHang.SDT),
                    new SqlParameter("@Email", khachHang.EMail),
                    new SqlParameter("@DiaChi", khachHang.DiaChi)
                };

                return db.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không thể sửa thông tin khách hàng trong Database: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý xóa khách hàng khỏi hệ thống
        /// <see langword="string"/> MaKhachHang là khóa chính, nên sẽ xóa toàn bộ thông tin liên quan đến khách hàng này trong hệ thống (ví dụ: hóa đơn, điểm tích lũy,...)
        /// </summary>
        /// <param name="maKhachHang">Mã khách hàng cần xóa</param>
        /// <returns>Trả về True nếu xóa khách hàng thành công.</returns>
        public bool XoaKhachHang(int maKhachHang)
        {
            try
            {
                string strSQL = "sp_KhachHang_Delete";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhachHang", maKhachHang)
                };

                return db.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không thể xóa khách hàng trong Database: " + ex.Message);
            }
        }
    }
}