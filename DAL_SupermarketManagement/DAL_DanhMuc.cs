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
    public class DAL_DanhMuc
    {
        // Tạo cổng kết nối chung tới Database
        Connect.ConnectData connect = new Connect.ConnectData();

        /// <summary>
        /// Lấy toàn bộ danh sách Danh mục sản phẩm
        /// </summary>
        /// <returns>DataTable chứa danh sách danh mục</returns>
        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_DanhMuc_Search";
                dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Thêm một danh mục mới vào siêu thị
        /// </summary>
        public bool ThemDanhMuc(ET_DanhMuc et_DanhMuc)
        {
            try
            {
                string strSQL = "sp_DanhMuc_Insert";

                // Thu thập dữ liệu để thêm danh mục mới
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@TenDanhMuc", et_DanhMuc.TenDanhMuc),
                    
                    // Xử lý an toàn: Mô tả có thể để trống
                    new SqlParameter("@MoTa", (object)et_DanhMuc.MoTa ?? DBNull.Value)
                };

                return connect.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa danh mục (Chỉ xóa được khi danh mục này đang trống, không có sản phẩm nào)
        /// </summary>
        public bool XoaDanhMuc(string maDanhMuc)
        {
            try
            {
                // Kiểm tra trước khi xóa để tránh lỗi vi phạm khóa ngoại
                if (KiemTraDanhMuc(maDanhMuc) == true)
                {
                    // ĐÃ FIX LỖI: Sửa tên Store Procedure cho đúng với Danh Mục
                    string strSQL = "sp_DanhMuc_Delete";

                    SqlParameter[] parameter = new SqlParameter[] {
                        new SqlParameter("@MaDanhMuc", maDanhMuc)
                    };

                    return connect.ChangeData(strSQL, parameter);
                }
                else
                {
                    // Trả về false nếu danh mục đang chứa sản phẩm, chặn không cho xóa
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem trong danh mục này có đang chứa sản phẩm nào không
        /// </summary>
        /// <returns>True: Được phép xóa (0 sản phẩm). False: Không được xóa (>0 sản phẩm)</returns>
        private bool KiemTraDanhMuc(string maDanhMuc)
        {
            int tongSanPham = 0;
            try
            {
                string strSQL = "sp_KiemTraSanPhamTrongDanhMuc";

                // ĐÃ FIX LỖI: Sửa tham số truyền xuống thành @MaDanhMuc
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaDanhMuc", maDanhMuc)
                };

                object result = connect.ResultData(strSQL, parameters);

                // LỚP BẢO VỆ: Tránh lỗi Null Reference nếu truy vấn trả về DBNull
                if (result != null && result != DBNull.Value)
                {
                    tongSanPham = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return false; // Nếu lỗi truy vấn, chặn xóa cho an toàn
            }

            if (tongSanPham > 0)
                return false; // Có sản phẩm trong danh mục -> Không cho xóa
            else
                return true;  // Không có sản phẩm -> Cho phép xóa
        }

        /// <summary>
        /// Cập nhật (Sửa) thông tin của danh mục
        /// </summary>
        public bool SuaDanhMuc(ET_DanhMuc et_DanhMuc)
        {
            try
            {
                string strSQL = "sp_DanhMuc_Update";

                // ĐÃ FIX LỖI CÚ PHÁP: Gộp đúng mảng Parameter lại
                SqlParameter[] parameter = new SqlParameter[] {
                    new SqlParameter("@MaDanhMuc", et_DanhMuc.MaDanhMuc),
                    new SqlParameter("@TenDanhMuc", et_DanhMuc.TenDanhMuc),
                    new SqlParameter("@MoTa", (object)et_DanhMuc.MoTa ?? DBNull.Value)
                };

                return connect.ChangeData(strSQL, parameter);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}