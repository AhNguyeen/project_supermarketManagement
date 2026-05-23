using BUS_SupermarketManagement;
using ET_SupermarketManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GUI_SupermarketManagement
{
    public partial class GUI_QLSP : Form
    {
        // Khởi tạo các đối tượng Thực thể (ET) và Nghiệp vụ (BUS)
        ET_QLSP sp = new ET_QLSP();
        BUS_QLSP bus = new BUS_QLSP();
        BUS_DanhMuc busDM = new BUS_DanhMuc();
        BUS_NhaCungCap busNCC = new BUS_NhaCungCap();

        public GUI_QLSP()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện khởi chạy đầu tiên khi Form Quản lý Sản phẩm được mở
        /// </summary>
        private void GUI_QLSP_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Hàm đổ dữ liệu vào các ComboBox để người dùng chọn thay vì phải nhập tay
        /// </summary>
        private void LoadComboBox()
        {
            // Đổ dữ liệu vào ComboBox Danh Mục
            cbDanhMuc.DataSource = busDM.LayDanhSach();
            cbDanhMuc.DisplayMember = "TenDanhMuc"; // Hiển thị tên
            cbDanhMuc.ValueMember = "MaDanhMuc";    // Lưu ngầm mã ID

            // Đổ dữ liệu vào ComboBox Nhà Cung Cấp
            cbNhaCungCap.DataSource = busNCC.LayDanhSach();
            cbNhaCungCap.DisplayMember = "TenNCC";
            cbNhaCungCap.ValueMember = "MaNCC";
        }

        /// <summary>
        /// Hàm làm mới toàn bộ dữ liệu trên giao diện
        /// </summary>
        private void LoadData()
        {
            LoadComboBox();
            dgvsanpham.DataSource = bus.LayDS();
            dgvsanpham.ClearSelection();
        }

        /// <summary>
        /// Xử lý logic Thêm một sản phẩm mới vào hệ thống
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // BƯỚC 1: Gọi hàm bắt lỗi
                if (ValidateInput() == false)
                {
                    return; // Dừng lại ngay lập tức nếu có lỗi nhập liệu
                }

                // BƯỚC 2: Nếu qua được ải trên, thì tiến hành gom dữ liệu và gọi xuống BUS / DAL
                // 2. Thu thập dữ liệu từ các ô nhập liệu
                sp.TenSanPham = txtTenSanPham.Text;
                sp.ChiTiet = rtxtMoTa.Text;
                sp.MaDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue);
                sp.MaNCC = Convert.ToInt32(cbNhaCungCap.SelectedValue);
                sp.GiaNhap = decimal.Parse(txtGiaNhap.Text);
                sp.GiaBan = decimal.Parse(txtGiaBan.Text);

                // 3. XỬ LÝ LƯU HÌNH ẢNH (OFFLINE)
                // Lấy tên file ảnh từ Tag của PictureBox (nếu người dùng có chọn ảnh)
                if (pbAnhSanPham.Tag != null)
                {
                    string nguon = pbAnhSanPham.Tag.ToString(); // Đường dẫn file gốc người dùng vừa chọn
                    string tenFile = Path.GetFileName(nguon); // Cắt lấy tên file (ví dụ: sp1.jpg)

                    // Tạo đường dẫn tới thư mục "Images" nằm trong thư mục chạy của phần mềm (bin/Debug/Images)
                    string thuMucAnh = Path.Combine(Application.StartupPath, "Images");

                    // Nếu chưa có thư mục Images thì tự động tạo mới
                    if (!Directory.Exists(thuMucAnh)) Directory.CreateDirectory(thuMucAnh);

                    // Đường dẫn đích để copy file vào
                    string dich = Path.Combine(thuMucAnh, tenFile);

                    // Copy ảnh từ máy người dùng vào folder dự án (nếu ảnh chưa tồn tại)
                    if (!File.Exists(dich)) File.Copy(nguon, dich);

                    // CHỈ LƯU TÊN FILE vào cơ sở dữ liệu để nhẹ máy
                    sp.HinhAnh = tenFile;
                }
                else
                {
                    sp.HinhAnh = ""; // Nếu không chọn ảnh thì lưu chuỗi rỗng
                }

                // 4. Gọi lệnh Thêm và kiểm tra kết quả
                if (bus.Them(sp) == true)
                {
                    MessageBox.Show("Thêm sản phẩm mới thành công!");
                    dgvsanpham.DataSource = bus.LayDS(); // Load lại bảng để thấy sản phẩm mới
                    btnLamMoi_Click(null, null); // Xóa sạch các form nhập để chuẩn bị thêm tiếp
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        /// <summary>
        /// Xử lý logic Sửa thông tin sản phẩm
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã chọn sản phẩm để sửa chưa (dựa vào ô Mã Sản Phẩm)
                if (string.IsNullOrEmpty(txtMaSanPham.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra nội dung sửa trước khi sửa
                if (ValidateInput() == false)
                {
                    return;
                }

                // 1. Gán dữ liệu cho đối tượng sp
                sp.MaSanPham = int.Parse(txtMaSanPham.Text);
                sp.TenSanPham = txtTenSanPham.Text;
                sp.ChiTiet = rtxtMoTa.Text;
                sp.MaDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue);
                sp.MaNCC = Convert.ToInt32(cbNhaCungCap.SelectedValue);
                sp.GiaNhap = decimal.Parse(txtGiaNhap.Text);
                sp.GiaBan = decimal.Parse(txtGiaBan.Text);

                // MẸO: Toán tử "??" nghĩa là nếu pbAnhSanPham.Tag bị NULL thì sẽ gán chuỗi rỗng ""
                sp.HinhAnh = pbAnhSanPham.Tag?.ToString() ?? "";

                // 2. Gửi lệnh Sửa
                if (bus.Sua(sp))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Load lại GridView để thấy thông tin mới
                    btnLamMoi_Click(null, null); // Làm mới form
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Hãy kiểm tra lại Procedure SQL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        /// <summary>
        /// Xử lý logic Xóa sản phẩm
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // CHỐT CHẶN BẢO VỆ: Đảm bảo người dùng đã chọn sản phẩm (TextBox chứa Mã SP không bị rỗng)
                if (string.IsNullOrEmpty(txtMaSanPham.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng chạy code ngay lập tức
                }

                // Hiển thị hộp thoại xác nhận để tránh xóa nhầm
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Tự tin ép kiểu vì đã qua được chốt chặn IsNullOrEmpty ở trên
                    int ma = int.Parse(txtMaSanPham.Text);

                    if (bus.Xoa(ma))
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Refresh bảng
                        btnLamMoi_Click(null, null); // Xóa sạch thông tin trên các TextBox
                    }
                    else
                    {
                        // Báo lỗi thân thiện nếu sản phẩm đang kẹt khóa ngoại (Đã bán hoặc đã nhập kho)
                        MessageBox.Show("Xóa thất bại! Sản phẩm này đang tồn tại trong hệ thống (Hóa đơn/Phiếu nhập kho).", "Từ chối thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt mọi lỗi ngoại lệ bất ngờ khác, giúp ứng dụng không bao giờ bị Crash
                MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa sạch các ô nhập liệu (Reset Form)
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            rtxtMoTa.Clear();
            LoadData();
        }

        /// <summary>
        /// Mở form chi tiết và truyền đối tượng sản phẩm hiện tại sang form đó
        /// </summary>
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            // Chắc chắn rằng người dùng đã chọn 1 dòng trên DataGridView
            if (dgvsanpham.CurrentRow != null)
            {
                DataGridViewRow row = dgvsanpham.CurrentRow;

                // Khởi tạo một đối tượng sản phẩm và nhét toàn bộ dữ liệu của dòng đó vào
                ET_QLSP spChon = new ET_QLSP
                {
                    MaSanPham = int.Parse(row.Cells["colMaSanPham"].Value.ToString()),
                    TenSanPham = row.Cells["colTenSanPham"].Value.ToString(),
                    GiaBan = decimal.Parse(row.Cells["colGiaBan"].Value.ToString()),
                    HinhAnh = row.Cells["colHinhAnh"].Value.ToString(),
                    ChiTiet = row.Cells["colChiTiet"].Value?.ToString() ?? "",
                    TenDanhMuc = row.Cells["colTenDanhMuc"].Value.ToString(),
                    TenNCC = row.Cells["colTenNCC"].Value.ToString(),
                    GiaNhap = decimal.Parse(row.Cells["colGiaNhap"].Value.ToString()),
                    SoLuongTon = int.Parse(row.Cells["colSoLuongTon"].Value.ToString())
                };

                // Gọi Form chi tiết và truyền dữ liệu sang
                QLChiTiet frmChiTiet = new QLChiTiet();
                frmChiTiet.LoadSanPham(spChon);
                frmChiTiet.ShowDialog();

                // Load lại data phòng trường hợp người dùng sửa dữ liệu bên form chi tiết
                LoadData();
            }
        }

        /// <summary>
        /// Mở hộp thoại chọn ảnh từ máy tính (khi click vào nút Thêm Hình Ảnh)
        /// </summary>
        private void btnThemHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // Lọc chỉ cho phép chọn các file định dạng hình ảnh
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";

            if (open.ShowDialog() == DialogResult.OK)
            {
                pbAnhSanPham.Image = Image.FromFile(open.FileName);
                pbAnhSanPham.Tag = open.FileName; // Lưu tạm đường dẫn gốc vào thuộc tính Tag
                pbAnhSanPham.SizeMode = PictureBoxSizeMode.StretchImage; // Ép ảnh vừa khung
            }
        }

        /// <summary>
        /// Chức năng tương tự nút thêm ảnh, mở hộp thoại khi click trực tiếp vào khung PictureBox
        /// </summary>
        private void pbAnhSanPham_Click(object sender, EventArgs e)
        {
            btnThemHinhAnh_Click(sender, e); // Gọi lại hàm trên cho gọn code
        }

        // =========================================================================================
        // LƯU Ý: HIỆN TẠI BẠN ĐANG CÓ 2 SỰ KIỆN CLICK VÀO BẢNG (dgvsanpham_Click & dgvsanpham_CellClick)
        // Một cái để đọc ảnh Offline (trong máy), một cái để đọc ảnh Online (link web).
        // =========================================================================================

        /// <summary>
        /// Bắt sự kiện click vào bảng - DÙNG ĐỂ LOAD ẢNH OFFLINE TỪ THƯ MỤC LÊN
        /// </summary>
        private void dgvsanpham_Click(object sender, EventArgs e)
        {
            int index = dgvsanpham.CurrentCell.RowIndex;

            // 1. Gán thông tin văn bản
            txtMaSanPham.Text = dgvsanpham.Rows[index].Cells[0].Value.ToString();
            txtTenSanPham.Text = dgvsanpham.Rows[index].Cells[1].Value.ToString();
            rtxtMoTa.Text = dgvsanpham.Rows[index].Cells[2].Value.ToString();

            // 2. Gán lựa chọn trên ComboBox
            cbDanhMuc.SelectedValue = dgvsanpham.Rows[index].Cells[4].Value;
            cbNhaCungCap.SelectedValue = dgvsanpham.Rows[index].Cells[5].Value;

            txtGiaNhap.Text = dgvsanpham.Rows[index].Cells[6].Value.ToString();
            txtGiaBan.Text = dgvsanpham.Rows[index].Cells[7].Value.ToString();

            // 3. XỬ LÝ LOAD ẢNH OFFLINE
            string tenFileAnh = dgvsanpham.Rows[index].Cells[3].Value.ToString();

            if (!string.IsNullOrEmpty(tenFileAnh))
            {
                try
                {
                    // Trỏ đường dẫn vào thư mục Images trong bin/Debug
                    string folderPath = Path.Combine(Application.StartupPath, "Images");
                    string fullPath = Path.Combine(folderPath, tenFileAnh);

                    // Nếu ảnh có tồn tại trong máy thì hiện lên
                    if (File.Exists(fullPath))
                    {
                        // Giải phóng bộ nhớ ảnh cũ trước khi nạp ảnh mới (Tránh lỗi File in use)
                        if (pbAnhSanPham.Image != null) pbAnhSanPham.Image.Dispose();

                        pbAnhSanPham.Image = Image.FromFile(fullPath);
                        pbAnhSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        pbAnhSanPham.Image = null; // Hoặc gán ảnh mặc định
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh offline: " + ex.Message);
                    pbAnhSanPham.Image = null;
                }
            }
            else
            {
                pbAnhSanPham.Image = null;
            }
        }

        private void dgvsanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo người dùng click vào một dòng hợp lệ (không click vào header)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvsanpham.Rows[e.RowIndex];

                try
                {
                    // =================================================================
                    // 1. ĐỔ DỮ LIỆU LÊN CÁC TEXTBOX THÔNG TIN CHUNG & GIÁ
                    // (Lưu ý: Thay đổi tên control txt..., cbo... cho đúng với tên bạn đặt)
                    // =================================================================

                    // Tên trong ngoặc kép vuông ["..."] phải TRÙNG với thuộc tính Name của cột trong DataGridView
                    txtMaSanPham.Text = row.Cells["colMaSanPham"].Value?.ToString();
                    txtTenSanPham.Text = row.Cells["colTenSanPham"].Value?.ToString();
                    rtxtMoTa.Text = row.Cells["colChiTiet"].Value?.ToString();
                    txtGiaNhap.Text = row.Cells["colGiaNhap"].Value?.ToString();
                    txtGiaBan.Text = row.Cells["colGiaBan"].Value?.ToString();
                    // =================================================================
                    // 2. ĐỔ DỮ LIỆU LÊN COMBOBOX TỪ CỘT MÃ (Cột đã bị ẩn)
                    // =================================================================
                    // Chúng ta lấy giá trị Mã Danh Mục và Mã NCC ẩn để gán vào SelectedValue
                    if (row.Cells["colMaDanhMuc"].Value != null)
                    {
                        cbDanhMuc.SelectedValue = row.Cells["colMaDanhMuc"].Value;
                    }

                    if (row.Cells["colMaNCC"].Value != null)
                    {
                        cbNhaCungCap.SelectedValue = row.Cells["colMaNCC"].Value;
                    }

                    // =================================================================
                    // 3. XỬ LÝ LOAD ẢNH SẢN PHẨM (Đã an toàn hơn)
                    // =================================================================
                    string imageUrl = row.Cells["colHinhAnh"].Value?.ToString();

                    if (!string.IsNullOrEmpty(imageUrl) && (imageUrl.StartsWith("http") || imageUrl.StartsWith("https")))
                    {
                        try
                        {
                            // LoadAsync giúp ứng dụng không bị đơ/treo
                            pbAnhSanPham.LoadAsync(imageUrl);
                            pbAnhSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        catch
                        {
                            // Lỗi mạng hoặc link chết
                            pbAnhSanPham.Image = null;
                        }
                    }
                    else
                    {
                        // Nếu URL rỗng hoặc không hợp lệ thì xóa hình cũ trên PictureBox
                        pbAnhSanPham.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. Cho phép nhập số (IsDigit), phím điều khiển như Backspace (IsControl) và dấu chấm/phẩy
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true; // Hủy bỏ ký tự vừa gõ nếu là chữ cái hoặc ký tự đặc biệt khác
            }

            // 2. Chống nhập đúp dấu thập phân (Chỉ cho phép 1 dấu chấm hoặc phẩy duy nhất)
            Control txt = sender as Control; // Lấy cái TextBox đang được gõ
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (txt.Text.Contains(".") || txt.Text.Contains(",")))
            {
                e.Handled = true; // Chặn lại nếu trong TextBox đã có dấu chấm/phẩy rồi
            }
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra không được để trống Tên Sản Phẩm
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            // 2. Tên sản phẩm phải có độ dài từ 3 đến 255 ký tự
            if (txtTenSanPham.Text.Length < 3 || txtTenSanPham.Text.Length > 255)
            {
                MessageBox.Show("Tên sản phẩm phải có độ dài từ 3 đến 255 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            // 3. Kiểm tra Tên Sản Phẩm không có ký tự đặc biệt (!@#$%^&...)
            Regex regexTen = new Regex(@"^[\p{L}\d\s\-\.\,\(\)]+$"); // Bổ sung dấu trừ, chấm, phẩy, ngoặc thường dùng trong sản phẩm
            if (!regexTen.IsMatch(txtTenSanPham.Text))
            {
                MessageBox.Show("Tên sản phẩm không được chứa ký tự đặc biệt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            // 4. Kiểm tra Danh mục và Nhà cung cấp phải được chọn
            if (cbDanhMuc.SelectedIndex < 0 || cbNhaCungCap.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Danh mục và Nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 5. Kiểm tra không được để trống Giá
            if (string.IsNullOrWhiteSpace(txtGiaNhap.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ giá nhập và giá bán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 6. Kiểm tra giá trị nhập vào phải là số hợp lệ và lớn hơn hoặc bằng 0
            double giaNhap, giaBan;
            if (!double.TryParse(txtGiaNhap.Text, out giaNhap) || giaNhap < 0 || !double.TryParse(txtGiaBan.Text, out giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá tiền nhập vào không hợp lệ hoặc nhỏ hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 7. Ràng buộc logic kinh doanh (Tùy chọn thêm)
            if (giaBan <= giaNhap)
            {
                MessageBox.Show("Cảnh báo: Giá bán đang nhỏ hơn hoặc bằng giá nhập!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Mọi thứ đều chuẩn xác
        }
    }
}