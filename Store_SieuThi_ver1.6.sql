USE QuanLySieuThi;
GO

-- =========================================================================================
-- FILE TỔNG HỢP STORED PROCEDURE - VER 1.6 (ĐÃ CHUẨN HÓA BẢNG CHỨC VỤ & LÀM SẠCH CODE)
-- Mẹo cho Coder: Nhấn Ctrl + F và gõ các từ khóa trong ngoặc vuông [] để tìm nhanh.
-- =========================================================================================

/* ================================================================
   [TÌM KIẾM: CHỨC VỤ] - BẢNG CHỨC VỤ
================================================================ */
CREATE PROC sp_ChucVu_Insert @TenChucVu NVARCHAR(100)
AS BEGIN
    INSERT INTO ChucVu (TenChucVu) VALUES (@TenChucVu)
END
GO

CREATE PROC sp_ChucVu_Update @MaChucVu INT, @TenChucVu NVARCHAR(100)
AS BEGIN
    UPDATE ChucVu SET TenChucVu = @TenChucVu WHERE MaChucVu = @MaChucVu
END
GO

CREATE PROC sp_ChucVu_All
AS BEGIN
    SELECT * FROM ChucVu
END
GO

/* ================================================================
   [TÌM KIẾM: DANH MỤC] - BẢNG DANH MỤC
================================================================ */
CREATE PROC sp_DanhMuc_Insert @TenDanhMuc NVARCHAR(255), @MoTa NVARCHAR(MAX)
AS BEGIN
    INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES (@TenDanhMuc, @MoTa)
END
GO

CREATE PROC sp_DanhMuc_Update @MaDanhMuc INT, @TenDanhMuc NVARCHAR(255), @MoTa NVARCHAR(MAX)
AS BEGIN
    UPDATE DanhMuc SET TenDanhMuc = @TenDanhMuc, MoTa = @MoTa WHERE MaDanhMuc = @MaDanhMuc
END
GO

CREATE PROC sp_DanhMuc_Delete @MaDanhMuc INT
AS BEGIN
    DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc
END
GO

CREATE PROC sp_DanhMuc_Search @MaDanhMuc INT = NULL, @TenDanhMuc NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM DanhMuc
    WHERE (@MaDanhMuc IS NULL OR MaDanhMuc = @MaDanhMuc)
      AND (@TenDanhMuc IS NULL OR TenDanhMuc LIKE N'%' + @TenDanhMuc + '%')
END
GO

/* ================================================================
   [TÌM KIẾM: SẢN PHẨM] - BẢNG SẢN PHẨM 
   (Đã gộp Update/Insert Ảnh vào chung 1 hàm duy nhất)
================================================================ */
CREATE PROC sp_SanPham_Insert
    @TenSanPham NVARCHAR(255), 
    @ChiTiet NVARCHAR(500), 
    @HinhAnh NVARCHAR(255), 
    @MaDanhMuc INT, 
    @MaNCC INT, 
    @GiaNhap DECIMAL(18,2), 
    @GiaBan DECIMAL(18,2)
AS BEGIN
    INSERT INTO SanPham (TenSanPham, ChiTiet, HinhAnh, MaDanhMuc, MaNCC, GiaNhap, GiaBan, SoLuongTon)
    VALUES (@TenSanPham, @ChiTiet, @HinhAnh, @MaDanhMuc, @MaNCC, @GiaNhap, @GiaBan, 0)
END
GO

CREATE PROC sp_SanPham_Update
    @MaSanPham INT,
    @TenSanPham NVARCHAR(255),
    @ChiTiet NVARCHAR(500),
    @HinhAnh NVARCHAR(255), 
    @MaDanhMuc INT,
    @MaNCC INT,
    @GiaNhap DECIMAL(18,2),
    @GiaBan DECIMAL(18,2)
