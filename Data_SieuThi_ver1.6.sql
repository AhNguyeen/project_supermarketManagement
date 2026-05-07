-- ================================================================
-- PHẦN 1: DỮ LIỆU NỀN TẢNG (MASTER DATA)
-- (Nhân viên, Danh mục, Nhà cung cấp, Khách hàng)
-- ================================================================
USE QuanLySieuThi;
GO

-- ================================================================
-- 1.1 Thêm dữ liệu bảng ChucVu (CHẠY TRƯỚC TIÊN)
-- ================================================================
INSERT INTO ChucVu (TenChucVu)
VALUES
(N'Thu ngân'),              -- MaChucVu = 1
(N'Nhân viên kho'),         -- MaChucVu = 2
(N'Quản lý'),               -- MaChucVu = 3
(N'Chăm sóc khách hàng'),   -- MaChucVu = 4
(N'Bảo vệ'),                -- MaChucVu = 5
(N'Quản lý hệ thống');      -- MaChucVu = 6
GO

-- ================================================================
-- 1.2 Thêm dữ liệu bảng NhanVien (Đã thay Cột ChucVu thành MaChucVu)
-- ================================================================
INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, MaChucVu, Luong)
VALUES
(N'Nguyễn Văn An', '1995-05-10', N'Nam', '0901234567', 'an.nguyen@example.com', N'Quận 1, TP.HCM', 1, 8500000),
(N'Trần Thị Bình', '1998-11-20', N'Nữ', '0912345678', 'binh.tran@example.com', N'Quận 3, TP.HCM', 2, 7500000),
(N'Lê Hoàng Cường', '1988-02-15', N'Nam', '0923456789', 'cuong.le@example.com', N'Quận Gò Vấp, TP.HCM', 3, 15000000),
(N'Phạm Mai Dung', '1999-08-05', N'Nữ', '0934567890', 'dung.pham@example.com', N'Quận Tân Bình, TP.HCM', 4, 8000000),
(N'Hoàng Văn Em', '1992-12-25', N'Nam', '0945678901', 'em.hoang@example.com', N'Quận 7, TP.HCM', 5, 7000000),
(N'Lý Tấn Phát', '1990-01-12', N'Nam', '0901000001', 'phat.ly@example.com', N'Quận 1, TP.HCM', 1, 8500000),
(N'Đào Thu Hà', '1996-03-24', N'Nữ', '0901000002', 'ha.dao@example.com', N'Quận 2, TP.HCM', 1, 8500000),
(N'Vương Đình Huệ', '1985-05-15', N'Nam', '0901000003', 'hue.vuong@example.com', N'Quận 3, TP.HCM', 3, 15000000),
(N'Tô Ngọc Vân', '1998-07-07', N'Nữ', '0901000004', 'van.to@example.com', N'Quận 4, TP.HCM', 4, 8000000),
(N'Phan Đình Phùng', '1992-09-09', N'Nam', '0901000005', 'phung.phan@example.com', N'Quận 5, TP.HCM', 5, 7000000),
(N'Mạc Văn Khoa', '1994-11-11', N'Nam', '0901000006', 'khoa.mac@example.com', N'Quận 6, TP.HCM', 2, 7500000),
(N'Lương Bích Hữu', '1989-12-12', N'Nữ', '0901000007', 'huu.luong@example.com', N'Quận 7, TP.HCM', 4, 8000000),
(N'Ngô Tất Tố', '1991-02-20', N'Nam', '0901000008', 'to.ngo@example.com', N'Quận 8, TP.HCM', 2, 7500000),
(N'Hồ Xuân Hương', '1995-04-14', N'Nữ', '0901000009', 'huong.ho@example.com', N'Quận 9, TP.HCM', 1, 8500000),
(N'Đặng Lệ Quân', '1997-06-16', N'Nữ', '0901000010', 'quan.dang@example.com', N'Quận 10, TP.HCM', 1, 8500000),
(N'Bùi Anh Tuấn', '1993-08-18', N'Nam', '0901000011', 'tuan.bui@example.com', N'Quận 11, TP.HCM', 5, 7000000),
(N'Chu Văn An', '1987-10-20', N'Nam', '0901000012', 'an.chu@example.com', N'Quận 12, TP.HCM', 3, 15000000),
(N'Lâm Tâm Như', '1999-12-22', N'Nữ', '0901000013', 'nhu.lam@example.com', N'Bình Thạnh, TP.HCM', 4, 8000000),
(N'Trương Vô Kỵ', '1990-01-25', N'Nam', '0901000014', 'ky.truong@example.com', N'Gò Vấp, TP.HCM', 2, 7500000),
(N'Triệu Mẫn', '1998-03-30', N'Nữ', '0901000015', 'man.trieu@example.com', N'Tân Bình, TP.HCM', 1, 8500000);
GO

-- 1.3 Thêm tài khoản Admin và User (Đã đổi chức vụ thành MaChucVu = 6)
INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, MaChucVu, Luong)
VALUES (N'Đặng Anh Nguyên', '2000-01-01', N'Nam', '0999999999', 'anhnguyen@example.com', N'Bình Dương', 6, 25000000);

