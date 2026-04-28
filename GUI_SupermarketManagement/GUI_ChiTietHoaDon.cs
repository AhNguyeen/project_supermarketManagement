using BUS_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_SupermarketManagement
{
    public partial class GUI_ChiTietHoaDon : Form
    {
        private int _maHoaDon;

        public GUI_ChiTietHoaDon(int maHD)
        {
            InitializeComponent();
            _maHoaDon = maHD; // Nhận mã từ form bên kia và cất vào biến toàn cục
        }
        BUS_ChiTietHoaDon bus_ChiTietHoaDon = new BUS_ChiTietHoaDon();
        BUS_HoaDon bus_HoaDon = new BUS_HoaDon();

        private void GUI_ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void LoadHoaDon()
        {
            //Load các thông tin của hóa đơn dựa vào _maHoaDon
            lblMaHD.Text = _maHoaDon.ToString();

            // 1. Gọi BUS lấy dữ liệu
            DataTable dt = bus_HoaDon.LayDanhSachTheoHoaDon(_maHoaDon.ToString());

            // 2.Kiểm tra 'PHẢI KHÁC NULL' và 'PHẢI CÓ DÒNG' mới làm tiếp
            if (dt != null && dt.Rows.Count > 0)
            {
                // Sử dụng DataRow cho gọn
                DataRow dr = dt.Rows[0];

                lblMaHD.Text = dr["MaHoaDon"].ToString();
                // Kiểm tra cột NgayLap có tồn tại trong SELECT của bạn không nhé
                lblNgay.Text = Convert.ToDateTime(dr["NgayLap"]).ToString("dd/MM/yyyy HH:mm:ss");
                lblTenKH.Text = dr["TenKhachHang"].ToString();
                lblTenNV.Text = dr["TenNhanVien"].ToString();
                lblSoDT.Text = dr["SoDienThoai"].ToString();
                lblThanhTien.Text = string.Format("{0:N0} VNĐ", dr["TongTien"]);
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu cho mã hóa đơn này!");
            }


            // Bây giờ bạn đã có _maHoaDon, hãy dùng nó để tìm kiếm
            ET_ChiTietHoaDon chiTietHoaDon = new ET_ChiTietHoaDon();
            chiTietHoaDon.MaHD = _maHoaDon;

            dgvChiTiet.DataSource = bus_ChiTietHoaDon.LayDachSachHoaDon(chiTietHoaDon);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
