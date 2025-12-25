using System;
using System.Collections.Generic;

namespace Model;

public partial class PhanCong
{
    public string MaPhanCong { get; set; } = null!;

    public string? MaLop { get; set; }

    public string? MaMonHoc { get; set; }

    public string? NamHoc { get; set; }

    public int? HocKy { get; set; }

    public string? MaGiaoVien { get; set; }

    public string? MaHocSinh { get; set; }

    public virtual GiaoVien? MaGiaoVienNavigation { get; set; }

    public virtual HocSinh? MaHocSinhNavigation { get; set; }

    public virtual ICollection<ThoiKhoaBieu> ThoiKhoaBieus { get; set; } = new List<ThoiKhoaBieu>();
}