-- ================================================================
-- 1.2 Thêm tài khoản dựa trên chức vụ (Username viết liền không dấu)
-- ================================================================

-- Tài khoản Quản lý hệ thống (Admin)
DECLARE @MaNV_Nguyen INT = (SELECT MaNhanVien FROM NhanVien WHERE HoTen = N'Đặng Anh Nguyên');
INSERT INTO TaiKhoan (MaNhanVien, Username, Password, PhanQuyen) 
VALUES (@MaNV_Nguyen, 'admin', 'admin123', 'Admin');

-- Tài khoản Thu ngân (User) - Lấy nhân viên Nguyễn Văn An (MaNV 1)
INSERT INTO TaiKhoan (MaNhanVien, Username, Password, PhanQuyen) 
VALUES (1, 'thungan', 'user123', 'User');

-- Tài khoản Nhân viên kho (User) - Lấy nhân viên Trần Thị Bình (MaNV 2)
INSERT INTO TaiKhoan (MaNhanVien, Username, Password, PhanQuyen) 
VALUES (2, 'nhanvienkho', 'user123', 'User');

-- Tài khoản Quản lý (User nhưng quyền cao hơn staff thường) - Lấy Lê Hoàng Cường (MaNV 3)
INSERT INTO TaiKhoan (MaNhanVien, Username, Password, PhanQuyen) 
VALUES (3, 'quanly', 'user123', 'User');
GO

-- 1.3 Thêm dữ liệu bảng DanhMuc
INSERT INTO DanhMuc (TenDanhMuc, MoTa)
VALUES
(N'Thực phẩm tươi sống', N'Các loại thịt, cá, rau củ quả tươi sử dụng trong ngày'),
(N'Đồ uống', N'Nước giải khát, nước lọc, sữa, bia, rượu'),
(N'Đồ gia dụng', N'Dụng cụ nhà bếp, chén bát, nồi chảo'),
(N'Hóa mỹ phẩm', N'Dầu gội, sữa tắm, bột giặt, nước rửa chén'),
(N'Bánh kẹo', N'Các loại bánh quy, kẹo ngọt, snack, chocolate'),
(N'Trái cây nhập khẩu', N'Táo, lê, nho, dâu tây, kiwi'),
(N'Thực phẩm đông lạnh', N'Xúc xích, cá viên, há cảo, thịt mát'),
(N'Gia vị & Nước chấm', N'Nước mắm, xì dầu, tương ớt, bột ngọt'),
(N'Sữa & Từ sữa', N'Sữa tươi, sữa chua, phô mai, bơ'),
(N'Văn phòng phẩm', N'Bút, thước, vở học sinh, giấy in');
GO

-- 1.4 Thêm dữ liệu bảng NhaCungCap
INSERT INTO NhaCungCap (TenNCC, DiaChi, SoDienThoai, Email)
VALUES
(N'CTY CP Chăn nuôi C.P', N'Biên Hòa, Đồng Nai', '0251311111', 'contact@cp.com.vn'),
(N'Suntory PepsiCo VN', N'Quận 1, TP.HCM', '0283222222', 'info@suntorypepsico.vn'),
(N'Tập đoàn Sunhouse', N'Cầu Giấy, Hà Nội', '0243333333', 'doitac@sunhouse.com.vn'),
(N'Unilever Việt Nam', N'Quận 7, TP.HCM', '0283444444', 'tuvankhachhang@unilever.com'),
(N'CTY CP Mondelez Kinh Đô', N'Thủ Dầu Một, Bình Dương', '0274555555', 'cskh@kinhdo.vn'),
(N'CTY CP Sữa Vinamilk', N'Quận 7, TP.HCM', '0281111222', 'cskh@vinamilk.com.vn'),
(N'Masan Consumer', N'Quận 1, TP.HCM', '0283333444', 'contact@masan.com.vn'),
(N'CTY Kỹ nghệ súc sản Vissan', N'Bình Thạnh, TP.HCM', '0285555666', 'info@vissan.com.vn'),
(N'Tập đoàn Thiên Long', N'Tân Phú, TP.HCM', '0287777888', 'sales@thienlong.vn'),
(N'Hoàng Anh Gia Lai (HAGL)', N'Pleiku, Gia Lai', '0269999000', 'nongnghiep@hagl.com.vn');
GO

