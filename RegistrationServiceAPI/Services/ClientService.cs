using RegistrationServiceAPI.Interfaces;
using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IRabbitMqService _rabbitMqService;

    public ClientService(IClientRepository clientRepository, IRabbitMqService rabbitMqService)
    {
        _clientRepository = clientRepository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task<List<ClientPath>> GetAsync() =>
        await _clientRepository.GetAsync();

    public async Task<ClientPath> GetAsyncById(string id) =>
        await _clientRepository.GetAsyncById(id);

    public async Task<ClientPath> GetAsyncByCPF(string cpf) =>
        await _clientRepository.GetAsyncByCPF(cpf);

    public async Task CreateAsync(ClientPath client)
    {
        var hasUser = await _clientRepository.GetAsyncByCPF(client.CPF);

        if (hasUser != null)
        {
            throw new SystemException("Usuário já registrado.");
        }

        await _clientRepository.CreateAsync(client);
        
        client.SecurityData = null;
        
        _rabbitMqService.Send(client);
    }

    public async Task UpdateAsync(string id, ClientPath client) =>
       await _clientRepository.UpdateAsync(id, client);

    public async Task RemoveAsync(string id) =>
         await _clientRepository.RemoveAsync(id);
}
