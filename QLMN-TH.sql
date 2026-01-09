CREATE DATABASE QLMN_TH_V2;
GO
USE QLMN_TH_V2;
GO


-- =============================================
-- 2. TẠO BẢNG LOẠI TÀI KHOẢN (Tạo bảng cha trước)
-- =============================================
CREATE TABLE LoaiTaiKhoan (
    MaLoai VARCHAR(20) PRIMARY KEY, 
    TenLoai NVARCHAR(50) NOT NULL, 
    MoTa NVARCHAR(200),
    TrangThai BIT DEFAULT 1
);
GO

-- =============================================
-- 3. TẠO BẢNG TÀI KHOẢN (Tạo bảng con sau)
-- =============================================
CREATE TABLE TaiKhoan (
    IDTaiKhoan VARCHAR(20) PRIMARY KEY,
    TenTaiKhoan VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(100) NOT NULL, 
    MaLoai VARCHAR(20) NOT NULL,
    CONSTRAINT FK_TaiKhoan_LoaiTaiKhoan FOREIGN KEY (MaLoai) REFERENCES LoaiTaiKhoan(MaLoai)
);
GO

-- =============================================
-- 4. THÊM DỮ LIỆU MẪU (Bắt buộc phải chạy để test Login)
-- =============================================
INSERT INTO LoaiTaiKhoan (MaLoai, TenLoai, MoTa) VALUES 
('ADMIN', N'Ban Giám Hiệu', N'Quản trị hệ thống'),
('GV', N'Giáo Viên', N'Giáo viên lớp'),
('PH', N'Phụ Huynh', N'Phụ huynh học sinh');
select*from TaiKhoan
select*from LoaiTaiKhoan
select*from GiaoVien
select*from HosoSucKhoe
select*from BangDiem
select*from PhanCong
select*from ThoiKhoaBieu
select*from HocPhi
select*from DiemDanh
select*from ThongBao
select*from HocSinh

INSERT INTO TaiKhoan (IDTaiKhoan, TenTaiKhoan, MatKhau, MaLoai) VALUES 
('AD01', 'admin', '123', 'ADMIN'),
('GV01', 'giaovien', '123', 'GV');
GO

CREATE TABLE GiaoVien (
    MaGiaoVien VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    SDT VARCHAR(15),
    ChuyenMon NVARCHAR(100),
    BangCap NVARCHAR(100),
    Email VARCHAR(100)
);

CREATE TABLE HocSinh (
    MaHocSinh VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    MaLop VARCHAR(20),
    PhuHuynh NVARCHAR(100),
    SDTLienHe VARCHAR(15)
);

CREATE TABLE HosoSucKhoe (
    MaPhieuKham VARCHAR(20) PRIMARY KEY,
    NgayKham DATE,
    ChieuCao FLOAT,
    CanNang FLOAT,
    TinhTrangSucKhoe NVARCHAR(200),
    GhiChu NVARCHAR(MAX),
    MaHocSinh VARCHAR(20) NOT NULL,
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh)
);

CREATE TABLE PhanCong (
    MaPhanCong VARCHAR(20) PRIMARY KEY,
    MaLop VARCHAR(20),
    MaMonHoc NVARCHAR(50),
    NamHoc VARCHAR(20),
    HocKy INT,
    MaGiaoVien VARCHAR(20),
    MaHocSinh VARCHAR(20),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh)
);

CREATE TABLE ThoiKhoaBieu (
    MaTKB VARCHAR(20) PRIMARY KEY,
    MaLop VARCHAR(20),
    MaMonHoc NVARCHAR(50),
    TenGiaoVien NVARCHAR(100),
    Thu NVARCHAR(10),
    TietHoc INT,
    MaPhanCong VARCHAR(20),
    FOREIGN KEY (MaPhanCong) REFERENCES PhanCong(MaPhanCong)
);

CREATE TABLE BangDiem (
    MaBangDiem VARCHAR(20) PRIMARY KEY,
    MaMonHoc NVARCHAR(50),
    NamHoc VARCHAR(20),
    HocKy INT,
    DiemSo FLOAT,
    NhanXet NVARCHAR(MAX),
    MaHocSinh VARCHAR(20),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh)
);

CREATE TABLE HocPhi (
    MaHocPhi VARCHAR(20) PRIMARY KEY,
    ThangThu INT,
    TongTien DECIMAL(18, 0),
    TrangThai NVARCHAR(50),
    NgayThanhToan DATETIME,
    HinhThuc NVARCHAR(50),
    MaHocSinh VARCHAR(20),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh)
);

CREATE TABLE DiemDanh (
    MaDiemDanh VARCHAR(20) PRIMARY KEY,
    NgayDiemDanh DATE NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(MAX),
    MaHocSinh VARCHAR(20) NOT NULL,
    MaGiaoVien VARCHAR(20) NOT NULL,
    GioDen DATETIME DEFAULT GETDATE(),
    GioVe DATETIME NULL,
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien)
);

CREATE TABLE ThongBao (
    MaThongBao VARCHAR(20) PRIMARY KEY,
    TieuDe NVARCHAR(200),
    NoiDung NVARCHAR(MAX),
    NguoiGui NVARCHAR(100),
    NguoiNhan NVARCHAR(100),
    NgayGui DATETIME,
    LoaiThongBao NVARCHAR(50),
    MaGiaoVien VARCHAR(20),
    MaHocSinh VARCHAR(20),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh)
);
USE QLMN_TH_V2;
GO

