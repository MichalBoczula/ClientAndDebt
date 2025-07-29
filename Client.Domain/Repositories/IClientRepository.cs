using Client.Domain.Models;

namespace Client.Domain.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(ClientInstance client);
        Task<ClientInstance?> GetByIdAsync(Guid id);
    }
}
