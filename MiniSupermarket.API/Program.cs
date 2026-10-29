using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniSupermarket.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Luôn nghe ở cổng 5000 (khớp ApiClient.BaseUrl) kể cả khi IDE không dùng launchSettings.json
if (string.IsNullOrEmpty(builder.Configuration["urls"]) && string.IsNullOrEmpty(builder.Configuration["ASPNETCORE_URLS"]))
    builder.WebHost.UseUrls("http://localhost:5000");

// Cho phép mọi client gọi API (môi trường học tập). Khi triển khai thật nên giới hạn bằng WithOrigins(...).
const string CorsPolicy = "AllowClient";
builder.Services.AddCors(options =>
    options.AddPolicy(CorsPolicy, policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Swagger UI gọi API cùng origin với trang đang mở, tránh lỗi "Failed to fetch"
    options.AddServer(new OpenApiServer { Url = "/" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicy);
app.MapControllers();

app.Run();
