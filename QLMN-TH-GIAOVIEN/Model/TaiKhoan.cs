using System;
using System.Collections.Generic;

namespace Model;

public partial class TaiKhoan
{
    public string IDTaiKhoan { get; set; } = null!;

    public string TenTaiKhoan { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    // Thuộc tính mở rộng (Không có trong bảng DB, dùng để hiển thị tên quyền lên giao diện)
    public string? TenLoai { get; set; }
}