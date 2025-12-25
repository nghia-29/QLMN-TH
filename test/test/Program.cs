using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using DAL; // Ch?a QLMNContext
using DAL.Interfaces;
using DAL.Repositories;
using BLL.Interfaces;
using BLL;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1. C?U H?NH JSON (QUAN TR?NG)
// Giúp tránh l?i v?ng l?p khi b?ng này tr? sang b?ng kia
// ==========================================================
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// ==========================================================
// 2. K?T N?I CÕ S? D? LI?U
// Ð?c chu?i k?t n?i t? appsettings.json
// ==========================================================
builder.Services.AddDbContext<QLMNContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================================
// 3. ÐÃNG K? D?CH V? (DEPENDENCY INJECTION)
// Ph?i khai báo ? ðây th? Controller m?i g?i ðý?c Business, Business m?i g?i ðý?c Repo
// ==========================================================

// --- Ch?c nãng H?c Sinh ---
//IServiceCollection serviceCollection = builder.Services.AddScoped<IHocSinhRepository, HocSinhRepository>();
//builder.Services.AddScoped<IHocSinhBusiness, HocSinhBusiness>();

// --- Ch?c nãng Thông Báo (V?a làm xong) ---
builder.Services.AddScoped<IThongBaoRepository, ThongBaoRepository>();
builder.Services.AddScoped<IThongBaoBusiness, ThongBaoBusiness>();

// (Sau này làm thêm Giáo viên, H?c phí... th? thêm d?ng týõng t? vào ðây)

// ==========================================================
// C?U H?NH SWAGGER (M?c ð?nh)
// ==========================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();