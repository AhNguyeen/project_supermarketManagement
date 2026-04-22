using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_SupermarketManagement
{
    public class ET_TaiKhoan
    {
        private int maTK;
        private int maNhanVien;
        private string userName;
        private string passWord;
        private string phanQuyen;

        public ET_TaiKhoan(int maTK, int maNhanVien, string userName, string passWord, string phanQuyen)
        {
            this.maTK = maTK;
            this.maNhanVien = maNhanVien;
            this.userName = userName;
            this.passWord = passWord;
            this.phanQuyen = phanQuyen;
        }
        public ET_TaiKhoan()
        {
        }

        public int MaTK { get => maTK; set => maTK = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string PhanQuyen { get => phanQuyen; set => phanQuyen = value; }
    }
}
