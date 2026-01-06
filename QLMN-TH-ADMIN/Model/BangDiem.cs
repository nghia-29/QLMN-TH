using System;
using System.Collections.Generic;

namespace Model;

public partial class BangDiem
{
    public string MaBangDiem { get; set; } = null!;

    public string? MaMonHoc { get; set; }

    public string? NamHoc { get; set; }

    public int? HocKy { get; set; }

    public double? DiemSo { get; set; }

    public string? NhanXet { get; set; }

    public string? MaHocSinh { get; set; }


}
