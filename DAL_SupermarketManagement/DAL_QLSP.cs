using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET_SupermarketManagement;

namespace DAL_SupermarketManagement
{
    public class DAL_QLSP
    {
        // Tạo cổng kết nối chung đến cơ sở dữ liệu
        Connect.ConnectData connect = new Connect.ConnectData();

        /// <summary>
        /// Lấy danh sách tất cả sản phẩm. 
        /// Nếu truyền tham số 'ten' vào thì sẽ hoạt động như hàm Tìm kiếm theo tên.
        /// </summary>
        /// <param name="ten">Tên sản phẩm cần tìm (Mặc định là null để lấy tất cả)</param>
        /// <returns>DataTable chứa danh sách sản phẩm</returns>
        public DataTable LayDanhSach(string ten = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_SanPham_Search";

                // Khởi tạo mảng tham số (Parameter) để truyền xuống SQL
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenSanPham", ten)
                };

                // Dùng ReadData để đọc dữ liệu trả về dạng bảng
                dt = connect.ReadData(strSQL, parameters);
            }
            catch (Exception)
            {
                // Nếu lỗi truy vấn sẽ trả về null để tầng BUS/GUI dễ dàng báo lỗi
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Thêm một sản phẩm mới vào cơ sở dữ liệu
        /// </summary>
        /// <param name="sp">Đối tượng thực thể Sản Phẩm chứa dữ liệu</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại</returns>
        public bool ThemSanPham(ET_QLSP sp)
        {
            try
            {
                string strSQL = "sp_SanPham_Insert";

                // Thu thập dữ liệu và map với các biến @ trong SQL
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@TenSanPham", sp.TenSanPham),
                    
                    // DBNull.Value: Dùng để đẩy giá trị rỗng (NULL) xuống SQL một cách an toàn nếu C# không có dữ liệu
                    new SqlParameter("@ChiTiet", (object)sp.ChiTiet ?? DBNull.Value),
                    new SqlParameter("@HinhAnh", (object)sp.HinhAnh ?? DBNull.Value),

                    new SqlParameter("@MaDanhMuc", sp.MaDanhMuc),
                    new SqlParameter("@MaNCC", sp.MaNCC),
                    new SqlParameter("@GiaNhap", sp.GiaNhap),
                    new SqlParameter("@GiaBan", sp.GiaBan)
                };

                // ChangeData dùng cho Insert/Update/Delete, trả về True/False
                return connect.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật (Sửa) thông tin của một sản phẩm đã có trong cơ sở dữ liệu
        /// </summary>
        /// <param name="sp">Đối tượng thực thể Sản Phẩm chứa dữ liệu cần sửa</param>
        /// <returns>True nếu sửa thành công, False nếu thất bại</returns>
        public bool SuaSanPham(ET_QLSP sp)
        {
            try
            {
                string strSQL = "sp_SanPham_Update";

                SqlParameter[] parameter = new SqlParameter[] {
                    // Cần thiết phải truyền MaSanPham để mệnh đề WHERE trong SQL biết đang sửa dòng nào
                    new SqlParameter("@MaSanPham", sp.MaSanPham),
                    new SqlParameter("@TenSanPham", sp.TenSanPham),
                    new SqlParameter("@ChiTiet", (object)sp.ChiTiet ?? DBNull.Value),
                    new SqlParameter("@HinhAnh", (object)sp.HinhAnh ?? DBNull.Value),
                    new SqlParameter("@MaDanhMuc", sp.MaDanhMuc),
                    new SqlParameter("@MaNCC", sp.MaNCC),
                    new SqlParameter("@GiaNhap", sp.GiaNhap),
                    new SqlParameter("@GiaBan", sp.GiaBan)
                };

                return connect.ChangeData(strSQL, parameter);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa vĩnh viễn một sản phẩm ra khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="maSP">Mã định danh của sản phẩm cần xóa</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại</returns>
        public bool XoaSanPham(int maSP)
        {
            try
            {
                // ĐÃ FIX LỖI Ở ĐÂY: Đổi "sp_SanPham_Update" thành "sp_SanPham_Delete"
                string strSQL = "sp_SanPham_Delete";

                SqlParameter[] parameter = new SqlParameter[] {
                    new SqlParameter("@MaSanPham", maSP)
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