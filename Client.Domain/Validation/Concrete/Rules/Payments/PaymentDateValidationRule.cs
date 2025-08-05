using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Payments
{
    public class PaymentDateValidationRule : IValidationRule<Payment>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = nameof(PaymentDateValidationRule),
            Message = "Payment date cannot be earlier than today.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Payment payment, ValidationResult result)
        {
            if (!payment.IsDateTodayOrLater())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}
