using Client.Domain.Models;
using Shouldly;

namespace Client.Domain.Tests.Models
{
    public class ClientInstanceTests
    {
        [Fact]
        public void IsFirstNameValid_Should_Return_Error_When_FirstName_Is_Whitespace()
        {
            var client = new ClientInstance { FirstName = " " };

            var result = client.IsFirstNameValid();

            result.ShouldBeFalse();
        }

        [Fact]
        public void IsFirstNameValid_Should_Return_Error_When_FirstName_Is_Null()
        {
            var client = new ClientInstance();

            var result = client.IsFirstNameValid();

            result.ShouldBeFalse();
        }

        [Fact]
        public void IsFirstNameValid_Should_Return_Null_When_FirstName_Is_Valid()
        {
            var client = new ClientInstance { FirstName = "Alice" };

            var result = client.IsFirstNameValid();

            result.ShouldBeTrue();
        }

        [Fact]
        public void IsLastNameValid_Should_Return_Error_When_LastName_Is_Whitespace()
        {
            var client = new ClientInstance { LastName = "" };

            var result = client.IsLastNameValid();

            result.ShouldBeFalse();
        }

        [Fact]
        public void IsLastNameValid_Should_Return_Error_When_LastName_Is_Null()
        {
            var client = new ClientInstance();

            var result = client.IsLastNameValid();

            result.ShouldBeFalse();
        }

        [Fact]
        public void IsLastNameValid_Should_Return_Null_When_LastName_Is_Valid()
        {
            var client = new ClientInstance { LastName = "Doe" };

            var result = client.IsLastNameValid();

            result.ShouldBeTrue();
        }

        [Fact]
        public void IsBankAccountNumberValid_Should_Return_Error_When_Is_WhiteSpace()
        {
            var client = new ClientInstance { BankAccountNumber = "" };

            var result = client.IsBankAccountNumberNullOrWhiteSpace();

            result.ShouldBeFalse();
        }

        [Fact]
        public void IsBankAccountNumberValid_Should_Return_Error_When_Is_Null()
        {
            var client = new ClientInstance();

            var result = client.IsBankAccountNumberNullOrWhiteSpace();

            result.ShouldBeFalse();
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abc")]
        [InlineData("123456")] 
        [InlineData("!!INVALID!!")]
        [InlineData("PL123")] 
        public void IsBankAccountNumberValid_Should_Return_Error_When_Format_Is_Invalid(string invalidAccount)
        {
            var client = new ClientInstance { BankAccountNumber = invalidAccount };

            var result = client.IsBankAccountNumberInCorrectForm();

            result.ShouldBeFalse();
        }

        [Theory]
        [InlineData("PL61109010140000071219812874")]
        [InlineData("DE89370400440532013000")]
        [InlineData("FR7630006000011234567890189")]
        [InlineData("GB29NWBK60161331926819")]
        [InlineData("GR1601101250000000012300695")]
        public void IsBankAccountNumberValid_Should_Return_Null_When_Format_Is_Valid(string validAccount)
        {
            var client = new ClientInstance { BankAccountNumber = validAccount };

            var result = client.IsBankAccountNumberInCorrectForm();

            result.ShouldBeTrue();
        }
    }
}