-- 1.5 Thêm dữ liệu bảng KhachHang
INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi)
VALUES
(N'Khách Vãng Lai', '0000000000', 'vanglai@gmail.com', N'Không Có'),
(N'Vũ Thị Bích', '0811112222', 'bich.vu@gmail.com', N'Quận 10, TP.HCM'),
(N'Đinh Trọng Hữu', '0822223333', 'huu.dinh@gmail.com', N'Quận Phú Nhuận, TP.HCM'),
(N'Lý Mỹ Châu', '0833334444', 'chau.ly@gmail.com', N'Quận Bình Thạnh, TP.HCM'),
(N'Ngô Kiến Huy', '0844445555', 'huy.ngo@gmail.com', N'Thành phố Thủ Đức, TP.HCM'),
(N'Bùi Minh Trí', '0855556666', 'tri.bui@gmail.com', N'Quận 4, TP.HCM'),
(N'Lưu Đức Hoa', '0911000001', 'hoa.luu@gmail.com', N'Quận 1, TP.HCM'),
(N'Quách Phú Thành', '0911000002', 'thanh.quach@gmail.com', N'Quận 2, TP.HCM'),
(N'Lê Minh', '0911000003', 'minh.le@gmail.com', N'Quận 3, TP.HCM'),
(N'Trương Học Hữu', '0911000004', 'huu.truong@gmail.com', N'Quận 4, TP.HCM'),
(N'Châu Kiệt Luân', '0911000005', 'luan.chau@gmail.com', N'Quận 5, TP.HCM'),
(N'Tạ Đình Phong', '0911000006', 'phong.ta@gmail.com', N'Quận 6, TP.HCM'),
(N'Trần Quán Hy', '0911000007', 'hy.tran@gmail.com', N'Quận 7, TP.HCM'),
(N'Dương Mịch', '0911000008', 'mich.duong@gmail.com', N'Quận 8, TP.HCM'),
(N'Triệu Lệ Dĩnh', '0911000009', 'dinh.trieu@gmail.com', N'Quận 9, TP.HCM'),
(N'Địch Lệ Nhiệt Ba', '0911000010', 'ba.dich@gmail.com', N'Quận 10, TP.HCM');
GO


-- ================================================================
-- PHẦN 2: SẢN PHẨM & TỒN KHO MẶC ĐỊNH
-- ================================================================

-- 2.1 Thêm dữ liệu Sản Phẩm
INSERT INTO SanPham (TenSanPham, HinhAnh, MaDanhMuc, MaNCC, GiaNhap, GiaBan)
VALUES
(N'Thịt đùi heo 500g','', 1, 1, 45000, 60000),
(N'Lốc 6 lon Pepsi 320ml','', 2, 2, 40000, 55000),
(N'Chảo chống dính 24cm','', 3, 3, 120000, 180000),
(N'Sữa tắm Lifebuoy 800g','', 4, 4, 110000, 145000),
(N'Bánh quy Cosy 400g','', 5, 5, 35000, 48000),
(N'Táo Fuji Nam Phi 1kg', '', 6, 10, 50000, 75000),
(N'Nho mẫu đơn Hàn Quốc 500g', '', 6, 10, 250000, 350000),
(N'Kiwi vàng New Zealand', '', 6, 10, 150000, 210000),
(N'Dưa lưới ruột cam 1.5kg', '', 6, 10, 60000, 85000),
(N'Cam sành Vĩnh Long 1kg', '', 6, 10, 20000, 35000),
(N'Xúc xích heo Vissan 500g', '', 7, 8, 45000, 62000),
(N'Cá viên chiên Vissan 500g', '', 7, 8, 40000, 55000),
(N'Há cảo tôm xào 250g', '', 7, 8, 30000, 42000),
(N'Thịt bò lúc lắc đông lạnh 300g', '', 7, 8, 70000, 95000),
(N'Chả giò rế thịt tôm 400g', '', 7, 8, 55000, 75000),
(N'Nước mắm Nam Ngư 500ml', '', 8, 7, 30000, 42000),
(N'Nước tương Chinsu 250ml', '', 8, 7, 12000, 18000),
(N'Tương ớt Chinsu 250g', '', 8, 7, 10000, 15000),
(N'Bột ngọt Ajinomoto 1kg', '', 8, 7, 50000, 65000),
(N'Hạt nêm Knorr thịt thăn 900g', '', 8, 7, 65000, 85000),
(N'Sữa tươi Vinamilk có đường 1L', '', 9, 6, 25000, 33000),
(N'Sữa chua nha đam Vinamilk Lốc 4', '', 9, 6, 22000, 30000),
(N'Phô mai con bò cười Hộp 8', '', 9, 6, 28000, 38000),
(N'Sữa đặc Ông Thọ đỏ 380g', '', 9, 6, 18000, 25000),
(N'Bơ thực vật Tường An 200g', '', 9, 6, 15000, 22000),
(N'Bút bi Thiên Long TL-027', '', 10, 9, 3000, 5000),
(N'Sổ tay gáy xoắn A5', '', 10, 9, 15000, 25000),
(N'Bút dạ quang vàng', '', 10, 9, 8000, 12000),
(N'Giấy in A4 Double A 70gsm', '', 10, 9, 60000, 80000),
(N'Thước kẻ nhựa 20cm', '', 10, 9, 2000, 4000),
(N'Cá hồi Nauy phi lê 300g', '', 1, 1, 150000, 220000),
(N'Gà thả vườn nguyên con', '', 1, 1, 110000, 150000),
(N'Trứng gà ba huân Hộp 10', '', 1, 1, 22000, 32000),
(N'Bia Heineken Lốc 6 lon', '', 2, 2, 95000, 115000),
(N'Nước suối Aquafina 500ml', '', 2, 2, 4000, 7000),
(N'Nước cam ép Twister 1L', '', 2, 2, 15000, 22000),
(N'Trà xanh không độ 500ml', '', 2, 2, 7000, 10000),
(N'Nồi cơm điện Sunhouse 1.8L', '', 3, 3, 400000, 550000),
(N'Ấm siêu tốc 1.5L Inox', '', 3, 3, 120000, 180000),
(N'Bình giữ nhiệt 500ml', '', 3, 3, 80000, 120000),
(N'Dầu gội Clear men 630g', '', 4, 4, 120000, 165000),
(N'Kem đánh răng PS 240g', '', 4, 4, 30000, 42000),
(N'Nước súc miệng Listerine 750ml', '', 4, 4, 90000, 130000),
(N'Bột giặt OMO hệ máy 3kg', '', 4, 4, 110000, 155000),
(N'Nước xả Downy túi 1.5L', '', 4, 4, 85000, 115000),
(N'Bánh Oreo kem Vani 133g', '', 5, 5, 12000, 18000),
(N'Kẹo dẻo Chupa Chups 90g', '', 5, 5, 15000, 22000),
(N'Bánh mì sandwich lạt', '', 5, 5, 18000, 25000),
(N'Snack khoai tây Lay''s 90g', '', 5, 5, 16000, 22000),
(N'Chocolate Kitkat 4 thanh', '', 5, 5, 10000, 15000);
GO

