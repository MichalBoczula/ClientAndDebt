using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Debts
{
    public class DebtAmountValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_Amount_Is_Less_Than_500()
        {
            // Arrange
            var debt = new Debt { Amount = 499 };
            var result = new ValidationResult();
            var rule = new DebtAmountValidationRule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("DebtAmountValidationRule");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_Amount_Is_Within_Valid_Range()
        {
            // Arrange
            var debt = new Debt { Amount = 800 };
            var result = new ValidationResult();
            var rule = new DebtAmountValidationRule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}