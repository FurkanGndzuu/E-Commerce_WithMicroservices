using CatalogService.API.Abstractions;
using CatalogService.API.Abstractions.Repositories.CatalogRepositories;
using CatalogService.API.Abstractions.Repositories.CategoryRepositories;
using CatalogService.API.Abstractions.Repositories.ProductRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Services;
using CatalogService.API.Services.Repositories.CategoryRepositories;
using CatalogService.API.Services.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedService.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatalogDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

});

builder.Services.AddScoped<IProductReadRepository , ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository  , ProductWriteRepository>();
builder.Services.AddScoped<ICategoryReadRepository , CategoryReadRepository>();
builder.Services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "catalog_read"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "catalog_write"));
  
});

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq(builder.Configuration["SeqUrl"])
            .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
