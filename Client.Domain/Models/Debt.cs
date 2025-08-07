using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Domain.Models
{
    public class Debt
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public List<Payment> Payments { get; set; } = new();

        public bool IsAmountInValidRange()
        {
            return Amount >= 500 && Amount <= 10_000;
        }

        public bool IsDueDateAtLeastOneMonthFromNow()
        {
            return DueDate >= DateTime.UtcNow.AddMonths(1);
        }

        public (bool isValid, decimal maxAllowedInstallment) HasInstallementValidSum(Payment payment)
        {
            var totalPaid = Payments.Sum(p => p.Amount);
            var maxAllowedInstallment = Amount - totalPaid;

            if (payment.Amount > maxAllowedInstallment)
            {
                return (false, maxAllowedInstallment);
            }

            Payments.Add(payment);
            return (true, maxAllowedInstallment);
        }

        public bool IsAmountNaturalAndDivisibleBy50()
        {
            return Amount > 0 && Amount % 50 == 0;
        }
    }
}
