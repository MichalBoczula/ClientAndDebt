using Client.Domain.Dto;
using Client.Domain.Exceptions;
using Client.Domain.Models;
using Client.Domain.Repositories;
using Client.Domain.Validation.Concrete.Policies;

namespace Client.Application.Services
{
    // TODO: Global validatior policy for null objects, empty collections and empty body
    public class ClientService(IClientRepository repository, ClientValidationPolicy clientValidationPolicy, DebtValidationPolicy debtValidationPolicy, PaymentValidationPolicy paymentValidationPolicy, PaymentInDebtValidationPolicy paymentInDebtValidationPolicy)
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

        public async Task<bool> AddDebtToClientAsync(Guid clientId, DebtDto debtDto)
        {
            var client = await repository.GetByIdAsync(clientId);

            if (client is null)
                return false;

            var debt = new Debt
            {
                Amount = debtDto.Amount,
                DueDate = debtDto.DueDate,
                Payments = new List<Payment>()
            };

            var validationResult = debtValidationPolicy.Validate(debt);

            if (!validationResult.IsValid)
            {
                throw new EntityValidationException(validationResult);
            }

            debt.Id = Guid.NewGuid();
            client.Debts.Add(debt);

            await repository.UpdateAsync(client);
            return true;
        }

        public async Task<bool> AddPaymentToDebtAsync(Guid clientId, Guid debtId, Payment payment)
        {
            var client = await repository.GetByIdAsync(clientId);

            if (client is null)
                return false;

            var debt = client.Debts.FirstOrDefault(d => d.Id == debtId);

            if (debt is null)
                return false;

            var paymentValidationResult = paymentValidationPolicy.Validate(payment);

            if (!paymentValidationResult.IsValid)
            {
                throw new EntityValidationException(paymentValidationResult);
            }

            var paymentInDebt = new PaymentInDebtDto()
            {
                Debt = debt,
                NewPayment = payment
            };

            var paymentInDebtResult = paymentInDebtValidationPolicy.Validate(paymentInDebt);

            if (!paymentInDebtResult.IsValid)
            {
                throw new EntityValidationException(paymentInDebtResult);
            }

            payment.Id = Guid.NewGuid();
            debt.Payments.Add(payment);

            await repository.UpdateAsync(client);
            return true;
        }

        private void AssigneIdsToEntities(ClientInstance client)
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

        public async Task<bool> UpdateClientData(Guid clientId, UpdateClientInstanceDto updateClientDto)
        {
            var client = await repository.GetByIdAsync(clientId);

            if (client is null)
                return false;

            client.FirstName = updateClientDto.FirstName;
            client.LastName = updateClientDto.LastName;
            client.BankAccountNumber = updateClientDto.BankAccountNumber;

            var validationResult = clientValidationPolicy.Validate(client);
            if (!validationResult.IsValid)
            {
                throw new EntityValidationException(validationResult);
            }

            await repository.UpdateAsync(client);
            return true;
        }
    }
}
