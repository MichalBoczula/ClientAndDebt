using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Domain.Models
{
    public class ClientInstance
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BankAccountNumber { get; set; } = null!;
        public List<Debt> Debts { get; set; } = new();
    }
}
