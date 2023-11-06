using EmailSendingService.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSendingService.SendGrid;

public class NotificationEmail : INotificationEmail
{
    public readonly ISendGridClient _client;

    public NotificationEmail(ISendGridClient client)
    {
        _client = client;
    }

    public async Task<Response?> Email(SendGridMessage msg)
    {
        var result = await _client.SendEmailAsync(msg);
        return result;
    }
}
