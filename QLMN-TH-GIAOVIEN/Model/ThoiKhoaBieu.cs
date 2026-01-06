using System;
using System.Collections.Generic;

namespace Model;

public partial class ThoiKhoaBieu
{
    public string MaTkb { get; set; } = null!;

    public string? MaLop { get; set; }

    public string? MaMonHoc { get; set; }

    public string? TenGiaoVien { get; set; }

    public string? Thu { get; set; }

    public int? TietHoc { get; set; }

    public string? MaPhanCong { get; set; }
}
