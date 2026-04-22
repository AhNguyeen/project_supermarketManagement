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
    public class BUS_NhaCungCap
    {
        // Khởi tạo đối tượng tầng DAL để giao tiếp với Cơ sở dữ liệu
        DAL_NhaCungCap dal_NhaCungCap = new DAL_NhaCungCap();

        /// <summary>
        /// Xử lý nghiệp vụ: Lấy toàn bộ danh sách nhà cung cấp đang hợp tác
        /// </summary>
        /// <returns>DataTable chứa thông tin nhà cung cấp</returns>
        public DataTable LayDanhSach()
        {
            return dal_NhaCungCap.layDanhSachNhaCungCap();
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Thêm một nhà cung cấp mới vào hệ thống
        /// </summary>
        /// <param name="et_NhaCungCap">Thực thể chứa thông tin nhà cung cấp cần thêm</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại</returns>
        public bool ThemNhaCungCap(ET_NhaCungCap et_NhaCungCap)
        {
            return dal_NhaCungCap.ThemNhaCungCap(et_NhaCungCap);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Xóa nhà cung cấp khỏi hệ thống.
        /// (Lưu ý: Logic kiểm tra nhà cung cấp có đang cung cấp sản phẩm hay không đã được bọc an toàn ở tầng DAL)
        /// </summary>
        /// <param name="maNCC">Mã ID của nhà cung cấp cần xóa</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại (hoặc do đang vướng khóa ngoại)</returns>
        public bool XoaNhaCungCap(int maNCC)
        {
            return dal_NhaCungCap.XoaNhaCungCap(maNCC);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Cập nhật (Sửa) thông tin nhà cung cấp
        /// </summary>
        /// <param name="et_NhaCungCap">Thực thể chứa thông tin mới cần cập nhật (Bắt buộc phải có Mã NCC)</param>
        /// <returns>True nếu sửa thành công, False nếu thất bại</returns>
        public bool SuaNhaCungCap(ET_NhaCungCap et_NhaCungCap)
        {
            return dal_NhaCungCap.SuaNhaCungCap(et_NhaCungCap);
        }
    }
}