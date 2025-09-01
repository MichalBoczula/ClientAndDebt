using Client.Application.Helpers;
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
        public async Task<ClientInstance> AddClientAsync(CreateClientDto clientDto)
        {
            ClientInstance client = ModelFactory.CreateClient(clientDto);

            var validationResult = clientValidationPolicy.Validate(client);

            if (!validationResult.IsValid)
            {
                throw new EntityValidationException(validationResult);
            }

            await repository.AddAsync(client);

            return client;
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
            Debt debt = ModelFactory.CreateDebt(debtDto);

            var validationResult = debtValidationPolicy.Validate(debt);

            if (!validationResult.IsValid)
            {
                throw new EntityValidationException(validationResult);
            }

            client.Debts.Add(debt);

            await repository.UpdateAsync(client);
            return true;
        }

        public async Task<bool> AddPaymentToDebtAsync(Guid clientId, Guid debtId, PaymentDto paymentDto)
        {
            var client = await repository.GetByIdAsync(clientId);

            if (client is null)
                return false;

            var debt = client.Debts.FirstOrDefault(d => d.Id == debtId);

            if (debt is null)
                return false;

            Payment payment = ModelFactory.CreatePayment(paymentDto);

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

            debt.Payments.Add(payment);

            await repository.UpdateAsync(client);
            return true;
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
