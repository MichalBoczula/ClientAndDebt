using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;
using Shouldly;

namespace Client.Domain.Tests.Models
{
    public class PaymentsTests
    {
        [Theory]
        [InlineData(30, false)]
        [InlineData(49, false)]
        [InlineData(100, true)]
        [InlineData(150, true)]
        public void IsAmountAboveMinimum_Should_Behave_As_Expected(decimal amount, bool expected)
        {
            // Arrange
            var payment = new Payment { Amount = amount };

            // Act
            var result = payment.IsAmountAboveMinimum();

            // Assert
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        public void IsDateTodayOrLater_Should_Behave_As_Expected(int daysOffset, bool expected)
        {
            // Arrange
            var date = DateTime.UtcNow.Date.AddDays(daysOffset);
            var payment = new Payment { Date = date };

            // Act
            var result = payment.IsDateTodayOrLater();

            // Assert
            result.ShouldBe(expected);
        }


        [Theory]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(75, false)]
        [InlineData(50, true)]
        [InlineData(-100, false)]
        public void IsValid_Should_Validate_DivisibleBy50_Rule(decimal amount, bool expectedValid)
        {
            // Arrange
            var payment = new Payment
            {
                Amount = amount,
            };

            // Act
            var result = payment.IsAmountNaturalAndDivisibleBy50();

            // Assert
            result.ShouldBe(expectedValid);
        }
    }
}