-- 2.2 Cập nhật Tồn kho Khởi tạo (Tránh âm kho cho các sản phẩm không có phiếu nhập)
UPDATE SanPham SET SoLuongTon = 100;
GO

-- 2.3 Cập nhật Hình Ảnh & Chi tiết sản phẩm
-- =========================================================================
-- CẬP NHẬT HÌNH ẢNH CHO 50 SẢN PHẨM BẰNG LINK ONLINE (KHÔNG XUNG ĐỘT DỮ LIỆU)
-- =========================================================================

-- 5 Sản phẩm đầu tiên
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff7f50/fff&text=Thit+Dui+Heo' WHERE TenSanPham = N'Thịt đùi heo 500g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/0047bb/fff&text=Pepsi+320ml' WHERE TenSanPham = N'Lốc 6 lon Pepsi 320ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/333333/fff&text=Chao+Chong+Dinh' WHERE TenSanPham = N'Chảo chống dính 24cm';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc0000/fff&text=Sua+Tam+Lifebuoy' WHERE TenSanPham = N'Sữa tắm Lifebuoy 800g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffaa00/fff&text=Banh+Quy+Cosy' WHERE TenSanPham = N'Bánh quy Cosy 400g';

-- Trái cây
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc0000/fff&text=Tao+Fuji' WHERE TenSanPham = N'Táo Fuji Nam Phi 1kg';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/4b8b3b/fff&text=Nho+Mau+Don' WHERE TenSanPham = N'Nho mẫu đơn Hàn Quốc 500g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/8b9c2a/fff&text=Kiwi+Vang' WHERE TenSanPham = N'Kiwi vàng New Zealand';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff8c00/fff&text=Dua+Luoi' WHERE TenSanPham = N'Dưa lưới ruột cam 1.5kg';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffa500/fff&text=Cam+Sanh' WHERE TenSanPham = N'Cam sành Vĩnh Long 1kg';

-- Đông lạnh
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff9999/fff&text=Xuc+Xich+Vissan' WHERE TenSanPham = N'Xúc xích heo Vissan 500g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffcc99/fff&text=Ca+Vien+Chien' WHERE TenSanPham = N'Cá viên chiên Vissan 500g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffdb58/fff&text=Ha+Cao' WHERE TenSanPham = N'Há cảo tôm xào 250g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/8b0000/fff&text=Bo+Luc+Lac' WHERE TenSanPham = N'Thịt bò lúc lắc đông lạnh 300g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/e6b800/fff&text=Cha+Gio' WHERE TenSanPham = N'Chả giò rế thịt tôm 400g';

-- Gia vị
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc5500/fff&text=Nuoc+Mam' WHERE TenSanPham = N'Nước mắm Nam Ngư 500ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/4a2511/fff&text=Nuoc+Tuong' WHERE TenSanPham = N'Nước tương Chinsu 250ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff2400/fff&text=Tuong+Ot' WHERE TenSanPham = N'Tương ớt Chinsu 250g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff0000/fff&text=Bot+Ngot' WHERE TenSanPham = N'Bột ngọt Ajinomoto 1kg';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffcc00/fff&text=Hat+Nem' WHERE TenSanPham = N'Hạt nêm Knorr thịt thăn 900g';

-- Sữa
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/0066cc/fff&text=Sua+Tuoi' WHERE TenSanPham = N'Sữa tươi Vinamilk có đường 1L';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/3399ff/fff&text=Sua+Chua' WHERE TenSanPham = N'Sữa chua nha đam Vinamilk Lốc 4';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc0000/fff&text=Pho+Mai' WHERE TenSanPham = N'Phô mai con bò cười Hộp 8';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff0000/fff&text=Sua+Dac' WHERE TenSanPham = N'Sữa đặc Ông Thọ đỏ 380g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffcc00/fff&text=Bo+Tuong+An' WHERE TenSanPham = N'Bơ thực vật Tường An 200g';

