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
    public partial class GUI_NhanVien : Form
    {
        public GUI_NhanVien()
        {
            InitializeComponent();
        }

        BUS_NhanVien bus_NhanVien = new BUS_NhanVien();
        BUS_ChucVu bus_ChucVu = new BUS_ChucVu();

        private void GUI_NhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVienVaChucVu();
        }

        private void LoadNhanVienVaChucVu()
        {
            dgvNhanVien.DataSource = bus_NhanVien.LayDanhSachNhanVien(); 
            cboChucVu.DataSource = bus_ChucVu.LayThongTinChucVu();
            cboChucVu.DisplayMember = "TenChucVu"; // Hiển thị tên chức vụ
            cboChucVu.ValueMember = "MaChucVu"; // Giá trị thực là mã chức vụ
        }

        private void ClearInputFields()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now; // Đặt ngày sinh về ngày hiện tại
            cboGioiTinh.SelectedIndex = -1; // Không chọn giới tính nào
            cboChucVu.SelectedIndex = -1; // Không chọn chức vụ nào
            txtLuong.Text = "";
            txtSoDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // BƯỚC BẢO VỆ: Đảm bảo DataGridView đang thực sự có dòng được chọn
            if (dgvNhanVien.CurrentRow != null && dgvNhanVien.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvNhanVien.CurrentRow;

                txtMaNV.Text = row.Cells["colMaNhanVien"].Value.ToString();
                txtTenNV.Text = row.Cells["colHoTen"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["colNgaySinh"].Value);
                cboGioiTinh.Text = row.Cells["colGioiTinh"].Value.ToString();
                cboChucVu.Text = row.Cells["colTenChucVu"].Value.ToString();
                txtLuong.Text = row.Cells["colLuong"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["colSoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["colEmail"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
            }
            else
            {
                // Nếu không có dòng nào được chọn, xóa sạch các trường nhập liệu để tránh nhầm lẫn
                ClearInputFields();
                MessageBox.Show("Không có nhân viên nào được chọn. Vui lòng chọn một nhân viên để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(ValidateInput() == false)
            {
                return; // Dừng lại nếu dữ liệu không hợp lệ
            }
            else
            {
                ET_NhanVien et_NhanVien = new ET_NhanVien
                {
                    TenNV = txtTenNV.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.Text,
                    MaChucVu = Convert.ToInt32(cboChucVu.SelectedValue),
                    Luong = Convert.ToInt32(txtLuong.Text),
                    SoDT = txtSoDienThoai.Text,
                    EMail = txtEmail.Text,
                    DiaChi = txtDiaChi.Text
                };
                bool success = bus_NhanVien.ThemNhanVien(et_NhanVien);
                if (success)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienVaChucVu(); // Tải lại danh sách nhân viên để hiển thị nhân viên mới thêm
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại. Vui lòng kiểm tra lại dữ liệu và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có nhân viên nào đang được chọn trong DataGridView hay không trước khi thực hiện sửa
            if(dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu không có nhân viên nào được chọn
            }
            else
            {
                if (ValidateInput() == false)
                {
                    return; // Dừng lại nếu dữ liệu không hợp lệ
                }
                else
                {
                    ET_NhanVien et_NhanVien = new ET_NhanVien
                    {
                        MaNV = Convert.ToInt32(txtMaNV.Text),
                        TenNV = txtTenNV.Text,
                        NgaySinh = dtpNgaySinh.Value,
                        GioiTinh = cboGioiTinh.Text,
                        MaChucVu = Convert.ToInt32(cboChucVu.SelectedValue),
                        Luong = Convert.ToDouble(txtLuong.Text),
                        SoDT = txtSoDienThoai.Text,
                        EMail = txtEmail.Text,
                        DiaChi = txtDiaChi.Text
                    };
                    bool success = bus_NhanVien.SuaNhanVien(et_NhanVien);
                    if (success)
                    {
                        MessageBox.Show("Sửa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhanVienVaChucVu(); // Tải lại danh sách nhân viên để hiển thị thông tin đã sửa
                    }
                    else
                    {
                        MessageBox.Show("Sửa thông tin nhân viên thất bại. Vui lòng kiểm tra lại dữ liệu và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có nhân viên nào đang được chọn trong DataGridView hay không trước khi thực hiện xóa
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu không có nhân viên nào được chọn
            }
            else
            {
                // Hiển thị hộp thoại xác nhận trước khi xóa để tránh xóa nhầm
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return; // Dừng lại nếu người dùng chọn không xóa
                }

                int maNhanVien = Convert.ToInt32(txtMaNV.Text);
                bool success = bus_NhanVien.XoaNhanVien(maNhanVien);
                if (success)
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienVaChucVu(); // Tải lại danh sách nhân viên để hiển thị sau khi xóa
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại. Vui lòng kiểm tra lại dữ liệu và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadNhanVienVaChucVu(); // Tải lại danh sách nhân viên để đảm bảo dữ liệu hiển thị luôn cập nhật
        }

        // Kiểm tra dữ liệu nhập vào trước khi thực hiện các thao tác Thêm/Sửa/Xóa để đảm bảo tính toàn vẹn và tránh lỗi không mong muốn
        private bool ValidateInput()
        {
            // Kiểm tra tên nhân viên không được để trống
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Tên nhân viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Giới hạn độ dài tên nhân viên (ví dụ: không quá 255 ký tự)
            if (txtTenNV.Text.Length > 255)
            {
                MessageBox.Show("Tên nhân viên không được vượt quá 255 ký tự.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Giới hạn độ dài địa chỉ (ví dụ: không quá 255 ký tự)
            if (txtDiaChi.Text.Length > 255)
            {
                MessageBox.Show("Địa chỉ không được vượt quá 255 ký tự.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra ngày sinh phải là ngày hợp lệ và không được lớn hơn ngày hiện tại
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra giới tính phải được chọn
            if (cboGioiTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra chức vụ phải được chọn
            if (cboChucVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra lương phải là số dương
            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || luong < 0)
            {
                MessageBox.Show("Lương phải là một số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra số điện thoại phải có định dạng hợp lệ (ví dụ: chỉ chứa số và có độ dài nhất định)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSoDienThoai.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Kiểm tra email phải có định dạng hợp lệ
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace trong trường Lương
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace trong trường Số Điện Thoại
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
