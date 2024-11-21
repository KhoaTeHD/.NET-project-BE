using GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // Cho phép Angular chạy trên http://localhost:4200
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); // Cho phép sử dụng Cookie hoặc thông tin xác thực nếu cần
});

builder.AddAppAuthentication();
builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Sử dụng CORS
app.UseCors("AllowAngularApp");

app.UseOcelot();


app.Run();
