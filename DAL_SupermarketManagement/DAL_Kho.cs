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
    public class DAL_Kho
    {
        // Tạo cổng kết nối chung tới Database
        Connect.ConnectData connect = new Connect.ConnectData();

        /// <summary>
        /// Lấy toàn bộ danh sách các phiếu nhập Kho
        /// </summary>
        /// <returns>DataTable chứa danh sách Kho</returns>
        public DataTable InDanhSachKho()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_Kho_Search";
                dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Thêm một phiếu nhập Kho mới vào cơ sở dữ liệu
        /// </summary>
        public bool ThemNhaKho(ET_Kho kho)
        {
            try
            {
                string strSQL = "sp_Kho_Insert";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    // Tên kho và các ID bắt buộc
                    new SqlParameter("@TenKho", kho.TenKho),
                    new SqlParameter("@MaNhanVien", kho.MaNV),
                    new SqlParameter("@MaNCC", kho.MaNCC),
                    new SqlParameter("@NgayNhap", kho.NgayNhap),
                    
                    // Xử lý an toàn: Ghi chú có thể bị rỗng (null) nên cần đẩy DBNull.Value xuống SQL
                    new SqlParameter("@GhiChu", (object)kho.GhiChu ?? DBNull.Value)
                };
                return connect.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem phiếu nhập kho này đã tồn tại trước đó chưa (Chống nhập đúp dữ liệu)
        /// </summary>
        /// <returns>True: Đã bị trùng (Không cho thêm). False: Chưa bị trùng (Được thêm)</returns>
        public bool KiemTraTrungThongTinKho(string tenKho, int maNhanVien, int maNCC, DateTime ngayNhap)
        {
            int soLuong = 0;
            try
            {
                // Tìm xem có phiếu kho nào có thông tin y hệt không
                string strSQL = "sp_KiemTraTrungKho";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@TenKho", tenKho),
                    new SqlParameter("@MaNV", maNhanVien),
                    new SqlParameter("@MaNCC", maNCC),
                    new SqlParameter("@NgayNhap", ngayNhap)
                };

                object result = connect.ResultData(strSQL, sqlParameters);

                // Xử lý an toàn tránh lỗi Null Reference
                if (result != null && result != DBNull.Value)
                {
                    soLuong = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return true; // Nếu có lỗi truy vấn thì cứ báo trùng để chặn việc Thêm dữ liệu bị sai
            }

            // Nếu count > 0 nghĩa là đã có phiếu kho trùng khớp trong DB, trả về true
            return soLuong > 0;
        }

        /// <summary>
        /// Sửa thông tin của phiếu nhập Kho
        /// </summary>
        public bool SuaNhaKho(ET_Kho kho)
        {
            try
            {
                string strSQL = "sp_Kho_Update";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    // Cần thiết truyền Mã Kho để SQL biết đang sửa dòng nào
                    new SqlParameter("@MaKho", kho.MaKho),
                    new SqlParameter("@TenKho", kho.TenKho),
                    new SqlParameter("@MaNhanVien", kho.MaNV),
                    new SqlParameter("@MaNCC", kho.MaNCC),
                    new SqlParameter("@NgayNhap", kho.NgayNhap),
                    new SqlParameter("@GhiChu", (object)kho.GhiChu ?? DBNull.Value) // Xử lý an toàn
                };
                return connect.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa một phiếu nhập Kho ra khỏi hệ thống
        /// </summary>
        public bool XoaNhaKho(string maKho)
        {
            try
            {
                string strSQL = "sp_Kho_Delete";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKho", maKho)
                };
                return connect.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}