-- Văn phòng phẩm
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/0033cc/fff&text=But+Bi' WHERE TenSanPham = N'Bút bi Thiên Long TL-027';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cccccc/000&text=So+Tay' WHERE TenSanPham = N'Sổ tay gáy xoắn A5';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ccff00/000&text=But+Da+Quang' WHERE TenSanPham = N'Bút dạ quang vàng';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffffff/000&text=Giay+A4' WHERE TenSanPham = N'Giấy in A4 Double A 70gsm';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/eeeeee/000&text=Thuoc+Ke' WHERE TenSanPham = N'Thước kẻ nhựa 20cm';

-- Sản phẩm Mix
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/fa8072/fff&text=Ca+Hoi' WHERE TenSanPham = N'Cá hồi Nauy phi lê 300g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffdb58/fff&text=Ga+Tha+Vuon' WHERE TenSanPham = N'Gà thả vườn nguyên con';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffebcd/000&text=Trung+Ga' WHERE TenSanPham = N'Trứng gà ba huân Hộp 10';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/008000/fff&text=Heineken' WHERE TenSanPham = N'Bia Heineken Lốc 6 lon';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/00a2e8/fff&text=Aquafina' WHERE TenSanPham = N'Nước suối Aquafina 500ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff8c00/fff&text=Cam+Twister' WHERE TenSanPham = N'Nước cam ép Twister 1L';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/00a859/fff&text=Tra+Khong+Do' WHERE TenSanPham = N'Trà xanh không độ 500ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc0000/fff&text=Noi+Com+Dien' WHERE TenSanPham = N'Nồi cơm điện Sunhouse 1.8L';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/aaaaaa/000&text=Am+Sieu+Toc' WHERE TenSanPham = N'Ấm siêu tốc 1.5L Inox';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/444444/fff&text=Binh+Giu+Nhiet' WHERE TenSanPham = N'Bình giữ nhiệt 500ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/000080/fff&text=Clear+Men' WHERE TenSanPham = N'Dầu gội Clear men 630g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/00cccc/fff&text=Kem+Danh+Rang' WHERE TenSanPham = N'Kem đánh răng PS 240g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/66ccff/fff&text=Listerine' WHERE TenSanPham = N'Nước súc miệng Listerine 750ml';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff0000/fff&text=Bot+Giat+OMO' WHERE TenSanPham = N'Bột giặt OMO hệ máy 3kg';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff66b2/fff&text=Nuoc+Xa+Downy' WHERE TenSanPham = N'Nước xả Downy túi 1.5L';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/0047bb/fff&text=Banh+Oreo' WHERE TenSanPham = N'Bánh Oreo kem Vani 133g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff3399/fff&text=Chupa+Chups' WHERE TenSanPham = N'Kẹo dẻo Chupa Chups 90g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffe4b5/000&text=Sandwich' WHERE TenSanPham = N'Bánh mì sandwich lạt';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ffcc00/000&text=Lays' WHERE TenSanPham = N'Snack khoai tây Lay''s 90g';
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/cc0000/fff&text=Kitkat' WHERE TenSanPham = N'Chocolate Kitkat 4 thanh';
GO

-- =========================================================================
-- CẬP NHẬT CHI TIẾT CHO 50 SẢN PHẨM MÀ KHÔNG LÀM THAY ĐỔI DỮ LIỆU CŨ
-- =========================================================================

-- 5 Sản phẩm đầu tiên
UPDATE SanPham SET ChiTiet = N'Thịt heo tươi sạch, đạt chuẩn VietGAP, đóng khay hút chân không vệ sinh.' WHERE TenSanPham = N'Thịt đùi heo 500g';
UPDATE SanPham SET ChiTiet = N'Nước giải khát có gas sảng khoái, vị nguyên bản, lốc 6 lon tiện lợi cho gia đình.' WHERE TenSanPham = N'Lốc 6 lon Pepsi 320ml';
UPDATE SanPham SET ChiTiet = N'Chảo hợp kim nhôm cao cấp, phủ lớp chống dính Whitford an toàn sức khỏe.' WHERE TenSanPham = N'Chảo chống dính 24cm';
UPDATE SanPham SET ChiTiet = N'Bảo vệ khỏi 99.9% vi khuẩn, công thức bọt mịn chăm sóc da nhạy cảm.' WHERE TenSanPham = N'Sữa tắm Lifebuoy 800g';
UPDATE SanPham SET ChiTiet = N'Bánh quy giòn tan, vị ngọt dịu, thích hợp làm bữa phụ hoặc dùng kèm trà.' WHERE TenSanPham = N'Bánh quy Cosy 400g';

