using Client.Domain.Dto;
using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;
using Shouldly;

namespace Client.Domain.Tests.Validation.Policies
{
    public class PaymentInDebtValidationPolicyTests
    {
        [Fact]
        public void Validate_Should_Return_One_Error_When_Installment_Exceeds_Remaining_Amount()
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

            var payment = new Payment { Amount = 300 };

            var paymentInDebt = new PaymentInDebtDto
            {
                Debt = debt,
                NewPayment = payment
            };

            var policy = new PaymentInDebtValidationPolicy();

            // Act
            var result = policy.Validate(paymentInDebt);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("DebtPaymentInstallmentLimitRule");
            result.ValidationErrors[0].Message.ShouldContain("Maximum allowed installment is 200");
        }

        [Fact]
        public void Validate_Should_Return_Valid_When_Installment_Is_Within_Remaining_Amount()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
                {
                    new Payment { Amount = 300 }
                }
            };

            var payment = new Payment { Amount = 400 };

            var paymentInDebt = new PaymentInDebtDto
            {
                Debt = debt,
                NewPayment = payment
            };

            var policy = new PaymentInDebtValidationPolicy();

            // Act
            var result = policy.Validate(paymentInDebt);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.ShouldBeEmpty();
        }
    }
}