using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;
using Shouldly;

namespace Client.Domain.Tests.Validation.Policies
{
    public class PaymentValidationPolicyTests
    {
        [Fact]
        public void Validate_Should_Return_Three_Errors_When_Amount_And_Date_Are_Invalid()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 30,
                Date = DateTime.UtcNow.AddDays(-1)
            };
            var policy = new PaymentValidationPolicy();

            // Act
            var result = policy.Validate(payment);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(3);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentAmountValidationRule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentDateValidationRule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentDivisibleBy50Rule");
        }

        [Fact]
        public void Validate_Should_Return_Two_Error_When_Only_Amount_Is_Invalid()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 30,
                Date = DateTime.UtcNow.AddDays(1)
            };
            var policy = new PaymentValidationPolicy();

            // Act
            var result = policy.Validate(payment);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(2);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentDivisibleBy50Rule");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentAmountValidationRule");
        }

        [Fact]
        public void Validate_Should_Return_One_Error_When_Only_Amount_Is_Invalid()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 130,
                Date = DateTime.UtcNow.AddDays(1)
            };
            var policy = new PaymentValidationPolicy();

            // Act
            var result = policy.Validate(payment);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "PaymentDivisibleBy50Rule");
        }

        [Fact]
        public void Validate_Should_Return_Valid_When_All_Fields_Are_Valid()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 200,
                Date = DateTime.UtcNow.AddDays(1)
            };
            var policy = new PaymentValidationPolicy();

            // Act
            var result = policy.Validate(payment);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.ShouldBeEmpty();
        }
    }
}
