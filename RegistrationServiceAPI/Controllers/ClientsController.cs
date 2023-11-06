using Microsoft.AspNetCore.Mvc;
using RegistrationServiceAPI.Interfaces;
using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase 
{
    private readonly IClientService _clientService;

    private ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<ClientPath>> GetAsync() =>
        await _clientService.GetAsync();

    [HttpPost]
    public async Task<IActionResult> Post(ClientPath client)
    {
        var income = client.FinancialData.Income;
        var patrimony = client.FinancialData.Patrimony;

        if (income + patrimony < 1000)
        {
            return BadRequest("Para realizar o cadastro, é necessário que a soma da renda e do patrimônio seja superior a 1000.");
        }

        await _clientService.CreateAsync(client);

        return Ok(client);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var client = await _clientService.GetAsyncById(id);

        if (client is null)
        {
            return NotFound("Usuário não encontrado!");
        }

        await _clientService.RemoveAsync(id);

        return Ok("Usuário excluído!");
    }
}
