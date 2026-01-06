using System;
using System.Collections.Generic;

namespace Model;

public partial class ThongBao
{
    public string MaThongBao { get; set; } = null!;

    public string? TieuDe { get; set; }

    public string? NoiDung { get; set; }

    public string? NguoiGui { get; set; }

    public string? NguoiNhan { get; set; }

    public DateTime? NgayGui { get; set; }

    public string? LoaiThongBao { get; set; }

    public string? MaGiaoVien { get; set; }

    public string? MaHocSinh { get; set; }


}
