using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_ChiTietHoaDon
    {
        private int maCTHD;
        private int maHD;
        private int maSP;
        private int soLuong;
        private int donGia;
        private int thanhTien;

        public ET_ChiTietHoaDon(int maCTHD, int maHD, int maSP, int soLuong, int donGia, int thanhTien)
        {
            this.maCTHD = maCTHD;
            this.maHD = maHD;
            this.maSP = maSP;
            this.soLuong = soLuong;
            this.donGia = donGia;
            this.thanhTien = thanhTien;
        }
        public ET_ChiTietHoaDon()
        {

        }
        public ET_ChiTietHoaDon(int maHD)
        {
            this.maHD = maHD;
        }

        public int MaCTHD { get => maCTHD; set => maCTHD = value; }
        public int MaHD { get => maHD; set => maHD = value; }
        public int MaSP { get => maSP; set => maSP = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
    }
}
