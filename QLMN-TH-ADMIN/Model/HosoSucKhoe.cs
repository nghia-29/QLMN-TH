using System;
using System.Collections.Generic;

namespace Model;

public partial class HosoSucKhoe
{
    public string MaPhieuKham { get; set; } = null!;

    public DateTime? NgayKham { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }

    public string? TinhTrangSucKhoe { get; set; }

    public string? GhiChu { get; set; }

    public string MaHocSinh { get; set; } = null!;

}
