using System;
using System.Collections.Generic;

namespace Model;

public partial class LoaiTaiKhoan
{
    public string MaLoai { get; set; } = null!;

    public string TenLoai { get; set; } = null!;

    public string? MoTa { get; set; }
}