AS BEGIN
    UPDATE SanPham SET 
        TenSanPham = @TenSanPham, 
        ChiTiet = @ChiTiet, 
        HinhAnh = @HinhAnh, 
        MaDanhMuc = @MaDanhMuc, 
        MaNCC = @MaNCC, 
        GiaNhap = @GiaNhap, 
        GiaBan = @GiaBan
    WHERE MaSanPham = @MaSanPham
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
        SP.MaSanPham, SP.TenSanPham, SP.ChiTiet, SP.HinhAnh, SP.GiaNhap, SP.GiaBan, SP.SoLuongTon,
        SP.MaDanhMuc, DM.TenDanhMuc, SP.MaNCC, NCC.TenNCC          
    FROM SanPham SP
    JOIN DanhMuc DM ON SP.MaDanhMuc = DM.MaDanhMuc
    JOIN NhaCungCap NCC ON SP.MaNCC = NCC.MaNCC
    WHERE (@MaSanPham IS NULL OR SP.MaSanPham = @MaSanPham)
      AND (@TenSanPham IS NULL OR SP.TenSanPham LIKE N'%' + @TenSanPham + '%')
END
GO

/* ================================================================
   [TÌM KIẾM: KHÁCH HÀNG] - BẢNG KHÁCH HÀNG
================================================================ */
CREATE PROC sp_KhachHang_Insert @HoTen NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255)
AS BEGIN
    INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi) VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi)
END
GO

CREATE PROC sp_KhachHang_Update @MaKhachHang INT, @HoTen NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100), @DiaChi NVARCHAR(255)
AS BEGIN
    UPDATE KhachHang SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, Email = @Email, DiaChi = @DiaChi WHERE MaKhachHang = @MaKhachHang
END
GO

CREATE PROC sp_KhachHang_Delete @MaKhachHang INT
AS BEGIN
    DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang
END
GO

CREATE PROC sp_KhachHang_Search @MaKhachHang INT = NULL, @HoTen NVARCHAR(255) = NULL
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
   [TÌM KIẾM: NHÂN VIÊN] - BẢNG NHÂN VIÊN (ĐÃ CẬP NHẬT MÃ CHỨC VỤ)
================================================================ */
CREATE PROC sp_NhanVien_Insert
    @HoTen NVARCHAR(255), @NgaySinh DATE, @GioiTinh NVARCHAR(10), @SoDienThoai VARCHAR(20), 
    @Email VARCHAR(100), @DiaChi NVARCHAR(255), @MaChucVu INT, @Luong DECIMAL(18,2)
AS BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, MaChucVu, Luong)
    VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @MaChucVu, @Luong)
END
GO

CREATE PROC sp_NhanVien_Update
    @MaNhanVien INT, @HoTen NVARCHAR(255), @NgaySinh DATE, @GioiTinh NVARCHAR(10), @SoDienThoai VARCHAR(20), 
    @Email VARCHAR(100), @DiaChi NVARCHAR(255), @MaChucVu INT, @Luong DECIMAL(18,2)
AS BEGIN
    UPDATE NhanVien 
    SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, 
        Email = @Email, DiaChi = @DiaChi, MaChucVu = @MaChucVu, Luong = @Luong
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
    SELECT NV.MaNhanVien, NV.HoTen, NV.NgaySinh, NV.GioiTinh, NV.SoDienThoai, NV.Email, NV.DiaChi, 
           NV.Luong, NV.MaChucVu, CV.TenChucVu
    FROM NhanVien NV
    LEFT JOIN ChucVu CV ON NV.MaChucVu = CV.MaChucVu
    WHERE (@MaNhanVien IS NULL OR NV.MaNhanVien = @MaNhanVien)
      AND (@HoTen IS NULL OR NV.HoTen LIKE N'%' + @HoTen + '%')
END
GO

CREATE PROC sp_NhanVien_All
AS BEGIN
    SELECT NV.MaNhanVien, NV.HoTen, NV.NgaySinh, NV.GioiTinh, NV.SoDienThoai, NV.Email, NV.DiaChi, 
           NV.Luong, NV.MaChucVu, CV.TenChucVu
    FROM NhanVien NV
    LEFT JOIN ChucVu CV ON NV.MaChucVu = CV.MaChucVu
END
GO

/* ================================================================
   [TÌM KIẾM: NHÀ CUNG CẤP] - BẢNG NHÀ CUNG CẤP
================================================================ */
CREATE PROC sp_NhaCungCap_Insert @TenNCC NVARCHAR(255), @DiaChi NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100)
AS BEGIN
    INSERT INTO NhaCungCap (TenNCC, DiaChi, SoDienThoai, Email) VALUES (@TenNCC, @DiaChi, @SoDienThoai, @Email)
