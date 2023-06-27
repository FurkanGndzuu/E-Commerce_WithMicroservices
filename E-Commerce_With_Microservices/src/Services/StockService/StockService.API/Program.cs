using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SharedService.Identity;
using SharedService.Settings;
using StockService.API.Abstractions;
using StockService.API.Consumers;
using StockService.API.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddScoped<IStockService , StockService.API.Services.StockServices>();


JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_stock";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "stock_read"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "stock_write"));

});

builder.Services.AddMassTransit(opt =>
{

    opt.AddConsumer<OrderCreatedEventConsumer>();
    opt.AddConsumer<PaymentFailedEventConsumer>();
   

    opt.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", x =>
        {
            x.Username(builder.Configuration["RabbitMQ:username"]);
            x.Password(builder.Configuration["RabbitMQ:password"]);

        });

        cfg.ReceiveEndpoint(RabbitmqSettings.OrderCreatedSaga, e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint(RabbitmqSettings.PaymentFailedSagaQueue, e =>
        {
            e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
        });


    });
});

builder.Services.AddOptions<MassTransitHostOptions>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
