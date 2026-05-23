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
    public partial class GUI_KhachHang : Form
    {
        public GUI_KhachHang()
        {
            InitializeComponent();
        }
        BUS_KhachHang bus_KhachHang = new BUS_KhachHang();
        private void GUI_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Load dữ liệu khách hàng vào DataGridView khi form được hiển thị
        private void LoadData()
        {
            dgvKhachHang.DataSource = bus_KhachHang.layDanhSach();
            dgvKhachHang.ClearSelection();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Nếu click vào tiêu đề cột (RowIndex < 0), bỏ qua
            if (e.RowIndex < 0) return;
            
            // BƯỚC BẢO VỆ: Đảm bảo DataGridView đang thực sự có dòng được chọn
            if (dgvKhachHang.CurrentRow != null && dgvKhachHang.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvKhachHang.CurrentRow;

                txtMaKhachHang.Text = row.Cells["colMaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["colHoTen"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["colSoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["colEmail"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
            }
            else
            {
                // Nếu không có dòng nào được chọn, xóa sạch các trường nhập liệu để tránh nhầm lẫn
                ClearInputFields();
                MessageBox.Show("Không có khách hàng nào được chọn. Vui lòng chọn một khách hàng để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearInputFields()
        {
            // Xóa sạch các trường nhập liệu
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            LoadData(); // Tải lại dữ liệu để đảm bảo DataGridView luôn hiển thị thông tin mới nhất
        }

        // Xử lý sự kiện khi người dùng nhấn nút "Thêm" để thêm khách hàng mới vào hệ thống
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào trước khi thêm khách hàng mới
            if (ValidateInput() == true)
            {
                // Tạo một đối tượng khách hàng mới từ dữ liệu nhập vào
                ET_KhachHang kh = new ET_KhachHang
                {
                    HoTen = txtTenKhachHang.Text,
                    SDT = txtSoDienThoai.Text,
                    EMail = txtEmail.Text,
                    DiaChi = txtDiaChi.Text
                };
                // Thêm khách hàng mới vào hệ thống thông qua lớp BUS
                if (bus_KhachHang.themKhachHang(kh))
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadData(); // Tải lại dữ liệu để hiển thị khách hàng mới
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Xử lý sự kiện khi người dùng nhấn nút "Sửa" để cập nhật thông tin khách hàng đã chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào trước khi cập nhật thông tin khách hàng
            if (ValidateInput() == true)
            {
                // BƯỚC BẢO VỆ: Đảm bảo rằng có một khách hàng đang được chọn để cập nhật
                if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text))
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Tạo một đối tượng khách hàng mới từ dữ liệu nhập vào
                ET_KhachHang kh = new ET_KhachHang
                {
                    MaKH = int.Parse(txtMaKhachHang.Text),
                    HoTen = txtTenKhachHang.Text,
                    SDT = txtSoDienThoai.Text,
                    EMail = txtEmail.Text,
                    DiaChi = txtDiaChi.Text
                };
                // Cập nhật thông tin khách hàng trong hệ thống thông qua lớp BUS
                if (bus_KhachHang.capNhatKhachHang(kh))
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadData(); // Tải lại dữ liệu để hiển thị thông tin khách hàng đã được cập nhật
                }
                else
                {
                    MessageBox.Show("Cập nhật khách hàng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Xử lý sự kiện khi người dùng nhấn nút "Xóa" để xóa khách hàng đã chọn khỏi hệ thống
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // BƯỚC BẢO VỆ: Đảm bảo rằng có một khách hàng đang được chọn để xóa
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int maKH = int.Parse(txtMaKhachHang.Text);
            // Hiển thị hộp thoại xác nhận trước khi xóa khách hàng
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Xóa khách hàng khỏi hệ thống thông qua lớp BUS
                if (bus_KhachHang.xoaKhachHang(maKH))
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Tải lại dữ liệu để hiển thị danh sách khách hàng đã được cập nhật
                }
                else
                {
                    MessageBox.Show("Xóa khách hàng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        // Kiểm tra các trường nhập liệu trước khi thực hiện thêm hoặc sửa khách hàng
        private bool ValidateInput()
        {
            // Kiểm tra xem các trường bắt buộc đã được điền đầy đủ hay chưa
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra giới hạn độ dài của nhập tên khách hàng
            if (txtTenKhachHang.Text.Length > 255)
            {
                MessageBox.Show("Giới hạn độ dài tên khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra các kí tự đặc biệt trong tên khách hàng
            if (txtTenKhachHang.Text.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
            {
                MessageBox.Show("Tên khách hàng không được chứa kí tự đặc biệt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra xem số điện thoại có phải là một chuỗi số hợp lệ hay không
            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text) && !txtSoDienThoai.Text.All(ch => char.IsDigit(ch) || ch == '-' || ch == '+'))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa các chữ số, dấu gạch ngang hoặc dấu cộng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra độ dài của số điện thoại (ví dụ: tối thiểu 10 chữ số và tối đa 11 chữ số)
            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text) && (txtSoDienThoai.Text.Count(ch => char.IsDigit(ch)) < 10 || txtSoDienThoai.Text.Count(ch => char.IsDigit(ch)) > 11))
            {
                MessageBox.Show("Số điện thoại phải chứa từ 10 đến 11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra độ dài của Email (ví dụ: tối đa 255 ký tự)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text.Length > 255)
            {
                MessageBox.Show("Giới hạn độ dài email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra xem email có phải là một chuỗi hợp lệ hay không (có thể sử dụng regex để kiểm tra định dạng email)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Định dạng email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra xem địa chỉ có phải là một chuỗi hợp lệ hay không
            if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && txtDiaChi.Text.Length > 255)
            {
                MessageBox.Show("Giới hạn độ dài địa chỉ không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Có thể thêm các kiểm tra khác như định dạng email, độ dài số điện thoại, v.v.
            return true;
        }

        // Xử lý sự kiện khi người dùng nhập dữ liệu vào trường số điện thoại
        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho phép nhập số và một số ký tự đặc biệt như dấu gạch ngang hoặc dấu cộng
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '+')
            {
                e.Handled = true; // Ngăn chặn ký tự không hợp lệ được nhập vào
            }
        }

    }
}