-- Trái cây
UPDATE SanPham SET ChiTiet = N'Táo nhập khẩu trực tiếp từ Nam Phi, giòn ngọt, vỏ mỏng, giàu vitamin C.' WHERE TenSanPham = N'Táo Fuji Nam Phi 1kg';
UPDATE SanPham SET ChiTiet = N'Nho thượng hạng Hàn Quốc, trái to, không hạt, vị ngọt thanh như sữa.' WHERE TenSanPham = N'Nho mẫu đơn Hàn Quốc 500g';
UPDATE SanPham SET ChiTiet = N'Kiwi ruột vàng, ngọt đậm đà, rất giàu Vitamin E và chất chống oxy hóa.' WHERE TenSanPham = N'Kiwi vàng New Zealand';
UPDATE SanPham SET ChiTiet = N'Dưa lưới canh tác nhà màng, ruột cam bắt mắt, độ ngọt brix cao.' WHERE TenSanPham = N'Dưa lưới ruột cam 1.5kg';
UPDATE SanPham SET ChiTiet = N'Cam sành mọng nước, vị chua ngọt hài hòa, vắt nước uống rất tốt.' WHERE TenSanPham = N'Cam sành Vĩnh Long 1kg';

-- Đông lạnh
UPDATE SanPham SET ChiTiet = N'Xúc xích làm từ thịt heo tươi, dai ngon, xông khói nhẹ, hợp chiên nướng.' WHERE TenSanPham = N'Xúc xích heo Vissan 500g';
UPDATE SanPham SET ChiTiet = N'Cá viên chiên dai giòn sần sật, món ăn vặt yêu thích của trẻ nhỏ.' WHERE TenSanPham = N'Cá viên chiên Vissan 500g';
UPDATE SanPham SET ChiTiet = N'Há cảo nhân tôm thịt xay nhuyễn, vỏ mỏng dẻo, hấp hoặc chiên đều ngon.' WHERE TenSanPham = N'Há cảo tôm xào 250g';
UPDATE SanPham SET ChiTiet = N'Thịt bò cắt khối vuông vức, tẩm ướp sẵn gia vị đậm đà, tiện lợi nấu nướng.' WHERE TenSanPham = N'Thịt bò lúc lắc đông lạnh 300g';
UPDATE SanPham SET ChiTiet = N'Chả giò cuốn bánh rế lưới, nhân thịt tôm, chiên lên giòn rụm lâu xỉu.' WHERE TenSanPham = N'Chả giò rế thịt tôm 400g';

-- Gia vị
UPDATE SanPham SET ChiTiet = N'Nước mắm cá cơm nguyên chất, độ đạm vừa phải, chấm hay nấu đều ngon.' WHERE TenSanPham = N'Nước mắm Nam Ngư 500ml';
UPDATE SanPham SET ChiTiet = N'Nước tương lên men tự nhiên, vị thanh ngọt, an toàn không chứa 3-MCPD.' WHERE TenSanPham = N'Nước tương Chinsu 250ml';
UPDATE SanPham SET ChiTiet = N'Tương ớt cay nồng vị ớt tươi tỏi băm, kích thích vị giác cực mạnh.' WHERE TenSanPham = N'Tương ớt Chinsu 250g';
UPDATE SanPham SET ChiTiet = N'Bột ngọt chiết xuất từ mía đường tự nhiên, tạo vị umami cho món ăn.' WHERE TenSanPham = N'Bột ngọt Ajinomoto 1kg';
UPDATE SanPham SET ChiTiet = N'Hạt nêm bổ sung chiết xuất từ xương ống và thịt thăn, món canh thêm ngọt.' WHERE TenSanPham = N'Hạt nêm Knorr thịt thăn 900g';

-- Sữa
UPDATE SanPham SET ChiTiet = N'Sữa tươi tiệt trùng 100%, bổ sung vitamin D3 và Canxi cho xương chắc khỏe.' WHERE TenSanPham = N'Sữa tươi Vinamilk có đường 1L';
UPDATE SanPham SET ChiTiet = N'Sữa chua lên men tự nhiên kết hợp hạt nha đam dai giòn thanh mát.' WHERE TenSanPham = N'Sữa chua nha đam Vinamilk Lốc 4';
UPDATE SanPham SET ChiTiet = N'Phô mai mềm mịn, cung cấp nguồn kẽm và canxi dồi dào, bé rất thích.' WHERE TenSanPham = N'Phô mai con bò cười Hộp 8';
UPDATE SanPham SET ChiTiet = N'Sữa đặc có đường nhãn đỏ, sánh mịn, thích hợp pha cà phê, làm bánh.' WHERE TenSanPham = N'Sữa đặc Ông Thọ đỏ 380g';
UPDATE SanPham SET ChiTiet = N'Bơ thực vật thơm béo ngậy, dùng để chiên xào hoặc phết bánh mì.' WHERE TenSanPham = N'Bơ thực vật Tường An 200g';

-- Văn phòng phẩm
UPDATE SanPham SET ChiTiet = N'Bút bi quốc dân, ngòi 0.5mm êm ái, mực ra đều không bị tắc.' WHERE TenSanPham = N'Bút bi Thiên Long TL-027';
UPDATE SanPham SET ChiTiet = N'Sổ tay gáy lò xo kép chắc chắn, giấy kẻ ngang chống lóa mắt.' WHERE TenSanPham = N'Sổ tay gáy xoắn A5';
UPDATE SanPham SET ChiTiet = N'Bút highlight dạ quang màu vàng chanh, dùng để đánh dấu ý chính.' WHERE TenSanPham = N'Bút dạ quang vàng';
UPDATE SanPham SET ChiTiet = N'Giấy in cao cấp, định lượng 70gsm, không kẹt giấy khi in 2 mặt.' WHERE TenSanPham = N'Giấy in A4 Double A 70gsm';
UPDATE SanPham SET ChiTiet = N'Thước kẻ nhựa dẻo trong suốt, vạch chia cm rõ nét, bền bỉ.' WHERE TenSanPham = N'Thước kẻ nhựa 20cm';

