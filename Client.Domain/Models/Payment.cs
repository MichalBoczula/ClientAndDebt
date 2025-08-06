using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Domain.Models
{
    // TODO: Change Amount 50
    // TODO: Payment must be nutural number divaded by 50
    public class Payment
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        
        public bool IsAmountAboveMinimum() => Amount >= 100;
        
        public bool IsDateTodayOrLater() => Date.Date >= DateTime.UtcNow.Date;
    }
}
