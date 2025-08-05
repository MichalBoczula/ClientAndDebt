using Client.Domain.Models;
using Client.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Client.Infrastructure.Context
{
    // TODO: Add connection to Atlas 
    public class ClientDbContext
    {
        private readonly IMongoDatabase _database;

        public ClientDbContext(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<ClientInstance> Clients => _database.GetCollection<ClientInstance>("Clients");
    }
}
