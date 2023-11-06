namespace EmailSendingService.Interfaces;

public interface IRabbitMqConsumer
{
    void Consumer(CancellationToken stoppingToken);
}
