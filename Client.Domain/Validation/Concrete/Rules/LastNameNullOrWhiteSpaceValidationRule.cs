using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules
{
    public class LastNameNullOrWhiteSpaceValidationRule : IValidationRule<ClientInstance>
    {
        private ValidationError error;

        public LastNameNullOrWhiteSpaceValidationRule()
        {
            error = new ValidationError()
            {
                Message = "Last name cannot be empty.",
                RuleName = "NotEmptyLastName",
                Severity = RuleSeverity.Error
            };
        }

        public void IsValid(ClientInstance entity, ValidationResult validationResults)
        {
            if (!entity.IsLastNameValid())
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
