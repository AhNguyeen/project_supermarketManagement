using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_NhaCungCap
    {
        private int maNCC;
        private string tenNCC;
        private string diaChi;
        private string soDT;
        private string eMail;

        public ET_NhaCungCap(string tenNCC, string diaChi, string soDT, string eMail)
        {
            this.maNCC = 0;
            this.tenNCC = tenNCC;
            this.diaChi = diaChi;
            this.soDT = soDT;
            this.eMail = eMail;
        }
        public ET_NhaCungCap()
        {
        }

        public int MaNCC { get => maNCC; set => maNCC = value; }
        public string TenNCC { get => tenNCC; set => tenNCC = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDT { get => soDT; set => soDT = value; }
        public string EMail { get => eMail; set => eMail = value; }
    }
}
