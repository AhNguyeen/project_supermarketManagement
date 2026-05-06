USE QuanLySieuThi;
GO

/* ================================================================
   1. BẢNG DANH MỤC (DanhMuc)
================================================================ */
-- CREATE
CREATE PROC sp_DanhMuc_Insert
    @TenDanhMuc NVARCHAR(255), @MoTa NVARCHAR(MAX)
AS BEGIN
    INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES (@TenDanhMuc, @MoTa)
END
GO

-- UPDATE
CREATE PROC sp_DanhMuc_Update
    @MaDanhMuc INT, @TenDanhMuc NVARCHAR(255), @MoTa NVARCHAR(MAX)
AS BEGIN
    UPDATE DanhMuc SET TenDanhMuc = @TenDanhMuc, MoTa = @MoTa WHERE MaDanhMuc = @MaDanhMuc
END
GO

-- DELETE
CREATE PROC sp_DanhMuc_Delete @MaDanhMuc INT
AS BEGIN
    DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc
END
GO

-- SEARCH (Lấy tất cả / Theo Mã / Theo Tên)
CREATE PROC sp_DanhMuc_Search
    @MaDanhMuc INT = NULL,
    @TenDanhMuc NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM DanhMuc
    WHERE (@MaDanhMuc IS NULL OR MaDanhMuc = @MaDanhMuc)
      AND (@TenDanhMuc IS NULL OR TenDanhMuc LIKE N'%' + @TenDanhMuc + '%')
END
GO

/* ================================================================
   2. BẢNG SẢN PHẨM (SanPham)
================================================================ */
CREATE PROC sp_SanPham_Insert
    @TenSanPham NVARCHAR(255), @ChiTiet NVARCHAR(500), @MaDanhMuc INT, @MaNCC INT, @GiaNhap DECIMAL(18,2), @GiaBan DECIMAL(18,2)
AS BEGIN
    INSERT INTO SanPham (TenSanPham, ChiTiet, MaDanhMuc, MaNCC, GiaNhap, GiaBan)
    VALUES (@TenSanPham, @ChiTiet, @MaDanhMuc, @MaNCC, @GiaNhap, @GiaBan)
END
GO
ALTER PROC sp_SanPham_Insert
    @TenSanPham NVARCHAR(255), @ChiTiet NVARCHAR(500), @HinhAnh NVARCHAR(255),
    @MaDanhMuc INT, @MaNCC INT, @GiaNhap DECIMAL(18,2), @GiaBan DECIMAL(18,2)
AS BEGIN
    INSERT INTO SanPham (TenSanPham, ChiTiet, HinhAnh, MaDanhMuc, MaNCC, GiaNhap, GiaBan, SoLuongTon)
    VALUES (@TenSanPham, @ChiTiet, @HinhAnh, @MaDanhMuc, @MaNCC, @GiaNhap, @GiaBan, 0)
END
GO

CREATE PROC sp_SanPham_Update
    @MaSanPham INT, @TenSanPham NVARCHAR(255), @ChiTiet NVARCHAR(500), @MaDanhMuc INT, @MaNCC INT, @GiaNhap DECIMAL(18,2), @GiaBan DECIMAL(18,2)
AS BEGIN
    UPDATE SanPham 
    SET TenSanPham = @TenSanPham, ChiTiet = @ChiTiet, MaDanhMuc = @MaDanhMuc, MaNCC = @MaNCC, GiaNhap = @GiaNhap, GiaBan = @GiaBan
    WHERE MaSanPham = @MaSanPham
END
GO
ALTER PROC sp_SanPham_Update
    @MaSanPham INT,
    @TenSanPham NVARCHAR(255),
    @ChiTiet NVARCHAR(500),
    @HinhAnh NVARCHAR(255), -- MỚI: Thêm để cập nhật ảnh
    @MaDanhMuc INT,
    @MaNCC INT,
    @GiaNhap DECIMAL(18,2),
    @GiaBan DECIMAL(18,2)