-- =============================================
-- 1. THÊM DỮ LIỆU BẢNG GIAO VIEN (10 Giáo viên)
-- Lưu ý: GV01 trùng khớp với tài khoản đăng nhập mẫu
-- =============================================
INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, SDT, ChuyenMon, BangCap, Email) VALUES 
('GV01', N'Nguyễn Thị Thu Hà', '1990-05-15', '0987654321', N'Sư phạm Mầm non', N'Đại học', 'thuha@gmail.com'),
('GV02', N'Trần Văn Nam', '1988-08-20', '0912345678', N'Giáo dục thể chất', N'Cao đẳng', 'trannam@gmail.com'),
('GV03', N'Lê Thị Mỹ Linh', '1995-02-10', '0909123456', N'Sư phạm Âm nhạc', N'Đại học', 'mylinh@gmail.com'),
('GV04', N'Phạm Thu Trang', '1992-11-30', '0933445566', N'Sư phạm Mầm non', N'Đại học', 'thutrang@gmail.com'),
('GV05', N'Hoàng Văn Hùng', '1985-07-25', '0977889900', N'Tiếng Anh', N'Thạc sĩ', 'hung.hoang@gmail.com'),
('GV06', N'Đặng Thị Tuyết', '1998-09-12', '0966112233', N'Sư phạm Mỹ thuật', N'Cao đẳng', 'tuyetdang@gmail.com'),
('GV07', N'Vũ Minh Tuấn', '1991-03-05', '0944556677', N'Quản lý giáo dục', N'Đại học', 'minhtuan@gmail.com'),
('GV08', N'Bùi Thị Lan', '1989-12-20', '0911223344', N'Sư phạm Mầm non', N'Trung cấp', 'lanbui@gmail.com'),
('GV09', N'Ngô Thanh Vân', '1993-06-18', '0988776655', N'Dinh dưỡng', N'Đại học', 'vanngo@gmail.com'),
('GV10', N'Đỗ Đức Thắng', '1990-01-15', '0999888777', N'Kỹ năng sống', N'Đại học', 'thangdo@gmail.com');
GO

-- =============================================
-- 2. THÊM DỮ LIỆU BẢNG HOC SINH (10 Học sinh)
-- =============================================
INSERT INTO HocSinh (MaHocSinh, HoTen, NgaySinh, GioiTinh, DiaChi, MaLop, PhuHuynh, SDTLienHe) VALUES 
('HS01', N'Nguyễn Gia Bảo', '2019-02-15', N'Nam', N'123 Lê Lợi, TP.HCM', 'MAM01', N'Nguyễn Văn A', '0901000001'),
('HS02', N'Trần Ngọc Hân', '2019-05-20', N'Nữ', N'456 Nguyễn Trãi, TP.HCM', 'MAM01', N'Trần Thị B', '0901000002'),
('HS03', N'Lê Minh Khôi', '2018-08-10', N'Nam', N'789 Trần Hưng Đạo, TP.HCM', 'CHOI01', N'Lê Văn C', '0901000003'),
('HS04', N'Phạm Bảo Châu', '2018-11-25', N'Nữ', N'321 Lý Thường Kiệt, TP.HCM', 'CHOI01', N'Phạm Thị D', '0901000004'),
('HS05', N'Hoàng Anh Tuấn', '2017-01-30', N'Nam', N'654 Điện Biên Phủ, TP.HCM', 'LA01', N'Hoàng Văn E', '0901000005'),
('HS06', N'Đặng Thảo My', '2017-04-12', N'Nữ', N'987 Võ Văn Kiệt, TP.HCM', 'LA01', N'Đặng Thị F', '0901000006'),
('HS07', N'Vũ Đức Minh', '2019-06-18', N'Nam', N'147 Hai Bà Trưng, TP.HCM', 'MAM02', N'Vũ Văn G', '0901000007'),
('HS08', N'Bùi Phương Linh', '2018-09-22', N'Nữ', N'258 Nam Kỳ Khởi Nghĩa, TP.HCM', 'CHOI02', N'Bùi Thị H', '0901000008'),
('HS09', N'Ngô Tấn Tài', '2017-12-05', N'Nam', N'369 Pasteur, TP.HCM', 'LA02', N'Ngô Văn I', '0901000009'),
('HS10', N'Đỗ Mai Anh', '2019-10-10', N'Nữ', N'741 Lê Duẩn, TP.HCM', 'MAM02', N'Đỗ Thị K', '0901000010');
GO

-- =============================================
-- 3. THÊM DỮ LIỆU HỒ SƠ SỨC KHỎE (10 Hồ sơ)
-- =============================================
INSERT INTO HosoSucKhoe (MaPhieuKham, NgayKham, ChieuCao, CanNang, TinhTrangSucKhoe, GhiChu, MaHocSinh) VALUES 
('SK01', '2023-09-01', 95.5, 14.2, N'Bình thường', N'Ăn uống tốt', 'HS01'),
('SK02', '2023-09-01', 92.0, 13.5, N'Hơi gầy', N'Cần bổ sung sữa', 'HS02'),
('SK03', '2023-09-02', 105.0, 18.0, N'Tốt', N'Phát triển tốt', 'HS03'),
('SK04', '2023-09-02', 102.5, 17.5, N'Bình thường', N'Không', 'HS04'),
('SK05', '2023-09-03', 115.0, 22.0, N'Thừa cân nhẹ', N'Hạn chế tinh bột', 'HS05'),
('SK06', '2023-09-03', 110.0, 19.5, N'Bình thường', N'Khỏe mạnh', 'HS06'),
('SK07', '2023-09-04', 96.0, 15.0, N'Tốt', N'Năng động', 'HS07'),
('SK08', '2023-09-04', 103.0, 16.5, N'Bình thường', N'Hay bị ho', 'HS08'),
('SK09', '2023-09-05', 112.0, 20.0, N'Tốt', N'Không', 'HS09'),
('SK10', '2023-09-05', 94.0, 13.8, N'Bình thường', N'Ngủ ít', 'HS10');
GO

-- =============================================
-- 4. THÊM DỮ LIỆU PHÂN CÔNG (10 Phân công)
-- =============================================
INSERT INTO PhanCong (MaPhanCong, MaLop, MaMonHoc, NamHoc, HocKy, MaGiaoVien, MaHocSinh) VALUES 
('PC01', 'MAM01', N'Nhận biết tập nói', '2023-2024', 1, 'GV01', 'HS01'),
('PC02', 'MAM01', N'Vận động', '2023-2024', 1, 'GV01', 'HS02'),
('PC03', 'CHOI01', N'Vẽ tranh', '2023-2024', 1, 'GV06', 'HS03'),
('PC04', 'CHOI01', N'Âm nhạc', '2023-2024', 1, 'GV03', 'HS04'),
('PC05', 'LA01', N'Làm quen chữ cái', '2023-2024', 1, 'GV04', 'HS05'),
('PC06', 'LA01', N'Toán sơ cấp', '2023-2024', 1, 'GV04', 'HS06'),
('PC07', 'MAM02', N'Kỹ năng sống', '2023-2024', 1, 'GV10', 'HS07'),
('PC08', 'CHOI02', N'Tiếng Anh', '2023-2024', 1, 'GV05', 'HS08'),
('PC09', 'LA02', N'Thể dục nhịp điệu', '2023-2024', 1, 'GV02', 'HS09'),
('PC10', 'MAM02', N'Kể chuyện', '2023-2024', 1, 'GV08', 'HS10');
GO

