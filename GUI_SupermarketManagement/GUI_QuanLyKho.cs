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
using System.Text.RegularExpressions;

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
            // Nếu kiểm tra thất bại thì dừng lại luôn, không chạy code thêm vào Database nữa
            if (!KiemTraTenKhoHopLe())
            {
                return;
            }

            // Bước 1: Kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(txtTenKho.Text) ||
                cboMaNhanVien.SelectedIndex == -1 ||
                cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng không để trống tên kho, chọn nhân viên và chọn nhà cung cấp!!");
                return; // Dừng lại, không chạy code phía dưới nữa
            }
            else
            {
                // Bước 4: Đóng gói dữ liệu vào thực thể (Entity)
                ET_Kho kho = new ET_Kho(txtTenKho.Text, Convert.ToInt32(cboMaNhanVien.SelectedValue), Convert.ToInt32(cboNCC.SelectedValue), dtpNgayNhap.Value, rtxtGhiChu.Text);

                // Bước 5: Gọi lớp BUS để thực hiện lệnh INSERT xuống SQL
                bus_Kho.ThemKho(kho);
                
                // Bước 6: Làm mới lại bảng để hiển thị kho vừa thêm
                LoadDanhSach();

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
                // Kiểm tra các trường dữ liệu có dữ liệu hợp lệ hay không trước khi gửi lệnh UPDATE xuống Database
                if (string.IsNullOrWhiteSpace(txtMaKho.Text))
                {
                    MessageBox.Show("Vui lòng chọn kho cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận lại với người dùng trước khi sửa để tránh bấm nhầm
                DialogResult r;
                r = MessageBox.Show("Bạn có chắc muốn sửa " + txtTenKho.Text + " hay không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes)
                {
                    // Đóng gói và gửi lệnh UPDATE
                    ET_Kho kho = new ET_Kho(Convert.ToInt32(txtMaKho.Text), txtTenKho.Text, Convert.ToInt32(cboMaNhanVien.SelectedValue), Convert.ToInt32(cboNCC.SelectedValue), dtpNgayNhap.Value, rtxtGhiChu.Text);

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
                cboNCC.SelectedValue = dgvKho.Rows[index].Cells[4].Value.ToString();

                dtpNgayNhap.Text = dgvKho.Rows[index].Cells[6].Value.ToString();
                rtxtGhiChu.Text = dgvKho.Rows[index].Cells[7].Value.ToString();
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

        private bool KiemTraTenKhoHopLe()
        {
            // Cắt bỏ khoảng trắng ở 2 đầu (nếu người dùng lỡ bấm phím cách)
            string tenKho = txtTenKho.Text.Trim();

            // 1. Kiểm tra không được để trống
            if (string.IsNullOrWhiteSpace(tenKho))
            {
                MessageBox.Show("Vui lòng nhập tên kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                return false;
            }

            // 2. Kiểm tra giới hạn ký tự (Từ 3 đến 255)
            if (tenKho.Length < 3 || tenKho.Length > 255)
            {
                MessageBox.Show("Tên kho phải có độ dài từ 3 đến 255 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                return false;
            }

            // 3. Kiểm tra không có ký tự đặc biệt
            // \p{L}: Chữ cái (bao gồm tiếng Việt có dấu)
            // \d: Chữ số
            // \s: Khoảng trắng
            Regex regex = new Regex(@"^[\p{L}\d\s]+$");
            if (!regex.IsMatch(tenKho))
            {
                MessageBox.Show("Tên kho không được chứa ký tự đặc biệt (chỉ cho phép chữ, số và khoảng trắng)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                return false;
            }

            // 4. Kiểm tra xem tên kho này đã tồn tại trong ngày hôm nay chưa (Chống trùng lặp)
            DateTime ngayNhap = dtpNgayNhap.Value;
            int maNV = Convert.ToInt32(cboMaNhanVien.SelectedValue);
            int maNCC = Convert.ToInt32(cboNCC.SelectedValue);
            if (bus_Kho.KiemTraTrung(tenKho, maNV, maNCC, ngayNhap) == true)
            {
                MessageBox.Show("Tên kho này đã tồn tại trong ngày hôm nay với cùng nhân viên phụ trách và nhà cung cấp! Vui lòng chọn tên khác hoặc thay đổi thông tin liên quan.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                return false;
            }

            // 5. Kiểm tra ngày nhập không được lớn 2100-01-01 hoặc nhỏ hơn 2000-01-01 (Giới hạn khoảng thời gian hợp lý)
            if (ngayNhap > new DateTime(2100, 1, 1) || ngayNhap < new DateTime(2000, 1, 1))
            {
                MessageBox.Show("Ngày nhập phải nằm trong khoảng từ 01/01/2000 đến 01/01/2100!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayNhap.Focus();
                return false;
            }

            return true; // Dữ liệu hoàn toàn hợp lệ
        }
    }
}