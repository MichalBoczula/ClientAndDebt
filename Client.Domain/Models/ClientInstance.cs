using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

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

        public bool IsFirstNameValid() => !string.IsNullOrWhiteSpace(FirstName);

        public bool IsLastNameValid() => !string.IsNullOrWhiteSpace(LastName);

        public bool IsBankAccountNumberNullOrWhiteSpace() => !string.IsNullOrWhiteSpace(BankAccountNumber);

        public bool IsBankAccountNumberInCorrectForm()
        {
            var pattern = @"^[A-Z0-9]{10,34}$";
            return Regex.IsMatch(BankAccountNumber, pattern);
        }
    }
}
