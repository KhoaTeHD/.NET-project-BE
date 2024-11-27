using GatewaySolution;
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

builder.Services.AddOcelot()
    .AddDelegatingHandler<RemoveReasonPhraseHandler>();


builder.AddAppAuthentication();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();


// Sử dụng CORS
app.UseCors("AllowAngularApp");

app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();


app.Run();
