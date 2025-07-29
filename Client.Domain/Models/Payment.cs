using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Domain.Models
{
    public class Payment
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
    }
}
