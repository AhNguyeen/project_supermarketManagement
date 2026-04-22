using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_HoaDon
    {
        private int maHD;
        private int maKH;
        private int maNV;
        private DateTime ngayLap;
        private int tongTien;
        private string trangThai;

        public ET_HoaDon(int maHD, int maKH, int maNV, DateTime ngayLap, int tongTien, string trangThai)
        {
            this.maHD = maHD;
            this.maKH = maKH;
            this.maNV = maNV;
            this.ngayLap = ngayLap;
            this.tongTien = tongTien;
            this.trangThai = trangThai;
        }
        public ET_HoaDon()
        {
        }

        public int MaHD { get => maHD; set => maHD = value; }
        public int MaKH { get => maKH; set => maKH = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public DateTime NgayLap { get => ngayLap; set => ngayLap = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
    }
}
