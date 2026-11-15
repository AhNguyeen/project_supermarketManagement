# SUPERMARKET MANAGEMENT SYSTEM
## Hệ thống quản lý siêu thị

## 1. Giới thiệu

**Supermarket Management System** là phần mềm quản lý siêu thị được xây dựng bằng ngôn ngữ C#, sử dụng Windows Forms để phát triển giao diện người dùng và Microsoft SQL Server để lưu trữ dữ liệu.

Hệ thống hỗ trợ quản lý các hoạt động cơ bản của siêu thị như quản lý nhân viên, khách hàng, sản phẩm, danh mục, nhà cung cấp, kho hàng, hóa đơn bán hàng, thanh toán và thống kê doanh thu.

Dự án được tổ chức theo mô hình kiến trúc 3 lớp kết hợp với lớp thực thể dữ liệu, giúp phân chia rõ ràng trách nhiệm giữa giao diện, xử lý nghiệp vụ, truy xuất dữ liệu và quản lý thông tin.

## 2. Công nghệ sử dụng

| Công nghệ | Mục đích |
|---|---|
| C# | Ngôn ngữ lập trình chính |
| Windows Forms | Xây dựng giao diện ứng dụng desktop |
| .NET Framework 4.7.2 | Nền tảng phát triển |
| Microsoft SQL Server | Quản lý cơ sở dữ liệu |
| ADO.NET | Kết nối và thao tác dữ liệu |
| Stored Procedure | Xử lý các thao tác dữ liệu trong SQL Server |
| Guna.UI2 WinForms | Hỗ trợ thiết kế giao diện |
| Guna Charts WinForms | Xây dựng biểu đồ thống kê |
| Crystal Reports | Thành phần hỗ trợ báo cáo |
| Visual Studio | Môi trường phát triển |

## 3. Kiến trúc hệ thống

Dự án áp dụng mô hình 3 lớp kết hợp với lớp thực thể ET.

```text
GUI_SupermarketManagement
          |
          v
BUS_SupermarketManagement
          |
          v
DAL_SupermarketManagement
          |
          v
     SQL Server

ET_SupermarketManagement
(Lớp thực thể dữ liệu được sử dụng giữa các tầng)
```

### 3.1. GUI – Presentation Layer

Thư mục: `GUI_SupermarketManagement`

Đây là tầng giao diện, chịu trách nhiệm hiển thị thông tin và tiếp nhận thao tác từ người dùng.

Các chức năng giao diện chính:

- Đăng nhập hệ thống.
- Giao diện menu quản lý.
- Quản lý nhân viên.
- Quản lý khách hàng.
- Quản lý danh mục sản phẩm.
- Quản lý sản phẩm.
- Quản lý nhà cung cấp.
- Quản lý kho.
- Quản lý hóa đơn.
- Thanh toán.
- Thống kê.

### 3.2. BUS – Business Logic Layer

Thư mục: `BUS_SupermarketManagement`

Đây là tầng xử lý nghiệp vụ, chịu trách nhiệm tiếp nhận yêu cầu từ GUI, kiểm tra và xử lý logic trước khi chuyển dữ liệu xuống DAL.

Các lớp xử lý nghiệp vụ bao gồm:

- `BUS_NhanVien`
- `BUS_KhachHang`
- `BUS_QLSP`
- `BUS_DanhMuc`
- `BUS_NhaCungCap`
- `BUS_Kho`
- `BUS_ChiTietKho`
- `BUS_HoaDon`
- `BUS_ChiTietHoaDon`
- `BUS_TaiKhoan`
- `BUS_ThanhToan`
- `BUS_ThongKe`
- `BUS_ChucVu`

### 3.3. DAL – Data Access Layer

Thư mục: `DAL_SupermarketManagement`

Đây là tầng truy xuất dữ liệu, chịu trách nhiệm kết nối SQL Server và thực hiện các thao tác với cơ sở dữ liệu.

Các chức năng chính:

- Mở và đóng kết nối cơ sở dữ liệu.
- Truy vấn dữ liệu.
- Thêm, sửa và xóa dữ liệu.
- Thực thi Stored Procedure.
- Trả kết quả truy vấn về tầng BUS.

Lớp `ConnectData` được sử dụng để hỗ trợ các thao tác kết nối và truy xuất dữ liệu.

