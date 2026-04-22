using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using DAL_SupermarketManagement;
using ET_SupermarketManagement;

namespace BUS_SupermarketManagement
{
    public class BUS_DanhMuc
    {
        DAL_DanhMuc dal_DanhMuc = new DAL_DanhMuc();
        public DataTable LayDanhSach()
        {
            return dal_DanhMuc.LayDanhSach();
        }
        public bool ThemDanhMuc(ET_DanhMuc et_DanhMuc)
        {
            return dal_DanhMuc.ThemDanhMuc(et_DanhMuc);
        }
        public bool XoaDanhMuc(string maDanhMuc)
        {
            return dal_DanhMuc.XoaDanhMuc(maDanhMuc);
        }
        public bool SuaDanhMuc(ET_DanhMuc et_DanhMuc)
        {
            return dal_DanhMuc.SuaDanhMuc(et_DanhMuc);
        }
    }
}
