using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules
{
    public class BankAccountNumberValidationRule : IValidationRule<ClientInstance>
    {
        public void IsValid(ClientInstance entity, ValidationResult validationResults)
        {
            var error = entity.IsBankAccountNumberValid();
            if (error is not null)
            {
                validationResults.ValidationErrors.Add(error);
            }
        }
    }
}
