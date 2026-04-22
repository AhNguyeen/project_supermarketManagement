using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using ET_SupermarketManagement;
using System.Globalization;

namespace DAL_SupermarketManagement
{
    public class DAL_ThongKe
    {
        // Cổng kết nối Database (gọi từ class ConnectData của bạn)
        Connect.ConnectData connect = new Connect.ConnectData();

        // Khởi tạo đối tượng thực thể Thống Kê
        ET_ThongKe thongKe_ET = new ET_ThongKe();

        /// <summary>
        /// Tính tổng doanh thu của siêu thị dựa trên các hóa đơn đã "Hoàn thành"
        /// </summary>
        /// <returns>Trả về Tổng tiền (int). Trả về -1 nếu lỗi.</returns>
        public int TinhTongDoanhThu()
        {
            int doanhThu = 0;
            try
            {
                string strSQL = "sp_TongDoanhThu";
                object result = connect.ResultData(strSQL); // ResultData dùng cho truy vấn ExecuteScalar trả về 1 giá trị

                if (result != null && result != DBNull.Value)
                {
                    doanhThu = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                // Lỗi truy vấn sẽ trả về -1 để báo cho tầng GUI biết
                return -1;
            }
            // Lưu vào ET và trả về kết quả
            return thongKe_ET.TongDoanhThu = doanhThu;
        }

        /// <summary>
        /// Tính tổng lợi nhuận của siêu thị (Tổng Doanh thu - Tổng Giá nhập)
        /// </summary>
        /// <returns>Trả về Lợi nhuận (int). Trả về -1 nếu lỗi.</returns>
        public int LoiNhuan()
        {
            int loiNhuan = 0;
            try
            {
                string strSQL = "sp_LoiNhuan";
                object result = connect.ResultData(strSQL);

                if (result != null && result != DBNull.Value)
                {
                    loiNhuan = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return thongKe_ET.LoiNhuan = loiNhuan;
        }

        /// <summary>
        /// Đếm tổng số lượng đơn hàng (Hóa đơn) bán ra thành công
        /// </summary>
        public int SoDonHang()
        {
            int donHang = 0;
            try
            {
                string strSQL = "sp_DemSoDonHang";
                object result = connect.ResultData(strSQL);

                if (result != null && result != DBNull.Value)
                {
                    donHang = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return thongKe_ET.SoDonHang = donHang;
        }

        /// <summary>
        /// Đếm tổng số lượng tất cả các sản phẩm đang còn tồn trong kho
        /// </summary>
        public int SanPhamTonKho()
        {
            int tonKho = 0;
            try
            {
                string strSQL = "sp_DemTongTonKho";
                object result = connect.ResultData(strSQL);

                if (result != null && result != DBNull.Value)
                {
                    tonKho = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return thongKe_ET.SanPhamTonKho = tonKho;
        }

        /// <summary>
        /// Lấy dữ liệu Doanh thu biến động theo từng ngày để vẽ Biểu đồ (Chart)
        /// </summary>
        /// <returns>DataTable chứa 2 cột: Ngay và DoanhThu</returns>
        public DataTable LayDoanhThuTheoNgay()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_ThongKeDoanhThuTheoNgay";
                dt = connect.ReadData(strSQL); // ReadData dùng SqlDataAdapter để fill dữ liệu vào DataTable
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Tìm ra 1 sản phẩm bán chạy nhất trong siêu thị (Top 1)
        /// </summary>
        public DataTable TopMotSanPhamBanChay()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_Top1SanPhamBanChay";
                dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách Top (n) sản phẩm bán chạy nhất (Để đổ vào DataGridView Xếp hạng)
        /// </summary>
        public DataTable LayNamSanPhamBanChayNhat()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi store procedure đã tạo: sp_TopSanPhamBanChay
                string strSQL = "sp_TopSanPhamBanChay";
                dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ sản phẩm và đánh giá trạng thái Tồn Kho (Sắp hết/Hết hàng)
        /// Để hiển thị lên Dashboard cảnh báo
        /// </summary>
        public DataTable ThongKeTonKho()
        {
            DataTable dt = new DataTable();
            try
            {
                // Gọi store procedure cảnh báo tồn kho: sp_ThongKeTonKho
                string strSQL = "sp_ThongKeTonKho";
                dt = connect.ReadData(strSQL);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
    }
}