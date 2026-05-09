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
            // Bước 1: Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (KiemTraTinhHopLe() == false)
            {
                return; // Nếu dữ liệu không hợp lệ, dừng lại và không thực hiện thêm
            }
            else
            {
                // Bước 2: Đóng gói dữ liệu vào đối tượng ET
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
            if(KiemTraTinhHopLe() == false)
            {
                return; // Nếu dữ liệu không hợp lệ, dừng lại và không thực hiện sửa
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

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số (IsDigit) và các phím điều khiển như Backspace, Delete (IsControl)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ ký tự vừa gõ nếu không phải là số
            }
        }

        private bool KiemTraTinhHopLe()
        {
            // Kiểm tra tính hợp lệ của dữ liệu trước khi Thêm/Sửa
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtSoDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra định dạng email đơn giản (có chứa '@' và '.')
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //Kiểm tra số điện thoại chỉ chứa hơn 9 kí tự và ít hơn 11 kí tự
            if (txtSoDT.Text.Length < 9 || txtSoDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải có độ dài 10 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra giới hạn kí tự cho tên nhà cung cấp (từ 3 đến 255 ký tự)
            if (txtTenNCC.Text.Length < 3 || txtTenNCC.Text.Length > 255)
            {
                MessageBox.Show("Tên nhà cung cấp phải có độ dài từ 3 đến 255 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra giới hạn kí tự cho địa chỉ (từ 5 đến 255 ký tự)
            if (txtDiaChi.Text.Length < 5 || txtDiaChi.Text.Length > 255)
            {
                MessageBox.Show("Địa chỉ phải có độ dài từ 5 đến 255 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra giới hạn kí tự cho email (từ 5 đến 255 ký tự)
            if (txtEmail.Text.Length < 5 || txtEmail.Text.Length > 50)
            {
                MessageBox.Show("Email phải có độ dài từ 5 đến 50 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra kí tự đặc biệt trong tên nhà cung cấp (chỉ cho phép chữ, số và khoảng trắng)
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTenNCC.Text, @"[^a-zA-Z0-9\s]"))
            {
                MessageBox.Show("Tên nhà cung cấp không được chứa ký tự đặc biệt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra kí tự đặc biệt trong địa chỉ (chỉ cho phép chữ, số, dấu chấm, dấu phẩy và khoảng trắng)
            if (System.Text.RegularExpressions.Regex.IsMatch(txtDiaChi.Text, @"[^a-zA-Z0-9\s.,]"))
            {
                MessageBox.Show("Địa chỉ không được chứa ký tự đặc biệt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Dữ liệu hợp lệ
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
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
    }
}