AS BEGIN
    UPDATE SanPham SET 
        TenSanPham = @TenSanPham, 
        ChiTiet = @ChiTiet, 
        HinhAnh = @HinhAnh, -- MỚI: Cập nhật đường dẫn ảnh
        MaDanhMuc = @MaDanhMuc, 
        MaNCC = @MaNCC, 
        GiaNhap = @GiaNhap, 
        GiaBan = @GiaBan
    WHERE MaSanPham = @MaSanPham
END
GO
ALTER PROC sp_SanPham_Insert
    @TenSanPham NVARCHAR(255), 
    @ChiTiet NVARCHAR(500), 
    @HinhAnh NVARCHAR(255), -- MỚI: Thêm để lưu ảnh
    @MaDanhMuc INT, 
    @MaNCC INT, 
    @GiaNhap DECIMAL(18,2), 
    @GiaBan DECIMAL(18,2)
AS BEGIN
    INSERT INTO SanPham (TenSanPham, ChiTiet, HinhAnh, MaDanhMuc, MaNCC, GiaNhap, GiaBan, SoLuongTon)
    VALUES (@TenSanPham, @ChiTiet, @HinhAnh, @MaDanhMuc, @MaNCC, @GiaNhap, @GiaBan, 0)
END
GO

CREATE PROC sp_SanPham_Delete @MaSanPham INT
AS BEGIN
    DELETE FROM SanPham WHERE MaSanPham = @MaSanPham
END
GO

CREATE PROC sp_SanPham_Search
    @MaSanPham INT = NULL,
    @TenSanPham NVARCHAR(255) = NULL
AS 
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Các thông tin cơ bản của sản phẩm
        SP.MaSanPham,
        SP.TenSanPham,
        SP.ChiTiet,
        SP.HinhAnh,
        SP.GiaNhap,
        SP.GiaBan,
        SP.SoLuongTon,
        
        -- Thông tin Danh Mục (Lấy cả Mã và Tên)
        SP.MaDanhMuc,       
        DM.TenDanhMuc,      
        
        -- Thông tin Nhà Cung Cấp (Lấy cả Mã và Tên)
        SP.MaNCC,           
        NCC.TenNCC          
        
    FROM SanPham SP
    JOIN DanhMuc DM ON SP.MaDanhMuc = DM.MaDanhMuc
    JOIN NhaCungCap NCC ON SP.MaNCC = NCC.MaNCC
    WHERE (@MaSanPham IS NULL OR SP.MaSanPham = @MaSanPham)
      AND (@TenSanPham IS NULL OR SP.TenSanPham LIKE N'%' + @TenSanPham + '%')
END
GO

/* ================================================================
   3. BẢNG KHÁCH HÀNG (KhachHang)
================================================================ */
CREATE PROC sp_KhachHang_Insert
    @HoTen NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255)
AS BEGIN
    INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi)
    VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi)
END
GO

CREATE PROC sp_KhachHang_Update
    @MaKhachHang INT, @HoTen NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255)
AS BEGIN
    UPDATE KhachHang 
    SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, Email = @Email, DiaChi = @DiaChi
    WHERE MaKhachHang = @MaKhachHang
END
GO

CREATE PROC sp_KhachHang_Delete @MaKhachHang INT
AS BEGIN
    DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang
END
GO

CREATE PROC sp_KhachHang_Search
    @MaKhachHang INT = NULL,
    @HoTen NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM KhachHang
    WHERE (@MaKhachHang IS NULL OR MaKhachHang = @MaKhachHang)
      AND (@HoTen IS NULL OR HoTen LIKE N'%' + @HoTen + '%')
END
GO

CREATE PROC sp_KhachHang_All
AS BEGIN
    SELECT * FROM KhachHang
