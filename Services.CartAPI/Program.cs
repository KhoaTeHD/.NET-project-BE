using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.CartItemAPI.Extensions;
using Services.CartItemAPI;
using Services.CartItemAPI.Data;
using Services.CartItemAPI.Service;
using Services.CartItemAPI.Service.IService;
using Services.CartItemAPI.Utility;
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

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddSingleton(mapper);

builder.Services.AddHttpClient("Product", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:ProductAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddScoped<IProductVariationService, ProductVariationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Auth load icon authorize tren backend
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });

});


builder.AddAppAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Sử dụng CORS
app.UseCors("AllowAngularApp");

//Auth
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
