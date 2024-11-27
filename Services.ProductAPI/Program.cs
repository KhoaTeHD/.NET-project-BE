using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.ProductAPI.Extensions;
using Services.ProductAPI;
using Services.ProductAPI.Data;
using Services.ProductAPI.Service.IService;
using Services.ProductAPI.Service;
using Services.ProductAPI.Utility;


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

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddSingleton(mapper);

builder.Services.AddHttpClient("Brand", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:BrandAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Nation", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:NationAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Category", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:CategoryAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Supplier", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:SupplierAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Size", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:SizeAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpClient("Color", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:ColorAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<INationService, NationService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();

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
