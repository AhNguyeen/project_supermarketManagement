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
    public class DAL_ChiTietHoaDon
    {
        Connect.ConnectData db = new Connect.ConnectData();
        public DAL_ChiTietHoaDon()
        {

        }

        public DataTable LayDachSachHoaDon(ET_ChiTietHoaDon chiTietHoaDon = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "sp_HienThiChiTietHoaDon";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaHoaDon", chiTietHoaDon.MaHD)
                };

                dt = db.ReadData(strSQL, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hóa đơn", ex);
                dt = null;
            }
            return dt;
        }

        public bool CapNhatHoaDon(int maHD, double tongTienMoi, List<Tuple<int, int, int>> danhSachSanPhamMoi)
        {
            db.OpenConnect();
            SqlConnection sqlConn = db.GetConnection();
            SqlTransaction transaction = sqlConn.BeginTransaction();

            try
            {
                // 1. UPDATE lại Tổng Tiền trong bảng Hóa Đơn
                SqlCommand cmdHD = new SqlCommand("sp_CapNhatTongTienHoaDon", sqlConn, transaction);
                cmdHD.CommandType = CommandType.StoredProcedure;
                cmdHD.Parameters.AddWithValue("@TongTien", tongTienMoi);
                cmdHD.Parameters.AddWithValue("@MaHoaDon", maHD);
                cmdHD.ExecuteNonQuery();

                // 2. XÓA SẠCH Chi Tiết Hóa Đơn cũ của bill này
                SqlCommand cmdXoa = new SqlCommand("sp_XoaChiTietHoaDon", sqlConn, transaction);
                cmdXoa.CommandType = CommandType.StoredProcedure;
                cmdXoa.Parameters.AddWithValue("@MaHoaDon", maHD);
                cmdXoa.ExecuteNonQuery();

                // 3. THÊM LẠI danh sách sản phẩm mới từ lưới (y hệt hàm Thanh toán)
                foreach (var sp in danhSachSanPhamMoi)
                {
                    SqlCommand cmdCT = new SqlCommand("sp_ChiTietHoaDon_Insert", sqlConn, transaction);
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    cmdCT.Parameters.AddWithValue("@MaHoaDon", maHD);
                    cmdCT.Parameters.AddWithValue("@MaSanPham", sp.Item1);
                    cmdCT.Parameters.AddWithValue("@SoLuong", sp.Item2);
                    cmdCT.Parameters.AddWithValue("@DonGia", sp.Item3);
                    cmdCT.ExecuteNonQuery();
                }

                transaction.Commit();
                db.CloseConnect();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                db.CloseConnect();
                throw new Exception("Lỗi cập nhật hóa đơn: " + ex.Message);
            }
        }
    }
}