-- Sản phẩm Mix
UPDATE SanPham SET ChiTiet = N'Cá hồi Nauy fillet không xương, giàu Omega-3, thích hợp làm sashimi, áp chảo.' WHERE TenSanPham = N'Cá hồi Nauy phi lê 300g';
UPDATE SanPham SET ChiTiet = N'Gà ta thả vườn làm sạch, thịt chắc, da giòn, dai ngon sần sật.' WHERE TenSanPham = N'Gà thả vườn nguyên con';
UPDATE SanPham SET ChiTiet = N'Trứng gà công nghiệp đạt chuẩn an toàn vệ sinh, lòng đỏ to.' WHERE TenSanPham = N'Trứng gà ba huân Hộp 10';
UPDATE SanPham SET ChiTiet = N'Bia lager danh tiếng thế giới, lốc 6 lon tiện lợi mang đi dã ngoại.' WHERE TenSanPham = N'Bia Heineken Lốc 6 lon';
UPDATE SanPham SET ChiTiet = N'Nước tinh khiết đóng chai qua hệ thống lọc RO tiêu chuẩn quốc tế.' WHERE TenSanPham = N'Nước suối Aquafina 500ml';
UPDATE SanPham SET ChiTiet = N'Nước ép cam có tép thật, bù nước và vitamin C nhanh chóng.' WHERE TenSanPham = N'Nước cam ép Twister 1L';
UPDATE SanPham SET ChiTiet = N'Trà xanh chiết xuất từ lá trà Thái Nguyên, thanh lọc cơ thể.' WHERE TenSanPham = N'Trà xanh không độ 500ml';
UPDATE SanPham SET ChiTiet = N'Nồi cơm điện nắp gài mâm nhiệt lớn, nấu cơm chín đều tơi xốp.' WHERE TenSanPham = N'Nồi cơm điện Sunhouse 1.8L';
UPDATE SanPham SET ChiTiet = N'Ấm đun siêu tốc inox 304 không gỉ, đun sôi nước siêu tốc trong 3 phút.' WHERE TenSanPham = N'Ấm siêu tốc 1.5L Inox';
UPDATE SanPham SET ChiTiet = N'Bình giữ nhiệt 2 lớp chân không, giữ nóng 8h và giữ lạnh tới 12h.' WHERE TenSanPham = N'Bình giữ nhiệt 500ml';
UPDATE SanPham SET ChiTiet = N'Dầu gội sạch gàu dành riêng cho nam giới, mát lạnh da đầu.' WHERE TenSanPham = N'Dầu gội Clear men 630g';
UPDATE SanPham SET ChiTiet = N'Kem đánh răng bảo vệ nướu, ngừa sâu răng, hơi thở thơm mát.' WHERE TenSanPham = N'Kem đánh răng PS 240g';
UPDATE SanPham SET ChiTiet = N'Nước súc miệng diệt khuẩn mảng bám, bảo vệ răng miệng suốt 24h.' WHERE TenSanPham = N'Nước súc miệng Listerine 750ml';
UPDATE SanPham SET ChiTiet = N'Bột giặt công thức xoáy bay vết bẩn cứng đầu, dùng cho máy giặt cửa trên.' WHERE TenSanPham = N'Bột giặt OMO hệ máy 3kg';
UPDATE SanPham SET ChiTiet = N'Nước xả vải đậm đặc, lưu hương hoa ngàn dặm trên quần áo bền lâu.' WHERE TenSanPham = N'Nước xả Downy túi 1.5L';
UPDATE SanPham SET ChiTiet = N'Bánh quy kẹp kem Vani đen trứ danh, xoay bánh, nếm kem, chấm sữa.' WHERE TenSanPham = N'Bánh Oreo kem Vani 133g';
UPDATE SanPham SET ChiTiet = N'Kẹo dẻo hương trái cây tự nhiên dạng tuýp dài, dai ngon thú vị.' WHERE TenSanPham = N'Kẹo dẻo Chupa Chups 90g';
UPDATE SanPham SET ChiTiet = N'Bánh mì gối mềm xốp, thích hợp kẹp thịt nguội làm bữa sáng tiện lợi.' WHERE TenSanPham = N'Bánh mì sandwich lạt';
UPDATE SanPham SET ChiTiet = N'Bim bim khoai tây cắt lát mỏng tẩm vị tự nhiên, giòn tan trong miệng.' WHERE TenSanPham = N'Snack khoai tây Lay''s 90g';
UPDATE SanPham SET ChiTiet = N'Bánh xốp phủ sô cô la sữa, bẻ đôi chia sẻ ngọt ngào.' WHERE TenSanPham = N'Chocolate Kitkat 4 thanh';
GO

