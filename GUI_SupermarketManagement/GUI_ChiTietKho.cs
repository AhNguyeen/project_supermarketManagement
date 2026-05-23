using BUS_SupermarketManagement;
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
    public partial class GUI_ChiTietKho : Form
    {
        public GUI_ChiTietKho()
        {
            InitializeComponent();

            // Thủ thuật chống giật lag (Flickering) khi load Form chứa DataGridView
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        // Khởi tạo các đối tượng tầng BUS
        BUS_ChiTietKho bus_ChiTietKho = new BUS_ChiTietKho();
        BUS_Kho bus_kho = new BUS_Kho();
        BUS_QLSP bus_SanPham = new BUS_QLSP();

        /// <summary>
        /// Hàm này được gọi TỪ FORM CHA (GUI_QuanLyKho) trước khi ShowDialog().
        /// Nhiệm vụ: Đổ dữ liệu vào ComboBox và hiển thị các sản phẩm thuộc về một mã kho cụ thể.
        /// </summary>
        /// <param name="maKho">Mã kho được truyền từ Form Quản Lý Kho sang</param>
        public void LoadDanhSach(string maKho = null)
        {
            // 1. Đổ dữ liệu vào ComboBox Kho
            cboMaKho.DataSource = bus_kho.InDanhSachKho();
            cboMaKho.ValueMember = "MaKho";
            cboMaKho.DisplayMember = "TenKho";

            // 2. Đổ dữ liệu vào ComboBox Sản Phẩm
            cboMaSanPham.DataSource = bus_SanPham.LayDS();
            cboMaSanPham.ValueMember = "MaSanPham";
            cboMaSanPham.DisplayMember = "TenSanPham";

            // 3. LOGIC UX QUAN TRỌNG: 
            // Nếu có mã kho truyền sang, ép ComboBox cboMaKho phải hiển thị đúng tên kho đó
            if (!string.IsNullOrEmpty(maKho))
            {
                cboMaKho.SelectedValue = Convert.ToInt32(maKho);

                // (Tùy chọn) Khóa ComboBox Kho lại không cho người dùng sửa sang kho khác để tránh lỗi logic
                cboMaKho.Enabled = false;
            }

            // 4. Load danh sách chi tiết các sản phẩm nằm trong kho đó lên DataGridView
            // (Bạn có thể thêm dgvChiTietKho.AutoGenerateColumns = false; ở đây nếu muốn tự thiết kế cột)
            dgvChiTietKho.DataSource = bus_ChiTietKho.LayDanhSachChiTietKho(maKho);
            dgvChiTietKho.ClearSelection();
        }

        /// <summary>
        /// Sự kiện Load Form (Hiện tại để trống vì ta đã dùng hàm LoadDanhSach làm nhiệm vụ khởi tạo dữ liệu rồi)
        /// </summary>
        private void GUI_ChiTietKho_Load(object sender, EventArgs e)
        {
            // Trống
        }

        /// <summary>
        /// Xử lý nút Thoát: Đóng Form con và quay lại Form Quản lý kho
        /// </summary>

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xử lý nút Thêm: Lấy thông tin từ các ComboBox và TextBox, gọi hàm thêm chi tiết kho mới vào database, sau đó refresh lại DataGridView để hiển thị cập nhật mới nhất.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboMaKho.SelectedValue?.ToString()) || string.IsNullOrEmpty(cboMaSanPham.SelectedValue?.ToString()) || string.IsNullOrEmpty(txtGiaNhap.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maKho = cboMaKho.SelectedValue.ToString();
                string maSanPham = cboMaSanPham.SelectedValue.ToString();
                int soLuong = 100; // Số lượng nhập cố định là 100 theo yêu cầu
                decimal gia = decimal.Parse(txtGiaNhap.Text);

                // Kiểm tra trùng thông tin chi tiết kho trước khi thêm
                if (bus_ChiTietKho.KiemTraTrungThongTinChiTietKho(maKho, maSanPham))
                {
                    MessageBox.Show("Chi tiết kho này đã tồn tại. Vui lòng kiểm tra lại thông tin và thử lại.", "Lỗi trùng thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = bus_ChiTietKho.ThemChiTietKho(maKho, maSanPham, soLuong, gia);

                if (success)
                {
                    MessageBox.Show("Thêm chi tiết kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh lại DataGridView để hiển thị cập nhật mới nhất
                    btnLamMoi_Click(null, null);
                    dgvChiTietKho.DataSource = bus_ChiTietKho.LayDanhSachChiTietKho(maKho);
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết kho thất bại. Vui lòng kiểm tra lại dữ liệu và thử lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho Số lượng và Giá nhập.\n" + ex.Message, "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Xử lý nút Xóa: Lấy thông tin từ các ComboBox và TextBox, gọi hàm xóa chi tiết kho khỏi database, sau đó refresh lại DataGridView để hiển thị cập nhật mới nhất.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn một chi tiết kho nào đó trong DataGridView chưa
                if (dgvChiTietKho.CurrentRow == null || dgvChiTietKho.CurrentCell.RowIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết kho để xóa.", "Chưa chọn chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy mã kho từ combobox và mã sản phẩm để xác định chi tiết kho cần xóa
                string maKho = cboMaKho.SelectedValue.ToString();
                string maSanPham = dgvChiTietKho.CurrentRow.Cells["colMaSanPham"].Value.ToString();

                // Đổi kiểu dữ liệu nếu cần thiết (ví dụ: nếu MaKho và MaSanPham là int trong database thì phải convert sang int trước khi gọi hàm xóa)
                int maKhoInt = int.Parse(maKho);
                int maSanPhamInt = int.Parse(maSanPham);

                // Xác nhận với người dùng trước khi xóa
                DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết kho này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.No)
                {
                    return; // Nếu người dùng chọn No, hủy bỏ thao tác xóa
                }

                bool success = bus_ChiTietKho.XoaChiTietKhoTheoMaKho(maKhoInt, maSanPhamInt);
                if (success)
                {
                    MessageBox.Show("Xóa chi tiết kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh lại DataGridView để hiển thị cập nhật mới nhất
                    dgvChiTietKho.DataSource = bus_ChiTietKho.LayDanhSachChiTietKho(maChiTietKho);
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết kho thất bại. Vui lòng thử lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa chi tiết kho thất bại.\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý nút Sửa: Lấy thông tin từ các ComboBox và TextBox, gọi hàm sửa chi tiết kho trong database, sau đó refresh lại DataGridView để hiển thị cập nhật mới nhất.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn một chi tiết kho nào đó trong DataGridView chưa
                if (dgvChiTietKho.CurrentRow == null || dgvChiTietKho.CurrentCell.RowIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết kho để sửa.", "Chưa chọn chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy mã kho và mã sản phẩm từ dòng được chọn để xác định chi tiết kho cần sửa
                string maChiTietKho = dgvChiTietKho.CurrentRow.Cells["colMaChiTietKho"].Value.ToString();
                string maKho = Convert.ToString(cboMaKho.SelectedValue); // Lấy mã kho từ ComboBox (nếu muốn cho phép sửa sang kho khác)    
                string maSanPham = dgvChiTietKho.CurrentRow.Cells["colMaSanPham"].Value.ToString();
                string soLuong = txtSoLuong.Text;
                string gia = txtGiaNhap.Text;

                // Đổi kiểu dữ liệu nếu cần thiết (ví dụ: nếu MaKho và MaSanPham là int trong database thì phải convert sang int trước khi gọi hàm sửa)
                int maChiTietKhoInt = int.Parse(maChiTietKho);
                int maKhoInt = int.Parse(maKho);
                int maSanPhamInt = int.Parse(maSanPham);
                int soLuongInt = int.Parse(soLuong);
                decimal giaDecimal = decimal.Parse(gia);

                bool success = bus_ChiTietKho.SuaChiTietKho(maChiTietKhoInt, maKho, maSanPham, soLuongInt, giaDecimal);
                if (success)
                {
                    MessageBox.Show("Sửa chi tiết kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh lại DataGridView để hiển thị cập nhật mới nhất
                    btnLamMoi_Click(null, null);
                    dgvChiTietKho.DataSource = bus_ChiTietKho.LayDanhSachChiTietKho(maKho);
                }
                else
                {
                    MessageBox.Show("Sửa chi tiết kho thất bại. Vui lòng kiểm tra lại dữ liệu và thử lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa chi tiết kho thất bại.\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý nút Làm mới: Xóa trắng các TextBox và ComboBox để người dùng có thể nhập thông tin mới dễ dàng hơn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa trắng các TextBox
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtMaChiTiet.Clear();
            txtThanhTien.Clear();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào một ô trong DataGridView để tự động điền thông tin của chi tiết kho đó vào các TextBox và ComboBox tương ứng, giúp cho việc sửa hoặc xóa dễ dàng hơn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvChiTietKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int row = e.RowIndex;
                txtMaChiTiet.Text = dgvChiTietKho.Rows[row].Cells["colMaChiTietKho"].Value.ToString();
                //cboMaKho.SelectedValue = dgvChiTietKho.Rows[row].Cells["colMaKho"].Value.ToString();
                cboMaSanPham.SelectedValue = dgvChiTietKho.Rows[row].Cells["colMaSanPham"].Value.ToString();
                txtSoLuong.Text = dgvChiTietKho.Rows[row].Cells["colSoLuongNhap"].Value.ToString();
                txtGiaNhap.Text = dgvChiTietKho.Rows[row].Cells["colGiaNhap"].Value.ToString();
                txtThanhTien.Text = dgvChiTietKho.Rows[row].Cells["colThanhTien"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn chi tiết kho.\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}