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
        }

        /// <summary>
        /// Xử lý logic Thêm một sản phẩm mới vào hệ thống
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra tính hợp lệ: Bắt buộc phải chọn Danh mục và Nhà cung cấp
                if (cbDanhMuc.SelectedValue == null || cbNhaCungCap.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Danh mục và Nhà cung cấp!");
                    return;
                }

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
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
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
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData(); // Load lại GridView để thấy thông tin mới
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Hãy kiểm tra lại Procedure SQL.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        /// <summary>
        /// Xử lý logic Xóa sản phẩm
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận để tránh xóa nhầm
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int ma = int.Parse(txtMaSanPham.Text);
                if (bus.Xoa(ma))
                {
                    LoadData(); // Refresh bảng
                    btnLamMoi_Click(null, null); // Xóa thông tin trên các TextBox
                }
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
                    MaSanPham = int.Parse(row.Cells["MaSanPham"].Value.ToString()),
                    TenSanPham = row.Cells["TenSanPham"].Value.ToString(),
                    GiaBan = decimal.Parse(row.Cells["GiaBan"].Value.ToString()),
                    HinhAnh = row.Cells["HinhAnh"].Value.ToString(),
                    ChiTiet = row.Cells["ChiTiet"].Value?.ToString() ?? "",
                    MaDanhMuc = int.Parse(row.Cells["MaDanhMuc"].Value.ToString()),
                    MaNCC = int.Parse(row.Cells["MaNCC"].Value.ToString()),
                    GiaNhap = decimal.Parse(row.Cells["GiaNhap"].Value.ToString())
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

        /// <summary>
        /// Bắt sự kiện click vào bảng - DÙNG ĐỂ LOAD ẢNH ONLINE TỪ LINK WEB (LoadAsync)
        /// </summary>
        private void dgvsanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo người dùng click vào một dòng hợp lệ
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvsanpham.Rows[e.RowIndex];

                // Lấy đường dẫn (URL) ảnh từ cột "HinhAnh"
                string imageUrl = row.Cells["HinhAnh"].Value?.ToString();

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    try
                    {
                        // Bắt buộc URL phải bắt đầu bằng http hoặc https thì mới chạy LoadAsync
                        if (imageUrl.StartsWith("http"))
                        {
                            // LoadAsync giúp ứng dụng không bị đơ/treo trong lúc chờ tải ảnh từ mạng về
                            pbAnhSanPham.LoadAsync(imageUrl);
                            pbAnhSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch (Exception)
                    {
                        // Nếu mạng lỗi hoặc link chết thì xóa hình
                        pbAnhSanPham.Image = null;
                    }
                }
            }
        }
    }
}