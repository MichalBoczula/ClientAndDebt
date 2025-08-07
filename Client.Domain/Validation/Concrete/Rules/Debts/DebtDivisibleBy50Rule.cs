using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Debts
{
    public class DebtDivisibleBy50Rule : IValidationRule<Debt>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = nameof(DebtDivisibleBy50Rule),
            Message = "Debt amount must be a natural number divisible by 50.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Debt debt, ValidationResult result)
        {
            if (!debt.IsAmountNaturalAndDivisibleBy50())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}
