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
    public partial class GUI_QuanLyHoaDon : Form
    {
        public GUI_QuanLyHoaDon()
        {
            InitializeComponent();
        }

        // Khởi tạo các đối tượng tầng BUS để tương tác với cơ sở dữ liệu
        BUS_HoaDon bus_HoaDon = new BUS_HoaDon();
        BUS_NhanVien bus_NhanVien = new BUS_NhanVien();
        BUS_KhachHang bus_KhachHang = new BUS_KhachHang();

        /// <summary>
        /// Sự kiện khởi chạy đầu tiên khi mở Form Quản Lý Hóa Đơn
        /// </summary>
        private void GUI_QuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        /// <summary>
        /// Hàm load toàn bộ dữ liệu Hóa đơn lên DataGridView và cấu hình bảng
        /// </summary>
        public void LoadDanhSach()
        {
            // Gọi hàm đổ dữ liệu vào các ComboBox trước
            comboBox();

            // Tắt tự động đẻ cột: Giúp DataGridView chỉ hiển thị những cột mình đã thiết kế sẵn trong giao diện, không bị sinh dư cột thừa.
            dgvHoaDon.AutoGenerateColumns = false;

            // Gán nguồn dữ liệu cho DataGridView
            dgvHoaDon.DataSource = bus_HoaDon.LayDanhSach();
        }

        /// <summary>
        /// Đổ dữ liệu danh sách Khách hàng và Nhân viên vào ComboBox để người dùng chọn thay vì phải gõ tay
        /// </summary>
        public void comboBox()
        {
            // Setup ComboBox Nhân Viên
            cboMaNV.DataSource = bus_NhanVien.LayTatCaNhanVien();
            cboMaNV.DisplayMember = "HoTen"; // Hiển thị tên
            cboMaNV.ValueMember = "MaNhanVien"; // Lưu ngầm mã ID

            // Setup ComboBox Khách Hàng
            cboMaKH.DataSource = bus_KhachHang.layDanhSach();
            cboMaKH.DisplayMember = "HoTen";
            cboMaKH.ValueMember = "MaKhachHang";
        }

        /// <summary>
        /// Hàm dọn dẹp giao diện, reset các ô nhập liệu về trạng thái trống ban đầu
        /// </summary>
        public void ClearTxtCbo()
        {
            txtMaHD.Clear();
            cboMaKH.SelectedIndex = 0;
            cboMaNV.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpNgayLap.Value = DateTime.Now;
            txtTongTien.Clear();
        }

        /// <summary>
        /// Sự kiện click vào 1 dòng trên bảng. 
        /// Đẩy ngược dữ liệu từ dòng đó lên các TextBox, ComboBox để xem hoặc sửa.
        /// </summary>
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. CHỐNG BẤM BẬY: Bỏ qua nếu click vào dòng tiêu đề (Header) hoặc khoảng trống
            if (e.RowIndex < 0) return;

            try
            {
                // Lấy nguyên cái dòng đang được click để code gọn gàng, dễ truy xuất qua tên cột
                DataRowView drv = (DataRowView)dgvHoaDon.CurrentRow.DataBoundItem;

                // 2. Gán dữ liệu cơ bản cho TextBox 
                txtMaHD.Text = drv["MaHoaDon"].ToString();
                txtTongTien.Text = drv["TongTien"].ToString();

                // 3. GÁN DỮ LIỆU CHO COMBOBOX (XỬ LÝ NULL AN TOÀN)
                // Phải kiểm tra DBNull.Value để tránh lỗi sập phần mềm nếu dữ liệu SQL bị rỗng
                if (drv["MaKhachHang"] != DBNull.Value)
                {
                    // Ép về đúng kiểu Int để gán vào ValueMember
                    cboMaKH.SelectedValue = Convert.ToInt32(drv["MaKhachHang"]);
                }

                if (drv["MaNhanVien"] != DBNull.Value)
                {
                    cboMaNV.SelectedValue = Convert.ToInt32(drv["MaNhanVien"]);
                }

                // 4. Gán dữ liệu cho Ngày tháng và Trạng thái
                if (drv["NgayLap"] != DBNull.Value)
                {
                    dtpNgayLap.Value = Convert.ToDateTime(drv["NgayLap"]);
                }

                if (drv["TrangThai"] != DBNull.Value)
                {
                    // Trim() để cắt khoảng trắng dư thừa do kiểu NCHAR/CHAR trong SQL sinh ra
                    string trangThai = drv["TrangThai"].ToString().Trim();
                    cboTrangThai.Text = trangThai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lên: " + ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================================
        // SỰ KIỆN KHI CHỌN COMBOBOX (ĐANG ĐỂ TRỐNG ĐỂ PHÁT TRIỂN SAU)
        // =====================================================================
        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKH.SelectedValue == null) return;
            int maKH;
            if (int.TryParse(cboMaKH.SelectedValue.ToString(), out maKH))
            {
                // TODO: Bổ sung code lấy thông tin (SĐT, Địa chỉ) khách hàng tự động điền vào giao diện
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedValue == null) return;
            int maNV;
            if (int.TryParse(cboMaNV.SelectedValue.ToString(), out maNV))
            {
                // TODO: Xử lý hiển thị thông tin nhân viên (nếu cần)
            }
        }

        /// <summary>
        /// Xử lý việc Lập (Thêm) một hóa đơn mới vào cơ sở dữ liệu
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu từ giao diện
            int maKH = Convert.ToInt32(cboMaKH.SelectedValue);
            int maNV = Convert.ToInt32(cboMaNV.SelectedValue);
            DateTime ngayLap = Convert.ToDateTime(dtpNgayLap.Value);

            // 2. Kiểm tra nghiệp vụ: Chống tạo đúp hóa đơn giống hệt nhau cùng một lúc
            if (bus_HoaDon.KiemTraTrungHoaDon(maKH, maNV, ngayLap) == true)
            {
                MessageBox.Show("Dữ liệu mới thêm vào đã bị trùng với dữ liệu đã có!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // 3. Đóng gói dữ liệu để Thêm mới
                int maHD = 0; // ID SQL tự tăng nên truyền tạm giá trị 0
                int tongTien = 0; // Hóa đơn mới chưa có sản phẩm nên Tổng tiền = 0

                // MẸO: Phải dùng .Text thay vì .SelectedText đối với ComboBox có kiểu DropDownList
                string trangThai = cboTrangThai.Text;

                ET_HoaDon hoaDon = new ET_HoaDon(maHD, maKH, maNV, ngayLap, tongTien, trangThai);

                // 4. Gọi hàm Thêm từ lớp BUS và kiểm tra kết quả
                if (bus_HoaDon.ThemHoaDon(hoaDon) == true)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo");
                    ClearTxtCbo(); // Xóa trắng form để làm tiếp
                    LoadDanhSach(); // Refresh lại bảng hiển thị
                    return;
                }
                else
                {
                    MessageBox.Show("Lỗi thêm thất bại!", "Thông báo");
                    return;
                }
            }
        }

        /// <summary>
        /// Xử lý cập nhật thông tin Hóa đơn hiện tại (Thường dùng để sửa trạng thái Hoàn Thành/Đã Hủy)
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // MẸO SỬA LỖI: Kiểm tra RowIndex < 0 để biết chưa có dòng nào được chọn
            if (dgvHoaDon.CurrentRow == null || dgvHoaDon.CurrentCell.RowIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn dữ liệu cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // 1. Lấy toàn bộ dữ liệu mới trên giao diện
                int maKH = Convert.ToInt32(cboMaKH.SelectedValue);
                int maNV = Convert.ToInt32(cboMaNV.SelectedValue);
                DateTime ngayLap = Convert.ToDateTime(dtpNgayLap.Value);
                int maHD = Convert.ToInt32(txtMaHD.Text);

                // Dùng TryParse để an toàn ép kiểu tổng tiền (Tránh lỗi văng app nếu ô chữ trống)
                int tongTien;
                int.TryParse(txtTongTien.Text, out tongTien);

                string trangThai = cboTrangThai.Text;

                // 2. Gửi lệnh cập nhật (Sửa)
                ET_HoaDon hoaDon = new ET_HoaDon(maHD, maKH, maNV, ngayLap, tongTien, trangThai);
                if (bus_HoaDon.SuaHoaDon(hoaDon) == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    ClearTxtCbo();
                    LoadDanhSach();
                    return;
                }
                else
                {
                    MessageBox.Show("Lỗi sửa thất bại!", "Thông báo");
                    return;
                }
            }
        }

        /// <summary>
        /// Xử lý hủy (xóa) một hóa đơn
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra chọn dòng
            if (dgvHoaDon.CurrentRow == null || dgvHoaDon.CurrentCell.RowIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng dữ liệu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Xin xác nhận cực kỳ quan trọng trước khi Xóa dữ liệu liên quan đến Tiền/Hóa Đơn
                DialogResult r;
                r = MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes)
                {
                    int maHD = Convert.ToInt32(txtMaHD.Text);
                    bus_HoaDon.XoaHoaDon(maHD); // Gọi hàm xóa từ Database

                    LoadDanhSach(); // Refresh bảng
                    MessageBox.Show("Xóa thành công !", "Thông báo");
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            // Sửa lại điều kiện check 1 chút cho an toàn hơn (bỏ qua dòng trống IsNewRow)
            if (dgvHoaDon.CurrentRow == null || dgvHoaDon.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy mã HD (Bạn lấy từ txtMaHD cũng được, nhưng lấy thẳng từ dòng đang chọn trên DataGridView thì chuẩn hơn)
                // int maHD = Convert.ToInt32(txtMaHD.Text); 
                int maHD = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells["colMaHoaDon"].Value); // Thay "colMaHD" bằng tên cột thực tế của bạn

                // Khởi tạo Form Chi Tiết và TRUYỀN MÃ HÓA ĐƠN VÀO TRONG NGOẶC
                GUI_ChiTietHoaDon chiTiet = new GUI_ChiTietHoaDon(maHD);

                // Mở Form
                chiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}