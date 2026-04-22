using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_ThongKe
    {
        private int tongDoanhThu;
        private int loiNhuan;
        private int soDonHang;
        private int sanPhamTonKho;
        private string tenSanPhamBanChay;

        public ET_ThongKe(int tongDoanhThu, int loiNhuan, int soDonHang, int sanPhamTonKho, string tenSanPhamBanChay)
        {
            this.tongDoanhThu = tongDoanhThu;
            this.loiNhuan = loiNhuan;
            this.soDonHang = soDonHang;
            this.sanPhamTonKho = sanPhamTonKho;
            this.tenSanPhamBanChay = tenSanPhamBanChay;
        }
        public ET_ThongKe()
        {
            this.tongDoanhThu = 0;
            this.loiNhuan = 0;
            this.soDonHang = 0;
            this.sanPhamTonKho = 0;
            this.tenSanPhamBanChay = "";
        }

        public int TongDoanhThu { get => tongDoanhThu; set => tongDoanhThu = value; }
        public int LoiNhuan { get => loiNhuan; set => loiNhuan = value; }
        public int SoDonHang { get => soDonHang; set => soDonHang = value; }
        public int SanPhamTonKho { get => sanPhamTonKho; set => sanPhamTonKho = value; }
        public string TenSanPhamBanChay { get => tenSanPhamBanChay; set => tenSanPhamBanChay = value; }
    }
}