-- =============================================
-- 5. THÊM DỮ LIỆU THỜI KHÓA BIỂU (10 TKB)
-- =============================================
INSERT INTO ThoiKhoaBieu (MaTKB, MaLop, MaMonHoc, TenGiaoVien, Thu, TietHoc, MaPhanCong) VALUES 
('TKB01', 'MAM01', N'Nhận biết tập nói', N'Nguyễn Thị Thu Hà', N'Thứ 2', 1, 'PC01'),
('TKB02', 'MAM01', N'Vận động', N'Nguyễn Thị Thu Hà', N'Thứ 3', 2, 'PC02'),
('TKB03', 'CHOI01', N'Vẽ tranh', N'Đặng Thị Tuyết', N'Thứ 4', 3, 'PC03'),
('TKB04', 'CHOI01', N'Âm nhạc', N'Lê Thị Mỹ Linh', N'Thứ 5', 4, 'PC04'),
('TKB05', 'LA01', N'Làm quen chữ cái', N'Phạm Thu Trang', N'Thứ 2', 1, 'PC05'),
('TKB06', 'LA01', N'Toán sơ cấp', N'Phạm Thu Trang', N'Thứ 6', 2, 'PC06'),
('TKB07', 'MAM02', N'Kỹ năng sống', N'Đỗ Đức Thắng', N'Thứ 4', 3, 'PC07'),
('TKB08', 'CHOI02', N'Tiếng Anh', N'Hoàng Văn Hùng', N'Thứ 3', 1, 'PC08'),
('TKB09', 'LA02', N'Thể dục nhịp điệu', N'Trần Văn Nam', N'Thứ 5', 2, 'PC09'),
('TKB10', 'MAM02', N'Kể chuyện', N'Bùi Thị Lan', N'Thứ 6', 4, 'PC10');
GO

-- =============================================
-- 6. THÊM DỮ LIỆU BẢNG ĐIỂM (10 Bảng điểm)
-- =============================================
INSERT INTO BangDiem (MaBangDiem, MaMonHoc, NamHoc, HocKy, DiemSo, NhanXet, MaHocSinh) VALUES 
('BD01', N'Nhận biết', '2023-2024', 1, 9.0, N'Bé nhận biết tốt', 'HS01'),
('BD02', N'Vận động', '2023-2024', 1, 8.5, N'Nhanh nhẹn', 'HS02'),
('BD03', N'Mỹ thuật', '2023-2024', 1, 9.5, N'Có năng khiếu vẽ', 'HS03'),
('BD04', N'Âm nhạc', '2023-2024', 1, 8.0, N'Hát đúng nhịp', 'HS04'),
('BD05', N'Chữ cái', '2023-2024', 1, 10.0, N'Thuộc bảng chữ cái', 'HS05'),
('BD06', N'Toán', '2023-2024', 1, 9.0, N'Đếm số tốt', 'HS06'),
('BD07', N'Kỹ năng', '2023-2024', 1, 8.5, N'Hòa đồng', 'HS07'),
('BD08', N'Tiếng Anh', '2023-2024', 1, 9.5, N'Phát âm chuẩn', 'HS08'),
('BD09', N'Thể dục', '2023-2024', 1, 9.0, N'Khỏe mạnh', 'HS09'),
('BD10', N'Kể chuyện', '2023-2024', 1, 8.5, N'Giọng kể hay', 'HS10');
GO

-- =============================================
-- 7. THÊM DỮ LIỆU HỌC PHÍ (10 Giao dịch)
-- =============================================
INSERT INTO HocPhi (MaHocPhi, ThangThu, TongTien, TrangThai, NgayThanhToan, HinhThuc, MaHocSinh) VALUES 
('HP01', 9, 2500000, N'Đã đóng', '2023-09-05', N'Tiền mặt', 'HS01'),
('HP02', 9, 2500000, N'Đã đóng', '2023-09-06', N'Chuyển khoản', 'HS02'),
('HP03', 9, 2800000, N'Chưa đóng', NULL, NULL, 'HS03'),
('HP04', 9, 2800000, N'Đã đóng', '2023-09-07', N'Tiền mặt', 'HS04'),
('HP05', 9, 3000000, N'Đã đóng', '2023-09-05', N'Chuyển khoản', 'HS05'),
('HP06', 10, 3000000, N'Chưa đóng', NULL, NULL, 'HS06'),
('HP07', 10, 2500000, N'Đã đóng', '2023-10-05', N'Ví điện tử', 'HS07'),
('HP08', 10, 2800000, N'Đã đóng', '2023-10-06', N'Chuyển khoản', 'HS08'),
('HP09', 10, 3000000, N'Đã đóng', '2023-10-07', N'Tiền mặt', 'HS09'),
('HP10', 10, 2500000, N'Chưa đóng', NULL, NULL, 'HS10');
GO

-- =============================================
-- 8. THÊM DỮ LIỆU ĐIỂM DANH (10 Lượt)
-- =============================================
INSERT INTO DiemDanh (MaDiemDanh, NgayDiemDanh, TrangThai, GhiChu, MaHocSinh, MaGiaoVien) VALUES 
('DD01', GETDATE(), N'Có mặt', N'Đến đúng giờ', 'HS01', 'GV01'),
('DD02', GETDATE(), N'Vắng', N'Bị ốm', 'HS02', 'GV01'),
('DD03', GETDATE(), N'Có mặt', N'', 'HS03', 'GV06'),
('DD04', GETDATE(), N'Có mặt', N'', 'HS04', 'GV03'),
('DD05', GETDATE(), N'Có mặt', N'Đến muộn', 'HS05', 'GV04'),
('DD06', GETDATE(), N'Vắng', N'Gia đình có việc', 'HS06', 'GV04'),
('DD07', GETDATE(), N'Có mặt', N'', 'HS07', 'GV10'),
('DD08', GETDATE(), N'Có mặt', N'', 'HS08', 'GV05'),
('DD09', GETDATE(), N'Có mặt', N'', 'HS09', 'GV02'),
('DD10', GETDATE(), N'Có mặt', N'', 'HS10', 'GV08');
GO

