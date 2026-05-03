using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_Kho
    {
        private int maKho;
        private string tenKho;
        private int maNV;
        private int maNCC;
        private DateTime ngayNhap;
        private string ghiChu;

        public ET_Kho()
        {
        }

        public ET_Kho(string tenKho, int maNV, int maNCC, DateTime ngayNhap, string ghiChu)
        {
            this.tenKho = tenKho;
            this.maNV = maNV;
            this.maNCC = maNCC;
            this.ngayNhap = ngayNhap;
            this.ghiChu = ghiChu;
        }
        public ET_Kho(int maKho, string tenKho, int maNV, int maNCC, DateTime ngayNhap, string ghiChu)
        {
            this.maKho = maKho;
            this.tenKho = tenKho;
            this.maNV = maNV;
            this.maNCC = maNCC;
            this.ngayNhap = ngayNhap;
            this.ghiChu = ghiChu;
        }

        public int MaKho { get => maKho; set => maKho = value; }
        public string TenKho { get => tenKho; set => tenKho = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public int MaNCC { get => maNCC; set => maNCC = value; }
    }
}
