using BasketService.API.Abstractions;
using BasketService.API.Consts;
using BasketService.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using SharedService.Identity;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_basket";
    options.RequireHttpsMetadata = false;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBasketService, BasketService.API.Services.BasketService>();
builder.Services.Configure<RedisSettingsConsts>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettingsConsts>>().Value;

    var redis = new RedisService(redisSettings.Host, redisSettings.Port);

    redis.Connect();

    return redis;
});

builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "basket_read"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "basket_write"));

});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341") // Seq URL'ini buraya yazýn
            .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