END
GO
/* ================================================================
   4. BẢNG NHÂN VIÊN (NhanVien)
================================================================ */
CREATE PROC sp_NhanVien_Insert
    @HoTen NVARCHAR(255), @NgaySinh DATE, @GioiTinh NVARCHAR(10), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255), @ChucVu NVARCHAR(100), @Luong DECIMAL(18,2)
AS BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, ChucVu, Luong)
    VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @ChucVu, @Luong)
END
GO

CREATE PROC sp_NhanVien_Update
    @MaNhanVien INT, @HoTen NVARCHAR(255), @NgaySinh DATE, @GioiTinh NVARCHAR(10), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255), @ChucVu NVARCHAR(100), @Luong DECIMAL(18,2)
AS BEGIN
    UPDATE NhanVien 
    SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, Email = @Email, DiaChi = @DiaChi, ChucVu = @ChucVu, Luong = @Luong
    WHERE MaNhanVien = @MaNhanVien
END
GO

CREATE PROC sp_NhanVien_Delete @MaNhanVien INT
AS BEGIN
    DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien
END
GO

CREATE PROC sp_NhanVien_Search
    @MaNhanVien INT = NULL,
    @HoTen NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM NhanVien
    WHERE (@MaNhanVien IS NULL OR MaNhanVien = @MaNhanVien)
      AND (@HoTen IS NULL OR HoTen LIKE N'%' + @HoTen + '%')
END
GO

CREATE PROC sp_NhanVien_All
AS BEGIN
    SELECT * FROM NhanVien
END
GO
/* ================================================================
   5. BẢNG NHÀ CUNG CẤP (NhaCungCap)
================================================================ */
CREATE PROC sp_NhaCungCap_Insert
    @TenNCC NVARCHAR(255), @DiaChi NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100)
AS BEGIN
    INSERT INTO NhaCungCap (TenNCC, DiaChi, SoDienThoai, Email)
    VALUES (@TenNCC, @DiaChi, @SoDienThoai, @Email)
END
GO

CREATE PROC sp_NhaCungCap_Update
    @MaNCC INT, @TenNCC NVARCHAR(255), @DiaChi NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100)
AS BEGIN
    UPDATE NhaCungCap 
    SET TenNCC = @TenNCC, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email
    WHERE MaNCC = @MaNCC
END
GO

CREATE PROC sp_NhaCungCap_Delete @MaNCC INT
AS BEGIN
    DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC
END
GO

CREATE PROC sp_NhaCungCap_Search
    @MaNCC INT = NULL,
    @TenNCC NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM NhaCungCap
    WHERE (@MaNCC IS NULL OR MaNCC = @MaNCC)
      AND (@TenNCC IS NULL OR TenNCC LIKE N'%' + @TenNCC + '%')
END
GO

/* ================================================================
   6. BẢNG HÓA ĐƠN (HoaDon) - Lưu ý: Không có cột Tên để tìm, thay bằng tìm theo trạng thái hoặc ngày
================================================================ */
CREATE PROC sp_HoaDon_Insert
    @MaKhachHang INT, @MaNhanVien INT, @NgayLap DATETIME, @TongTien DECIMAL(18,2), @TrangThai NVARCHAR(50)
AS BEGIN
    INSERT INTO HoaDon (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai)
    VALUES (@MaKhachHang, @MaNhanVien, ISNULL(@NgayLap, GETDATE()), @TongTien, @TrangThai)
END
GO

CREATE PROC sp_HoaDon_Update
    @MaHoaDon INT, @MaKhachHang INT, @MaNhanVien INT, @TongTien DECIMAL(18,2), @TrangThai NVARCHAR(50)
AS BEGIN
    UPDATE HoaDon 
    SET MaKhachHang = @MaKhachHang, MaNhanVien = @MaNhanVien, TongTien = @TongTien, TrangThai = @TrangThai
    WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC   sp_HoaDon_Delete
    @MaHoaDon INT
