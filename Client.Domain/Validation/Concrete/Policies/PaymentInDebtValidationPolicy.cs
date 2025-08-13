using Client.Domain.Dto;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;

namespace Client.Domain.Validation.Concrete.Policies
{
    public class PaymentInDebtValidationPolicy
    {
        private readonly List<IValidationRule<PaymentInDebtDto>> _rules = new();

        public PaymentInDebtValidationPolicy()
        {
            _rules.Add(new DebtPaymentInstallmentLimitRule());
        }

        public ValidationResult Validate(PaymentInDebtDto paymentInDebt)
        {
            var validationResult = new ValidationResult();

            foreach (var rule in _rules)
            {
                rule.IsValid(paymentInDebt, validationResult);
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