END
GO

CREATE PROC sp_NhaCungCap_Update @MaNCC INT, @TenNCC NVARCHAR(255), @DiaChi NVARCHAR(255), @SoDienThoai VARCHAR(20), @Email VARCHAR(100)
AS BEGIN
    UPDATE NhaCungCap SET TenNCC = @TenNCC, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE MaNCC = @MaNCC
END
GO

CREATE PROC sp_NhaCungCap_Delete @MaNCC INT
AS BEGIN
    DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC
END
GO

CREATE PROC sp_NhaCungCap_Search @MaNCC INT = NULL, @TenNCC NVARCHAR(255) = NULL
AS BEGIN
    SELECT * FROM NhaCungCap
    WHERE (@MaNCC IS NULL OR MaNCC = @MaNCC)
      AND (@TenNCC IS NULL OR TenNCC LIKE N'%' + @TenNCC + '%')
END
GO

/* ================================================================
   [TÌM KIẾM: HÓA ĐƠN] - BẢNG HÓA ĐƠN
================================================================ */
CREATE PROC sp_HoaDon_Insert @MaKhachHang INT, @MaNhanVien INT, @NgayLap DATETIME, @TongTien DECIMAL(18,2), @TrangThai NVARCHAR(50)
AS BEGIN
    INSERT INTO HoaDon (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai)
    VALUES (@MaKhachHang, @MaNhanVien, ISNULL(@NgayLap, GETDATE()), @TongTien, @TrangThai)
END
GO

CREATE PROC sp_HoaDon_Update @MaHoaDon INT, @MaKhachHang INT, @MaNhanVien INT, @TongTien DECIMAL(18,2), @TrangThai NVARCHAR(50)
AS BEGIN
    UPDATE HoaDon SET MaKhachHang = @MaKhachHang, MaNhanVien = @MaNhanVien, TongTien = @TongTien, TrangThai = @TrangThai WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC sp_HoaDon_Delete @MaHoaDon INT
AS BEGIN
    DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC sp_HoaDon_Search @MaHoaDon INT = NULL
AS BEGIN
    SELECT 
        HD.MaHoaDon, HD.MaKhachHang, HD.MaNhanVien,
        KH.HoTen AS TenKhachHang, KH.SoDienThoai,
        NV.HoTen AS TenNhanVien, HD.NgayLap, HD.TongTien, HD.TrangThai
    FROM HoaDon HD
    JOIN KhachHang KH ON HD.MaKhachHang = KH.MaKhachHang
    JOIN NhanVien NV ON HD.MaNhanVien = NV.MaNhanVien
    WHERE (@MaHoaDon IS NULL OR MaHoaDon = @MaHoaDon)
END
GO

CREATE PROC sp_KiemTraTrungHoaDon @MaKhachHang INT, @MaNhanVien INT, @NgayLap DATETIME
AS BEGIN
    SELECT COUNT(*) FROM HoaDon 
    WHERE MaKhachHang = @MaKhachHang AND MaNhanVien = @MaNhanVien AND NgayLap = @NgayLap
END
GO

CREATE PROC sp_ThemHoaDonVALuuMaHoaDonVuaTao @MaKhachHang INT, @MaNhanVien INT, @TongTien DECIMAL(18,2)
AS BEGIN
    INSERT INTO HoaDon (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai) 
    VALUES (@MaKhachHang, @MaNhanVien, GETDATE(), @TongTien, N'Hoàn thành');
    SELECT SCOPE_IDENTITY(); -- Lấy mã ID vừa sinh
END 
GO

CREATE PROCEDURE sp_CapNhatHoaDon @MaHoaDon INT, @TongTienMoi DECIMAL(18,2)
AS BEGIN
    SET NOCOUNT ON;
    UPDATE HoaDon SET TongTien = @TongTienMoi WHERE MaHoaDon = @MaHoaDon;
    DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon;
END
GO

/* ================================================================
   [TÌM KIẾM: CHI TIẾT HÓA ĐƠN] - BẢNG CHI TIẾT HÓA ĐƠN
================================================================ */
CREATE PROC sp_ChiTietHoaDon_Insert @MaHoaDon INT, @MaSanPham INT, @SoLuong INT, @DonGia DECIMAL(18,2)
AS BEGIN
    INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGia) VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @DonGia)
