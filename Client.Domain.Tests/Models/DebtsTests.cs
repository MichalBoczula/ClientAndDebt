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

        [Fact]
        public void HasInstallementValidSum_Should_Return_True_And_Add_Payment_When_Valid()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
            {
                new Payment { Amount = 200 },
                new Payment { Amount = 300 }
            }
            };

            var newPayment = new Payment { Amount = 400 };

            // Act
            var (isValid, maxAllowed) = debt.HasInstallementValidSum(newPayment);

            // Assert
            isValid.ShouldBeTrue();
            maxAllowed.ShouldBe(500); 
        }

        [Fact]
        public void HasInstallementValidSum_Should_Return_False_And_Not_Add_Payment_When_Too_Much()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
            {
                new Payment { Amount = 400 },
                new Payment { Amount = 300 }
            }
            };

            var newPayment = new Payment { Amount = 400 };

            // Act
            var (isValid, maxAllowed) = debt.HasInstallementValidSum(newPayment);

            // Assert
            isValid.ShouldBeFalse();
            maxAllowed.ShouldBe(300);
            debt.Payments.ShouldNotContain(newPayment); 
        }

        [Fact]
        public void HasInstallementValidSum_Should_Allow_Exact_Remaining_Amount()
        {
            // Arrange
            var debt = new Debt
            {
                Amount = 1000,
                Payments = new List<Payment>
            {
                new Payment { Amount = 600 }
            }
            };

            var newPayment = new Payment { Amount = 400 }; 

            // Act
            var (isValid, maxAllowed) = debt.HasInstallementValidSum(newPayment);

            // Assert
            isValid.ShouldBeTrue();
            maxAllowed.ShouldBe(400);
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
            var debt = new Debt
            {
                Amount = amount,
            };

            // Act
            var result = debt.IsAmountNaturalAndDivisibleBy50();

            // Assert
            result.ShouldBe(expectedValid);
        }
    }
}