### 3.4. ET – Entity Layer

Thư mục: `ET_SupermarketManagement`

Đây là tầng chứa các lớp thực thể đại diện cho dữ liệu trong hệ thống.

Một số lớp thực thể:

- `ET_NhanVien`
- `ET_KhachHang`
- `ET_QLSP`
- `ET_DanhMuc`
- `ET_NhaCungCap`
- `ET_Kho`
- `ET_ChiTietKho`
- `ET_HoaDon`
- `ET_ChiTietHoaDon`
- `ET_TaiKhoan`
- `ET_ThongKe`

## 4. Các chức năng chính

### 4.1. Đăng nhập và phân quyền

- Kiểm tra thông tin tài khoản đăng nhập.
- Lưu thông tin nhân viên đang đăng nhập.
- Phân biệt quyền Admin và User.
- Giới hạn quyền truy cập một số chức năng theo vai trò.
- Hỗ trợ đăng xuất và quay lại màn hình đăng nhập.

### 4.2. Quản lý nhân viên

- Quản lý thông tin nhân viên.
- Thêm, cập nhật và xóa nhân viên.
- Lưu thông tin cá nhân, số điện thoại, email và địa chỉ.
- Quản lý chức vụ và mức lương.

### 4.3. Quản lý khách hàng

- Quản lý danh sách khách hàng.
- Thêm, sửa và xóa thông tin khách hàng.
- Lưu họ tên, số điện thoại, email và địa chỉ.
- Hỗ trợ tra cứu thông tin khách hàng.

### 4.4. Quản lý danh mục

- Quản lý danh mục sản phẩm.
- Thêm, cập nhật và xóa danh mục.
- Lưu tên danh mục và mô tả.
- Phục vụ việc phân loại sản phẩm.

### 4.5. Quản lý sản phẩm

- Quản lý danh sách sản phẩm.
- Thêm, cập nhật và xóa sản phẩm.
- Quản lý tên sản phẩm và thông tin chi tiết.
- Quản lý hình ảnh sản phẩm.
- Liên kết sản phẩm với danh mục và nhà cung cấp.
- Theo dõi giá nhập, giá bán và số lượng tồn kho.

### 4.6. Quản lý nhà cung cấp

- Quản lý thông tin nhà cung cấp.
- Thêm, sửa và xóa nhà cung cấp.
- Lưu thông tin liên hệ.
- Liên kết nhà cung cấp với sản phẩm và hoạt động nhập kho.

### 4.7. Quản lý kho

- Quản lý thông tin kho hàng.
- Quản lý phiếu nhập kho.
- Quản lý chi tiết hàng hóa nhập kho.
- Theo dõi số lượng và giá nhập.
- Liên kết phiếu nhập với nhân viên và nhà cung cấp.

### 4.8. Quản lý hóa đơn

- Quản lý hóa đơn bán hàng.
- Theo dõi thông tin khách hàng và nhân viên lập hóa đơn.
- Quản lý chi tiết sản phẩm trong hóa đơn.
- Cập nhật thông tin hóa đơn.
- Theo dõi số lượng sản phẩm và thành tiền.

### 4.9. Thanh toán

- Hỗ trợ quy trình thanh toán hóa đơn.
- Tiếp nhận thông tin giao dịch từ giao diện thanh toán.
- Kết hợp dữ liệu hóa đơn và chi tiết hóa đơn trong quá trình xử lý.

### 4.10. Thống kê

- Thống kê tổng doanh thu.
- Thống kê lợi nhuận.
- Thống kê số lượng đơn hàng.
- Theo dõi số lượng sản phẩm tồn kho.
- Hiển thị biểu đồ doanh thu.
- Thống kê sản phẩm bán chạy.
- Thống kê doanh thu theo danh mục.

## 5. Cấu trúc thư mục

