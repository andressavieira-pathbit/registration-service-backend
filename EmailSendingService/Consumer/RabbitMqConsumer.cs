using EmailSendingService.Interfaces;
using EmailSendingService.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using System.Text.Json;

namespace EmailSendingService.Consumer;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly INotificationEmail _notificationEmail;
    private readonly IConfiguration _configuration;
    public RabbitMqConsumer(INotificationEmail notificationEmail)
    {

        _notificationEmail = notificationEmail;
    }
    public void Consumer(CancellationToken stoppingToken)
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
        }

        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var clientPath = JsonSerializer.Deserialize<ClientPath>(message);

                _notificationEmail.Email(CreateEmail(clientPath));

                // testes
                Console.WriteLine($"Leu a mensagem!");

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                // Logger
                channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };
        channel.BasicConsume(queue: "cadastro.em.analise.email",
                             autoAck: true, // Lê e limpa a fila
                             consumer: consumer);

        while (!stoppingToken.IsCancellationRequested) { }
    }

    internal SendGridMessage CreateEmail(ClientPath client)
    {
    // Criando um email com o SendGrid passando o objeto client
    var msg = new SendGridMessage()
        {
            From = new EmailAddress("andressavieiradsantos@gmail.com", "Andressa Santos"),
            Subject = "Cadastro em análise",
            HtmlContent = $"Olá <strong>{client.Name}</strong>," + // Formantando a mensagem com HTML
                          $"</br></br>O seu cadastro está em análise e em breve você receberá um e-mail com novas atualizações sobre seu cadastro." +
                          $"</br></br> Atenciosamente,</br></br>" +
                          $"<strong>Equipe PATHBIT</strong>"
        };
        var emailTo = new EmailAddress(client.Email, client.Name);
        msg.AddTo(emailTo);
        
        return msg;
    }
}
