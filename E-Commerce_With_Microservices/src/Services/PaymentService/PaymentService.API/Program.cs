using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using PaymentService.API.Abstractions;
using PaymentService.API.Consumers;
using PaymentService.API.Settings;
using SharedService.Identity;
using SharedService.Settings;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService , SharedIdentityService>();


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddScoped<IPaymentService, PaymentService.API.Services.PaymentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
options.Authority = builder.Configuration["IdentityServerURL"];
options.Audience = "resource_payment";
options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("Read", policy => policy.RequireClaim("scope", "payment_read"));
    _.AddPolicy("Write", policy => policy.RequireClaim("scope", "payment_write"));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockReservedRequestPaymentEventConsumer>();
    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(builder.Configuration["RabbitMQ:Host"], "/", x => {

            x.Username(builder.Configuration["RabbitMQ:username"]);
            x.Password(builder.Configuration["RabbitMQ:password"]);
        });

        configuration.ReceiveEndpoint(RabbitmqSettings.StockReservedSaga, e =>
        {
            e.ConfigureConsumer<StockReservedRequestPaymentEventConsumer>(context);
        });

    });
});

builder.Services.AddOptions<MassTransitHostOptions>();


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
