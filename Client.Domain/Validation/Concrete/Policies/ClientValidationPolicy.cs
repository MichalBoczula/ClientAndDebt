using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules;

namespace Client.Domain.Validation.Concrete.Policies
{
    public class ClientValidationPolicy
    {
        private readonly List<IValidationRule<ClientInstance>> _rules = new();

        public ClientValidationPolicy()
        {
            _rules.Add(new FirstNameNullOrWhiteSpaceValidationRule());
            _rules.Add(new LastNameNullOrWhiteSpaceValidationRule());
            _rules.Add(new BankAccountNumberFormatValidationRule());
        }

        public ValidationResult Validate(ClientInstance client)
        {
            var validationResult = new ValidationResult();

            foreach (var rule in _rules)
            {
                rule.IsValid(client, validationResult);
            }

            return validationResult;
        }

        public void Describe()
        {
            throw new NotImplementedException("This method is not implemented yet. Please implement it if needed.");
        }
    }
}
