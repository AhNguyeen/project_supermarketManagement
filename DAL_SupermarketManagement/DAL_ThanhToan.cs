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

        // Bạn có thể tạo 1 class DTO_ChiTiet (Mã SP, Số Lượng, Đơn Giá) 
        // Hoặc đơn giản là truyền thẳng cái DataGridView.Rows xuống (hơi vi phạm mô hình 3 lớp nhưng chạy được)
        // Ở đây mình dùng List<Tuple> để truyền danh sách sản phẩm cho đúng chuẩn 3 lớp:
        // Tuple<int, int, int> = <Mã SP, Số Lượng, Đơn Giá>

        public bool ThanhToanToanBo(int maKH, int maNV, double tongTien, List<Tuple<int, int, int>> danhSachSanPham)
        {
            // Mở kết nối thủ công
            db.OpenConnect();
            SqlConnection sqlConn = db.GetConnection();
            SqlTransaction transaction = sqlConn.BeginTransaction();

            try
            {
                // 1. LƯU HÓA ĐƠN & LẤY MÃ
                SqlCommand cmdHD = new SqlCommand("sp_ThemHoaDonVALuuMaHoaDonVuaTao", sqlConn, transaction);
                cmdHD.CommandType = CommandType.StoredProcedure;
                cmdHD.Parameters.AddWithValue("@MaKhachHang", maKH);
                cmdHD.Parameters.AddWithValue("@MaNhanVien", maNV);
                cmdHD.Parameters.AddWithValue("@TongTien", tongTien);

                // ExecuteScalar lấy mã hóa đơn (Giống ResultData của bạn)
                object result = cmdHD.ExecuteScalar();
                int maHoaDonMoi = 0;
                if (result != null && result != DBNull.Value)
                {
                    maHoaDonMoi = Convert.ToInt32(result);
                }

                if (maHoaDonMoi <= 0)
                {
                    throw new Exception("Không thể lấy được Mã Hóa Đơn mới sinh ra!");
                }

                // 2. LƯU CHI TIẾT HÓA ĐƠN VÀO CÙNG TRANSACTION ĐÓ
                foreach (var sp in danhSachSanPham)
                {
                    SqlCommand cmdCT = new SqlCommand("sp_ChiTietHoaDon_Insert", sqlConn, transaction);
                    cmdCT.CommandType = CommandType.StoredProcedure;

                    cmdCT.Parameters.AddWithValue("@MaHoaDon", maHoaDonMoi);
                    cmdCT.Parameters.AddWithValue("@MaSanPham", sp.Item1); // Mã SP
                    cmdCT.Parameters.AddWithValue("@SoLuong", sp.Item2); // Số lượng
                    cmdCT.Parameters.AddWithValue("@DonGia", sp.Item3); // Đơn giá

                    cmdCT.ExecuteNonQuery();
                }

                // 3. NẾU MỌI THỨ TRÓT LỌT, MỚI COMMIT TOÀN BỘ VÀO DB
                transaction.Commit();
                db.CloseConnect();
                return true;
            }
            catch (SqlException ex)
            {
                // Nếu có bất kỳ lỗi nào (kể cả lỗi kho hàng từ Trigger), Rollback toàn bộ!
                transaction.Rollback();
                db.CloseConnect();

                // Bắn lỗi lên BLL/GUI để hiện MessageBox cho người dùng biết chính xác bị gì
                throw new Exception("Lỗi Database: " + ex.Message);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                db.CloseConnect();
                throw new Exception("Lỗi hệ thống: " + ex.Message);
            }
        }
    }
}
