using System;
using System.Collections.Generic;

namespace Model;

public partial class HocSinh
{
    public string MaHocSinh { get; set; } = null!;

    public string? HoTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? MaLop { get; set; }

    public string? PhuHuynh { get; set; }

    public string? SdtlienHe { get; set; }

    public virtual ICollection<BangDiem> BangDiems { get; set; } = new List<BangDiem>();

    public virtual ICollection<DiemDanh> DiemDanhs { get; set; } = new List<DiemDanh>();

    public virtual ICollection<HocPhi> HocPhis { get; set; } = new List<HocPhi>();

    public virtual ICollection<HosoSucKhoe> HosoSucKhoes { get; set; } = new List<HosoSucKhoe>();

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
