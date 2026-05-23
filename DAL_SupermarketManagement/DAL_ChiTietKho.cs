using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        /// <summary>
        /// Thêm một chi tiết kho mới (Sản phẩm, số lượng, giá) vào một phiếu nhập kho cụ thể.
        /// </summary>
        /// <param name="maKho">Mã kho cần thêm chi tiết</param>
        /// <param name="maSanPham">Mã sản phẩm cần thêm</param>
        /// <param name="soLuong">Số lượng sản phẩm</param>
        /// <param name="gia">Giá sản phẩm</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại</returns>
        public bool ThemChiTietKho(string maKho, string maSanPham, int soLuong, decimal gia)
        {
            try
            {
                string strSQL = "sp_ChiTietKho_Insert";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@MaSanPham", maSanPham),
                    new SqlParameter("@SoLuongNhap", soLuong),
                    new SqlParameter("@GiaNhap", gia)
                };
                return db.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa toàn bộ chi tiết của một kho khi phiếu nhập kho đó bị xóa (Dùng để duy trì tính toàn vẹn dữ liệu)
        /// </summary>
        /// <param name="maKho">Mã kho cần xóa chi tiết</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại</returns>
        public bool XoaSanPhamKhoiChiTietKho(int maKho, int maSanPham)
        {
            try
            {
                string strSQL = "sp_ChiTietKho_DeleteSanPham";
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@MaKho", maKho),
                new SqlParameter("@MaSanPham", maSanPham)
                };

                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                // Có thể thêm log lỗi ở đây nếu cần
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin chi tiết kho (Số lượng, giá) của một sản phẩm trong một phiếu nhập kho cụ thể.
        /// </summary>
        /// <param name="maKho">Mã kho cần sửa chi tiết</param>
        /// <param name="maSanPham">Mã sản phẩm cần sửa chi tiết</param>
        /// <param name="soLuong">Số lượng sản phẩm mới</param>
        /// <param name="gia">Giá sản phẩm mới</param>
        /// <returns>True nếu sửa thành công, False nếu thất bại</returns>
        public bool SuaChiTietKho(int maChiTietKho, string maKho, string maSanPham, int soLuong, decimal gia)
        {
            try
            {
                string strSQL = "sp_ChiTietKho_Update";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaChiTietKho", maChiTietKho),
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@MaSanPham", maSanPham),
                    new SqlParameter("@SoLuongNhap", soLuong),
                    new SqlParameter("@GiaNhap", gia)
                };
                return db.ChangeData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem chi tiết kho này đã tồn tại trước đó chưa (Chống nhập đúp dữ liệu)
        /// </summary>
        /// <param name="maKho">Mã kho cần kiểm tra</param>
        /// <param name="maSanPham">Mã sản phẩm cần kiểm tra</param>    
        /// <param name="soLuong">Số lượng sản phẩm cần kiểm tra</param>
        /// <param name="gia">Giá sản phẩm cần kiểm tra</param>
        /// <returns>True: Đã bị trùng (Không cho thêm). False: Chưa bị trùng (Được thêm)</returns>
        public bool KiemTraTrungThongTinChiTietKho(string maKho, string maSanPham)
        {
            int soLuongBanGhi = 0;
            try
            {
                string strSQL = "sp_ChiTietKho_CheckDuplicate";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@MaSanPham", maSanPham)
                };
                soLuongBanGhi = (int)db.ResultData(strSQL, sqlParameters);
            }
            catch (Exception)
            {
                return false;
            }
            return soLuongBanGhi > 0; // Nếu có bản ghi nào trùng thì trả về true
        }
    }
}