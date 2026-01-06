var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. PHẦN CẤU HÌNH DỊCH VỤ (SERVICES)
// ==========================================

// Thêm Controllers
builder.Services.AddControllers();

// Cấu hình Swagger (Tài liệu API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- QUAN TRỌNG: BẬT CORS ---
// (Cho phép mọi nơi truy cập vào API - Cần thiết khi làm Frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("*") // Cho phép tất cả nguồn
                  .AllowAnyMethod() // Cho phép GET, POST, PUT, DELETE...
                  .AllowAnyHeader(); // Cho phép mọi Header
        });
});

var app = builder.Build();

// ==========================================
// 2. PHẦN CẤU HÌNH PIPELINE (MIDDLEWARE)
// ==========================================

// Cấu hình Swagger UI khi chạy ở môi trường Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Tạm thời tắt chuyển hướng HTTPS để tránh lỗi chứng chỉ SSL ở Localhost (nếu có)
// app.UseHttpsRedirection(); 

// --- KÍCH HOẠT CORS ---
// (Phải đặt trước UseAuthorization)
app.UseCors("AllowAll");

app.UseAuthorization();

// Ánh xạ các Controller (API)
app.MapControllers();

// Chạy ứng dụng
app.Run();