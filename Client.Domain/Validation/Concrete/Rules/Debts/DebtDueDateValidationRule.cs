using Client.Domain.Enums;
using Client.Domain.Models;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Debts
{
    public class DebtDueDateValidationRule : IValidationRule<Debt>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = "DebtDueDateValidationRule",
            Message = "Due date must be at least 1 month in the future.",
            Severity = RuleSeverity.Error
        };

        public void IsValid(Debt debt, ValidationResult result)
        {
            if (!debt.IsDueDateAtLeastOneMonthFromNow())
            {
                result.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}
