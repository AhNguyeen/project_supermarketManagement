using BUS_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_SupermarketManagement
{
    public partial class GUI_ThanhToan : Form
    {
        public GUI_ThanhToan()
        {
            InitializeComponent();
        }
        BUS_ThanhToan bus_ThanhToan = new BUS_ThanhToan();
        BUS_KhachHang bus_KhachHang = new BUS_KhachHang(); 


        private void GUI_ThanhToan_Load(object sender, EventArgs e)
        {
            LoadDanhSachDGV();
        }

        public void LoadDanhSachDGV(string tenSP = null)
        {
            // Tắt tự động đẻ cột: Giúp DataGridView chỉ hiển thị những cột mình đã thiết kế sẵn trong giao diện, không bị sinh dư cột thừa.
            dgvSanPham.AutoGenerateColumns = false;

            // Gán nguồn dữ liệu cho DataGridView
            dgvSanPham.DataSource = bus_ThanhToan.HienThiSanPham(tenSP);

            cboKhachHang.DataSource = bus_KhachHang.layDanhSach();
            cboKhachHang.DisplayMember = "SoDienThoai";
            cboKhachHang.ValueMember = "MaKhachHang";
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

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có sản phẩm nào trong giỏ hàng không
            if (dgvChiTietHoaDon.Rows.Count == 0 || (dgvChiTietHoaDon.Rows.Count == 1 && dgvChiTietHoaDon.Rows[0].IsNewRow))
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại luôn, không làm các bước dưới nữa
            }

            // Không cần dùng else vì ở trên đã có return
            try
            {
                // 2. Lấy thông tin Hóa Đơn
                int maNhanVien = UserSession.MaNhanVien;

                // Xử lý chuỗi tổng tiền an toàn
                string tienChuoi = lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", "").Trim();
                double tongTien = 0;
                double.TryParse(tienChuoi, out tongTien);

                // Xử lý lấy mã khách hàng chuẩn xác
                int maKH = 1; // Mặc định là 1 (Khách vãng lai)
                //if (cboKhachHang.SelectedIndex >= 0 && cboKhachHang.SelectedValue != null)
                //{
                //    int.TryParse(cboKhachHang.SelectedValue.ToString(), out maKH);
                //}

                // 3. Gom dữ liệu Chi Tiết Hóa Đơn vào một List
                // Tuple<int, int, int> tương ứng với <Mã SP, Số Lượng, Đơn Giá>
                List<Tuple<int, int, int>> danhSachSanPham = new List<Tuple<int, int, int>>();

                foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống

                    if (row.Cells["colMaSP_CTHD"].Value != null)
                    {
                        int maSanPham = Convert.ToInt32(row.Cells["colMaSP_CTHD"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["colSoLuong_CTHD"].Value);
                        int donGia = Convert.ToInt32(row.Cells["colDonGia"].Value);

                        // Thêm sản phẩm vào danh sách
                        danhSachSanPham.Add(new Tuple<int, int, int>(maSanPham, soLuong, donGia));
                    }
                }

                // 4. GỌI XUỐNG BUS ĐỂ THANH TOÁN 1 LẦN DUY NHẤT
                // Lưu ý: Tầng BUS của bạn cũng phải cập nhật để gọi đúng hàm ThanhToanToanBo của DAL nhé
                bool kq = bus_ThanhToan.ThanhToanToanBo(maKH, maNhanVien, tongTien, danhSachSanPham);

                // 5. Xử lý kết quả
                if (kq)
                {
                    MessageBox.Show("Thanh toán và lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm sạch DataGridView và Tổng tiền chuẩn bị cho đơn hàng tiếp theo
                    dgvChiTietHoaDon.Rows.Clear();
                    lblTongTien.Text = "0 VNĐ";
                    cboKhachHang.SelectedIndex = -1; // Reset khách hàng (tùy chọn)
                }
                else
                {
                    MessageBox.Show("Không thể thanh toán. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Bất kỳ lỗi nào (kể cả lỗi SQL từ DAL ném lên) đều sẽ rơi vào đây để báo cho bạn biết
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChiTietHoaDon_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.IsCurrentCellDirty)
            {
                // Ép DataGridView lưu thay đổi ngay lập tức, từ đó kích hoạt CellValueChanged
                dgvChiTietHoaDon.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