```text
project_supermarketManagement/
│
├── GUI_SupermarketManagement/
│   ├── GUI_Login.cs
│   ├── menu.cs
│   ├── GUI_NhanVien.cs
│   ├── GUI_KhachHang.cs
│   ├── GUI_DanhMuc.cs
│   ├── GUI_QLSP.cs
│   ├── GUI_NhaCungCap.cs
│   ├── GUI_QuanLyKho.cs
│   ├── GUI_ChiTietKho.cs
│   ├── GUI_QuanLyHoaDon.cs
│   ├── GUI_ChiTietHoaDon.cs
│   ├── GUI_ChinhSuaHoaDon.cs
│   ├── GUI_ThanhToan.cs
│   ├── GUI_ThongKe.cs
│   ├── UserSession.cs
│   ├── App.config
│   ├── Images/
│   ├── Resources/
│   └── Program.cs
│
├── BUS_SupermarketManagement/
│   ├── BUS_NhanVien.cs
│   ├── BUS_KhachHang.cs
│   ├── BUS_QLSP.cs
│   ├── BUS_DanhMuc.cs
│   ├── BUS_NhaCungCap.cs
│   ├── BUS_Kho.cs
│   ├── BUS_HoaDon.cs
│   ├── BUS_ThanhToan.cs
│   ├── BUS_ThongKe.cs
│   └── ...
│
├── DAL_SupermarketManagement/
│   ├── Connect/
│   │   └── ConnectData.cs
│   ├── DAL_NhanVien.cs
│   ├── DAL_KhachHang.cs
│   ├── DAL_QLSP.cs
│   ├── DAL_DanhMuc.cs
│   ├── DAL_NhaCungCap.cs
│   ├── DAL_Kho.cs
│   ├── DAL_HoaDon.cs
│   ├── DAL_ThanhToan.cs
│   └── ...
│
├── ET_SupermarketManagement/
│   ├── ET_NhanVien.cs
│   ├── ET_KhachHang.cs
│   ├── ET_QLSP.cs
│   ├── ET_DanhMuc.cs
│   ├── ET_NhaCungCap.cs
│   ├── ET_Kho.cs
│   ├── ET_HoaDon.cs
│   └── ...
│
├── database_Supermarket/
│   ├── Database_SieuThi_ver1.5.sql
│   ├── Data_SieuThi_ver1.6.sql
│   └── Store_SieuThi_ver1.6.sql
│
├── Setup_Supermarket/
│
├── project_supermarketManagement.sln
│
└── README.md
```

*Lưu ý: Cấu trúc trên trình bày các thành phần tiêu biểu, không liệt kê toàn bộ file Designer, Resource và Properties.*

## 6. Cài đặt và chạy chương trình

### 6.1. Yêu cầu môi trường

Cần chuẩn bị các công cụ sau:

- Windows 10 hoặc Windows 11.
- Visual Studio hỗ trợ .NET Framework 4.7.2.
- Microsoft SQL Server.
- SQL Server Management Studio (SSMS).
- .NET Framework 4.7.2 Developer Pack hoặc phiên bản tương thích.

### 6.2. Clone repository

Mở Terminal hoặc Git Bash và thực hiện:

```bash
git clone https://github.com/AhNguyeen/project_supermarketManagement.git
```

Di chuyển vào thư mục dự án:

```bash
cd project_supermarketManagement
```

Mở Solution bằng Visual Studio:

```text
project_supermarketManagement.sln
```

### 6.3. Khởi tạo cơ sở dữ liệu

Mở SQL Server Management Studio và thực hiện các script trong thư mục:

```text
database_Supermarket/
```

Thứ tự thực hiện:

**Bước 1: Tạo cơ sở dữ liệu và các bảng**

Chạy file:

```text
Database_SieuThi_ver1.5.sql
```

File này tạo cơ sở dữ liệu `QuanLySieuThi`, các bảng dữ liệu, khóa chính, khóa ngoại và những ràng buộc cần thiết.

**Bước 2: Tạo dữ liệu mẫu**

Chạy file:

```text
Data_SieuThi_ver1.6.sql
```

File này bổ sung dữ liệu nền tảng và dữ liệu mẫu cho hệ thống.

**Bước 3: Tạo Stored Procedure**

Chạy file:

```text
Store_SieuThi_ver1.6.sql
```

File này định nghĩa các Stored Procedure phục vụ các thao tác truy vấn, thêm, sửa, xóa và xử lý dữ liệu.

Lưu ý: Các script cần được chạy trên cùng cơ sở dữ liệu `QuanLySieuThi`. Nếu gặp lỗi do phiên bản script hoặc dữ liệu đã tồn tại, cần kiểm tra trạng thái database trước khi chạy lại.

### 6.4. Cấu hình kết nối SQL Server

