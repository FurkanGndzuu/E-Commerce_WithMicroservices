using ComplationService.API.Abstractions;
using ComplationService.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using SharedService.Identity;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IComplaintService, ComplationService.API.Services.ComplaintService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("given_name");
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("Surname");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_complaint";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "complaint_read"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "complaint_write"));

});

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341") // Seq URL'ini buraya yazýn
            .CreateLogger();

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