AS BEGIN
    DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC sp_HoaDon_Search
@MaHoaDon INT = NULL
AS
BEGIN
    SELECT 
        HD.MaHoaDon,
        HD.MaKhachHang,           
        HD.MaNhanVien,
        KH.HoTen AS TenKhachHang, -- Lấy tên khách hàng và đổi tên cột thành TenKhachHang
        KH.SoDienThoai,
        NV.HoTen AS TenNhanVien,  -- Lấy tên nhân viên
        HD.NgayLap,
        HD.TongTien,
        HD.TrangThai
    FROM HoaDon HD
    JOIN KhachHang KH ON HD.MaKhachHang = KH.MaKhachHang
    JOIN NhanVien NV ON HD.MaNhanVien = NV.MaNhanVien
    WHERE (@MaHoaDon IS NULL OR MaHoaDon = @MaHoaDon)
END
GO

CREATE PROC sp_KiemTraTrungHoaDon @MaKhachHang INT, @MaNhanVien INT, @NgayLap DATETIME
AS BEGIN
    SELECT COUNT(*) 
    FROM HoaDon 
            WHERE MaKhachHang = @MaKhachHang 
            AND MaNhanVien = @MaNhanVien 
            AND NgayLap = @NgayLap
END
GO

CREATE PROC sp_ThemHoaDonVALuuMaHoaDonVuaTao @MaKhachHang INT, @MaNhanVien INT, @TongTien DECIMAL(18,2)
AS
BEGIN
                INSERT INTO HoaDon (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai) 
                VALUES (@MaKhachHang, @MaNhanVien, GETDATE(), @TongTien, N'Hoàn thành');
                
                -- Lệnh này cực kỳ quan trọng: Lấy ra cái ID (MaHoaDon) vừa tự động sinh ra
                SELECT SCOPE_IDENTITY();
END 
GO

CREATE PROCEDURE sp_CapNhatHoaDon
    @MaHoaDon INT,
    @TongTienMoi DECIMAL(18,2) -- Lưu ý: Đổi kiểu dữ liệu này cho khớp với cột TongTien trong bảng HoaDon của bạn
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Cập nhật lại tổng tiền mới cho Hóa Đơn
    UPDATE HoaDon
    SET TongTien = @TongTienMoi
    WHERE MaHoaDon = @MaHoaDon;

    -- 2. Xóa toàn bộ chi tiết hóa đơn cũ thuộc MaHD này
    -- (Để dọn chỗ cho C# insert lại danh sách các sản phẩm sau khi đã chỉnh sửa)
    DELETE FROM ChiTietHoaDon
    WHERE MaHoaDon = @MaHoaDon;
END
GO
/* ================================================================
   7. BẢNG CHI TIẾT HÓA ĐƠN (ChiTietHoaDon) 
================================================================ */
CREATE PROC sp_ChiTietHoaDon_Insert
    @MaHoaDon INT, @MaSanPham INT, @SoLuong INT, @DonGia DECIMAL(18,2)
AS BEGIN
    -- Cột ThanhTien là PERSISTED tính toán tự động nên không cần INSERT
    INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGia)
    VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @DonGia)
END
GO

CREATE PROC sp_ChiTietHoaDon_Update
    @MaChiTiet INT, @MaHoaDon INT, @MaSanPham INT, @SoLuong INT, @DonGia DECIMAL(18,2)
AS BEGIN
    UPDATE ChiTietHoaDon 
    SET MaHoaDon = @MaHoaDon, MaSanPham = @MaSanPham, SoLuong = @SoLuong, DonGia = @DonGia
    WHERE MaChiTiet = @MaChiTiet
END
GO

CREATE PROC sp_ChiTietHoaDon_Delete @MaChiTiet INT
AS BEGIN
    DELETE FROM ChiTietHoaDon WHERE MaChiTiet = @MaChiTiet
END
GO

