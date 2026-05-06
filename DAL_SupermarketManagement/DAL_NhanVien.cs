using ET_SupermarketManagement;
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
    public class DAL_NhanVien
    {
        // Tạo cổng kết nối chung đến cơ sở dữ liệu
        Connect.ConnectData db = new Connect.ConnectData();

        /// <summary>
        /// Lấy danh sách các nhân viên có chức vụ là "Nhân viên kho"
        /// Dùng để đổ dữ liệu vào ComboBox phân công người phụ trách khi nhập kho.
        /// </summary>
        /// <returns>DataTable chứa danh sách nhân viên kho. Trả về null nếu có lỗi truy vấn.</returns>
        public DataTable InTenNhanVienKho()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi Store Procedure đã tạo sẵn trong SQL Server
                string strSQL = "sp_InTenNhanVienChucVuLamKho";
                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                // Bắt lỗi an toàn, trả về null để các tầng trên (BUS/GUI) tự xử lý thông báo
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên trong siêu thị
        /// Dùng để đổ dữ liệu vào DataGridView quản lý hoặc các ComboBox chọn nhân viên lập hóa đơn.
        /// </summary>
        /// <returns>DataTable chứa danh sách tất cả nhân viên. Trả về null nếu có lỗi truy vấn.</returns>
        public DataTable LayTatCaNhanVien()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi Store Procedure lấy toàn bộ nhân viên
                string strSQL = "sp_NhanVien_All";
                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên trong siêu thị
        /// Dùng để đổ dữ liệu vào DataGridView quản lý
        /// </summary>
        /// <returns>DataTable chứa danh sách tất cả nhân viên. Trả về null nếu có lỗi truy vấn.</returns>
        public DataTable LayDanhSachNhanVien()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi Store Procedure lấy toàn bộ nhân viên
                string strSQL = "sp_NhanVien_Search";
                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Thêm một nhân viên mới vào cơ sở dữ liệu dựa trên thông tin được cung cấp trong đối tượng ET_NhanVien.
        /// </summary>
        /// <param name="et_NhanVien"></param>
        /// <returns></returns>
        public bool ThemNhanVien(ET_NhanVien et_NhanVien)
        {
            try
            {
                string strSQL = "sp_NhanVien_Insert";

                // Thu thập đủ 8 trường dữ liệu để thêm nhân viên mới (Mã NV tự tăng nên bỏ qua)
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@HoTen", et_NhanVien.TenNV),
                    new SqlParameter("@NgaySinh", et_NhanVien.NgaySinh),
                    new SqlParameter("@GioiTinh", et_NhanVien.GioiTinh),
                    new SqlParameter("@SoDienThoai", et_NhanVien.SoDT),
                    new SqlParameter("@Email", et_NhanVien.EMail),
                    new SqlParameter("@DiaChi", et_NhanVien.DiaChi),
                    new SqlParameter("@MaChucVu", et_NhanVien.MaChucVu),
                    new SqlParameter("@Luong", et_NhanVien.Luong)
                };

                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                // Trả về false nếu có lỗi (ví dụ: mất kết nối DB, trùng Email do ràng buộc UNIQUE...)
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một nhân viên dựa trên đối tượng ET_NhanVien.
        /// </summary>
        /// <param name="et_NhanVien"></param>
        /// <returns></returns>
        public bool SuaNhanVien(ET_NhanVien et_NhanVien)
        {
            try
            {
                string strSQL = "sp_NhanVien_Update";
                // Thu thập đủ 9 trường dữ liệu để cập nhật nhân viên (Bao gồm Mã NV để xác định bản ghi cần sửa)
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaNhanVien", et_NhanVien.MaNV),
                    new SqlParameter("@HoTen", et_NhanVien.TenNV),
                    new SqlParameter("@NgaySinh", et_NhanVien.NgaySinh),
                    new SqlParameter("@GioiTinh", et_NhanVien.GioiTinh),
                    new SqlParameter("@SoDienThoai", et_NhanVien.SoDT),
                    new SqlParameter("@Email", et_NhanVien.EMail),
                    new SqlParameter("@DiaChi", et_NhanVien.DiaChi),
                    new SqlParameter("@MaChucVu", et_NhanVien.MaChucVu),
                    new SqlParameter("@Luong", et_NhanVien.Luong)
                };
                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa một nhân viên khỏi hệ thống dựa trên mã nhân viên. Lưu ý: Chỉ xóa được nếu nhân viên này không có giao dịch nào liên quan trong CSDL (ví dụ: hóa đơn, phiếu nhập kho...). Nếu có liên quan sẽ trả về false để tầng BUS/GUI báo lỗi cho người dùng.
        /// </summary>
        /// <param name="maNhanVien"></param>
        /// <returns></returns>
        public bool XoaNhanVien(int maNhanVien)
        {
            try
            {
                string strSQL = "sp_NhanVien_Delete";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaNhanVien", maNhanVien)
                };
                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}