Mở file:

```text
GUI_SupermarketManagement/App.config
```

Kiểm tra connection string có tên `myConnection`:

```xml
<connectionStrings>
    <add name="myConnection"
         connectionString="Data Source=YOUR_SERVER;Initial Catalog=QuanLySieuThi;Integrated Security=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Thay `YOUR_SERVER` bằng tên SQL Server Instance trên máy tính.

Ví dụ:

```text
Data Source=.\SQLEXPRESS
```

hoặc:

```text
Data Source=(localdb)\MSSQLLocalDB
```

Đảm bảo tên database trong connection string là `QuanLySieuThi`.

**Lưu ý bảo mật:** Không nên đưa mật khẩu SQL Server hoặc thông tin đăng nhập máy chủ thật lên repository Public. Hãy sử dụng cấu hình riêng cho môi trường cá nhân và thay đổi các thông tin xác thực đã từng bị công khai.

### 6.5. Khôi phục và kiểm tra thư viện

Sau khi mở Solution:

1. Kiểm tra các project trong Solution Explorer.
2. Khôi phục các NuGet package nếu Visual Studio yêu cầu.
3. Kiểm tra các thư viện Guna.UI2 và Guna Charts.
4. Đảm bảo các thư viện Crystal Reports tương thích đã được cài đặt nếu sử dụng chức năng báo cáo.
5. Kiểm tra các project reference giữa GUI, BUS, DAL và ET.

### 6.6. Chạy ứng dụng

1. Chọn project `GUI_SupermarketManagement` làm Startup Project.
2. Kiểm tra kết nối SQL Server.
3. Build Solution để kiểm tra lỗi biên dịch.
4. Nhấn `F5` hoặc chọn **Start** để chạy chương trình.
5. Đăng nhập bằng tài khoản đã được khởi tạo trong bảng `TaiKhoan`.

Thông tin tài khoản đăng nhập phụ thuộc vào dữ liệu hiện có trong database.

## 7. Cơ sở dữ liệu

Tên cơ sở dữ liệu:

```text
QuanLySieuThi
```

Một số bảng dữ liệu chính:

| Bảng | Chức năng |
|---|---|
| ChucVu | Lưu thông tin chức vụ |
| NhanVien | Quản lý nhân viên |
| TaiKhoan | Quản lý tài khoản đăng nhập và phân quyền |
| DanhMuc | Quản lý danh mục sản phẩm |
| SanPham | Quản lý thông tin sản phẩm |
| NhaCungCap | Quản lý nhà cung cấp |
| KhachHang | Quản lý khách hàng |
| Kho | Quản lý phiếu nhập kho |
| ChiTietKho | Quản lý chi tiết hàng hóa nhập kho |
| HoaDon | Quản lý hóa đơn bán hàng |
| ChiTietHoaDon | Quản lý chi tiết sản phẩm bán |

Ngoài các bảng trên, hệ thống còn sử dụng Stored Procedure và Trigger để hỗ trợ xử lý nghiệp vụ.

Một số Trigger hỗ trợ tự động cập nhật số lượng tồn kho khi nhập hàng, bán hàng và xử lý các thay đổi liên quan đến chi tiết kho.

## 8. Hướng phát triển

Một số định hướng có thể tiếp tục cải thiện:

- Hoàn thiện kiểm tra dữ liệu đầu vào tại các chức năng quản lý.
- Tăng cường bảo mật tài khoản và mật khẩu.
- Chuẩn hóa việc quản lý connection string.
- Hoàn thiện chức năng tìm kiếm và lọc dữ liệu.
- Mở rộng báo cáo doanh thu và lợi nhuận.
- Bổ sung lịch sử giao dịch và hoạt động người dùng.
- Tối ưu hiệu năng truy vấn và xử lý dữ liệu.
- Hoàn thiện quy trình quản lý nhập, xuất và tồn kho.

## 9. Tác giả và repository

**GitHub:** [AhNguyeen](https://github.com/AhNguyeen)

**Repository:** [project_supermarketManagement](https://github.com/AhNguyeen/project_supermarketManagement)

---

*Dự án được xây dựng phục vụ mục đích học tập và nghiên cứu về lập trình C#, kiến trúc phần mềm 3 lớp và quản lý cơ sở dữ liệu SQL Server.*
