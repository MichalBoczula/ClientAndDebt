using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Clients
{
    public class BankAccountNumberFormatValidationRule : IValidationRule<ClientInstance>
    {
        private readonly ValidationError nullOrWhiteSpace;
        private readonly ValidationError incorrectForm;

        public BankAccountNumberFormatValidationRule()
        {
            nullOrWhiteSpace = new ValidationError
            {
                Message = "Bank account number cannot be null or whitespace.",
                RuleName = "BankAccountNumberNullOrWhiteSpace",
                Severity = RuleSeverity.Error
            };

            incorrectForm = new ValidationError
            {
                Message = "Bank account number format is incorrect.",
                RuleName = "IncorrectBankAccountNumberFormat",
                Severity = RuleSeverity.Error
            };
        }

        public void IsValid(ClientInstance entity, ValidationResult validationResults)
        {
            if (string.IsNullOrWhiteSpace(entity.BankAccountNumber))
            {
                validationResults.ValidationErrors.Add(nullOrWhiteSpace);
                return;
            }
         
            if (!entity.IsBankAccountNumberInCorrectForm())
            {
                validationResults.ValidationErrors.Add(incorrectForm);
            }
        }
        
        public List<ValidationError> Describe()
        {
            return new List<ValidationError> { nullOrWhiteSpace, incorrectForm };
        }
    }
}
