namespace RegistrationServiceAPI.Data;

public class ClientDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ClientCollectionName { get; set; } = null!;
}

