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
    public partial class GUI_ChinhSuaHoaDon : Form
    {
        BUS_ThanhToan bus_ThanhToan = new BUS_ThanhToan();
        BUS_KhachHang bus_KhachHang = new BUS_KhachHang();
        BUS_ChiTietHoaDon bus_ChiTietHoaDon = new BUS_ChiTietHoaDon();
        BUS_HoaDon bus_HoaDon = new BUS_HoaDon();

        private int _maHoaDon;

        public GUI_ChinhSuaHoaDon(int maHD)
        {
            InitializeComponent();
            _maHoaDon = maHD; // Nhận mã từ form bên kia và cất vào biến toàn cục
        }

        public void LoadDanhSachDGV(string tenSP = null)
        {
            // Tắt tự động đẻ cột: Giúp DataGridView chỉ hiển thị những cột mình đã thiết kế sẵn trong giao diện, không bị sinh dư cột thừa.
            dgvSanPham.AutoGenerateColumns = false;

            // Gán nguồn dữ liệu cho DataGridView
            dgvSanPham.DataSource = bus_ThanhToan.HienThiSanPham(tenSP);
        }

        private void GUI_ChinhSuaHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachDGV();

            // Load chi tiết hóa đơn cũ vào bên phải
            LoadChiTietHoaDonCu();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Đảm bảo người dùng không click vào tiêu đề cột (Header)
            if (e.RowIndex >= 0)
            {
                // 2. Lấy dữ liệu từ dòng được chọn bên dgvSanPham
                DataGridViewRow rowSP = dgvSanPham.Rows[e.RowIndex];

                // Lưu ý: Tên trong ngoặc kép "..." phải là tên cột (Name) hoặc DataPropertyName của dgvSanPham
                string maSP = rowSP.Cells["colMaSanPham"].Value.ToString();
                string tenSP = rowSP.Cells["colTenSanPham"].Value.ToString();
                double giaBan = Convert.ToDouble(rowSP.Cells["colGiaBan"].Value);

                // Biến cờ kiểm tra xem sản phẩm đã có trong dgvChiTietHoaDon chưa
                bool isExists = false;

                // 3. Duyệt qua dgvChiTietHoaDon để kiểm tra trùng lặp
                foreach (DataGridViewRow rowCTHD in dgvChiTietHoaDon.Rows)
                {
                    // Tránh dòng trống cuối cùng của DataGridView
                    if (rowCTHD.IsNewRow) continue;

                    // Giả sử cột chứa mã SP bên dgvChiTietHoaDon tên là "colMaSP_CTHD"
                    if (rowCTHD.Cells["colMaSP_CTHD"].Value != null &&
                        rowCTHD.Cells["colMaSP_CTHD"].Value.ToString() == maSP)
                    {
                        // Nếu ĐÃ TỒN TẠI: Tăng số lượng và tính lại tiền
                        int soLuongHienTai = Convert.ToInt32(rowCTHD.Cells["colSoLuong_CTHD"].Value);
                        soLuongHienTai++;

                        rowCTHD.Cells["colSoLuong_CTHD"].Value = soLuongHienTai;
                        rowCTHD.Cells["colThanhTien"].Value = soLuongHienTai * giaBan;

                        isExists = true;
                        break; // Tìm thấy rồi thì thoát vòng lặp
                    }
                }

                // 4. Nếu CHƯA TỒN TẠI: Thêm một dòng mới
                if (!isExists)
                {
                    int soLuongMoi = 1;
                    double thanhTienMoi = soLuongMoi * giaBan;

                    // Thứ tự các biến truyền vào hàm Add() phải TƯƠNG ỨNG với thứ tự cột trên dgvChiTietHoaDon
                    // VD: Mã SP, Tên SP, Số Lượng, Đơn Giá, Thành Tiền
                    dgvChiTietHoaDon.Rows.Add(maSP, tenSP, soLuongMoi, giaBan, thanhTienMoi);
                }

                // 5. (Tùy chọn) Gọi hàm tính Tổng Tiền cho toàn bộ Hóa Đơn ở đây
                CapNhatTongTienHoaDon();
            }
        }

        private void dgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Đảm bảo người dùng click vào một dòng hợp lệ (không phải Header)
            if (e.RowIndex >= 0)
            {
                // 2. Kiểm tra xem cột vừa click có đúng là cột chứa Button Delete hay không.
                // Thay "colDelete" bằng tên cột (Name) thực tế của cột button trong dgv của bạn.
                if (dgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "colDelete")
                {
                    // 3. Hiển thị hộp thoại xác nhận trước khi xóa (Rất cần thiết để tránh bấm nhầm)
                    DialogResult dialogResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa sản phẩm này khỏi hóa đơn?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // 4. Thực hiện xóa dòng tại vị trí vừa click
                        dgvChiTietHoaDon.Rows.RemoveAt(e.RowIndex);

                        // 5. (Tùy chọn) Gọi lại hàm tính Tổng Tiền vì giỏ hàng vừa bị thay đổi
                        CapNhatTongTienHoaDon();
                    }
                }
            }
        }

        //Sự kiện cột thanh toán tự động tính khi thay đổ giá trị số lượng trực tiếp trên dgvChiTietHoaDon
        private void dgvChiTietHoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Đảm bảo không click vào Header
            if (e.RowIndex >= 0)
            {
                // 2. Kiểm tra xem cột vừa bị thay đổi có đúng là cột "Số lượng" không?
                // Thay "colSoLuong" bằng tên thực tế của cột số lượng trong dgv của bạn
                if (dgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "colSoLuong_CTHD")
                {
                    DataGridViewRow row = dgvChiTietHoaDon.Rows[e.RowIndex];

                    // 3. Khai báo biến để tính toán an toàn
                    int soLuong = 0;
                    decimal donGia = 0;

                    // Lấy Số lượng mới (Dùng TryParse để đề phòng người dùng nhập chữ thay vì số)
                    if (row.Cells["colSoLuong_CTHD"].Value != null)
                    {
                        if (!int.TryParse(row.Cells["colSoLuong_CTHD"].Value.ToString(), out soLuong))
                        {
                            MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells["colSoLuong_CTHD"].Value = 1; // Gán lại bằng 1 nếu nhập sai
                            soLuong = 1;
                        }
                    }

                    // Lấy Đơn giá hiện tại
                    if (row.Cells["colDonGia"].Value != null)
                    {
                        decimal.TryParse(row.Cells["colDonGia"].Value.ToString(), out donGia);
                    }

                    // 4. Tính lại Thành tiền và gán vào cột Thành tiền
                    // Thay "colThanhTien" bằng tên thực tế của cột Thành tiền
                    row.Cells["colThanhTien"].Value = soLuong * donGia;

                    // 5. (Rất quan trọng) Gọi lại hàm tính Tổng tiền của cả Hóa đơn
                    CapNhatTongTienHoaDon();
                }
            }
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtTimSP.Text == null)
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                LoadDanhSachDGV(txtTimSP.Text);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra giỏ hàng có rỗng không
            if (dgvChiTietHoaDon.Rows.Count == 0 || (dgvChiTietHoaDon.Rows.Count == 1 && dgvChiTietHoaDon.Rows[0].IsNewRow))
            {
                MessageBox.Show("Hóa đơn phải có ít nhất 1 sản phẩm. Nếu khách trả lại toàn bộ, vui lòng dùng chức năng Hủy Hóa Đơn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. HIỆN THÔNG BÁO XÁC NHẬN TRƯỚC KHI CẬP NHẬT
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu các thay đổi cho hóa đơn này không?\nThao tác này sẽ ghi đè lên dữ liệu cũ.",
                "Xác nhận cập nhật",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Nếu người dùng chọn No thì dừng lại luôn
            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 3. Xử lý lấy tổng tiền mới
                string tienChuoi = lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", "").Trim();
                double tongTienMoi = 0;
                double.TryParse(tienChuoi, out tongTienMoi);

                // 4. Gom danh sách sản phẩm trên DataGridView hiện tại
                List<Tuple<int, int, int>> danhSachSanPhamMoi = new List<Tuple<int, int, int>>();

                foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
                {
                    if (row.IsNewRow) continue;

                    // LƯU Ý: Đổi tên cột trong ngoặc kép cho đúng với Name của cột trong dgvChiTietHoaDon của Form này nhé!
                    if (row.Cells["colMaSP_CTHD"].Value != null)
                    {
                        int maSanPham = Convert.ToInt32(row.Cells["colMaSP_CTHD"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["colSoLuong_CTHD"].Value);
                        int donGia = Convert.ToInt32(row.Cells["colDonGia"].Value);

                        danhSachSanPhamMoi.Add(new Tuple<int, int, int>(maSanPham, soLuong, donGia));
                    }
                }

                // 5. Gọi hàm cập nhật dưới BUS (hàm sử dụng thuật toán Xóa Cũ -> Thêm Mới mà mình đã gợi ý)
                bool kq = bus_HoaDon.CapNhatHoaDon(_maHoaDon, tongTienMoi, danhSachSanPhamMoi);

                // 6. Xử lý kết quả
                if (kq)
                {
                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form chỉnh sửa sau khi thành công (trả về DialogResult.OK để Form Quản lý biết đường load lại dữ liệu)
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật hóa đơn. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietHoaDonCu()
        {
            try
            {
                // Bây giờ bạn đã có _maHoaDon, hãy dùng nó để tìm kiếm
                ET_ChiTietHoaDon chiTietHoaDon = new ET_ChiTietHoaDon();
                chiTietHoaDon.MaHD = _maHoaDon;
                // Gọi BUS để lấy chi tiết hóa đơn theo _maHoaDon
                // (Dùng lại hàm sp_HienThiChiTietHoaDon mà bạn đã có)
                DataTable dtChiTiet = bus_ChiTietHoaDon.LayDachSachHoaDon(chiTietHoaDon);
                dgvChiTietHoaDon.Rows.Clear(); // Xóa trắng giỏ hàng trước khi load

                if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtChiTiet.Rows)
                    {
                        // Lấy dữ liệu từ DB
                        int maSP = Convert.ToInt32(dr["MaSanPham"]);
                        string tenSP = dr["TenSanPham"].ToString();
                        int soLuong = Convert.ToInt32(dr["SoLuong"]);
                        double donGia = Convert.ToDouble(dr["DonGia"]);
                        double thanhTien = soLuong * donGia; // Hoặc lấy thẳng cột dr["ThanhTien"]

                        // Thêm một dòng mới vào dgvChiTietHoaDon
                        // LƯU Ý: Thứ tự các biến phải khớp với thứ tự cột trên dgv của bạn
                        dgvChiTietHoaDon.Rows.Add(maSP, tenSP, soLuong, donGia, thanhTien);
                    }
                }

                // Tính lại tổng tiền sau khi load xong
                CapNhatTongTienHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết: " + ex.Message);
            }
        }

        private void CapNhatTongTienHoaDon()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["colThanhTien"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["colThanhTien"].Value);
                }
            }

            lblTongTien.Text = tongTien.ToString("N0");
        }
    }
}
