using Client.Domain.Exceptions;
using Client.Domain.Models;
using Client.Domain.Repositories;
using Client.Domain.Validation.Concrete.Policies;

namespace Client.Application.Services
{
    public class ClientService(IClientRepository repository, ClientValidationPolicy clientValidationPolicy)
    {
        public async Task AddClientAsync(ClientInstance client)
        {
            var validationResult = clientValidationPolicy.Validate(client);

            if (!validationResult.IsValid)
            {
                throw new EntityValidationException(validationResult);
            }

            AssigneIdsToEntities(client);

            await repository.AddAsync(client);
        }

        public async Task<ClientInstance?> GetClientByIdAsync(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        private static void AssigneIdsToEntities(ClientInstance client)
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
        }
    }
}
