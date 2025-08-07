using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Payments;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Payments
{
    public class PaymentDivisibleBy50RuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Not_Divisible_By_50()
        {
            // Arrange
            var payment = new Payment { Amount = 125 };
            var result = new ValidationResult();
            var rule = new PaymentDivisibleBy50Rule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe(nameof(PaymentDivisibleBy50Rule));
        }

        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Zero()
        {
            // Arrange
            var payment = new Payment { Amount = 0 };
            var result = new ValidationResult();
            var rule = new PaymentDivisibleBy50Rule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe(nameof(PaymentDivisibleBy50Rule));
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Amount_Is_Valid_And_Divisible_By_50()
        {
            // Arrange
            var payment = new Payment { Amount = 150 };
            var result = new ValidationResult();
            var rule = new PaymentDivisibleBy50Rule();

            // Act
            rule.IsValid(payment, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