CREATE PROC sp_ChiTietHoaDon_Search
    @MaChiTiet INT = NULL,
    @MaHoaDon INT = NULL
AS BEGIN
    SELECT * FROM ChiTietHoaDon
    WHERE (@MaChiTiet IS NULL OR MaChiTiet = @MaChiTiet)
      AND (@MaHoaDon IS NULL OR MaHoaDon = @MaHoaDon)
END
GO

--Tiếp nhận mã hóa đơn và hiển thị các thông tin cần thiết
CREATE PROCEDURE sp_HienThiChiTietHoaDon
    @MaHoaDon INT = NULL
AS
BEGIN
    -- SET NOCOUNT ON giúp tăng hiệu suất bằng cách chặn thông báo số dòng bị ảnh hưởng
    SET NOCOUNT ON;

    SELECT 
        CT.MaSanPham, 
        SP.TenSanPham, 
        CT.SoLuong, 
        CT.DonGia, 
        CT.ThanhTien
    FROM 
        ChiTietHoaDon CT
    INNER JOIN 
        SanPham SP ON CT.MaSanPham = SP.MaSanPham
    WHERE 
        CT.MaHoaDon = @MaHoaDon;
END
GO

CREATE PROC sp_CapNhatTongTienHoaDon @TongTien DECIMAL(18,2), @MaHoaDon INT
AS
BEGIN
    UPDATE HoaDon SET TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC sp_XoaChiTietHoaDon @MaHoaDon INT
AS
BEGIN
    DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon
END
GO

/*================================================================ */
CREATE PROC sp_TongDoanhThu
AS
SELECT SUM(HoaDon.TongTien) as TongDoanhThu
FROM HoaDon
WHERE TrangThai LIKE N'Hoàn thành'
GO

CREATE PROC sp_TongGiaNhapSanPham
AS
SELECT SUM(SanPham.GiaNhap) as TongGiaNhap
FROM SanPham
GO

CREATE PROC sp_LoiNhuan
AS
SELECT 
    SUM(CT.SoLuong * CT.DonGia) AS TongDoanhThu,
    SUM(CT.SoLuong * SP.GiaNhap) AS TongChiPhiNhap,
    SUM(CT.SoLuong * CT.DonGia) - SUM(CT.SoLuong * SP.GiaNhap) AS LoiNhuanThucTe
FROM HoaDon HD
JOIN ChiTietHoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
WHERE HD.TrangThai LIKE N'Hoàn thành';
GO

CREATE PROC sp_DemSoDonHang
AS
BEGIN
    -- Bạn có thể thêm WHERE TrangThai = N'Hoàn thành' nếu chỉ muốn đếm đơn thành công
    SELECT COUNT(MaHoaDon) FROM HoaDon
    WHERE TrangThai LIKE N'Hoàn Thành'
END
GO

CREATE PROC sp_ThongKeTonKho
    @NguongTonKho INT = 15 -- Số lượng dưới mức này sẽ bị cảnh báo "Sắp hết"
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Đánh số STT
        ROW_NUMBER() OVER(ORDER BY SP.SoLuongTon ASC) AS STT,
        
        SP.MaSanPham, -- Nên có mã SP để xử lý ngầm trong C#
        SP.TenSanPham, 
        DM.TenDanhMuc, 
        SP.SoLuongTon, 
        
        -- Cột trạng thái tự động đánh giá
        CASE 
            WHEN SP.SoLuongTon <= 0 THEN N'Hết hàng'
            WHEN SP.SoLuongTon <= @NguongTonKho THEN N'Sắp hết'
            ELSE N'Còn hàng'
        END AS TrangThai
        
    FROM SanPham SP
    JOIN DanhMuc DM ON SP.MaDanhMuc = DM.MaDanhMuc
    
    -- Sắp xếp tăng dần: Sản phẩm hết hàng (0) sẽ nổi lên đầu bảng
    ORDER BY SP.SoLuongTon ASC
