using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_DanhMuc
    {
        private int maDanhMuc = 0;
        private string tenDanhMuc;
        private string moTa;

        public ET_DanhMuc(int maDanhMuc, string tenDanhMuc, string moTa)
        {
            this.maDanhMuc = maDanhMuc;
            this.tenDanhMuc = tenDanhMuc;
            this.moTa = moTa;
        }
        public ET_DanhMuc(string tenDanhMuc, string moTa)
        {
            this.tenDanhMuc = tenDanhMuc;
            this.moTa = moTa;
        }
        public ET_DanhMuc()
        {
            this.tenDanhMuc = "";
            this.moTa = "";
        }

        public string TenDanhMuc { get => tenDanhMuc; set => tenDanhMuc = value; }
        public string MoTa { get => moTa; set => moTa = value; }
        public int MaDanhMuc { get => maDanhMuc; set => maDanhMuc = value; }
    }
}
