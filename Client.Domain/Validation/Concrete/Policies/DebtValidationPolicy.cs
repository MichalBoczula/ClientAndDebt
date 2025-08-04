using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;

namespace Client.Domain.Validation.Concrete.Policies
{
    public class DebtValidationPolicy
    {
        private readonly List<IValidationRule<Debt>> _rules = new();

        public DebtValidationPolicy()
        {
            _rules.Add(new DebtAmountValidationRule());
            _rules.Add(new DebtDueDateValidationRule());
        }

        public ValidationResult Validate(Debt debt)
        {
            var validationResult = new ValidationResult();

            foreach (var rule in _rules)
            {
                rule.IsValid(debt, validationResult);
            }

            return validationResult;
        }

        public ValidationPolicyDescriptor Describe()
        {
            var allErrors = _rules
                .Select(rule => new ValidationRuleDescriptor()
                {
                    RuleName = rule.GetType().Name,
                    Rules = rule.Describe()
                })
                .ToList();

            return new ValidationPolicyDescriptor()
            {
                PolicyName = nameof(ClientValidationPolicy),
                Rules = allErrors
            };
        }
    }
}
