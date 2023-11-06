using SendGrid.Helpers.Mail;
using SendGrid;

namespace EmailSendingService.Interfaces;

public interface INotificationEmail
{
    Task<Response?> Email(SendGridMessage msg);
}