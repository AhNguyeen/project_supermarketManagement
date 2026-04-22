using DAL_SupermarketManagement;
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
    }
}