-- =============================================
-- 9. THÊM DỮ LIỆU THÔNG BÁO (10 Thông báo)
-- =============================================
INSERT INTO ThongBao (MaThongBao, TieuDe, NoiDung, NguoiGui, NguoiNhan, NgayGui, LoaiThongBao, MaGiaoVien, MaHocSinh) VALUES 
('TB01', N'Họp phụ huynh', N'Mời họp phụ huynh đầu năm', N'BGH', N'Phụ huynh HS01', GETDATE(), N'Chung', 'GV01', 'HS01'),
('TB02', N'Nhắc nhở học phí', N'Quý phụ huynh vui lòng đóng học phí', N'Kế toán', N'Phụ huynh HS03', GETDATE(), N'Học phí', NULL, 'HS03'),
('TB03', N'Thông báo nghỉ lễ', N'Học sinh nghỉ lễ 2/9', N'BGH', N'Toàn trường', GETDATE(), N'Chung', NULL, NULL),
('TB04', N'Sức khỏe bé', N'Hôm nay bé ăn ít', N'Cô Hà', N'Mẹ bé Bảo', GETDATE(), N'Riêng', 'GV01', 'HS01'),
('TB05', N'Khen thưởng', N'Bé ngoan tuần này', N'Cô Trang', N'Ba bé Tuấn', GETDATE(), N'Khen thưởng', 'GV04', 'HS05'),
('TB06', N'Lịch tiêm phòng', N'Thông báo lịch tiêm vacxin', N'Y tế', N'Phụ huynh HS07', GETDATE(), N'Sức khỏe', NULL, 'HS07'),
('TB07', N'Dã ngoại', N'Đăng ký tham gia dã ngoại', N'BGH', N'Lớp Lá 1', GETDATE(), N'Hoạt động', 'GV04', NULL),
('TB08', N'Mang đồ dùng', N'Nhờ PH mang thêm quần áo cho bé', N'Cô Lan', N'Mẹ bé Mai Anh', GETDATE(), N'Riêng', 'GV08', 'HS10'),
('TB09', N'Thực đơn tuần', N'Gửi thực đơn tuần mới', N'Nhà bếp', N'Toàn trường', GETDATE(), N'Dinh dưỡng', NULL, NULL),
('TB10', N'Về sớm', N'Xin đón bé về sớm', N'Mẹ bé Linh', N'Cô giáo', GETDATE(), N'Phụ huynh gửi', 'GV05', 'HS08');
GO



USE QLMN_TH_V2;
GO

-- 1. Thủ tục lấy danh sách học sinh
CREATE PROCEDURE sp_HocSinh_Get_All
AS
BEGIN
    SELECT * FROM HocSinh;
END;
GO

CREATE PROCEDURE sp_HocSinh_Get_ById
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    SELECT * FROM HocSinh WHERE MaHocSinh = @MaHocSinh;
END;
GO

-- 3. Thủ tục Thêm mới (Create)
drop PROCEDURE sp_HocSinh_Create
CREATE PROCEDURE sp_HocSinh_Create
    @MaHocSinh VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @DiaChi NVARCHAR(200),
    @MaLop VARCHAR(20),
    @PhuHuynh NVARCHAR(100),
    @SDTLienHe VARCHAR(15)
AS
BEGIN
    -- Kiểm tra trùng mã
    IF EXISTS (SELECT 1 FROM HocSinh WHERE MaHocSinh = @MaHocSinh)
    BEGIN
        SELECT N'Mã học sinh đã tồn tại!' AS ErrorMsg;
        RETURN;
    END

    INSERT INTO HocSinh (MaHocSinh, HoTen, NgaySinh, GioiTinh, DiaChi, MaLop, PhuHuynh, SDTLienHe)
    VALUES (@MaHocSinh, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @MaLop, @PhuHuynh, @SDTLienHe);
    
    SELECT ''; -- Trả về rỗng nghĩa là thành công
END;
GO

-- 4. Thủ tục Cập nhật (Update)
CREATE PROCEDURE sp_HocSinh_Update
    @MaHocSinh VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @DiaChi NVARCHAR(200),
    @MaLop VARCHAR(20),
    @PhuHuynh NVARCHAR(100),
    @SDTLienHe VARCHAR(15)
AS
BEGIN
    UPDATE HocSinh
    SET HoTen = @HoTen,
        NgaySinh = @NgaySinh,
        GioiTinh = @GioiTinh,
        DiaChi = @DiaChi,
        MaLop = @MaLop,
        PhuHuynh = @PhuHuynh,
        SDTLienHe = @SDTLienHe
    WHERE MaHocSinh = @MaHocSinh;
END;
GO

-- 5. Thủ tục Xóa (Delete)
CREATE PROCEDURE sp_HocSinh_Delete
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    DELETE FROM HocSinh WHERE MaHocSinh = @MaHocSinh;
END;
GO

USE QLMN_TH_V2;
GO



-- 1. Lấy danh sách giáo viên

CREATE PROCEDURE sp_GiaoVien_DanhSach
AS
BEGIN
    SELECT * FROM GiaoVien
END
GO

-- 2. Lấy chi tiết giáo viên
CREATE PROCEDURE sp_GiaoVien_ChiTiet
    @MaGiaoVien VARCHAR(20)
AS
BEGIN
    SELECT * FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien
END
GO

-- 3. Thêm giáo viên
CREATE PROCEDURE sp_GiaoVien_Them
    @MaGiaoVien VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @SDT VARCHAR(15),
    @ChuyenMon NVARCHAR(100),
    @BangCap NVARCHAR(100),
    @Email VARCHAR(100)
AS
BEGIN
    -- Kiểm tra trùng mã
    IF EXISTS (SELECT 1 FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien)
    BEGIN
        RAISERROR(N'Mã giáo viên đã tồn tại', 16, 1);
        RETURN;
    END

    INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, SDT, ChuyenMon, BangCap, Email)
    VALUES (@MaGiaoVien, @HoTen, @NgaySinh, @SDT, @ChuyenMon, @BangCap, @Email)