END
GO

CREATE PROC sp_ThongKeDoanhThuTheoNgay
AS
BEGIN
    SELECT 
        CAST(NgayLap AS DATE) AS Ngay, -- Cắt bỏ phần giờ/phút, chỉ giữ lại Ngày/Tháng/Năm
        SUM(TongTien) AS DoanhThu      -- Tính tổng doanh thu trong ngày
    FROM HoaDon
    WHERE TrangThai LIKE N'Hoàn thành' -- Bỏ qua các đơn "Đã hủy"
    GROUP BY CAST(NgayLap AS DATE)     -- Gom nhóm theo ngày
    ORDER BY Ngay ASC;                 -- Sắp xếp tăng dần theo thời gian
END
GO

CREATE PROC sp_TopSanPhamBanChay
    @Top INT = 10 -- Mặc định sẽ lấy Top 5, bạn có thể truyền số khác vào (ví dụ 10) từ C#
AS
BEGIN
    SELECT TOP (@Top)
        -- Dùng hàm ROW_NUMBER để tự động sinh ra cột số thứ tự 1, 2, 3... dựa trên doanh thu
        ROW_NUMBER() OVER(ORDER BY SUM(CT.ThanhTien) DESC) AS [Xếp Hạng],
        
        SP.TenSanPham AS [Tên Sản Phẩm],
        
        -- Tính tổng số lượng đã bán
        SUM(CT.SoLuong) AS [Số Lượng],
        
        -- Tính tổng doanh thu (Vì bảng ChiTietHoaDon của bạn đã có cột ThanhTien Persisted)
        SUM(CT.ThanhTien) AS [Doanh Thu]
        
    FROM ChiTietHoaDon CT
    JOIN HoaDon HD ON CT.MaHoaDon = HD.MaHoaDon
    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
    
    -- Chỉ tính những hóa đơn đã thanh toán thành công
    WHERE HD.TrangThai = N'Hoàn thành'
    
    -- Bắt buộc phải Group By theo Sản phẩm thì mới dùng hàm SUM được
    GROUP BY SP.MaSanPham, SP.TenSanPham
    
    -- Sắp xếp giảm dần để người bán nhiều nhất lên đầu
    ORDER BY [Doanh Thu] DESC
END
GO

CREATE PROC sp_KiemTraSanPhamTrongDanhMuc(@MaDanhMuc INT)
AS
BEGIN
    SELECT COUNT(MaSanPham) AS TongSoSanPham
    FROM SanPham
    WHERE MaDanhMuc = @MaDanhMuc
END
GO

EXEC sp_KiemTraSanPhamTrongDanhMuc 5
GO

CREATE PROC sp_KiemTraSanPhamCuaNhaCungCap(@MaNCC INT)
AS
BEGIN
    SELECT COUNT(MaSanPham) AS TongSoSanPham
    FROM SanPham SP
    JOIN NhaCungCap NCC ON SP.MaNCC = NCC.MaNCC
    WHERE SP.MaNCC = @MaNCC
END
GO

CREATE PROC sp_InTenNhanVienChucVuLamKho 
AS 
BEGIN
    SELECT * 
    FROM NhanVien
    WHERE ChucVu = N'Nhân viên kho'
END
GO

CREATE PROC sp_TongHopTonKho
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SUM(SoLuongTon) AS TongSoLuongTonTatCa
    FROM SanPham
END
GO

CREATE PROC sp_ThongKeDoanhThuTheoDanhMuc
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DM.TenDanhMuc,
        ISNULL(SUM(CT.ThanhTien), 0) AS TongDoanhThu
    FROM DanhMuc DM
    JOIN SanPham SP ON DM.MaDanhMuc = SP.MaDanhMuc
    JOIN ChiTietHoaDon CT ON SP.MaSanPham = CT.MaSanPham
    JOIN HoaDon HD ON CT.MaHoaDon = HD.MaHoaDon
    -- (Tùy chọn) Có thể thêm điều kiện WHERE HD.NgayLap >= ... để lọc theo tháng hiện tại
    GROUP BY DM.TenDanhMuc
    -- Sắp xếp giảm dần để danh mục kiếm nhiều tiền nhất hiện lên đầu
    ORDER BY TongDoanhThu DESC 
