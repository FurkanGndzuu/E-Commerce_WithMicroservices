using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using NotificationService.API.Abstractions;
using NotificationService.API.Consumers;
using NotificationService.API.Services;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using SharedService.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMailService , MailService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCompletedRequestEventConsumer>();
    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(builder.Configuration["RabbitMQ:Host"], "/", x => {

            x.Username(builder.Configuration["RabbitMQ:username"]);
            x.Password(builder.Configuration["RabbitMQ:password"]);
        });

        configuration.ReceiveEndpoint(RabbitmqSettings.OrderCompletedNotificationQueue, e =>
        {
            e.ConfigureConsumer<OrderCompletedRequestEventConsumer>(context);
        });

    });
});

builder.Services.AddOptions<MassTransitHostOptions>();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341") // Seq URL'ini buraya yaz�n
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