END
GO

-- 4. Sửa giáo viên
CREATE PROCEDURE sp_GiaoVien_Sua
    @MaGiaoVien VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @SDT VARCHAR(15),
    @ChuyenMon NVARCHAR(100),
    @BangCap NVARCHAR(100),
    @Email VARCHAR(100)
AS
BEGIN
    UPDATE GiaoVien
    SET HoTen = @HoTen,
        NgaySinh = @NgaySinh,
        SDT = @SDT,
        ChuyenMon = @ChuyenMon,
        BangCap = @BangCap,
        Email = @Email
    WHERE MaGiaoVien = @MaGiaoVien
END
GO

-- 5. Xóa giáo viên
CREATE PROCEDURE sp_GiaoVien_Xoa
    @MaGiaoVien VARCHAR(20)
AS
BEGIN
    DELETE FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien
END
GO

USE QLMN_TH_V2;
GO

-- 1. SP Đăng nhập (Quan trọng nhất)
CREATE OR ALTER PROCEDURE sp_TaiKhoan_Login
    @TenTaiKhoan VARCHAR(50),
    @MatKhau VARCHAR(100)
AS
BEGIN
    SELECT tk.*, l.TenLoai 
    FROM TaiKhoan tk
    JOIN LoaiTaiKhoan l ON tk.MaLoai = l.MaLoai
    WHERE tk.TenTaiKhoan = @TenTaiKhoan AND tk.MatKhau = @MatKhau
END
GO

-- 2. SP Lấy danh sách (Kèm tên loại)
CREATE OR ALTER PROCEDURE sp_TaiKhoan_GetAll
AS
BEGIN
    SELECT tk.*, l.TenLoai 
    FROM TaiKhoan tk
    LEFT JOIN LoaiTaiKhoan l ON tk.MaLoai = l.MaLoai
END
GO

-- 3. SP Lấy chi tiết theo ID
CREATE OR ALTER PROCEDURE sp_TaiKhoan_GetById
    @IDTaiKhoan VARCHAR(20)
AS
BEGIN
    SELECT * FROM TaiKhoan WHERE IDTaiKhoan = @IDTaiKhoan
END
GO

-- 4. SP Thêm mới
CREATE OR ALTER PROCEDURE sp_TaiKhoan_Insert
    @IDTaiKhoan VARCHAR(20),
    @TenTaiKhoan VARCHAR(50),
    @MatKhau VARCHAR(100),
    @MaLoai VARCHAR(20)
AS
BEGIN
    -- Check trùng ID
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE IDTaiKhoan = @IDTaiKhoan)
    BEGIN
        SELECT N'Lỗi: ID tài khoản đã tồn tại'
        RETURN;
    END
    -- Check trùng Username
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan)
    BEGIN
        SELECT N'Lỗi: Tên đăng nhập đã tồn tại'
        RETURN;
    END

    INSERT INTO TaiKhoan (IDTaiKhoan, TenTaiKhoan, MatKhau, MaLoai)
    VALUES (@IDTaiKhoan, @TenTaiKhoan, @MatKhau, @MaLoai)

    SELECT N'Thêm thành công'
END
GO

-- 5. SP Cập nhật (Đổi mật khẩu, đổi quyền)
CREATE OR ALTER PROCEDURE sp_TaiKhoan_Update
    @IDTaiKhoan VARCHAR(20),
    @MatKhau VARCHAR(100),
    @MaLoai VARCHAR(20)
AS
BEGIN
    UPDATE TaiKhoan 
    SET MatKhau = @MatKhau, 
        MaLoai = @MaLoai
    WHERE IDTaiKhoan = @IDTaiKhoan

    SELECT N'Cập nhật thành công'
END
GO

-- 6. SP Xóa
CREATE OR ALTER PROCEDURE sp_TaiKhoan_Delete
    @IDTaiKhoan VARCHAR(20)
AS
BEGIN
    DELETE FROM TaiKhoan WHERE IDTaiKhoan = @IDTaiKhoan
    SELECT N'Xóa thành công'
END
GO


USE QLMN_TH_V2;
GO

-- 1. Lấy danh sách Phân công (Kèm tên Giáo viên và Tên Học sinh)
CREATE OR ALTER PROCEDURE sp_PhanCong_GetAll
AS
BEGIN
    SELECT pc.*, gv.HoTen AS TenGiaoVien, hs.HoTen AS TenHocSinh
    FROM PhanCong pc
    LEFT JOIN GiaoVien gv ON pc.MaGiaoVien = gv.MaGiaoVien
    LEFT JOIN HocSinh hs ON pc.MaHocSinh = hs.MaHocSinh
END
GO
EXEC sp_PhanCong_GetAll;
-- 2. Lấy chi tiết theo ID
CREATE OR ALTER PROCEDURE sp_PhanCong_GetById
    @MaPhanCong VARCHAR(20)
AS
BEGIN
    SELECT * FROM PhanCong WHERE MaPhanCong = @MaPhanCong
END
GO

-- 3. Thêm mới Phân công
CREATE OR ALTER PROCEDURE sp_PhanCong_Them
    @MaPhanCong VARCHAR(20),
    @MaLop VARCHAR(20),
    @MaMonHoc NVARCHAR(50),
    @NamHoc VARCHAR(20),
    @HocKy INT,
    @MaGiaoVien VARCHAR(20),
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    -- Kiểm tra trùng mã
    IF EXISTS (SELECT 1 FROM PhanCong WHERE MaPhanCong = @MaPhanCong)
    BEGIN
        SELECT N'Lỗi: Mã phân công đã tồn tại'
        RETURN;
    END

    INSERT INTO PhanCong (MaPhanCong, MaLop, MaMonHoc, NamHoc, HocKy, MaGiaoVien, MaHocSinh)
    VALUES (@MaPhanCong, @MaLop, @MaMonHoc, @NamHoc, @HocKy, @MaGiaoVien, @MaHocSinh)

    SELECT N'Thêm thành công'
END
GO

