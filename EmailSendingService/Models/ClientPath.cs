using RegistrationServiceAPI.Models;

namespace EmailSendingService.Models;

public class ClientPath
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
