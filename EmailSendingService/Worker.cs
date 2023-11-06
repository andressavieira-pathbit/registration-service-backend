using EmailSendingService.Interfaces;

namespace EmailSendingService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitMqConsumer _rabbitMqConsumer;
    private readonly INotificationEmail _notificationEmail;

    public Worker(ILogger<Worker> logger, IRabbitMqConsumer rabbitMqConsumer, INotificationEmail notificationEmail)
    {
        _logger = logger;
        _rabbitMqConsumer = rabbitMqConsumer;
        _notificationEmail = notificationEmail;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("WORKER RODANDO!");

        _rabbitMqConsumer.Consumer(stoppingToken);
    }
}