CREATE DATABASE QLMN_TH_V2;
GO
USE QLMN_TH_V2;
GO

CREATE TABLE TaiKhoan (
    IDTaiKhoan VARCHAR(20) PRIMARY KEY,
    TenTaiKhoan VARCHAR(50) NOT NULL UNIQUE, 
    MaLoai VARCHAR(20) NOT NULL, 
    CONSTRAINT FK_TaiKhoan_LoaiTaiKhoan FOREIGN KEY (MaLoai) REFERENCES LoaiTaiKhoan(MaLoai)
);

CREATE TABLE LoaiTaiKhoan (
    MaLoai VARCHAR(20) PRIMARY KEY,
    TenLoai NVARCHAR(50) NOT NULL, -- Ví dụ: Admin, Giáo viên, Phụ huynh
    MoTa NVARCHAR(200)
);

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