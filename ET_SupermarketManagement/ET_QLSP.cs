using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_QLSP
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string ChiTiet { get; set; }
        public string HinhAnh { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaNCC { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
     

        public ET_QLSP() { }

        public ET_QLSP(int ma, string ten, string chiTiet, string hinh, int maDM, int maNCC, decimal giaN, decimal giaB, int ton)
        {
            this.MaSanPham = ma;
            this.TenSanPham = ten;
            this.ChiTiet = chiTiet;
            this.HinhAnh = hinh;
            this.MaDanhMuc = maDM;
            this.MaNCC = maNCC;
            this.GiaNhap = giaN;
            this.GiaBan = giaB;
            this.SoLuongTon = ton;
        }
    }
}
