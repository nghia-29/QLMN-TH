using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. ĐĂNG KÝ DỊCH VỤ (SERVICES)
// ==========================================

builder.Services.AddControllers();

// Load file cấu hình Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// CORS: Cho phép mọi nguồn truy cập
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// 2. CẤU HÌNH PIPELINE (MIDDLEWARE) - QUAN TRỌNG
// ==========================================

// Bật CORS đầu tiên
app.UseCors("AllowAll");

// Swagger (Chỉ hiện khi ở chế độ Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- KHU VỰC SỬA LỖI (BẮT BUỘC PHẢI CÓ THỨ TỰ NÀY) ---

// B1: Bật tính năng định tuyến (Xác định xem request muốn đi đâu)
app.UseRouting();

// B2: Bật xác thực (Nếu sau này có dùng JWT)
app.UseAuthorization();

// B3: ĐỊNH NGHĨA ĐIỂM ĐẾN CHO CÁC CONTROLLER NỘI BỘ
// (Code này bắt buộc Request /api/TaiKhoan/login phải chạy vào Controller, không được đi qua Ocelot)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// B4: SAU CÙNG MỚI ĐẾN OCELOT
// (Những cái gì không khớp Controller ở trên thì mới rơi xuống đây để Ocelot chuyển đi)
await app.UseOcelot();

app.Run();