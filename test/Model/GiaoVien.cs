using System;
using System.Collections.Generic;

namespace Model;

public partial class GiaoVien
{
    public string MaGiaoVien { get; set; } = null!;

    public string? HoTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Sdt { get; set; }

    public string? ChuyenMon { get; set; }

    public string? BangCap { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DiemDanh> DiemDanhs { get; set; } = new List<DiemDanh>();

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
