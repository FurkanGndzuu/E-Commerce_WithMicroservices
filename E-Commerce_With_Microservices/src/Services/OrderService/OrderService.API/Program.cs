using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Application.Consumers;
using OrderService.Application.Repositories.Order;
using OrderService.Application.Repositories.OrderItem;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories.Order;
using OrderService.Infrastructure.Repositories.OrderItem;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using SharedService.Identity;
using SharedService.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IOrderItemReadRepository , OrderItemReadRepository>();
builder.Services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

builder.Services.AddMediatR(con =>
{
    con.RegisterServicesFromAssemblyContaining(typeof(OrderService.Application.CQRS.Queries.GetOrderQueryRequest));
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());    

builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("ReadAdmin", policy => policy.RequireClaim("scope", "order_read_admin"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "order_write"));
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "order_read"));

});

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCompletedEventConsumer>();
    x.AddConsumer<OrderRequestFailedEventConsumer>();
   
    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(builder.Configuration["RabbitMQ:Host"], "/", x => {

            x.Username(builder.Configuration["RabbitMQ:username"]);
            x.Password(builder.Configuration["RabbitMQ:password"]);
        });

        configuration.ReceiveEndpoint(RabbitmqSettings.OrderSagaFailed , e =>
        {
            e.ConfigureConsumer<OrderRequestFailedEventConsumer>(context);
        });
        configuration.ReceiveEndpoint(RabbitmqSettings.OrderSagaCompleted, e =>
        {
            e.ConfigureConsumer<OrderCompletedEventConsumer>(context);
        });

    });
});



builder.Services.AddOptions<MassTransitHostOptions>();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341") // Seq URL'ini buraya yaz�n
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
