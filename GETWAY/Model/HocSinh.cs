using System;
using System.Collections.Generic;

namespace Model;

public partial class HocSinh
{
    public string MaHocSinh { get; set; } = null!;

    public string? HoTen { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? MaLop { get; set; }

    public string? PhuHuynh { get; set; }

    public string? SdtlienHe { get; set; }
}
