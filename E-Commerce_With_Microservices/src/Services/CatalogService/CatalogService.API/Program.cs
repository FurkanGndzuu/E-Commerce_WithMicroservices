using CatalogService.API.Abstractions;
using CatalogService.API.Abstractions.Repositories.CatalogRepositories;
using CatalogService.API.Abstractions.Repositories.CategoryRepositories;
using CatalogService.API.Abstractions.Repositories.ProductRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Services;
using CatalogService.API.Services.Repositories.CategoryRepositories;
using CatalogService.API.Services.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatalogDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlService"));
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
