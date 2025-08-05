using Client.Domain.Models;
using Shouldly;

namespace Client.Domain.Tests.Models
{
    public class PaymentsTests
    {
        [Theory]
        [InlineData(50, false)]
        [InlineData(99.99, false)]
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
    }
}
