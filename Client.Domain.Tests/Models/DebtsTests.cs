using Client.Domain.Models;
using Shouldly;

namespace Client.Domain.Tests.Models
{
    public class DebtsTests
    {
        [Theory]
        [InlineData(-100, false)]
        [InlineData(100, false)]
        [InlineData(499, false)]
        [InlineData(500, true)]
        [InlineData(10000, true)]
        [InlineData(10001, false)]
        [InlineData(7500, true)]
        public void IsAmountInValidRange_Should_Return_Correct_Result(decimal amount, bool expected)
        {
            // Arrange
            var debt = new Debt { Amount = amount };

            // Act
            var result = debt.IsAmountInValidRange();

            // Assert
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(20, false)]
        [InlineData(600, true)]
        [InlineData(0, false)]
        [InlineData(-10, false)]
        public void IsDueDateAtLeastOneMonthFromNow_AddDays_ShouldBehaveAsExpected(int daysFromNow, bool expected)
        {
            // Arrange
            var debt = new Debt { DueDate = DateTime.UtcNow.AddDays(daysFromNow) };

            // Act
            var result = debt.IsDueDateAtLeastOneMonthFromNow();

            // Assert
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void IsDueDateAtLeastOneMonthFromNow_AddMonths_ShouldBehaveAsExpected(int months, bool expected)
        {
            // Arrange
            var debt = new Debt { DueDate = DateTime.UtcNow.AddMonths(months).AddDays(1) };

            // Act
            var result = debt.IsDueDateAtLeastOneMonthFromNow();

            // Assert
            result.ShouldBe(expected);
        }
    }
}
