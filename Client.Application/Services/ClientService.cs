using Client.Domain.Models;
using Client.Domain.Repositories;

namespace Client.Application.Services
{
    public class ClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task AddClientAsync(ClientInstance client)
        {
            client.Id = Guid.NewGuid();

            foreach (var debt in client.Debts)
            {
                debt.Id = Guid.NewGuid();

                foreach (var payment in debt.Payments)
                {
                    payment.Id = Guid.NewGuid();
                }
            }

            await _repository.AddAsync(client);
        }

        public async Task<ClientInstance?> GetClientByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
