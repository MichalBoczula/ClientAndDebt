using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Payments
{
    public class PaymentDivisibleBy50Rule : IValidationRule<Payment>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = nameof(PaymentDivisibleBy50Rule),
            Message = "Payment amount must be a natural number divisible by 50.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Payment payment, ValidationResult result)
        {
            if (!payment.IsAmountNaturalAndDivisibleBy50())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}
