using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.Interfaces;

public interface IRabbitMqService
{
    void Send(ClientPath client);
}
