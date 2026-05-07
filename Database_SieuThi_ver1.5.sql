CREATE DATABASE QuanLySieuThi;
GO

USE QuanLySieuThi;
GO

-- ================================================================
-- 1. TẠO CÁC BẢNG DANH MỤC CƠ BẢN (Không chứa khóa ngoại)
-- ================================================================
CREATE TABLE ChucVu (
    MaChucVu INT IDENTITY(1,1) PRIMARY KEY,
    TenChucVu NVARCHAR(100)
);

CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(255) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SoDienThoai VARCHAR(20),
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255),
    MaChucVu INT,
    Luong DECIMAL(18,2) CHECK (Luong >= 0)
    FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);
GO

CREATE TABLE DanhMuc (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX)
);
GO

CREATE TABLE NhaCungCap (
    MaNCC INT IDENTITY(1,1) PRIMARY KEY,
    TenNCC NVARCHAR(255) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(20),
    Email VARCHAR(100) UNIQUE
);
GO

CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(20),
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255)
);
GO

-- ================================================================
-- 2. TẠO CÁC BẢNG DỮ LIỆU CỐT LÕI (Chứa khóa ngoại)
-- ================================================================
CREATE TABLE SanPham (
    MaSanPham INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(255) NOT NULL,
    ChiTiet NVARCHAR(500),
    HinhAnh NVARCHAR(255) NULL, 
    MaDanhMuc INT NOT NULL,
    MaNCC INT NOT NULL,
    GiaNhap DECIMAL(18,2) CHECK (GiaNhap >= 0),
    GiaBan DECIMAL(18,2) CHECK (GiaBan >= 0),
    SoLuongTon INT DEFAULT 0,   
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);
GO

CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    MaNhanVien INT NOT NULL UNIQUE, 
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,  
    PhanQuyen VARCHAR(20) NOT NULL CHECK (PhanQuyen IN ('Admin', 'User')),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien) ON DELETE CASCADE
);
GO

-- ================================================================
-- 3. TẠO CÁC BẢNG QUẢN LÝ KHO (Phiếu Nhập Kho)
-- ================================================================
CREATE TABLE Kho (
    MaKho INT IDENTITY(1,1) PRIMARY KEY,
    TenKho NVARCHAR(50) NOT NULL,
    MaNhanVien INT NOT NULL, 
    MaNCC INT NOT NULL,      
    NgayNhap DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);
GO

CREATE TABLE ChiTietKho (
    MaChiTietKho INT IDENTITY(1,1) PRIMARY KEY,
    MaKho INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuongNhap INT CHECK (SoLuongNhap > 0),
    GiaNhap DECIMAL(18,2) CHECK (GiaNhap >= 0),
    ThanhTien AS (SoLuongNhap * GiaNhap) PERSISTED, 
    FOREIGN KEY (MaKho) REFERENCES Kho(MaKho) ON DELETE CASCADE,
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
GO
    
-- ================================================================
-- 4. TẠO CÁC BẢNG GIAO DỊCH (Hóa đơn bán hàng)
-- ================================================================
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    MaNhanVien INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(50) DEFAULT N'Đang xử lý',
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
GO

CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,    
    MaHoaDon INT,
    MaSanPham INT,
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon) ON DELETE CASCADE,
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
GO

-- ================================================================
-- 5. CÁC TRIGGER TỰ ĐỘNG CẬP NHẬT TỒN KHO
-- ================================================================

-- Trigger tự động CỘNG TỒN KHO khi nhập hàng vào ChiTietKho
CREATE TRIGGER trg_CongTonKhoKhiNhap
ON ChiTietKho
AFTER INSERT
AS
BEGIN
    UPDATE SanPham
    SET SoLuongTon = SoLuongTon + inserted.SoLuongNhap
    FROM SanPham
    JOIN inserted ON SanPham.MaSanPham = inserted.MaSanPham
END;
GO

-- Trigger tự động TRỪ TỒN KHO khi bán hàng (Đã sửa đổi thành CREATE TRIGGER)
CREATE TRIGGER trg_TruTonKhoKhiBan
ON ChiTietHoaDon
AFTER INSERT
AS
BEGIN
    -- 1. Trừ tồn kho như bình thường
    UPDATE SanPham
    SET SoLuongTon = SoLuongTon - inserted.SoLuong
    FROM SanPham
    JOIN inserted ON SanPham.MaSanPham = inserted.MaSanPham;

    -- 2. Kiểm tra nếu có sản phẩm nào bị âm số lượng sau khi bán
    IF EXISTS (SELECT 1 FROM SanPham WHERE SoLuongTon < 0)
    BEGIN
        -- Hủy bỏ thao tác bán hàng và báo lỗi
        ROLLBACK TRANSACTION;
        THROW 50000, N'Lỗi: Số lượng tồn kho không đủ để bán!', 1;
    END
END;
GO
