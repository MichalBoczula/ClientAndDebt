using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Payments;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Payments
{
    public class PaymentAmountValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Less_Than_100()
        {
            // Arrange
            var payment = new Payment { Amount = 99 };
            var result = new ValidationResult();
            var rule = new PaymentAmountValidationRule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("PaymentAmountValidationRule");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Amount_Is_Valid()
        {
            // Arrange
            var payment = new Payment { Amount = 150 };
            var result = new ValidationResult();
            var rule = new PaymentAmountValidationRule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
