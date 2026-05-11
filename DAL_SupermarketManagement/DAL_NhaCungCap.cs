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
    public class DAL_NhaCungCap
    {
        // Tạo cổng kết nối chung tới Database
        Connect.ConnectData connect = new Connect.ConnectData();

        /// <summary>
        /// Lấy toàn bộ danh sách các nhà cung cấp đang có trong CSDL
        /// </summary>
        /// <returns>DataTable chứa danh sách nhà cung cấp</returns>
        public DataTable layDanhSachNhaCungCap()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_NhaCungCap_Search";
                return dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                throw new Exception("Lỗi truy vấn dữ liệu nhà cung cấp!");
            }
        }

        /// <summary>
        /// Thêm một nhà cung cấp mới vào siêu thị
        /// </summary>
        public bool ThemNhaCungCap(ET_NhaCungCap et_NhaCungCap)
        {
            try
            {
                string strSQL = "sp_NhaCungCap_Insert";

                // Thu thập dữ liệu mới để thêm nhà cung cấp (Mã NCC tự tăng nên không cần truyền)
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@TenNCC", et_NhaCungCap.TenNCC),
                    new SqlParameter("@DiaChi", et_NhaCungCap.DiaChi),
                    new SqlParameter("@SoDienThoai", et_NhaCungCap.SoDT),
                    new SqlParameter("@Email", et_NhaCungCap.EMail)
                };

                return connect.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa nhà cung cấp khi ngừng hợp tác.
        /// Lưu ý: Chỉ xóa được nếu nhà cung cấp này KHÔNG có sản phẩm nào trong CSDL.
        /// </summary>
        public bool XoaNhaCungCap(int maNhaCungCap)
        {
            try
            {
                // Gọi hàm kiểm tra an toàn trước khi xóa
                if (KiemTraSanPhamCuaNhaCungCap(maNhaCungCap) == true)
                {
                    string strSQL = "sp_NhaCungCap_Delete";
                    SqlParameter[] parameter = new SqlParameter[] {
                        new SqlParameter("@MaNCC", maNhaCungCap)
                    };
                    return connect.ChangeData(strSQL, parameter);
                }
                else
                {
                    // Trả về false nếu nhà cung cấp đang có sản phẩm, không cho phép xóa
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm hỗ trợ: Kiểm tra xem nhà cung cấp này có đang cung cấp sản phẩm nào không
        /// (Giúp chống lỗi vi phạm khóa ngoại khi xóa)
        /// </summary>
        /// <returns>True: Được phép xóa (0 sản phẩm). False: Không được xóa (> 0 sản phẩm)</returns>
        private bool KiemTraSanPhamCuaNhaCungCap(int maNhaCungCap)
        {
            int tongSanPham = 0;
            try
            {
                string strSQL = "sp_KiemTraSanPhamCuaNhaCungCap";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaNCC", maNhaCungCap)
                };

                object result = connect.ResultData(strSQL, parameters);

                // Bổ sung kiểm tra DBNull.Value để code an toàn tuyệt đối
                if (result != null && result != DBNull.Value)
                {
                    tongSanPham = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return false; // Nếu có lỗi truy vấn thì cứ chặn việc xóa cho an toàn
            }

            // Logic: Nếu tổng sản phẩm lớn hơn 0 -> Trả về False (Chặn không cho xóa)
            if (tongSanPham > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Cập nhật (Sửa) thông tin của nhà cung cấp
        /// </summary>
        public bool SuaNhaCungCap(ET_NhaCungCap et_NhaCungCap)
        {
            try
            {
                string strSQL = "sp_NhaCungCap_Update";

                SqlParameter[] parameter = new SqlParameter[] {
                    // ĐÃ FIX LỖI: Bổ sung Mã NCC để SQL biết cần Update dòng nào
                    new SqlParameter("@MaNCC", et_NhaCungCap.MaNCC),

                    new SqlParameter("@TenNCC", et_NhaCungCap.TenNCC),
                    new SqlParameter("@DiaChi", et_NhaCungCap.DiaChi),
                    new SqlParameter("@SoDienThoai", et_NhaCungCap.SoDT),
                    new SqlParameter("@Email", et_NhaCungCap.EMail)
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