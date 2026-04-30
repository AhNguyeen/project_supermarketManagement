using DAL_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_SupermarketManagement
{
    public class BUS_ThanhToan
    {
        DAL_ThanhToan dal_ThanhToan = new DAL_ThanhToan();

        public DataTable HienThiSanPham(string tenSP = null)
        {
            return dal_ThanhToan.HienThiDanhSachSanPham(tenSP);
        }
        // Nằm ở class BUS_ThanhToan
        public int ThanhToanToanBo(int maKH, int maNV, double tongTien, List<Tuple<int, int, int>> danhSachSanPham)
        {
            // dal_ThanhToan là đối tượng gọi từ tầng DAL
            return dal_ThanhToan.ThanhToanToanBo(maKH, maNV, tongTien, danhSachSanPham);
        }
    }
}
