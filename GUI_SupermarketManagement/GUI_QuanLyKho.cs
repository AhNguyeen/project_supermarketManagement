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
    public partial class GUI_QuanLyKho : Form
    {
        public GUI_QuanLyKho()
        {
            InitializeComponent();

            // Thủ thuật chống giật lag khi DataGridView có nhiều dữ liệu hoặc form load nhiều control
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        // Khởi tạo các đối tượng BUS để giao tiếp với cơ sở dữ liệu
        BUS_Kho bus_Kho = new BUS_Kho();
        BUS_NhanVien nhanVien = new BUS_NhanVien();
        BUS_NhaCungCap nhaCungCap = new BUS_NhaCungCap();

        /// <summary>
        /// Sự kiện chạy đầu tiên khi Form Quản lý kho được mở lên
        /// </summary>
        private void GUI_QuanLyKho_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        /// <summary>
        /// Hàm tổng hợp dùng để đổ dữ liệu từ Database lên giao diện (DataGridView và ComboBox).
        /// Viết tách riêng để dễ dàng gọi lại (Refresh) sau khi Thêm/Sửa/Xóa thành công.
        /// </summary>
        public void LoadDanhSach()
        {
            // 1. Đổ dữ liệu toàn bộ danh sách kho lên bảng DataGridView
            dgvKho.DataSource = bus_Kho.InDanhSachKho();

            // 2. Đổ dữ liệu vào ComboBox Nhân viên
            cboMaNhanVien.DataSource = nhanVien.InTenNhanVienKho();
            cboMaNhanVien.DisplayMember = "HoTen";      // Chữ hiển thị cho người dùng xem
            cboMaNhanVien.ValueMember = "MaNhanVien";   // Giá trị ngầm (ID) dùng để lưu vào CSDL

            // 3. Đổ dữ liệu vào ComboBox Nhà cung cấp
            cboNCC.DataSource = nhaCungCap.LayDanhSach();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
        }

        /// <summary>
        /// Xóa trắng các ô nhập liệu để chuẩn bị thêm một kho mới
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKho.Clear();
            txtTenKho.Clear();
            rtxtGhiChu.Clear();

            // Trả ComboBox về dòng đầu tiên (thường là "Chọn...")
            cboNCC.SelectedIndex = 0;
            cboMaNhanVien.SelectedIndex = 0;

            // Trả ngày về ngày hiện tại
            dtpNgayNhap.Value = DateTime.Now;

            // Đưa con trỏ chuột nhấp nháy vào ô Tên Kho để người dùng gõ luôn
            txtTenKho.Focus();
        }

        /// <summary>
        /// Xử lý logic Thêm một kho mới vào CSDL
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Bước 1: Kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(txtTenKho.Text) ||
                cboMaNhanVien.SelectedIndex == 0 ||
                cboNCC.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng không để trống TÊN KHO, chọn NHÂN VIÊN PHỤ TRÁCH và chọn NHÀ CUNG CẤP!!");
                return; // Dừng lại, không chạy code phía dưới nữa
            }
            else
            {
                // Bước 2: Thu thập thông tin từ giao diện để chuẩn bị thêm vào CSDL
                DateTime ngayNhap = dtpNgayNhap.Value;
                int maNV = Convert.ToInt32(cboMaNhanVien.SelectedValue);
                int maNCC = Convert.ToInt32(cboNCC.SelectedValue);
                int maKho = Convert.ToInt32(txtMaKho.Text);

                // Bước 3: Kiểm tra xem kho này đã tồn tại trong ngày hôm nay chưa (Chống trùng lặp)
                if (bus_Kho.KiemTraTrung(txtTenKho.Text, maNV, maNCC, ngayNhap) == true)
                {
                    MessageBox.Show("Dữ liệu trùng lặp!");
                    return;
                }
                else
                {
                    // Bước 4: Đóng gói dữ liệu vào thực thể (Entity)
                    ET_Kho kho = new ET_Kho(maKho, txtTenKho.Text, maNV, maNCC, ngayNhap, rtxtGhiChu.Text);

                    // Bước 5: Gọi lớp BUS để thực hiện lệnh INSERT xuống SQL
                    bus_Kho.ThemKho(kho);

                    // Bước 6: Làm mới lại bảng để hiển thị kho vừa thêm
                    LoadDanhSach();
                }
            }
        }

        /// <summary>
        /// Xử lý logic Cập nhật (Sửa) thông tin của một kho đã có
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // MẸO SỬA LỖI: Dùng CurrentRow == null hoặc RowIndex < 0 vì Index = 0 là dòng đầu tiên.
            if (dgvKho.CurrentRow == null || dgvKho.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Xác nhận lại với người dùng trước khi sửa để tránh bấm nhầm
                DialogResult r;
                r = MessageBox.Show("Bạn có chắc muốn sửa " + txtTenKho.Text + " hay không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes)
                {
                    // Lấy dữ liệu mới từ các ô nhập liệu
                    DateTime ngayNhap = dtpNgayNhap.Value;
                    int maNV = Convert.ToInt32(cboMaNhanVien.SelectedValue);
                    int maNCC = Convert.ToInt32(cboNCC.SelectedValue);
                    int maKho = Convert.ToInt32(txtMaKho.Text);

                    // Đóng gói và gửi lệnh UPDATE
                    ET_Kho kho = new ET_Kho(maKho, txtTenKho.Text, maNV, maNCC, ngayNhap, rtxtGhiChu.Text);
                    bus_Kho.SuaKho(kho);

                    LoadDanhSach();
                    MessageBox.Show("Sửa thông tin kho thành công!");
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào 1 dòng trên DataGridView.
        /// Dữ liệu từ dòng đó sẽ được đẩy ngược lên các ô TextBox, ComboBox tương ứng.
        /// </summary>
        private void dgvKho_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy vị trí (Index) của dòng đang được click
                int index = dgvKho.CurrentCell.RowIndex;

                // Gán giá trị từ từng cột (Cell) của dòng đó lên giao diện
                txtMaKho.Text = dgvKho.Rows[index].Cells[0].Value.ToString();
                txtTenKho.Text = dgvKho.Rows[index].Cells[1].Value.ToString();

                // Gán giá trị cho ComboBox (chọn theo ValueMember là ID)
                cboMaNhanVien.SelectedValue = dgvKho.Rows[index].Cells[2].Value.ToString();
                cboNCC.SelectedValue = dgvKho.Rows[index].Cells[3].Value.ToString();

                dtpNgayNhap.Text = dgvKho.Rows[index].Cells[4].Value.ToString();
                rtxtGhiChu.Text = dgvKho.Rows[index].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu click vào vùng trống hoặc dòng không có dữ liệu
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo");
            }
        }

        /// <summary>
        /// Xử lý logic Xóa một kho khỏi CSDL
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn dòng nào chưa (Sửa thành < 0)
            if (dgvKho.CurrentRow == null || dgvKho.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn dòng dữ liệu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Cảnh báo xóa dữ liệu
                DialogResult r;
                r = MessageBox.Show("Bạn có chắc muốn xóa " + txtTenKho.Text + " hay không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes)
                {
                    // Truyền tham số Mã Kho xuống BUS để thực thi lệnh DELETE
                    bus_Kho.XoaKho(txtMaKho.Text);

                    LoadDanhSach();
                    MessageBox.Show("Xóa " + txtTenKho.Text + " thành công !", "Thông báo");
                }
            }
        }

        /// <summary>
        /// Mở form chi tiết để xem bên trong kho đang chứa những sản phẩm gì
        /// </summary>
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn kho nào trên DataGridView chưa
            if (dgvKho.CurrentRow == null || dgvKho.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn kho để có thể xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Khởi tạo Form Chi tiết kho
                GUI_ChiTietKho chiTiet = new GUI_ChiTietKho();

                // Gọi hàm load dữ liệu của Form Chi tiết và truyền Mã Kho hiện tại sang
                chiTiet.LoadDanhSach(txtMaKho.Text);

                // Hiển thị Form Chi tiết lên dưới dạng Dialog (phải tắt form con mới thao tác được form cha)
                chiTiet.ShowDialog();
            }
        }
    }
}