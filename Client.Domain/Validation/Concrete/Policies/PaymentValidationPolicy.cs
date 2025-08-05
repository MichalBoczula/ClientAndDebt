using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Payments;

namespace Client.Domain.Validation.Concrete.Policies
{
    public class PaymentValidationPolicy
    {
        private readonly List<IValidationRule<Payment>> _rules = new();

        public PaymentValidationPolicy()
        {
            _rules.Add(new PaymentAmountValidationRule());
            _rules.Add(new PaymentDateValidationRule());
        }

        public ValidationResult Validate(Payment payment)
        {
            var validationResult = new ValidationResult();

            foreach (var rule in _rules)
            {
                rule.IsValid(payment, validationResult);
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
                PolicyName = nameof(PaymentValidationPolicy),
                Rules = allErrors
            };
        }
    }
}
