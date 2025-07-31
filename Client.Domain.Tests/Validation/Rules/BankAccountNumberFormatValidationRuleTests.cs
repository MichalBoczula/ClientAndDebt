using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules
{
    public class BankAccountNumberFormatValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_ValidationError_For_Empty()
        {
            // Arrange
            var client = new ClientInstance { BankAccountNumber = "" };
            var result = new ValidationResult();
            var rule = new BankAccountNumberFormatValidationRule();

            // Act
            rule.IsValid(client, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("BankAccountNumberNullOrWhiteSpace");
        }

        [Fact]
        public void IsValid_Should_Add_ValidationError_For_Invalid_Accounts()
        {
            // Arrange
            var client = new ClientInstance { BankAccountNumber = "test" };
            var result = new ValidationResult();
            var rule = new BankAccountNumberFormatValidationRule();

            // Act
            rule.IsValid(client, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("IncorrectBankAccountNumberFormat");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_For_Valid_Accounts()
        {
            // Arrange
            var client = new ClientInstance { BankAccountNumber = "PL61109010140000071219812874" };
            var result = new ValidationResult();
            var rule = new BankAccountNumberFormatValidationRule();

            // Act
            rule.IsValid(client, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
