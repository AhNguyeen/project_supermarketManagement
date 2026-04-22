using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_NhanVien
    {
        private int maNV;
        private string tenNV;
        private DateTime ngaySinh;
        private string gioiTinh;
        private string soDT;
        private string eMail;
        private string diaChi;
        private string chucVu;
        private double luong;

        public ET_NhanVien(int maNV, string tenNV, DateTime ngaySinh, string gioiTinh, string soDT, string eMail, string diaChi, string chucVu, double luong)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.soDT = soDT;
            this.eMail = eMail;
            this.diaChi = diaChi;
            this.chucVu = chucVu;
            this.luong = luong;
        }

        public ET_NhanVien()
        {
        }

        public int MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string SoDT { get => soDT; set => soDT = value; }
        public string EMail { get => eMail; set => eMail = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public double Luong { get => luong; set => luong = value; }
    }
}
