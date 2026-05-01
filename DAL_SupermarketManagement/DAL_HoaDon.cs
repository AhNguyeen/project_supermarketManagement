using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_SupermarketManagement
{
    public class DAL_HoaDon
    {
        // Khởi tạo đối tượng kết nối chung tới Database
        Connect.ConnectData db = new Connect.ConnectData();

        /// <summary>
        /// Lấy toàn bộ danh sách Hóa đơn từ cơ sở dữ liệu
        /// </summary>
        /// <returns>DataTable chứa danh sách Hóa đơn</returns>
        public DataTable LayDanhSachHoaDon()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_HoaDon_Search";

                dt = db.ReadData(strSQL);
            }
            catch (Exception)
            {
                // Trả về null nếu có lỗi để tầng trên dễ dàng xử lý
                return null;
            }
            return dt;
        }

        public DataTable LayDanhSachHoaDonTheoHoaDon(string maHD = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_HoaDon_Search";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaHoaDon", maHD)
                };
                dt = db.ReadData(strSQL, parameters);
            }
            catch (Exception)
            {
                // Trả về null nếu có lỗi để tầng trên dễ dàng xử lý
                return null;
            }
            return dt;
        }

        /// <summary>
        /// Lập (Thêm) một hóa đơn mới vào cơ sở dữ liệu
        /// </summary>
        public bool ThemHoaDon(ET_HoaDon hoaDon)
        {
            try
            {
                string strSQL = "sp_HoaDon_Insert";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhachHang", hoaDon.MaKH),
                    new SqlParameter("@MaNhanVien", hoaDon.MaNV),
                    new SqlParameter("@NgayLap", hoaDon.NgayLap),
                    new SqlParameter("@TongTien", hoaDon.TongTien),
                    
                    // Xử lý an toàn: Nếu trạng thái bị bỏ trống (null) thì đẩy DBNull xuống SQL
                    new SqlParameter("@TrangThai", (object)hoaDon.TrangThai ?? DBNull.Value)
                };
                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem hóa đơn này đã tồn tại chưa để chống thu ngân bấm đúp tạo ra 2 bill giống nhau
        /// </summary>
        /// <returns>True: Đã bị trùng (Chặn lại). False: Chưa bị trùng (Cho phép tạo)</returns>
        public bool KiemTraTrungThongTinHoaDon(int maKhachHang, int maNhanVien, DateTime ngayLap)
        {
            int soLuong = 0;
            try
            {
                // Tìm xem có hóa đơn nào có thông tin y hệt cùng lúc không
                string strSQL = "sp_KiemTraTrungHoaDon";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhachHang", maKhachHang),
                    new SqlParameter("@MaNhanVien", maNhanVien),
                    new SqlParameter("@NgayLap", ngayLap)
                };

                object result = db.ResultData(strSQL, sqlParameters);

                // LỚP BẢO VỆ: Chống lỗi văng app nếu kết quả trả về là DBNull
                if (result != null && result != DBNull.Value)
                {
                    soLuong = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                return true; // Nếu truy vấn lỗi, trả về true để chặn tạo hóa đơn sai lệch
            }

            // Nếu soLuong > 0 nghĩa là đã bị trùng, trả về true
            return soLuong > 0;
        }

        /// <summary>
        /// Cập nhật thông tin Hóa đơn (Thường dùng để cập nhật Tổng tiền sau khi mua hàng hoặc đổi Trạng thái sang "Đã hủy")
        /// </summary>
        public bool SuaHoaDon(ET_HoaDon hoaDon)
        {
            try
            {
                string strSQL = "sp_HoaDon_Update";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    // Cần thiết phải truyền Mã HD để SQL biết đang cập nhật hóa đơn nào
                    new SqlParameter("@MaHoaDon", hoaDon.MaHD),
                    new SqlParameter("@MaKhachHang", hoaDon.MaKH),
                    new SqlParameter("@MaNhanVien", hoaDon.MaNV),
                    
                    // ĐÃ BỔ SUNG: Thêm NgayLap vì tầng GUI có truyền tham số này xuống
                    new SqlParameter("@NgayLap", hoaDon.NgayLap),

                    new SqlParameter("@TongTien", hoaDon.TongTien),
                    new SqlParameter("@TrangThai", (object)hoaDon.TrangThai ?? DBNull.Value)
                };
                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa hoàn toàn một hóa đơn ra khỏi hệ thống (Cần cẩn trọng khi dùng)
        /// </summary>
        public bool XoaHoaDon(int maHD)
        {
            try
            {
                string strSQL = "sp_HoaDon_Delete";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaHoaDon", maHD)
                };
                return db.ChangeData(strSQL, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHoaDon(int maHD, double tongTienMoi, List<Tuple<int, int, int>> danhSachSanPhamMoi)
        {
            db.OpenConnect();
            SqlConnection sqlConn = db.GetConnection();
            // Vẫn dùng Transaction để đảm bảo an toàn: Xóa cũ xong phải thêm mới thành công thì mới lưu
            SqlTransaction transaction = sqlConn.BeginTransaction();

            try
            {
                // 1. GỌI STORE: CẬP NHẬT TỔNG TIỀN VÀ XÓA CHI TIẾT CŨ
                SqlCommand cmdCapNhat = new SqlCommand("sp_CapNhatHoaDon", sqlConn, transaction);
                cmdCapNhat.CommandType = CommandType.StoredProcedure;
                cmdCapNhat.Parameters.AddWithValue("@MaHoaDon", maHD);
                cmdCapNhat.Parameters.AddWithValue("@TongTienMoi", tongTienMoi);
                cmdCapNhat.ExecuteNonQuery();

                // 2. GỌI LẠI STORE INSERT ĐỂ THÊM CHI TIẾT MỚI VÀO
                foreach (var sp in danhSachSanPhamMoi)
                {
                    // Tận dụng lại Store Proc Insert Chi Tiết mà bạn dùng bên màn hình Thanh Toán
                    SqlCommand cmdCT = new SqlCommand("sp_ChiTietHoaDon_Insert", sqlConn, transaction);
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    cmdCT.Parameters.AddWithValue("@MaHoaDon", maHD);
                    cmdCT.Parameters.AddWithValue("@MaSanPham", sp.Item1); 
                    cmdCT.Parameters.AddWithValue("@SoLuong", sp.Item2);
                    cmdCT.Parameters.AddWithValue("@DonGia", sp.Item3);
                    cmdCT.ExecuteNonQuery();
                }

                // 3. THÀNH CÔNG: LƯU TẤT CẢ
                transaction.Commit();
                db.CloseConnect();
                return true;
            }
            catch (Exception ex)
            {
                // THẤT BẠI: HOÀN TÁC TOÀN BỘ (Undo cả lệnh xóa và update)
                transaction.Rollback();
                db.CloseConnect();
                throw new Exception("Lỗi cập nhật hóa đơn: " + ex.Message);
            }
        }
    }
}