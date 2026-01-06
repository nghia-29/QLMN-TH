using System;
using System.Collections.Generic;

namespace Model;

public partial class DiemDanh
{
    public string MaDiemDanh { get; set; } = null!;

    public DateOnly NgayDiemDanh { get; set; }

    public string TrangThai { get; set; } = null!;

    public string? GhiChu { get; set; }

    public string MaHocSinh { get; set; } = null!;

    public string MaGiaoVien { get; set; } = null!;

    public DateTime? GioDen { get; set; }

    public DateTime? GioVe { get; set; }


}
