using Client.API;
using Client.Infrastructure.Configuration;
using Client.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Client.Domain.Acceptance.Tests
{
    public class ApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private const string Database = "IntegrationTestDb";
        private const string Username = "root";
        private const string Password = "yourStrong(!)Password";
        private const ushort MongoPort = 27017;

        private readonly MongoDbContainer _mongoContainer;
        private string _connectionString = string.Empty;

        public ApplicationFactory()
        {
            _mongoContainer = new MongoDbBuilder()
                .WithImage("mongo:7.0")
                .WithUsername(Username)
                .WithPassword(Password)
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {

                services.RemoveAll<IMongoClient>();
                services.RemoveAll<IMongoDatabase>();
                services.RemoveAll<ClientDbContext>();

                services.AddSingleton<IMongoClient>(_ => new MongoClient(_connectionString));
                services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(Database));

                services.AddScoped<ClientDbContext>(_ =>
                {
                    var settings = new MongoDbSettings
                    {
                        ConnectionString = _connectionString,
                        DatabaseName = Database
                    };
                    return new ClientDbContext(Options.Create(settings));
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _mongoContainer.StartAsync();

            var host = _mongoContainer.Hostname;
            var port = _mongoContainer.GetMappedPublicPort(MongoPort);

            _connectionString =
                $"mongodb://{Username}:{Password}@{host}:{port}/{Database}?authSource=admin";

            var client = new MongoClient(_connectionString);
            await client.GetDatabase(Database).RunCommandAsync<MongoDB.Bson.BsonDocument>("{ ping: 1 }");

            using var scope = Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<MongoInitializer>();
            await initializer.EnsureIndexesCreatedAsync();
        }

        public new async Task DisposeAsync()
        {
            await _mongoContainer.DisposeAsync();
        }
    }
}