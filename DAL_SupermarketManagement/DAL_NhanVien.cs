using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    }
}