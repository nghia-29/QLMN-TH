using System;

namespace Model
{
    public class HosoSucKhoe
    {
        public string MaPhieuKham { get; set; } = null!;
        public DateTime? NgayKham { get; set; }
        public double? ChieuCao { get; set; }
        public double? CanNang { get; set; }
        public string? TinhTrangSucKhoe { get; set; }
        public string? GhiChu { get; set; }
        public string MaHocSinh { get; set; } = null!;

        // Thuộc tính hiển thị thêm (Do lệnh SQL có JOIN)
        public string? TenHocSinh { get; set; }
    }
}