-- 4. Cập nhật Phân công
CREATE OR ALTER PROCEDURE sp_PhanCong_Sua
    @MaPhanCong VARCHAR(20),
    @MaLop VARCHAR(20),
    @MaMonHoc NVARCHAR(50),
    @NamHoc VARCHAR(20),
    @HocKy INT,
    @MaGiaoVien VARCHAR(20),
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    UPDATE PhanCong
    SET MaLop = @MaLop,
        MaMonHoc = @MaMonHoc,
        NamHoc = @NamHoc,
        HocKy = @HocKy,
        MaGiaoVien = @MaGiaoVien,
        MaHocSinh = @MaHocSinh
    WHERE MaPhanCong = @MaPhanCong

    SELECT N'Cập nhật thành công'
END
GO

-- 5. Xóa Phân công
CREATE OR ALTER PROCEDURE sp_PhanCong_Xoa
    @MaPhanCong VARCHAR(20)
AS
BEGIN
    DELETE FROM PhanCong WHERE MaPhanCong = @MaPhanCong
    SELECT N'Xóa thành công'
END
GO

USE QLMN_TH_V2;
GO

-- 1. Lấy danh sách Học phí (Kèm tên học sinh để dễ nhìn)
CREATE OR ALTER PROCEDURE sp_HocPhi_GetAll
AS
BEGIN
    SELECT hp.*, hs.HoTen 
    FROM HocPhi hp
    LEFT JOIN HocSinh hs ON hp.MaHocSinh = hs.MaHocSinh
    ORDER BY hp.ThangThu DESC
END
GO

-- 2. Lấy chi tiết 1 phiếu thu
CREATE OR ALTER PROCEDURE sp_HocPhi_GetById
    @MaHocPhi VARCHAR(20)
AS
BEGIN
    SELECT * FROM HocPhi WHERE MaHocPhi = @MaHocPhi
END
GO

-- 3. Tạo phiếu thu mới (Mặc định là Chưa đóng)
CREATE OR ALTER PROCEDURE sp_HocPhi_Them
    @MaHocPhi VARCHAR(20),
    @ThangThu INT,
    @TongTien DECIMAL(18,0),
    @TrangThai NVARCHAR(50), -- Ví dụ: 'Chưa đóng'
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM HocPhi WHERE MaHocPhi = @MaHocPhi)
    BEGIN
        SELECT N'Lỗi: Mã học phí đã tồn tại' RETURN;
    END

    INSERT INTO HocPhi (MaHocPhi, ThangThu, TongTien, TrangThai, NgayThanhToan, HinhThuc, MaHocSinh)
    VALUES (@MaHocPhi, @ThangThu, @TongTien, @TrangThai, NULL, NULL, @MaHocSinh)

    SELECT N'Tạo phiếu thu thành công'
END
GO

-- 4. Cập nhật (Thường dùng để xác nhận Đã đóng tiền)
CREATE OR ALTER PROCEDURE sp_HocPhi_CapNhat
    @MaHocPhi VARCHAR(20),
    @ThangThu INT,
    @TongTien DECIMAL(18,0),
    @TrangThai NVARCHAR(50),     -- 'Đã đóng'
    @NgayThanhToan DATETIME,     -- Ngày đóng
    @HinhThuc NVARCHAR(50),      -- 'Tiền mặt'/'Chuyển khoản'
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    UPDATE HocPhi
    SET ThangThu = @ThangThu,
        TongTien = @TongTien,
        TrangThai = @TrangThai,
        NgayThanhToan = @NgayThanhToan,
        HinhThuc = @HinhThuc,
        MaHocSinh = @MaHocSinh
    WHERE MaHocPhi = @MaHocPhi

    SELECT N'Cập nhật thành công'
END
GO

-- 5. Xóa phiếu thu
CREATE OR ALTER PROCEDURE sp_HocPhi_Xoa
    @MaHocPhi VARCHAR(20)
AS
BEGIN
    DELETE FROM HocPhi WHERE MaHocPhi = @MaHocPhi
    SELECT N'Xóa thành công'
END
GO
USE QLMN_TH_V2;
GO

-- 1. Lấy TKB theo Lớp (Quan trọng nhất để hiển thị dạng bảng)
-- Tạo thủ tục lấy danh sách Thời Khóa Biểu
CREATE PROCEDURE sp_ThoiKhoaBieu_GetAll
AS
BEGIN
    SELECT MaTKB, MaLop, MaMonHoc, TenGiaoVien, Thu, TietHoc, MaPhanCong
    FROM ThoiKhoaBieu
END
GO

-- 2. Lấy chi tiết
CREATE OR ALTER PROCEDURE sp_ThoiKhoaBieu_GetById
    @MaTKB VARCHAR(20)
AS
BEGIN
    SELECT * FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB
END
GO

-- 3. Thêm mới
CREATE OR ALTER PROCEDURE sp_ThoiKhoaBieu_Them
    @MaTKB VARCHAR(20),
    @MaLop VARCHAR(20),
    @MaMonHoc NVARCHAR(50),
    @TenGiaoVien NVARCHAR(100),
    @Thu NVARCHAR(10),
    @TietHoc INT,
    @MaPhanCong VARCHAR(20) -- Có thể NULL nếu không buộc chặt
AS
BEGIN
    IF EXISTS (SELECT 1 FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB)
    BEGIN
        SELECT N'Lỗi: Mã TKB đã tồn tại' RETURN;
    END

    -- Kiểm tra trùng lịch (Cùng lớp, cùng thứ, cùng tiết đã có môn khác chưa)
    IF EXISTS (SELECT 1 FROM ThoiKhoaBieu WHERE MaLop = @MaLop AND Thu = @Thu AND TietHoc = @TietHoc)
    BEGIN
         SELECT N'Lỗi: Tiết học này đã có môn khác' RETURN;
    END

    INSERT INTO ThoiKhoaBieu (MaTKB, MaLop, MaMonHoc, TenGiaoVien, Thu, TietHoc, MaPhanCong)
    VALUES (@MaTKB, @MaLop, @MaMonHoc, @TenGiaoVien, @Thu, @TietHoc, @MaPhanCong)

    SELECT N'Thêm thành công'
END
GO

-- 4. Cập nhật
CREATE OR ALTER PROCEDURE sp_ThoiKhoaBieu_Sua
    @MaTKB VARCHAR(20),
    @MaLop VARCHAR(20),
    @MaMonHoc NVARCHAR(50),
    @TenGiaoVien NVARCHAR(100),
    @Thu NVARCHAR(10),
    @TietHoc INT,
    @MaPhanCong VARCHAR(20)
