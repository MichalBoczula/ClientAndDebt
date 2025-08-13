using Client.Domain.Dto;
using Client.Domain.Enums;
using Client.Domain.Validation.Abstract;
using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Concrete.Rules.Debts
{
    public class DebtPaymentInstallmentLimitRule : IValidationRule<PaymentInDebtDto>
    {
        private readonly ValidationError _error = new()
        {
            RuleName = nameof(DebtPaymentInstallmentLimitRule),
            Message = "Installment exceeds remaining balance. Maximum allowed installment is",
            Severity = RuleSeverity.Error
        };

        public void IsValid(PaymentInDebtDto paymentInDebt, ValidationResult validationResults)
        {
            var result = paymentInDebt.Debt.HasInstallementValidSum(paymentInDebt.NewPayment);
            if (!result.isValid)
            {
                _error.Message = $"{_error.Message} {result.maxAllowedInstallment}.";
                validationResults.ValidationErrors.Add(_error);
            }
        }

        public List<ValidationError> Describe() => new() { _error };
    }
}