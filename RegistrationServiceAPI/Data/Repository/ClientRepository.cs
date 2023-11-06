using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RegistrationServiceAPI.Helper;
using RegistrationServiceAPI.Interfaces;
using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly IMongoCollection<ClientPath> _clientCollection;

    public ClientRepository(IOptions<ClientDatabaseSettings> clientDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            clientDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            clientDatabaseSettings.Value.DatabaseName);

        _clientCollection = mongoDatabase.GetCollection<ClientPath>(
            clientDatabaseSettings.Value.ClientCollectionName);
    }

    public async Task<List<ClientPath>> GetAsync() =>
        await _clientCollection.Find(x => true).ToListAsync();

    public async Task<ClientPath> GetAsyncById(string id) =>
        await _clientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<ClientPath> GetAsyncByCPF(string cpf) =>
        await _clientCollection.Find(x => x.CPF == cpf).FirstOrDefaultAsync();

    public async Task CreateAsync(ClientPath client)
    {
        client.SecurityData.Password = client.SecurityData.Password.EncryptPassword();  
        client.SecurityData.PasswordConfirmation = client.SecurityData.PasswordConfirmation.EncryptPassword();

        await _clientCollection.InsertOneAsync(client);

        client.SecurityData = null;
    }

    public async Task UpdateAsync(string id, ClientPath client) =>
        await _clientCollection.ReplaceOneAsync(x => x.Id == id, client);

    public async Task RemoveAsync(string id) =>
         await _clientCollection.DeleteOneAsync(x => x.Id == id);
}
