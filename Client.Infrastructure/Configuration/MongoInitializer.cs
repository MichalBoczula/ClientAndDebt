using Client.Domain.Models;
using Client.Infrastructure.Context;
using MongoDB.Driver;

namespace Client.Infrastructure.Configuration
{
    public class MongoInitializer
    {
        private readonly ClientDbContext _context;

        public MongoInitializer(ClientDbContext context)
        {
            _context = context;
        }

        public async Task EnsureIndexesCreatedAsync()
        {
            var indexKeys = Builders<ClientInstance>.IndexKeys
                .Ascending(c => c.BankAccountNumber);

            var model = new CreateIndexModel<ClientInstance>(indexKeys);
            await _context.Clients.Indexes.CreateOneAsync(model);
        }
    }
}
