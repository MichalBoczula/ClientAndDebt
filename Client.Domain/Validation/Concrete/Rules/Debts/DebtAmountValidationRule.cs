using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Debts
{
    public class DebtAmountValidationRule : IValidationRule<Debt>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = "DebtAmountValidationRule",
            Message = "Debt amount must be between 500 and 10,000.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Debt debt, ValidationResult result)
        {
            if (!debt.IsAmountInValidRange())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}