AS
BEGIN
    UPDATE ThoiKhoaBieu
    SET MaLop = @MaLop,
        MaMonHoc = @MaMonHoc,
        TenGiaoVien = @TenGiaoVien,
        Thu = @Thu,
        TietHoc = @TietHoc,
        MaPhanCong = @MaPhanCong
    WHERE MaTKB = @MaTKB

    SELECT N'Cập nhật thành công'
END
GO

-- 5. Xóa
CREATE OR ALTER PROCEDURE sp_ThoiKhoaBieu_Xoa
    @MaTKB VARCHAR(20)
AS
BEGIN
    DELETE FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB
    SELECT N'Xóa thành công'
END
GO

USE QLMN_TH_V2;
GO

-- 1. Lấy danh sách học sinh
CREATE OR ALTER PROCEDURE sp_HocSinh_GetAll
AS
BEGIN
    SELECT * FROM HocSinh
END
GO

-- 2. Lấy chi tiết học sinh theo mã
CREATE OR ALTER PROCEDURE sp_HocSinh_GetById
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    SELECT * FROM HocSinh WHERE MaHocSinh = @MaHocSinh
END
GO

-- 3. Thêm mới học sinh
CREATE OR ALTER PROCEDURE sp_HocSinh_Them
    @MaHocSinh VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @DiaChi NVARCHAR(200),
    @MaLop VARCHAR(20),
    @PhuHuynh NVARCHAR(100),
    @SDTLienHe VARCHAR(15)
AS
BEGIN
    -- Kiểm tra trùng mã
    IF EXISTS (SELECT 1 FROM HocSinh WHERE MaHocSinh = @MaHocSinh)
    BEGIN
        SELECT N'Lỗi: Mã học sinh đã tồn tại'
        RETURN;
    END

    INSERT INTO HocSinh (MaHocSinh, HoTen, NgaySinh, GioiTinh, DiaChi, MaLop, PhuHuynh, SDTLienHe)
    VALUES (@MaHocSinh, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @MaLop, @PhuHuynh, @SDTLienHe)

    SELECT N'Thêm thành công'
END
GO

-- 4. Cập nhật thông tin học sinh
CREATE OR ALTER PROCEDURE sp_HocSinh_Sua
    @MaHocSinh VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @DiaChi NVARCHAR(200),
    @MaLop VARCHAR(20),
    @PhuHuynh NVARCHAR(100),
    @SDTLienHe VARCHAR(15)
AS
BEGIN
    UPDATE HocSinh
    SET HoTen = @HoTen,
        NgaySinh = @NgaySinh,
        GioiTinh = @GioiTinh,
        DiaChi = @DiaChi,
        MaLop = @MaLop,
        PhuHuynh = @PhuHuynh,
        SDTLienHe = @SDTLienHe
    WHERE MaHocSinh = @MaHocSinh

    SELECT N'Cập nhật thành công'
END
GO

-- 5. Xóa học sinh
CREATE OR ALTER PROCEDURE sp_HocSinh_Xoa
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    DELETE FROM HocSinh WHERE MaHocSinh = @MaHocSinh
    SELECT N'Xóa thành công'
END
GO

USE QLMN_TH_V2;
GO

-- =============================================



-- 1. Xóa thủ tục cũ (Nếu tồn tại)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DiemDanh_GetAll')
BEGIN
    DROP PROCEDURE sp_DiemDanh_GetAll
END
GO

CREATE OR ALTER PROCEDURE sp_DiemDanh_GetAll
    @MaLop NVARCHAR(50) = NULL, -- Mặc định là NULL
    @NgayDiemDanh DATE = NULL   -- Mặc định là NULL
AS
BEGIN
    -- Nếu không truyền ngày, lấy ngày hiện tại
    IF @NgayDiemDanh IS NULL SET @NgayDiemDanh = CAST(GETDATE() AS DATE);

    SELECT 
        hs.MaHocSinh, hs.HoTen, hs.NgaySinh, hs.MaLop, -- Lấy thêm MaLop
        dd.MaDiemDanh, ISNULL(dd.TrangThai, '') AS TrangThai,
        dd.GhiChu, dd.MaGiaoVien, dd.GioDen, dd.GioVe
    FROM HocSinh hs
    LEFT JOIN DiemDanh dd ON hs.MaHocSinh = dd.MaHocSinh AND dd.NgayDiemDanh = @NgayDiemDanh
    WHERE (@MaLop IS NULL OR @MaLop = '' OR hs.MaLop = @MaLop)
END
GO
exec sp_DiemDanh_GetAll
CREATE OR ALTER PROCEDURE sp_DiemDanh_Save
    @MaDiemDanh NVARCHAR(50),
    @NgayDiemDanh DATE,
    @TrangThai NVARCHAR(50),
    @GhiChu NVARCHAR(200),
    @MaHocSinh NVARCHAR(50),
    @MaGiaoVien NVARCHAR(50),
    @GioDen DATETIME,
    @GioVe DATETIME
AS
BEGIN
    -- Kiểm tra tồn tại: Nếu HS này, vào ngày này đã có dữ liệu -> UPDATE
    IF EXISTS (SELECT 1 FROM DiemDanh WHERE MaHocSinh = @MaHocSinh AND NgayDiemDanh = @NgayDiemDanh)
    BEGIN
        UPDATE DiemDanh
        SET TrangThai = @TrangThai,
            GhiChu = @GhiChu,
            MaGiaoVien = @MaGiaoVien,
            GioDen = @GioDen,
            GioVe = @GioVe
        WHERE MaHocSinh = @MaHocSinh AND NgayDiemDanh = @NgayDiemDanh;
        
        -- Trả về thông báo để C# biết
        SELECT N'Cập nhật thành công' AS ThongBao;
    END
    ELSE -- Chưa có -> INSERT
    BEGIN
        -- Nếu mã điểm danh rỗng (do FE không gửi), tự sinh mã
        IF @MaDiemDanh IS NULL OR @MaDiemDanh = ''
            SET @MaDiemDanh = 'DD_' + @MaHocSinh + '_' + FORMAT(@NgayDiemDanh, 'yyyyMMdd');

        INSERT INTO DiemDanh(MaDiemDanh, NgayDiemDanh, TrangThai, GhiChu, MaHocSinh, MaGiaoVien, GioDen, GioVe)
        VALUES(@MaDiemDanh, @NgayDiemDanh, @TrangThai, @GhiChu, @MaHocSinh, @MaGiaoVien, @GioDen, @GioVe);

        SELECT N'Thêm mới thành công' AS ThongBao;
    END
