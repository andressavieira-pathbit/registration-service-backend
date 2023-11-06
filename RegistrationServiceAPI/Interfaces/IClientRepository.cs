using Microsoft.AspNetCore.Mvc;
using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.Interfaces;

public interface IClientRepository
{
    Task<List<ClientPath>> GetAsync();
    Task<ClientPath> GetAsyncById(string id);
    Task<ClientPath> GetAsyncByCPF(string cpf);
    Task CreateAsync(ClientPath client);
    Task UpdateAsync(string id, ClientPath client);
    Task RemoveAsync(string id);
}
