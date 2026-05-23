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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowSP = dgvSanPham.Rows[e.RowIndex];

                // ĐÃ FIX: Xử lý an toàn tránh lỗi NullReferenceException hoặc FormatException
                string maSP = rowSP.Cells["colMaSanPham"].Value?.ToString() ?? "";
                string tenSP = rowSP.Cells["colTenSanPham"].Value?.ToString() ?? "Sản phẩm không tên";

                double giaBan = 0;
                if (rowSP.Cells["colGiaBan"].Value != null && rowSP.Cells["colGiaBan"].Value != DBNull.Value)
                {
                    double.TryParse(rowSP.Cells["colGiaBan"].Value.ToString(), out giaBan);
                }

                int soLuongTon = 0;
                if (rowSP.Cells["colSoLuong"].Value != null && rowSP.Cells["colSoLuong"].Value != DBNull.Value)
                {
                    int.TryParse(rowSP.Cells["colSoLuong"].Value.ToString(), out soLuongTon);
                }

                bool isExists = false;

                foreach (DataGridViewRow rowCTHD in dgvChiTietHoaDon.Rows)
                {
                    if (rowCTHD.IsNewRow) continue;

                    if (rowCTHD.Cells["colMaSP_CTHD"].Value != null && rowCTHD.Cells["colMaSP_CTHD"].Value.ToString() == maSP)
                    {
                        int soLuongHienTai = Convert.ToInt32(rowCTHD.Cells["colSoLuong_CTHD"].Value);
                        if (soLuongHienTai + 1 > soLuongTon)
                        {
                            MessageBox.Show("Số lượng mua (" + (soLuongHienTai + 1) + ") vượt quá số lượng tồn kho hiện có (" + soLuongTon + ")!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        soLuongHienTai++;

                        rowCTHD.Cells["colSoLuong_CTHD"].Value = soLuongHienTai;
                        rowCTHD.Cells["colThanhTien"].Value = soLuongHienTai * giaBan;

                        isExists = true;
                        break;
                    }
                }

                if (!isExists)
                {
                    if (1 > soLuongTon)
                    {
                        MessageBox.Show("Sản phẩm này đã hết hàng trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    int soLuongMoi = 1;
                    double thanhTienMoi = soLuongMoi * giaBan;
                    dgvChiTietHoaDon.Rows.Add(maSP, tenSP, soLuongMoi, giaBan, thanhTienMoi);
                }

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
                if (dgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "colSoLuong_CTHD")
                {
                    DataGridViewRow row = dgvChiTietHoaDon.Rows[e.RowIndex];

                    // 3. Khai báo biến để tính toán an toàn
                    int soLuong = 0;
                    decimal donGia = 0;
                    string maSP = row.Cells["colMaSP_CTHD"].Value?.ToString() ?? "";

                    // Lấy số lượng tồn kho từ bảng dgvSanPham
                    int soLuongTon = 0;
                    foreach (DataGridViewRow rowSP in dgvSanPham.Rows)
                    {
                        if (rowSP.Cells["colMaSanPham"].Value != null && rowSP.Cells["colMaSanPham"].Value.ToString() == maSP)
                        {
                            if (rowSP.Cells["colSoLuong"].Value != null)
                            {
                                int.TryParse(rowSP.Cells["colSoLuong"].Value.ToString(), out soLuongTon);
                            }
                            break;
                        }
                    }

                    // Lấy Số lượng mới (Dùng TryParse để đề phòng người dùng nhập chữ thay vì số)
                    if (row.Cells["colSoLuong_CTHD"].Value != null)
                    {
                        if (!int.TryParse(row.Cells["colSoLuong_CTHD"].Value.ToString(), out soLuong))
                        {
                            MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells["colSoLuong_CTHD"].Value = 1; // Gán lại bằng 1 nếu nhập sai
                            soLuong = 1;
                        }
                        else if (soLuong > soLuongTon)
                        {
                            MessageBox.Show("Số lượng mua (" + soLuong + ") vượt quá số lượng tồn kho hiện có (" + soLuongTon + ")!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells["colSoLuong_CTHD"].Value = soLuongTon; // Ép về mức tối đa
                            soLuong = soLuongTon;
                        }
                        else if (soLuong <= 0)
                        {
                            MessageBox.Show("Số lượng mua phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells["colSoLuong_CTHD"].Value = 1;
                            soLuong = 1;
                        }
                    }

                    // Lấy Đơn giá hiện tại
                    if (row.Cells["colDonGia"].Value != null)
                    {
                        decimal.TryParse(row.Cells["colDonGia"].Value.ToString(), out donGia);
                    }

                    // 4. Tính lại Thành tiền và gán vào cột Thành tiền
                    row.Cells["colThanhTien"].Value = soLuong * donGia;

                    // 5. (Rất quan trọng) Gọi lại hàm tính Tổng tiền của cả Hóa đơn
                    CapNhatTongTienHoaDon();
                }
            }
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimSP.Text))
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

                // Thay thế đoạn xử lý lấy mã khách hàng trong btnThanhToan_Click của bạn bằng đoạn này:

                int maKH = 1; // Mặc định là 1 (Khách vãng lai) nếu không chọn ai

                // ĐÃ FIX: Mở comment và xử lý an toàn để lấy đúng khách VIP
                if (cboKhachHang.SelectedIndex >= 0 && cboKhachHang.SelectedValue != null)
                {
                    int.TryParse(cboKhachHang.SelectedValue.ToString(), out maKH);
                }

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
                int kq = bus_ThanhToan.ThanhToanToanBo(maKH, maNhanVien, tongTien, danhSachSanPham);

                // 5. Xử lý kết quả
                if (kq > 0)
                {
                    MessageBox.Show("Thanh toán và lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm sạch DataGridView và Tổng tiền chuẩn bị cho đơn hàng tiếp theo
                    dgvChiTietHoaDon.Rows.Clear();
                    lblTongTien.Text = "0 VNĐ";
                    cboKhachHang.SelectedIndex = -1; // Reset khách hàng (tùy chọn)

                    // HIỂN THỊ FORM CHI TIẾT HÓA ĐƠN TRỰC TIẾP
                    // Chuyển mã hóa đơn vừa tạo vào form chi tiết
                    GUI_ChiTietHoaDon frmChiTiet = new GUI_ChiTietHoaDon(kq);
                    frmChiTiet.ShowDialog();
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
