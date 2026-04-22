using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_ThanhToan
    {
        Connect.ConnectData db = new Connect.ConnectData();

        public DataTable HienThiDanhSachSanPham(string tenSP = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_SanPham_Search";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@TenSanPham",tenSP)
                };
                dt = db.ReadData(strSQL,parameters);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public int ThanhToan(int maKH, int maNV, double thanhToan)
        {

            // 1. Chuẩn bị dữ liệu Hóa Đơn
            // Giả sử mã khách vãng lai trong DB của bạn là 1 (Nếu có nhập sđt thì tìm mã KH tương ứng)
            int maKhachHang = maKH;

            // Lấy mã nhân viên đang đăng nhập (Bạn cần lưu biến này khi Login thành công)
            int maNhanVien = maNV; // Ví dụ: 1

            // Lấy tổng tiền từ Label hoặc TextBox hiển thị tổng tiền trên form
            double tongTien = thanhToan;

            //Lấy mã hóa đơn khi thực hiện thanh toán
            int maHoaDonMoi = 0;
            try
            {
                // Sử dụng SqlTransaction để đảm bảo: Nếu lỗi ở ChiTietHoaDon thì sẽ hủy luôn HoaDon (Tránh rác DB)
                // ==========================================
                // BƯỚC 1: LƯU HÓA ĐƠN & LẤY MÃ HÓA ĐƠN VỪA TẠO
                // ==========================================
                string strSQL = "sp_ThemHoaDonVALuuMaHoaDonVuaTao";

                SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@MaKH", maKhachHang),
                new SqlParameter("@MaNV", maNhanVien),
                new SqlParameter("@TongTien", tongTien)
                };

                object result = db.ResultData(strSQL, parameters);

                // ExecuteScalar trả về cột đầu tiên của dòng đầu tiên (chính là cái ID vừa sinh ra)
                if (result != null && result != DBNull.Value)
                {
                    maHoaDonMoi = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return maHoaDonMoi;
        }

        public bool ThemSanPhamVaoHoaDon(int maHoaDon, int maSanPham, int soLuong, int donGia)
        {
            try
            {
                // ==========================================
                // BƯỚC 2: LƯU CHI TIẾT HÓA ĐƠN
                // ==========================================
                string strSQL = "sp_ChiTietHoaDon_Insert";

                SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@MaHD", maHoaDon),
                new SqlParameter("@MaSP", maSanPham),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@DonGia", donGia)
                };

                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception) { return false; }
        }
    }
}
