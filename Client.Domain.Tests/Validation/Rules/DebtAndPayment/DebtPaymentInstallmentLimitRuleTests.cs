using Client.Domain.Dto;
using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.DebtAndPayment
{
    public class DebtPaymentInstallmentLimitRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Installment_Exceeds_Remaining_Amount()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
                {
                    new Payment { Amount = 800 }
                }
            };

            var newPayment = new Payment { Amount = 300 };
            var paymentInDebt = new PaymentInDebtDto
            {
                Debt = debt,
                NewPayment = newPayment
            };

            var result = new ValidationResult();
            var rule = new DebtPaymentInstallmentLimitRule();

            // Act
            rule.IsValid(paymentInDebt, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe(nameof(DebtPaymentInstallmentLimitRule));
            result.ValidationErrors[0].Message.ShouldContain("Maximum allowed installment is 200");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Installment_Is_Within_Limit()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
                {
                    new Payment { Amount = 200 }
                }
            };

            var newPayment = new Payment { Amount = 300 };
            var paymentInDebt = new PaymentInDebtDto
            {
                Debt = debt,
                NewPayment = newPayment
            };

            var result = new ValidationResult();
            var rule = new DebtPaymentInstallmentLimitRule();

            // Act
            rule.IsValid(paymentInDebt, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
