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
            _rules.Add(new FirstNameValidationRule());
            _rules.Add(new LastNameValidationRule());
            _rules.Add(new BankAccountNumberValidationRule());
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
    }
}
