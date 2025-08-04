using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Rules;
using Client.Domain.Validation.Concrete.Rules.Clients;
using Shouldly;

namespace Client.Domain.Tests.Validation.Rules.Clients
{
    public class FirstNameNullOrWhiteSpaceValidationRuleTests
    {
        [Fact]
        public void IsValid_Should_Add_ValidationError_For_Invalid_Accounts()
        {
            // Arrange
            var client = new ClientInstance { FirstName = "" };
            var result = new ValidationResult();
            var rule = new FirstNameNullOrWhiteSpaceValidationRule();

            // Act
            rule.IsValid(client, result);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("NotEmptyFirstName");
        }

        [Fact]
        public void IsValid_Should_Not_Add_Error_For_Valid_Accounts()
        {
            // Arrange
            var client = new ClientInstance { FirstName = "John" };
            var result = new ValidationResult();
            var rule = new FirstNameNullOrWhiteSpaceValidationRule();

            // Act
            rule.IsValid(client, result);

            // Assert
            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.Count.ShouldBe(0);
        }
    }
}
