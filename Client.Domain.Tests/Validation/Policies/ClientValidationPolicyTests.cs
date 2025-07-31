using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;
using Shouldly;

namespace Client.Domain.Tests.Validation.Policies
{
    public class ClientValidationPolicyTests
    {
        [Fact]
        public void Validate_Should_Return_3_Errors_When_All_Fields_Are_Invalid()
        {
            // Arrange
            var client = new ClientInstance
            {
                FirstName = "",
                LastName = null,
                BankAccountNumber = "123"
            };

            var policy = new ClientValidationPolicy();

            // Act
            var result = policy.Validate(client);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(3);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "NotEmptyFirstName");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "NotEmptyLastName");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "IncorrectBankAccountNumberFormat");
        }

        [Fact]
        public void Validate_Should_Return_2_Errors_When_Two_Fields_Are_Invalid()
        {
            var client = new ClientInstance
            {
                FirstName = "John",
                LastName = "",
                BankAccountNumber = "abc"
            };

            var policy = new ClientValidationPolicy();

            var result = policy.Validate(client);

            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(2);
            result.ValidationErrors.ShouldContain(e => e.RuleName == "NotEmptyLastName");
            result.ValidationErrors.ShouldContain(e => e.RuleName == "IncorrectBankAccountNumberFormat");
        }

        [Fact]
        public void Validate_Should_Return_1_Error_When_One_Field_Is_Invalid()
        {
            var client = new ClientInstance
            {
                FirstName = "Jane",
                LastName = "Doe",
                BankAccountNumber = "bad"
            };

            var policy = new ClientValidationPolicy();

            var result = policy.Validate(client);

            result.IsValid.ShouldBeFalse();
            result.ValidationErrors.Count.ShouldBe(1);
            result.ValidationErrors[0].RuleName.ShouldBe("IncorrectBankAccountNumberFormat");
        }

        [Fact]
        public void Validate_Should_Return_No_Errors_When_Everything_Is_Valid()
        {
            var client = new ClientInstance
            {
                FirstName = "Alice",
                LastName = "Smith",
                BankAccountNumber = "PL61109010140000071219812874"
            };

            var policy = new ClientValidationPolicy();

            var result = policy.Validate(client);

            result.IsValid.ShouldBeTrue();
            result.ValidationErrors.ShouldBeEmpty();
        }
    }

}
