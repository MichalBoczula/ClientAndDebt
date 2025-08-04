using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules.Debts;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Debts
{
    public class DebtDueDateValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_Error_When_DueDate_Is_Less_Than_One_Month_From_Now()
        {
            // Arrange
            var debt = new Debt { DueDate = DateTime.UtcNow.AddDays(10) };
            var result = new ValidationResult();
            var rule = new DebtDueDateValidationRule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("DebtDueDateValidationRule");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_When_DueDate_Is_At_Least_One_Month_From_Now()
        {
            // Arrange
            var debt = new Debt { DueDate = DateTime.UtcNow.AddMonths(1).AddDays(1) };
            var result = new ValidationResult();
            var rule = new DebtDueDateValidationRule();

            // Act
            rule.IsValid(debt, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}