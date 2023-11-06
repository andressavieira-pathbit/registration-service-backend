using EmailSendingService;
using EmailSendingService.Consumer;
using EmailSendingService.Interfaces;
using EmailSendingService.SendGrid;
using SendGrid.Extensions.DependencyInjection;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.Extensions.Hosting;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddDotNetEnv(".env", LoadOptions.TraversePath());

builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddSingleton<INotificationEmail, NotificationEmail>();

builder.Services.AddSendGrid(options =>
    options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? "");

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
await host.RunAsync();