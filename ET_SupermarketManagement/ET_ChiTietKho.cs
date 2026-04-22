using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_ChiTietKho
    {
        private int maChiTietKho;
        private int maKho;
        private int maSanPham;
        private int soLuong;
        private int giaNhap;
        private int thanhTien;


        public ET_ChiTietKho()
        {
        }

        public ET_ChiTietKho(int maChiTietKho, int maKho, int maSanPham, int soLuong, int giaNhap, int thanhTien)
        {
            this.maChiTietKho = maChiTietKho;
            this.maKho = maKho;
            this.maSanPham = maSanPham;
            this.soLuong = soLuong;
            this.giaNhap = giaNhap;
            this.thanhTien = thanhTien;
        }

        public int MaChiTietKho { get => maChiTietKho; set => maChiTietKho = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int GiaNhap { get => giaNhap; set => giaNhap = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
        public int MaKho { get => maKho; set => maKho = value; }
        public int MaSanPham { get => maSanPham; set => maSanPham = value; }
    }
}
