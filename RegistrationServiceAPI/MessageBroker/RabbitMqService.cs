using RabbitMQ.Client;
using RegistrationServiceAPI.Interfaces;
using RegistrationServiceAPI.Models;
using System.Text;
using System.Text.Json;

namespace RegistrationServiceAPI.MessageBroker;

public class RabbitMqService : IRabbitMqService
{
    private readonly ILogger<RabbitMqService> _logger;

    public RabbitMqService(ILogger<RabbitMqService> logger)
    {
        _logger = logger;
    }

    public void Send(ClientPath client)
    {
        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: "cadastro.em.analise.email",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(client);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "cadastro.em.analise.email",
                                     basicProperties: null,
                                     body: body);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao tentar criar uma nova fila!", ex);
        }
    }
}