-- (Phần này giữ nguyên đoạn UPDATE HinhAnh và ChiTiet của bạn, mình gom tắt lại để code gọn)
UPDATE SanPham SET HinhAnh = 'https://dummyimage.com/400x400/ff7f50/fff&text=Thit+Dui+Heo', ChiTiet = N'Thịt heo tươi sạch, đạt chuẩn VietGAP, đóng khay hút chân không vệ sinh.' WHERE TenSanPham = N'Thịt đùi heo 500g';
-- ... (Bạn có thể ném toàn bộ đoạn lệnh UPDATE HinhAnh và ChiTiet của bạn vào vị trí này, không ảnh hưởng gì tới logic)


-- ================================================================
-- PHẦN 3: NHẬP KHO (PHẢI LÀM TRƯỚC KHI BÁN HÀNG)
-- (Trigger trg_CongTonKhoKhiNhap sẽ kích hoạt ở đây)
-- ================================================================

-- 3.1 Tạo Phiếu Nhập Kho
INSERT INTO Kho (TenKho, MaNhanVien, MaNCC, NgayNhap, GhiChu)
VALUES
(N'Kho Thực Phẩm', 2, 1, '2023-10-01 08:00:00', N'Nhập lô thịt và thực phẩm tươi sống đầu tháng'),
(N'Kho Đồ Uống', 11, 2, '2023-10-02 09:30:00', N'Nhập nước giải khát phục vụ dịp lễ'),
(N'Kho Đông Lạnh', 13, 8, '2023-10-05 14:15:00', N'Nhập thực phẩm đông lạnh và chế biến sẵn'),
(N'Kho Thực Phẩm', 19, 6, '2023-10-10 10:00:00', N'Nhập các sản phẩm từ sữa và bơ phô mai'),
(N'Kho Tổng Hợp', 2, 9, '2023-10-12 15:45:00', N'Nhập bổ sung văn phòng phẩm');
GO

-- 3.2 Thêm Chi Tiết Phiếu Nhập
INSERT INTO ChiTietKho (MaKho, MaSanPham, SoLuongNhap, GiaNhap)
VALUES
(1, 1, 100, 45000), (1, 31, 50, 150000), (1, 32, 80, 110000), (1, 33, 200, 22000),
(2, 2, 300, 40000), (2, 34, 150, 95000), (2, 35, 500, 4000), (2, 36, 200, 15000), (2, 37, 250, 7000),
(3, 11, 120, 45000), (3, 12, 100, 40000), (3, 13, 80, 30000), (3, 14, 60, 70000),
(4, 21, 400, 25000), (4, 22, 350, 22000), (4, 23, 150, 28000), (4, 24, 200, 18000),
(5, 26, 1000, 3000), (5, 27, 200, 15000), (5, 28, 300, 8000), (5, 29, 100, 60000); 
GO


-- ================================================================
-- PHẦN 4: BÁN HÀNG (LÚC NÀY KHO ĐÃ CÓ HÀNG ĐỂ TRỪ)
-- (Trigger trg_TruTonKhoKhiBan sẽ kích hoạt ở đây)
-- ================================================================

-- 4.1 Tạo Hóa Đơn
INSERT INTO HoaDon (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai)
VALUES
(1, 1, '2023-10-15 08:30:00', 120000, N'Hoàn thành'),
(2, 1, '2023-10-15 09:15:00', 110000, N'Hoàn thành'),
(3, 4, '2023-10-16 14:00:00', 360000, N'Hoàn thành'),
(4, 1, '2023-10-16 18:45:00', 145000, N'Hoàn thành'),
(5, 4, '2023-10-17 19:30:00', 96000, N'Đã hủy'),
(6, 6, '2023-11-01 08:30:00', 150000, N'Hoàn thành'),
(7, 7, '2023-11-01 10:15:00', 350000, N'Hoàn thành'),
(8, 8, '2023-11-02 14:00:00', 420000, N'Hoàn thành'),
(9, 9, '2023-11-02 18:45:00', 170000, N'Hoàn thành'),
(10, 10, '2023-11-03 19:30:00', 70000, N'Đã hủy'),
(11, 11, '2023-11-04 09:20:00', 124000, N'Hoàn thành'),
(12, 12, '2023-11-05 11:10:00', 110000, N'Hoàn thành'),
(13, 13, '2023-11-06 15:30:00', 84000, N'Hoàn thành'),
(14, 14, '2023-11-07 17:00:00', 99000, N'Hoàn thành'),
(15, 15, '2023-11-08 20:00:00', 25000, N'Hoàn thành');
GO

-- 4.2 Thêm Chi Tiết Hóa Đơn (Hệ thống sẽ trừ tồn kho từ đây)
INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGia)
VALUES
(1, 1, 2, 60000),  
(2, 2, 2, 55000),  
(3, 3, 2, 180000), 
(4, 4, 1, 145000), 
(5, 5, 2, 48000),  
(6, 6, 2, 75000),   
(7, 7, 1, 350000),  
(8, 8, 2, 210000),  
(9, 9, 2, 85000),   
(10, 10, 2, 35000), 
(11, 11, 2, 62000), 
(12, 12, 2, 55000), 
(13, 13, 2, 42000), 
(14, 21, 3, 33000), 
(15, 26, 5, 5000);  
GO
