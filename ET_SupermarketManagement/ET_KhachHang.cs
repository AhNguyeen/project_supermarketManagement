using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_KhachHang
    {
        private int maKH;
        private string hoTen;
        private string sDT;
        private string eMail;
        private string diaChi;

        public ET_KhachHang(int maKH, string hoTen, string sDT, string eMail, string diaChi)
        {
            this.maKH = maKH;
            this.hoTen = hoTen;
            this.sDT = sDT;
            this.eMail = eMail;
            this.diaChi = diaChi;
        }

        public ET_KhachHang()
        {
        }

        public int MaKH { get => maKH; set => maKH = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string EMail { get => eMail; set => eMail = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
