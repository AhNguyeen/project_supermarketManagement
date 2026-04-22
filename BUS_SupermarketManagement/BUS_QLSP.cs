using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_SupermarketManagement;
using ET_SupermarketManagement;

namespace BUS_SupermarketManagement
{
    public class BUS_QLSP
    {
        // Khởi tạo đối tượng tầng DAL để giao tiếp với Cơ sở dữ liệu
        DAL_QLSP dal = new DAL_QLSP();

        /// <summary>
        /// Lấy danh sách toàn bộ sản phẩm. Nếu có truyền tham số 'ten' thì đóng vai trò tìm kiếm.
        /// </summary>
        /// <param name="ten">Tên sản phẩm cần tìm (Mặc định là null)</param>
        /// <returns>DataTable chứa thông tin sản phẩm</returns>
        public DataTable LayDS(string ten = null)
        {
            return dal.LayDanhSach(ten);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Thêm một sản phẩm mới vào hệ thống
        /// </summary>
        public bool Them(ET_QLSP sp)
        {
            // QUY TẮC NGHIỆP VỤ KINH DOANH: Chống bán lỗ! Giá bán phải lớn hơn hoặc bằng giá nhập.
            // Nếu vi phạm, lập tức trả về false chặn lại, không cho phép lưu xuống Database.
            if (sp.GiaBan < sp.GiaNhap)
            {
                return false;
            }

            return dal.ThemSanPham(sp);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Cập nhật thông tin sản phẩm
        /// </summary>
        public bool Sua(ET_QLSP sp)
        {
            // ĐÃ BỔ SUNG LỖ HỔNG LOGIC: Bắt buộc phải áp dụng quy tắc chống bán lỗ cả khi người dùng Sửa giá!
            if (sp.GiaBan < sp.GiaNhap)
            {
                return false;
            }

            return dal.SuaSanPham(sp);
        }

        /// <summary>
        /// Xử lý nghiệp vụ: Xóa sản phẩm ra khỏi hệ thống
        /// (Dùng cú pháp expression-bodied => cực kỳ hiện đại và Clean Code)
        /// </summary>
        public bool Xoa(int ma) => dal.XoaSanPham(ma);
    }
}