END
GO


CREATE OR ALTER PROCEDURE sp_DiemDanh_Delete
    @MaDiemDanh NVARCHAR(50)
AS
BEGIN
    DELETE FROM DiemDanh 
    WHERE MaDiemDanh = @MaDiemDanh;

    SELECT N'Xóa thành công' AS ThongBao;
END
GO


USE QLMN_TH_V2;
GO

-- 1. Lấy toàn bộ danh sách thông báo (Mới nhất lên đầu)
CREATE OR ALTER PROCEDURE sp_ThongBao_GetAll
AS
BEGIN
    SELECT 
        tb.*,
        ISNULL(gv.HoTen, '') AS TenGiaoVien,
        ISNULL(hs.HoTen, '') AS TenHocSinh
    FROM ThongBao tb
    LEFT JOIN GiaoVien gv ON tb.MaGiaoVien = gv.MaGiaoVien
    LEFT JOIN HocSinh hs ON tb.MaHocSinh = hs.MaHocSinh
    ORDER BY tb.NgayGui DESC
END
GO

-- 2. Lấy chi tiết thông báo
CREATE OR ALTER PROCEDURE sp_ThongBao_GetById
    @MaThongBao VARCHAR(20)
AS
BEGIN
    SELECT * FROM ThongBao WHERE MaThongBao = @MaThongBao
END
GO

-- 3. Thêm thông báo mới
CREATE OR ALTER PROCEDURE sp_ThongBao_Them
    @MaThongBao VARCHAR(20),
    @TieuDe NVARCHAR(200),
    @NoiDung NVARCHAR(MAX),
    @NguoiGui NVARCHAR(100),
    @NguoiNhan NVARCHAR(100),
    @LoaiThongBao NVARCHAR(50),
    @MaGiaoVien VARCHAR(20) = NULL,
    @MaHocSinh VARCHAR(20) = NULL
AS
BEGIN
    IF EXISTS (SELECT 1 FROM ThongBao WHERE MaThongBao = @MaThongBao)
    BEGIN
        SELECT N'Lỗi: Mã thông báo đã tồn tại' RETURN;
    END

    INSERT INTO ThongBao (MaThongBao, TieuDe, NoiDung, NguoiGui, NguoiNhan, NgayGui, LoaiThongBao, MaGiaoVien, MaHocSinh)
    VALUES (@MaThongBao, @TieuDe, @NoiDung, @NguoiGui, @NguoiNhan, GETDATE(), @LoaiThongBao, @MaGiaoVien, @MaHocSinh)

    SELECT N'Gửi thông báo thành công'
END
GO

-- 4. Xóa thông báo
CREATE OR ALTER PROCEDURE sp_ThongBao_Xoa
    @MaThongBao VARCHAR(20)
AS
BEGIN
    DELETE FROM ThongBao WHERE MaThongBao = @MaThongBao
    SELECT N'Xóa thành công'
END
GO



-- User
USE QLMN_TH_V2;
GO

-- 1. Lấy toàn bộ bảng điểm (Kèm tên học sinh)
CREATE OR ALTER PROCEDURE sp_BangDiem_GetAll
AS
BEGIN
    SELECT 
        bd.*,
        ISNULL(hs.HoTen, '') AS TenHocSinh
    FROM BangDiem bd
    LEFT JOIN HocSinh hs ON bd.MaHocSinh = hs.MaHocSinh
    ORDER BY bd.NamHoc DESC, bd.HocKy ASC
END
GO

-- 2. Lấy danh sách điểm theo Mã Học Sinh (Chức năng tìm kiếm bạn yêu cầu)
CREATE OR ALTER PROCEDURE sp_BangDiem_GetByHocSinh
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    SELECT 
        bd.*,
        ISNULL(hs.HoTen, '') AS TenHocSinh
    FROM BangDiem bd
    LEFT JOIN HocSinh hs ON bd.MaHocSinh = hs.MaHocSinh
    WHERE bd.MaHocSinh = @MaHocSinh
    ORDER BY bd.NamHoc DESC
END
GO

-- 3. Lấy chi tiết 1 bảng điểm
CREATE OR ALTER PROCEDURE sp_BangDiem_GetById
    @MaBangDiem VARCHAR(20)
AS
BEGIN
    SELECT * FROM BangDiem WHERE MaBangDiem = @MaBangDiem
END
GO

USE QLMN_TH_V2;
GO

-- 1. Lấy toàn bộ danh sách hồ sơ (Kèm tên học sinh)
-- Sắp xếp ngày khám mới nhất lên đầu
CREATE OR ALTER PROCEDURE sp_HosoSucKhoe_GetAll
AS
BEGIN
    SELECT 
        sk.*,
        ISNULL(hs.HoTen, '') AS TenHocSinh
    FROM HosoSucKhoe sk
    LEFT JOIN HocSinh hs ON sk.MaHocSinh = hs.MaHocSinh
    ORDER BY sk.NgayKham DESC
END
GO

-- 2. Lấy lịch sử khám sức khỏe của 1 học sinh
-- (Chức năng quan trọng nhất cho Phụ huynh)
CREATE OR ALTER PROCEDURE sp_HosoSucKhoe_GetByHocSinh
    @MaHocSinh VARCHAR(20)
AS
BEGIN
    SELECT 
        sk.*,
        ISNULL(hs.HoTen, '') AS TenHocSinh
    FROM HosoSucKhoe sk
    LEFT JOIN HocSinh hs ON sk.MaHocSinh = hs.MaHocSinh
    WHERE sk.MaHocSinh = @MaHocSinh
    ORDER BY sk.NgayKham DESC
END
GO

-- 3. Lấy chi tiết 1 phiếu khám (Để xem hoặc sửa)
CREATE OR ALTER PROCEDURE sp_HosoSucKhoe_GetById
    @MaPhieuKham VARCHAR(20)
AS
BEGIN
    SELECT * FROM HosoSucKhoe WHERE MaPhieuKham = @MaPhieuKham
END
GO