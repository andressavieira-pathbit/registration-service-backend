using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using RegistrationServiceAPI.Data;
using RegistrationServiceAPI.Data.Repository;
using RegistrationServiceAPI.Interfaces;
using RegistrationServiceAPI.MessageBroker;
using RegistrationServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});

builder.Services.Configure<ClientDatabaseSettings>(builder.Configuration.GetSection("ClientDatabase"));

builder.Services.AddSingleton<IClientService, ClientService>();

builder.Services.AddSingleton<IClientRepository, ClientRepository>(
    c =>
    {
        var opt = c.GetService<IOptions<ClientDatabaseSettings>>();
        return new ClientRepository(opt);
    });

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
