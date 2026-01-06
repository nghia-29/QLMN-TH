using System;
using System.Collections.Generic;

namespace Model;

public partial class HocPhi
{
    public string MaHocPhi { get; set; } = null!;

    public int? ThangThu { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public string? HinhThuc { get; set; }

    public string? MaHocSinh { get; set; }
}
