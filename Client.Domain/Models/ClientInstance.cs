using Client.Domain.Enums;
using Client.Domain.Validation.Common;
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

        public ValidationError IsFirstNameValid()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return new ValidationError()
                {
                    Message = "First name cannot be empty.",
                    RuleName = "NotEmptyFirstName",
                    Severity = RuleSeverity.Error
                };
            }
            else
            {
                return null;
            }
        }

        public ValidationError IsLastNameValid()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return new ValidationError()
                {
                    Message = "Last name cannot be empty.",
                    RuleName = "NotEmptyLastName",
                    Severity = RuleSeverity.Error
                };
            }
            else
            {
                return null;
            }
        }

        public ValidationError IsBankAccountNumberValid()
        {
            var pattern = @"^[A-Z0-9]{10,34}$";

            if (string.IsNullOrWhiteSpace(BankAccountNumber))
            {
                return new ValidationError()
                {
                    Message = "Bank account number cannot be empty.",
                    RuleName = "NotEmptyBankAccountNumber",
                    Severity = RuleSeverity.Error
                };
            }

            if (!Regex.IsMatch(BankAccountNumber, pattern))
            {
                return new ValidationError()
                {
                    Message = "Bank account number format is invalid.",
                    RuleName = "IncorrectBankAccountNumberFormat",
                    Severity = RuleSeverity.Error
                };
            }

            return null;
        }
    }
}
