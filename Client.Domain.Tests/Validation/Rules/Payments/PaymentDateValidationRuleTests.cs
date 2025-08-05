using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Payments;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Payments
{
    public class PaymentDateValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Date_Is_In_The_Past()
        {
            // Arrange
            var payment = new Payment { Date = DateTime.UtcNow.AddDays(-1) };
            var result = new ValidationResult();
            var rule = new PaymentDateValidationRule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("PaymentDateValidationRule");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Date_Is_Today_Or_Later()
        {
            // Arrange
            var payment = new Payment { Date = DateTime.UtcNow.AddDays(1) };
            var result = new ValidationResult();
            var rule = new PaymentDateValidationRule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
