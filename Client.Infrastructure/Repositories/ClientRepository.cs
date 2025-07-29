using Client.Domain.Models;
using Client.Domain.Repositories;
using Client.Infrastructure.Context;
using MongoDB.Driver;

namespace Client.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<ClientInstance> _clients;

        public ClientRepository(ClientDbContext context)
        {
            _clients = context.Clients;
        }

        public async Task AddAsync(ClientInstance client)
        {
            await _clients.InsertOneAsync(client);
        }

        public async Task<ClientInstance?> GetByIdAsync(Guid id)
        {
            return await _clients.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