END
GO
/* ================================================================
   8. BẢNG KHO (Kho) 
================================================================ */
CREATE PROC sp_Kho_Insert
     @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME, @GhiChu NVARCHAR(500)
AS BEGIN
    INSERT INTO Kho (TenKho, MaNhanVien, MaNCC, NgayNhap, GhiChu)
    VALUES (@TenKho, @MaNhanVien, @MaNCC, ISNULL(@NgayNhap, GETDATE()),  @GhiChu)
END
GO

CREATE PROC sp_Kho_Update
    @MaKho INT, @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME, @GhiChu NVARCHAR(500)
AS BEGIN
    UPDATE Kho 
    SET TenKho = @TenKho, MaNhanVien = @MaNhanVien, MaNCC = @MaNCC, NgayNhap = @NgayNhap, GhiChu = @GhiChu
    WHERE MaKho = @MaKho
END
GO

CREATE PROC sp_Kho_Delete @MaKho INT
AS BEGIN
    DELETE FROM Kho WHERE MaKho = @MaKho
END
GO

CREATE PROC sp_Kho_Search
AS BEGIN
    SELECT KH.MaKho, KH.TenKho, KH.MaNCC, NCC.TenNCC, KH.MaNhanVien, NV.HoTen, KH.NgayNhap, KH.GhiChu FROM Kho KH
    JOIN NhanVien NV ON NV.MaNhanVien = KH.MaNhanVien
    JOIN NhaCungCap NCC ON NCC.MaNCC = KH.MaNCC
END
GO
/* ================================================================
   8. BẢNG CHI TIET KHO (ChiTietKho) 
================================================================ */
CREATE PROC sp_ChiTietKho_Search
    @MaKho INT = NULL
AS 
BEGIN
    SELECT 
        CTK.MaSanPham, 
        SP.TenSanPham, 
        CTK.SoLuongNhap AS [Số Lượng Nhập Lần Này], -- Đổi tên cột cho dễ nhìn trên C#
        SP.SoLuongTon AS [Tồn Kho Hiện Tại],        -- Lấy thêm Số lượng tồn từ bảng Sản Phẩm
        CTK.GiaNhap
    FROM ChiTietKho CTK
    JOIN SanPham SP ON CTK.MaSanPham = SP.MaSanPham
    WHERE (@MaKho IS NULL OR CTK.MaKho = @MaKho)
END
GO

CREATE PROC sp_KiemTraSanPhamChuaCoTrongKho
AS 
BEGIN
    -- Lấy các thông tin cần thiết từ bảng Sản Phẩm
    SELECT 
        SP.MaSanPham, 
        SP.TenSanPham, 
        SP.GiaNhap,
        SP.SoLuongTon
    FROM SanPham SP
    WHERE SP.MaSanPham NOT IN (
        -- Mệnh đề này lấy ra tất cả các Mã Sản Phẩm đã từng được nhập vào kho
        SELECT DISTINCT MaSanPham 
        FROM ChiTietKho
    )
END
GO

CREATE PROC sp_KiemTraTrungKho @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME
AS BEGIN
    SELECT COUNT(*) 
    FROM Kho 
            WHERE TenKho = @TenKho 
            AND MaNhanVien = @MaNhanVien 
            AND MaNCC = @MaNCC 
            AND NgayNhap = @NgayNhap
END
GO

EXEC sp_InTenNhanVienChucVuLamKho

/* ================================================================
   9. BẢNG KHÁCH HÀNG (KhachHang) 
================================================================ */
