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
    public partial class GUI_NhaCungCap : Form
    {
        public GUI_NhaCungCap()
        {
            InitializeComponent();
        }

        // Khởi tạo đối tượng tầng BUS để giao tiếp với cơ sở dữ liệu
        BUS_NhaCungCap bus_NhaCungCap = new BUS_NhaCungCap();

        /// <summary>
        /// Hàm load dữ liệu tự động khi form vừa mở lên
        /// </summary>
        private void GUI_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        /// <summary>
        /// Hàm Load danh sách Nhà cung cấp lên bảng
        /// Viết tách ra để dễ dàng gọi lại (Refresh) sau khi Thêm/Sửa/Xóa
        /// </summary>
        private void LoadDanhSach()
        {
            dgvNCC.DataSource = bus_NhaCungCap.LayDanhSach();
        }

        /// <summary>
        /// Hàm dọn dẹp các TextBox về trạng thái trống
        /// (Gom lại thành 1 hàm để code ngắn gọn, tái sử dụng được nhiều lần)
        /// </summary>
        private void ClearForm()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSoDT.Clear();
            txtEmail.Clear();
            txtTenNCC.Focus(); // Đưa con trỏ chuột vào ô Tên NCC
        }

        /// <summary>
        /// Xử lý logic Thêm một Nhà cung cấp mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ: Không được để trống bất kỳ ô nào
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtSoDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin để thêm nhà cung cấp mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Đóng gói dữ liệu từ giao diện vào đối tượng Thực thể (ET)
                // Lưu ý: Thêm mới nên không cần truyền Mã NCC (SQL tự tăng)
                ET_NhaCungCap nhaCungCap = new ET_NhaCungCap(txtTenNCC.Text, txtDiaChi.Text, txtSoDT.Text, txtEmail.Text);

                // Gọi BUS để xử lý thêm xuống DB
                if (bus_NhaCungCap.ThemNhaCungCap(nhaCungCap) == true)
                {
                    MessageBox.Show("Thêm nhà cung cấp mới thành công!", "Thông báo");
                    LoadDanhSach(); // Refresh bảng
                    ClearForm();    // Xóa sạch form
                }
                else
                {
                    MessageBox.Show("Lỗi: Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Xử lý logic Sửa thông tin Nhà cung cấp
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn Nhà cung cấp nào chưa (Dựa vào ô Mã NCC)
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp từ danh sách bên dưới để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đóng gói dữ liệu mới vào đối tượng
            ET_NhaCungCap nhaCungCap = new ET_NhaCungCap(txtTenNCC.Text, txtDiaChi.Text, txtSoDT.Text, txtEmail.Text);

            // CỰC KỲ QUAN TRỌNG: Phải gán Mã NCC thì lệnh UPDATE trong SQL mới biết đang sửa dòng nào
            nhaCungCap.MaNCC = Convert.ToInt32(txtMaNCC.Text);

            if (bus_NhaCungCap.SuaNhaCungCap(nhaCungCap) == true)
            {
                MessageBox.Show("Sửa thông tin nhà cung cấp thành công!", "Thông báo");
                LoadDanhSach();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lỗi: Sửa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện click vào 1 dòng trên DataGridView
        /// </summary>
        private void dgvNCC_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có click trúng dòng chứa dữ liệu không
                if (dgvNCC.CurrentRow != null && dgvNCC.CurrentCell.RowIndex >= 0)
                {
                    int index = dgvNCC.CurrentCell.RowIndex;

                    // Đẩy dữ liệu từ dòng được chọn lên các TextBox
                    txtMaNCC.Text = dgvNCC.Rows[index].Cells[0].Value.ToString();
                    txtTenNCC.Text = dgvNCC.Rows[index].Cells[1].Value.ToString();
                    txtDiaChi.Text = dgvNCC.Rows[index].Cells[2].Value.ToString();
                    txtSoDT.Text = dgvNCC.Rows[index].Cells[3].Value.ToString();
                    txtEmail.Text = dgvNCC.Rows[index].Cells[4].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo");
            }
        }

        /// <summary>
        /// Xử lý logic Xóa Nhà cung cấp
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Bắt buộc phải chọn 1 nhà cung cấp mới được xóa
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // HỘP THOẠI XÁC NHẬN - Bắt buộc phải có để tránh xóa nhầm
                DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp '{txtTenNCC.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    int maNCC = Convert.ToInt32(txtMaNCC.Text);

                    if (bus_NhaCungCap.XoaNhaCungCap(maNCC) == true)
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp thành công!", "Thông báo");
                        LoadDanhSach();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Xóa không thành công! Có thể nhà cung cấp này đang có dữ liệu trong kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Nút dọn dẹp form
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}