END
GO

CREATE PROC sp_ChiTietHoaDon_Update @MaChiTiet INT, @MaHoaDon INT, @MaSanPham INT, @SoLuong INT, @DonGia DECIMAL(18,2)
AS BEGIN
    UPDATE ChiTietHoaDon SET MaHoaDon = @MaHoaDon, MaSanPham = @MaSanPham, SoLuong = @SoLuong, DonGia = @DonGia WHERE MaChiTiet = @MaChiTiet
END
GO

CREATE PROC sp_ChiTietHoaDon_Delete @MaChiTiet INT
AS BEGIN
    DELETE FROM ChiTietHoaDon WHERE MaChiTiet = @MaChiTiet
END
GO

CREATE PROCEDURE sp_HienThiChiTietHoaDon @MaHoaDon INT = NULL
AS BEGIN
    SET NOCOUNT ON;
    SELECT CT.MaSanPham, SP.TenSanPham, CT.SoLuong, CT.DonGia, CT.ThanhTien
    FROM ChiTietHoaDon CT
    INNER JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
    WHERE CT.MaHoaDon = @MaHoaDon;
END
GO

CREATE PROC sp_CapNhatTongTienHoaDon @TongTien DECIMAL(18,2), @MaHoaDon INT
AS BEGIN
    UPDATE HoaDon SET TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon
END
GO

CREATE PROC sp_XoaChiTietHoaDon @MaHoaDon INT
AS BEGIN
    DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon
END
GO

/* ================================================================
   [TÌM KIẾM: THỐNG KÊ] - NGHIỆP VỤ THỐNG KÊ DASHBOARD
================================================================ */
CREATE PROC sp_TongDoanhThu
AS SELECT SUM(HoaDon.TongTien) as TongDoanhThu FROM HoaDon WHERE TrangThai LIKE N'Hoàn thành'
GO

CREATE PROC sp_TongGiaNhapSanPham
AS SELECT SUM(SanPham.GiaNhap) as TongGiaNhap FROM SanPham
GO

CREATE PROC sp_LoiNhuan
AS BEGIN
    SELECT 
        SUM(CT.SoLuong * CT.DonGia) AS TongDoanhThu,
        SUM(CT.SoLuong * SP.GiaNhap) AS TongChiPhiNhap,
        SUM(CT.SoLuong * CT.DonGia) - SUM(CT.SoLuong * SP.GiaNhap) AS LoiNhuanThucTe
    FROM HoaDon HD
    JOIN ChiTietHoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
    WHERE HD.TrangThai LIKE N'Hoàn thành';
END
GO

CREATE PROC sp_DemSoDonHang
AS BEGIN
    SELECT COUNT(MaHoaDon) FROM HoaDon WHERE TrangThai LIKE N'Hoàn Thành'
END
GO

CREATE PROC sp_ThongKeTonKho @NguongTonKho INT = 15
AS BEGIN
    SET NOCOUNT ON;
    SELECT 
        ROW_NUMBER() OVER(ORDER BY SP.SoLuongTon ASC) AS STT,
        SP.MaSanPham, SP.TenSanPham, DM.TenDanhMuc, SP.SoLuongTon, 
        CASE 
            WHEN SP.SoLuongTon <= 0 THEN N'Hết hàng'
            WHEN SP.SoLuongTon <= @NguongTonKho THEN N'Sắp hết'
            ELSE N'Còn hàng'
        END AS TrangThai
    FROM SanPham SP
    JOIN DanhMuc DM ON SP.MaDanhMuc = DM.MaDanhMuc
    ORDER BY SP.SoLuongTon ASC
END
GO

CREATE PROC sp_ThongKeDoanhThuTheoNgay
AS BEGIN
    SELECT CAST(NgayLap AS DATE) AS Ngay, SUM(TongTien) AS DoanhThu 
    FROM HoaDon WHERE TrangThai LIKE N'Hoàn thành' 
    GROUP BY CAST(NgayLap AS DATE) ORDER BY Ngay ASC;
END
GO

