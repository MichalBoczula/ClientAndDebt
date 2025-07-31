using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules
{
    public class FirstNameNullOrWhiteSpaceValidationRule : IValidationRule<ClientInstance>
    {
        private ValidationError error;

        public FirstNameNullOrWhiteSpaceValidationRule()
        {
            error = new ValidationError()
            {
                Message = "First name cannot be empty.",
                RuleName = "NotEmptyFirstName",
                Severity = RuleSeverity.Error
            };
        }

        public void IsValid(ClientInstance entity, ValidationResult validationResults)
        {
            if (!entity.IsFirstNameValid())
            {
                validationResults.ValidationErrors.Add(error);
            }
        }

        public List<ValidationError> Describe()
        {
            return new List<ValidationError>() { error };
        }
    }
}
