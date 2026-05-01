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
    public class BUS_ThongKe
    {
        // Khởi tạo đối tượng tầng DAL để lấy dữ liệu từ Database
        DAL_ThongKe thongKe_DAL = new DAL_ThongKe();

        /// <summary>
        /// Lấy tổng doanh thu của siêu thị (Từ các hóa đơn đã hoàn thành)
        /// </summary>
        public int TongDoanhThu()
        {
            return thongKe_DAL.TinhTongDoanhThu();
        }

        /// <summary>
        /// Lấy tổng lợi nhuận của siêu thị (Doanh thu - Vốn nhập hàng)
        /// </summary>
        public int LoiNhuan()
        {
            return thongKe_DAL.LoiNhuan();
        }

        /// <summary>
        /// Lấy tổng số lượng đơn hàng (Hóa đơn) đã bán ra
        /// </summary>
        public int SoDonHang()
        {
            return thongKe_DAL.SoDonHang();
        }

        /// <summary>
        /// Lấy tổng số lượng tất cả các sản phẩm đang còn tồn trong kho
        /// </summary>
        public int SanPhamTonKho()
        {
            return thongKe_DAL.SanPhamTonKho();
        }

        /// <summary>
        /// Lấy dữ liệu doanh thu theo từng ngày để vẽ Biểu đồ (Chart) trên Dashboard
        /// </summary>
        /// <returns>DataTable chứa [NgayLap] và [DoanhThu]</returns>
        public DataTable VeBieuDoDoanhThu()
        {
            return thongKe_DAL.LayDoanhThuTheoNgay();
        }

        /// <summary>
        /// Lấy ra 1 sản phẩm có số lượng bán ra cao nhất (Top 1 Best Seller)
        /// </summary>
        public DataTable TopSanPhamBanChay()
        {
            return thongKe_DAL.TopMotSanPhamBanChay();
        }

        /// <summary>
        /// Lấy danh sách Top các sản phẩm bán chạy nhất để hiển thị lên bảng Xếp hạng
        /// </summary>
        public DataTable LaySanPhamBanChay()
        {
            return thongKe_DAL.LayNamSanPhamBanChayNhat();
        }

        /// <summary>
        /// Lấy danh sách thống kê tồn kho của các sản phẩm (Phục vụ cho tính năng Cảnh báo hết hàng)
        /// </summary>
        public DataTable ThongKeTonKho()
        {
            return thongKe_DAL.ThongKeTonKho();
        }

        public DataTable LayDoanhThuTheoDanhMuc()
        {
            return thongKe_DAL.LayDoanhThuTheoDanhMuc();
        }
    }
}