CREATE PROC sp_TopSanPhamBanChay @Top INT = 10 
AS BEGIN
    SELECT TOP (@Top)
        ROW_NUMBER() OVER(ORDER BY SUM(CT.SoLuong) DESC) AS [Xếp Hạng],
        SP.TenSanPham AS [Tên Sản Phẩm],
        SUM(CT.SoLuong) AS [Số Lượng],
        SUM(CT.ThanhTien) AS [Doanh Thu]
    FROM ChiTietHoaDon CT
    JOIN HoaDon HD ON CT.MaHoaDon = HD.MaHoaDon
    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
    WHERE HD.TrangThai = N'Hoàn thành'
    GROUP BY SP.MaSanPham, SP.TenSanPham
    ORDER BY [Số Lượng] DESC
END
GO

CREATE PROC sp_ThongKeDoanhThuTheoDanhMuc
AS BEGIN
    SET NOCOUNT ON;
    SELECT 
        DM.TenDanhMuc, ISNULL(SUM(CT.ThanhTien), 0) AS TongDoanhThu
    FROM DanhMuc DM
    JOIN SanPham SP ON DM.MaDanhMuc = SP.MaDanhMuc
    JOIN ChiTietHoaDon CT ON SP.MaSanPham = CT.MaSanPham
    JOIN HoaDon HD ON CT.MaHoaDon = HD.MaHoaDon
    GROUP BY DM.TenDanhMuc
    ORDER BY TongDoanhThu DESC 
END
GO

CREATE PROC sp_TongHopTonKho
AS BEGIN
    SET NOCOUNT ON;
    SELECT SUM(SoLuongTon) AS TongSoLuongTonTatCa FROM SanPham
END
GO

CREATE PROC sp_InTenNhanVienChucVuLamKho 
AS BEGIN
    SELECT NV.MaNhanVien, NV.HoTen, CV.TenChucVu 
    FROM NhanVien NV
    JOIN ChucVu CV ON NV.MaChucVu = CV.MaChucVu
    WHERE CV.TenChucVu = N'Nhân viên kho'
END
GO

CREATE PROC sp_KiemTraSanPhamTrongDanhMuc(@MaDanhMuc INT)
AS BEGIN
    SELECT COUNT(MaSanPham) AS TongSoSanPham FROM SanPham WHERE MaDanhMuc = @MaDanhMuc
END
GO

CREATE PROC sp_KiemTraSanPhamCuaNhaCungCap(@MaNCC INT)
AS BEGIN
    SELECT COUNT(MaSanPham) AS TongSoSanPham FROM SanPham SP
    JOIN NhaCungCap NCC ON SP.MaNCC = NCC.MaNCC WHERE SP.MaNCC = @MaNCC
END
GO

/* ================================================================
   [TÌM KIẾM: KHO] - QUẢN LÝ NHẬP KHO
================================================================ */
CREATE PROC sp_Kho_Insert @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME, @GhiChu NVARCHAR(500)
AS BEGIN
    INSERT INTO Kho (TenKho, MaNhanVien, MaNCC, NgayNhap, GhiChu)
    VALUES (@TenKho, @MaNhanVien, @MaNCC, ISNULL(@NgayNhap, GETDATE()),  @GhiChu)
END
GO

CREATE PROC sp_Kho_Update @MaKho INT, @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME, @GhiChu NVARCHAR(500)
AS BEGIN
    UPDATE Kho SET TenKho = @TenKho, MaNhanVien = @MaNhanVien, MaNCC = @MaNCC, NgayNhap = @NgayNhap, GhiChu = @GhiChu WHERE MaKho = @MaKho
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

CREATE PROC sp_KiemTraTrungKho @TenKho NVARCHAR(50), @MaNhanVien INT, @MaNCC INT, @NgayNhap DATETIME
AS BEGIN
    SELECT COUNT(*) FROM Kho WHERE TenKho = @TenKho AND MaNhanVien = @MaNhanVien AND MaNCC = @MaNCC AND NgayNhap = @NgayNhap
END
GO

/* ================================================================
   [TÌM KIẾM: CHI TIẾT KHO] - QUẢN LÝ CHI TIẾT PHIẾU NHẬP
================================================================ */
CREATE PROC sp_ChiTietKho_Search @MaKho INT = NULL
AS BEGIN
    SELECT 
        CTK.MaChiTietKho, CTK.MaSanPham, SP.TenSanPham,
        CTK.SoLuongNhap, 
        SP.SoLuongTon, CTK.GiaNhap, CTK.ThanhTien
    FROM ChiTietKho CTK
    JOIN SanPham SP ON CTK.MaSanPham = SP.MaSanPham
    WHERE (@MaKho IS NULL OR CTK.MaKho = @MaKho)
