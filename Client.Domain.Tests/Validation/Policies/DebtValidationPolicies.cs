using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;
using Shouldly;

namespace Client.Domain.Tests.Validation.Policies
{
    public class DebtValidationPolicies
    {
        [Fact]
        public void Validate_Should_Return_Three_Errors_When_Amount_And_DueDate_Are_Invalid()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 125,
                DueDate = DateTime.UtcNow.AddDays(5)
            };
            var policy = new DebtValidationPolicy();

            // Act
            var result = policy.Validate(debt);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(3);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "DebtAmountValidationRule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "DebtDueDateValidationRule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "DebtDivisibleBy50Rule");
        }

        [Fact]
        public void Validate_Should_Return_Two_Errors_When_Amount_And_DueDate_Are_Invalid()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 100,
                DueDate = DateTime.UtcNow.AddDays(5)
            };
            var policy = new DebtValidationPolicy();

            // Act
            var result = policy.Validate(debt);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(2);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "DebtAmountValidationRule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "DebtDueDateValidationRule");
        }

        [Fact]
        public void Validate_Should_Return_One_Error_When_Only_Amount_Is_Invalid()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 200,
                DueDate = DateTime.UtcNow.AddMonths(2)
            };
            var policy = new DebtValidationPolicy();

            // Act
            var result = policy.Validate(debt);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("DebtAmountValidationRule");
        }

        [Fact]
        public void Validate_Should_Return_Valid_When_All_Fields_Are_Valid()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 700,
                DueDate = DateTime.UtcNow.AddMonths(2)
            };
            var policy = new DebtValidationPolicy();

            // Act
            var result = policy.Validate(debt);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.ShouldBeEmpty();
        }
    }
}