using Client.Domain.Dto;
using Client.Domain.Models;

namespace Client.Application.Helpers
{
    public static class ModelFactory
    {
        public static ClientInstance CreateClient(CreateClientDto clientDto)
        {
            return new ClientInstance
            {
                Id = Guid.NewGuid(),
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                BankAccountNumber = clientDto.BankAccountNumber,
                Debts = clientDto.Debts?
                    .Select(d => CreateDebt(d))
                    .ToList() ?? []
            };
        }

        public static Debt CreateDebt(DebtDto debtDto)
        {
            return new Debt
            {
                Id = Guid.NewGuid(),
                Amount = debtDto.Amount,
                DueDate = debtDto.DueDate,
                Payments = debtDto.Payments?
                    .Select(p => CreatePayment(p))
                    .ToList() ?? []
            };
        }

        public static Payment CreatePayment(PaymentDto paymentDto)
        {
            return new Payment
            {
                Id = Guid.NewGuid(),
                Amount = paymentDto.Amount,
                Date = paymentDto.Date,
                Note = paymentDto.Note
            };
        }
    }
}
