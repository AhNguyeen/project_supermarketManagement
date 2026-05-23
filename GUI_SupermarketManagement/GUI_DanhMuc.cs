using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BUS_SupermarketManagement;
using ET_SupermarketManagement;

namespace GUI_SupermarketManagement
{
    public partial class GUI_DanhMuc : Form
    {
        // Khởi tạo đối tượng tầng BUS để tương tác với Cơ sở dữ liệu
        BUS_DanhMuc bus_DanhMuc = new BUS_DanhMuc();

        public GUI_DanhMuc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện khởi chạy đầu tiên khi Form Quản Lý Danh Mục được mở
        /// </summary>
        private void GUI_DanhMuc_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        /// <summary>
        /// Đổ dữ liệu danh sách danh mục lên DataGridView
        /// (Tách thành hàm riêng để dễ gọi lại khi Thêm/Sửa/Xóa thành công)
        /// </summary>
        private void LoadDanhSach()
        {
            dgvDanhMuc.DataSource = bus_DanhMuc.LayDanhSach();
        }

        /// <summary>
        /// Hàm dọn dẹp các TextBox về trạng thái trống
        /// </summary>
        private void ClearForm()
        {
            txtMaDanhMuc.Clear();
            txtTenDanhMuc.Clear();
            rtxtMoTa.Clear();
            txtTenDanhMuc.Focus(); // Đưa con trỏ nhấp nháy vào ô Tên Danh Mục
        }

        /// <summary>
        /// Xử lý logic Thêm một Danh mục mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra trước khi thêm 
            if (ValidateInput() == false)
            {
                // Nếu có TextBox nào bị bỏ trống hoặc chứa ký tự đặc biệt thì sẽ không thực hiện thêm
                return;
            }

            // Bắt lỗi: Không cho phép thêm nếu bỏ trống Tên Danh Mục
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đóng gói dữ liệu (Chỉ truyền Tên và Mô tả, Mã sẽ tự tăng trong SQL)
            ET_DanhMuc et_DanhMuc = new ET_DanhMuc(txtTenDanhMuc.Text, rtxtMoTa.Text);

            if (bus_DanhMuc.ThemDanhMuc(et_DanhMuc) == true)
            {
                MessageBox.Show("Thêm danh mục mới thành công!", "Thông báo");
                LoadDanhSach();
                ClearForm(); // Xóa form để tiếp tục thêm
            }
            else
            {
                MessageBox.Show("Lỗi: Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Xử lý logic Xóa Danh mục
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn danh mục nào để xóa chưa
            if (string.IsNullOrWhiteSpace(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa từ danh sách bên dưới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Hộp thoại xác nhận chống xóa nhầm
                DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục '" + txtTenDanhMuc.Text + "' không?", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    // Thực hiện lệnh Xóa
                    if (bus_DanhMuc.XoaDanhMuc(txtMaDanhMuc.Text) == true)
                    {
                        MessageBox.Show("Xóa danh mục thành công!", "Thông báo");
                        LoadDanhSach();
                        ClearForm();
                    }
                    else
                    {
                        // Cảnh báo nếu danh mục đang chứa sản phẩm (Ràng buộc Khóa Ngoại trong CSDL)
                        MessageBox.Show("Không thể xóa! Có sản phẩm đang thuộc danh mục này.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Đẩy ngược dữ liệu từ DataGridView lên TextBox để chuẩn bị Sửa hoặc Xóa
        /// </summary>
        private void dgvDanhMuc_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra người dùng click có trúng vào dòng dữ liệu hay không (Tránh văng app)
                if (dgvDanhMuc.CurrentRow != null && dgvDanhMuc.CurrentCell.RowIndex >= 0)
                {
                    int index = dgvDanhMuc.CurrentCell.RowIndex;

                    txtMaDanhMuc.Text = dgvDanhMuc.Rows[index].Cells[0].Value.ToString();
                    txtTenDanhMuc.Text = dgvDanhMuc.Rows[index].Cells[1].Value.ToString();
                    rtxtMoTa.Text = dgvDanhMuc.Rows[index].Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Thông báo");
            }
        }

        /// <summary>
        /// Làm mới các ô nhập liệu
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Xử lý logic Cập nhật (Sửa) thông tin Danh mục
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn danh mục để sửa chưa (Đã sửa lại thông báo do copy nhầm từ nút Xóa)
            if (string.IsNullOrWhiteSpace(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra không để trống tên
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Tên danh mục không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin danh mục '" + txtTenDanhMuc.Text + "' không?", "Xác nhận Sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                int maDanhMuc = Convert.ToInt32(txtMaDanhMuc.Text);

                // Gói cả Mã Danh Mục vào để lệnh UPDATE biết sửa ở dòng nào
                ET_DanhMuc et_DanhMuc = new ET_DanhMuc(maDanhMuc, txtTenDanhMuc.Text, rtxtMoTa.Text);

                if (bus_DanhMuc.SuaDanhMuc(et_DanhMuc) == true)
                {
                    MessageBox.Show("Sửa thông tin danh mục thành công!", "Thông báo");
                    LoadDanhSach();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Lỗi: Sửa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        //private bool KiemTraTrungTenDanhMuc(string tenDanhMuc)
        //{
        //    // Gọi BUS để kiểm tra xem tên danh mục đã tồn tại chưa
        //    return bus_DanhMuc.KiemTraTrungTenDanhMuc(tenDanhMuc);
        //}

        private bool ValidateInput()
        {
            // Kiểm tra xem có TextBox nào đang bị bỏ trống hay không
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi thực hiện thao tác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Có TextBox bị bỏ trống
            }

            // Kiểm tra kí tự đặc biệt (Chỉ cho phép chữ, số và khoảng trắng)
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTenDanhMuc.Text, @"[^a-zA-Z0-9\s]"))
            {
                MessageBox.Show("Tên danh mục không được chứa ký tự đặc biệt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Có TextBox chứa ký tự đặc biệt
            }

            // Bắt giới hạn độ dài kí tự từ 3 đến 255 ký tự
            if (txtTenDanhMuc.Text.Length < 3 || txtTenDanhMuc.Text.Length > 255)
            {
                MessageBox.Show("Tên danh mục phải có độ dài từ 3 đến 255 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Độ dài không hợp lệ
            }

            return true; // Tất cả TextBox đều đã được điền
        }
    }
}