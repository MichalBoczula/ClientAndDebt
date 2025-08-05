using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Domain.Models
{
    // TODO: Validation for Max amount of debt and max payment amount
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
    }
}
