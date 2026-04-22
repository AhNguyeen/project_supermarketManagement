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
            txtTenSanPham.Text = sp.TenSanPham;
            txtMaSanPham.Text = sp.MaSanPham.ToString();
            txtGiaBan.Text = sp.GiaBan.ToString();
            rtxtMoTa.Text = sp.ChiTiet;
            HienThiAnh(sp.HinhAnh);
            rtxtThongSoKiThuat.Clear();
            rtxtThongSoKiThuat.Text =
                $"Mã Nhà Cung Cấp: {sp.MaNCC}\n" +
                $"Mã Danh Mục: {sp.MaDanhMuc}\n" +
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
        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                // Hiển thị ảnh lên PictureBox
                picHinhAnh.Image = Image.FromFile(open.FileName);
                // Lưu đường dẫn file vào Tag để nút "Sửa" có thể sử dụng
                picHinhAnh.Tag = open.FileName;
                picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra dữ liệu số tránh lỗi văng chương trình (Validation)
                if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan))
                {
                    MessageBox.Show("Giá bán phải là số!");
                    return;
                }

                // 2. Cập nhật dữ liệu từ các ô nhập vào đối tượng sản phẩm hiện tại
                spHienTai.TenSanPham = txtTenSanPham.Text;
                spHienTai.GiaBan = giaBan;
                spHienTai.ChiTiet = rtxtMoTa.Text;

                // Bổ sung thêm các thông số khác nếu bạn cho phép sửa ở màn hình này
                // spHienTai.GiaNhap = decimal.Parse(txtGiaNhap.Text);

                // 3. Xử lý Ảnh: Chỉ thực hiện nếu người dùng chọn ảnh mới (Tag chứa đường dẫn file)
                if (picHinhAnh.Tag != null && File.Exists(picHinhAnh.Tag.ToString()))
                {
                    string nguon = picHinhAnh.Tag.ToString(); // Đường dẫn file bên ngoài
                    string tenFile = Path.GetFileName(nguon); // Lấy tên file
                    string thuMucAnh = Path.Combine(Application.StartupPath, "Images");

                    // Tạo thư mục Images nếu chưa có
                    if (!Directory.Exists(thuMucAnh)) Directory.CreateDirectory(thuMucAnh);

                    string dich = Path.Combine(thuMucAnh, tenFile);

                    // Nếu ảnh mới khác ảnh cũ thì mới copy vào thư mục dự án
                    if (nguon != dich)
                    {
                        File.Copy(nguon, dich, true); // true để ghi đè nếu trùng tên
                    }

                    spHienTai.HinhAnh = tenFile; // Lưu tên file vào đối tượng
                }

                // 4. Gọi tầng BUS để thực hiện cập nhật xuống SQL Server
                if (busSP.Sua(spHienTai))
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    // Đóng form sau khi sửa xong
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Vui lòng kiểm tra lại Store Procedure trong SQL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }

        }

        //Nút xóa sản phẩm
        private void button2_Click(object sender, EventArgs e)
        {
            //Thông báo cho người dùng thật sự có muốn xóa hay không
            DialogResult r = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                //Nếu người dùng xác nhận YES thì xóa sản phẩm
                if (busSP.Xoa(spHienTai.MaSanPham))
                {
                    //Xóa thành công sẽ thông báo
                    MessageBox.Show("Đã xóa sản phẩm thành công!");
                    this.Close();
                }
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
                    MaDanhMuc = int.Parse(row["MaDanhMuc"].ToString())
                };
                LoadSanPham(spTimThay);
                HienThiAnh(spTimThay.HinhAnh);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm này!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
