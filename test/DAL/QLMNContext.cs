
using Microsoft.EntityFrameworkCore;
using Model; // Quan trọng: Để nhận diện được các class HocSinh, GiaoVien... bên project Model

namespace DAL
{
    public class QLMNContext : DbContext
    {
        public QLMNContext()
        {
        }

        public QLMNContext(DbContextOptions<QLMNContext> options)
            : base(options)
        {
        }

        // Khai báo các bảng trong Database để EF Core hiểu
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }
        public virtual DbSet<GiaoVien> GiaoViens { get; set; }
        public virtual DbSet<HocSinh> HocSinhs { get; set; }
        public virtual DbSet<HosoSucKhoe> HosoSucKhoes { get; set; }
        public virtual DbSet<PhanCong> PhanCongs { get; set; }
        public virtual DbSet<ThoiKhoaBieu> ThoiKhoaBieus { get; set; }
        public virtual DbSet<BangDiem> BangDiems { get; set; }
        public virtual DbSet<HocPhi> HocPhis { get; set; }
        public virtual DbSet<DiemDanh> DiemDanhs { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Dự phòng nếu chưa cấu hình ở Program.cs (nhưng mình đã cấu hình rồi nên dòng này chỉ để an toàn)
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Fluent API (nếu cần thiết lập khóa chính, khóa ngoại phức tạp)
            // Hiện tại để mặc định, EF Core đủ thông minh để tự hiểu dựa trên tên cột ID
            base.OnModelCreating(modelBuilder);
        }
    }
}