using DAL_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_NhanVien
    {
        // Khởi tạo đối tượng tầng DAL để giao tiếp với Cơ sở dữ liệu
        DAL_NhanVien nhanVien = new DAL_NhanVien();

        /// <summary>
        /// Xử lý nghiệp vụ: Lấy danh sách các nhân viên đảm nhận chức vụ "Nhân viên kho"
        /// (Dùng để đổ dữ liệu vào ComboBox phân công người phụ trách khi nhập hàng vào kho)
        /// </summary>
        /// <returns>DataTable chứa thông tin các nhân viên kho</returns>
        public DataTable InTenNhanVienKho()
        {
            return nhanVien.InTenNhanVienKho();
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Lấy danh sách toàn bộ nhân viên đang làm việc tại siêu thị
        /// (Dùng để đổ dữ liệu vào ComboBox chọn nhân viên thu ngân khi lập hóa đơn)
        /// </summary>
        /// <returns>DataTable chứa thông tin tất cả nhân viên</returns>
        public DataTable LayTatCaNhanVien()
        {
            return nhanVien.LayTatCaNhanVien();
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Lấy danh sách toàn bộ nhân viên trong siêu thị
        /// (Dùng để đổ dữ liệu vào DataGridView quản lý)
        /// </summary>
        /// <returns>DataTable chứa thông tin tất cả nhân viên</returns>
        public DataTable LayDanhSachNhanVien()
        {
            return nhanVien.LayDanhSachNhanVien();
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Thêm mới một nhân viên vào hệ thống
        /// </summary>
        /// <param name="et_NhanVien"></param>
        /// <returns></returns>
        public bool ThemNhanVien(ET_NhanVien et_NhanVien)
        {
            return nhanVien.ThemNhanVien(et_NhanVien);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Sửa thông tin của một nhân viên đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="et_NhanVien"></param>
        /// <returns></returns>
        public bool SuaNhanVien(ET_NhanVien et_NhanVien)
        {
            return nhanVien.SuaNhanVien(et_NhanVien);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Xóa một nhân viên khỏi hệ thống khi họ nghỉ việc
        /// </summary>
        /// <param name="maNhanVien">Mã nhân viên cần xóa</param>
        /// <returns>True nếu xóa thành công, False nếu có lỗi</returns>
        public bool XoaNhanVien(int maNhanVien)
        {
            return nhanVien.XoaNhanVien(maNhanVien);
        }
    }
}