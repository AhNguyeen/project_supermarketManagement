using BUS_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI_SupermarketManagement
{
    public partial class QLChiTiet : Form
    {
        BUS_QLSP busSP = new BUS_QLSP();
        ET_QLSP spHienTai = null;
        public QLChiTiet()
        {
            InitializeComponent();
            NapGoiYTimKiem();
        }
        public void LoadSanPham(ET_QLSP sp)
        {
            spHienTai = sp;
            // Thay label bằng txt (TextBox) để người dùng có thể xóa/sửa chữ
            lblTenSanPham.Text = sp.TenSanPham;
            lblMaSanPham.Text = sp.MaSanPham.ToString();
            lblGiaBan.Text = string.Format("{0:N0} VNĐ", sp.GiaBan);
            rtxtMoTa.Text = sp.ChiTiet;
            HienThiAnh(sp.HinhAnh);
            rtxtThongSoKiThuat.Clear();
            rtxtThongSoKiThuat.Text =
                $"Tên Nhà Cung Cấp: {sp.TenNCC}\n" +
                $"Tên Danh Mục: {sp.TenDanhMuc}\n" +
                $"Số Lượng Tồn Kho: {sp.SoLuongTon} sản phẩm\n" +
                $"Giá Nhập: {string.Format("{0:N0}", sp.GiaNhap)}đ\n" +
                $"----------------------------\n" +
                "Tình trạng: " + (sp.SoLuongTon > 0 ? "Còn hàng" : "Hết hàng");

            // Giữ nguyên phần load hình ảnh
            try
            {
                string path = Path.Combine(Application.StartupPath, "Images", sp.HinhAnh);
                if (File.Exists(path))
                {
                    picHinhAnh.Image = Image.FromFile(path);
                    picHinhAnh.Tag = path; // Lưu lại đường dẫn để sau này lưu
                }
            }
            catch { picHinhAnh.Image = null; }
        }
        private void NapGoiYTimKiem()
        {
            try
            {
                // 1. Tạo bộ sưu tập chứa các chuỗi gợi ý
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

                // 2. Gọi BUS lấy toàn bộ danh sách sản phẩm
                DataTable dt = busSP.LayDS();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Thêm tên sản phẩm vào bộ sưu tập
                        collection.Add(row["TenSanPham"].ToString());
                    }
                }

                // 3. Gán bộ sưu tập vào TextBox tìm kiếm
                txtTimKiem.AutoCompleteCustomSource = collection;
            }
            catch (Exception ex)
            {
                // Tránh làm dừng chương trình nếu lỗi nạp gợi ý
                Console.WriteLine("Lỗi nạp gợi ý: " + ex.Message);
            }
        }

        //Nút đóng form chi tiết sản phẩm để quay lại giao diện quản lý sản phẩm
        private void btntrolai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        //Nút tìm kiếm chi tiết sản phẩm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa)) return;

            // Gọi BUS để tìm kiếm (Procedure sp_SanPham_Search)
            DataTable dt = busSP.LayDS(tuKhoa);
            if (dt.Rows.Count > 0)
            {
                // Nếu tìm thấy, cập nhật dữ liệu lên giao diện
                DataRow row = dt.Rows[0];
                ET_QLSP spTimThay = new ET_QLSP
                {
                    MaSanPham = int.Parse(row["MaSanPham"].ToString()),
                    TenSanPham = row["TenSanPham"].ToString(),
                    GiaBan = decimal.Parse(row["GiaBan"].ToString()),
                    ChiTiet = row["ChiTiet"].ToString(),
                    HinhAnh = row["HinhAnh"].ToString(),
                    TenDanhMuc = row["TenDanhMuc"].ToString(),
                    TenNCC = row["TenNCC"].ToString(),
                    GiaNhap = decimal.Parse(row["GiaNhap"].ToString()),
                    SoLuongTon = int.Parse(row["SoLuongTon"].ToString())
                };
                LoadSanPham(spTimThay);
                HienThiAnh(spTimThay.HinhAnh);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm này!");
            }
        }
        // Đặt tên PictureBox của bạn thay cho "picHinhAnh" nếu bạn đặt tên khác
        private void HienThiAnh(string imageUrl)
        {
            // Chỉnh SizeMode để ảnh tự co giãn vừa vặn khung hình
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    picHinhAnh.LoadAsync(imageUrl);
                }
                catch (Exception)
                {
                    // Nếu lỗi mạng hoặc link hỏng thì gán null hoặc ảnh mặc định
                    picHinhAnh.Image = null;
                }
            }
            else
            {
                picHinhAnh.Image = null;
            }
        }
    }
}
