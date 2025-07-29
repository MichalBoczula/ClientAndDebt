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
    }
}
