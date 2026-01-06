namespace Model
{
    public class TaiKhoan
    {
        public string TenTaiKhoan { get; set; }

        // --- BỔ SUNG DÒNG NÀY ---
        public string MatKhau { get; set; }
        // -------------------------

        public string? IDTaiKhoan { get; set; }
        public string? MaLoai { get; set; }
        public string? TenLoai { get; set; }
    }
}