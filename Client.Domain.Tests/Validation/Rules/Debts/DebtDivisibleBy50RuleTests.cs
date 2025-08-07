using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Debts
{
    public class DebtDivisibleBy50RuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Not_Divisible_By_50()
        {
            // Arrange
            var debt = new Debt { Amount = 125 };
            var result = new ValidationResult();
            var rule = new DebtDivisibleBy50Rule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors.ShouldContain(error => error.RuleName == nameof(DebtDivisibleBy50Rule));
        }

        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Zero()
        {
            // Arrange
            var debt = new Debt { Amount = 0 };
            var result = new ValidationResult();
            var rule = new DebtDivisibleBy50Rule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe(nameof(DebtDivisibleBy50Rule));
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Amount_Is_Valid_And_Divisible_By_50()
        {
            // Arrange
            var debt = new Debt { Amount = 150 };
            var result = new ValidationResult();
            var rule = new DebtDivisibleBy50Rule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
