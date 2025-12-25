using System;
using System.Collections.Generic;

namespace Model;

public partial class TaiKhoan
{
    public string IdtaiKhoan { get; set; } = null!;

    public string TenTaiKhoan { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public virtual LoaiTaiKhoan MaLoaiNavigation { get; set; } = null!;
}
