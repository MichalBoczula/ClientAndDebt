using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Payments
{
    public class PaymentAmountValidationRule : IValidationRule<Payment>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = nameof(PaymentAmountValidationRule),
            Message = "Payment amount must be at least 100.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Payment payment, ValidationResult result)
        {
            if (!payment.IsAmountAboveMinimum())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}