END
GO

    CREATE PROCEDURE sp_ChiTietKho_Insert
    @MaKho INT, 
    @MaSanPham INT, 
    @SoLuongNhap INT, 
    @GiaNhap DECIMAL(18,2)
AS 
BEGIN
    INSERT INTO ChiTietKho (MaKho, MaSanPham, SoLuongNhap, GiaNhap)
    VALUES (@MaKho, @MaSanPham, @SoLuongNhap, @GiaNhap)
END
GO

CREATE PROCEDURE sp_ChiTietKho_Update
    @MaChiTietKho INT, 
    @MaKho INT, 
    @MaSanPham INT, 
    @SoLuongNhap INT, 
    @GiaNhap DECIMAL(18,2)
AS 
BEGIN
    UPDATE ChiTietKho 
    SET 
        MaKho = @MaKho, 
        MaSanPham = @MaSanPham, 
        SoLuongNhap = @SoLuongNhap, 
        GiaNhap = @GiaNhap
    WHERE MaChiTietKho = @MaChiTietKho
END
GO

CREATE PROCEDURE sp_ChiTietKho_UpdateSoLuong
    @MaKho INT,
    @MaSanPham INT,
    @SoLuongThem INT
AS 
BEGIN
    -- Nếu sản phẩm đã tồn tại trong phiếu, chỉ cần cộng thêm số lượng nhập vào
    UPDATE ChiTietKho
    SET SoLuongNhap = SoLuongNhap + @SoLuongThem
    WHERE MaKho = @MaKho AND MaSanPham = @MaSanPham
END
GO

CREATE PROCEDURE sp_ChiTietKho_DeleteSanPham
    @MaKho INT,
    @MaSanPham INT
AS 
BEGIN
    DELETE FROM ChiTietKho 
    WHERE MaKho = @MaKho AND MaSanPham = @MaSanPham
END
GO

CREATE PROC sp_KiemTraSanPhamChuaCoTrongKho
AS BEGIN
    SELECT SP.MaSanPham, SP.TenSanPham, SP.GiaNhap, SP.SoLuongTon
    FROM SanPham SP
    WHERE SP.MaSanPham NOT IN (SELECT DISTINCT MaSanPham FROM ChiTietKho)
END
GO

CREATE PROCEDURE sp_ChiTietKho_CheckDuplicate
    @MaKho INT,
    @MaSanPham INT
AS
BEGIN
    -- SET NOCOUNT ON giúp tăng hiệu suất bằng cách chặn thông báo số dòng bị ảnh hưởng
    SET NOCOUNT ON;

    -- Trả về số lượng dòng tìm thấy (Nếu > 0 nghĩa là đã bị trùng)
    SELECT COUNT(*) AS SoLuongTonTai
    FROM ChiTietKho 
    WHERE MaKho = @MaKho AND MaSanPham = @MaSanPham;
END
GO

/* ================================================================
   [TÌM KIẾM: TÀI KHOẢN] - ĐĂNG NHẬP (CẬP NHẬT TRẢ VỀ TÊN CHỨC VỤ)
================================================================ */
CREATE PROC sp_TaiKhoan_Login
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS 
BEGIN
    SELECT TK.MaNhanVien, TK.Username, TK.PhanQuyen, NV.HoTen, NV.MaChucVu, CV.TenChucVu
    FROM TaiKhoan TK
    JOIN NhanVien NV ON TK.MaNhanVien = NV.MaNhanVien
    LEFT JOIN ChucVu CV ON NV.MaChucVu = CV.MaChucVu
    WHERE TK.Username = @Username AND TK.Password = @Password
END;
GO

/* ================================================================
   [TÌM KIẾM: CHỨC VỤ] - HIỂN THỊ VAI TRÒ CỦA NHÂN VIÊN
================================================================ */
CREATE PROC sp_ChucVu_Search
AS
BEGIN
    SELECT * 
    FROM ChucVu
END
GO
