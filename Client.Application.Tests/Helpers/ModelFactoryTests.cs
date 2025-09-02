using Client.Application.Helpers;
using Client.Domain.Dto;
using Shouldly;

namespace Client.Application.Tests.Helpers
{
    public class ModelFactoryTests
    {
        [Fact]
        public void CreatePaymentTest_ShouldCreatePayment()
        {
            //arrange
            var paymentDto = new PaymentDto
            {
                Amount = 100.0m,
                Date = DateTime.UtcNow,
                Note = "Test payment"
            };

            //act
            var result = ModelFactory.CreatePayment(paymentDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Amount.ShouldBe(paymentDto.Amount);
            result.Date.ShouldBe(paymentDto.Date);
            result.Note.ShouldBe(paymentDto.Note);
        }

        [Fact]
        public void CreatePayment_ShouldCreateDebt()
        {
            //arrange
            var debtDto = new DebtDto
            {
                Amount = 500.0m,
                DueDate = DateTime.UtcNow.AddDays(30)
            };

            //act
            var result = ModelFactory.CreateDebt(debtDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Amount.ShouldBe(debtDto.Amount);
            result.DueDate.ShouldBe(debtDto.DueDate);
            result.Payments.ShouldBe([]);
        }

        [Fact]
        public void CreatePayment2_ShouldCreateDebt()
        {
            //arrange
            var debtDto = new DebtDto
            {
                Amount = 500.0m,
                DueDate = DateTime.UtcNow.AddDays(30),
                Payments = new List<PaymentDto>
                {
                    new PaymentDto
                    {
                        Amount = 100.0m,
                        Date = DateTime.UtcNow,
                        Note = "Test payment"
                    }
                }
            };

            //act
            var result = ModelFactory.CreateDebt(debtDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Amount.ShouldBe(debtDto.Amount);
            result.DueDate.ShouldBe(debtDto.DueDate);
            result.Payments.ShouldNotBeEmpty();
            result.Payments.Count.ShouldBe(1);
            result.Payments.ForEach(p =>
            {
                p.Id.ShouldNotBe(Guid.Empty);
                p.Amount.ShouldBe(debtDto.Payments[0].Amount);
                p.Date.ShouldBe(debtDto.Payments[0].Date);
                p.Note.ShouldBe(debtDto.Payments[0].Note);
            });
        }

        [Fact]
        public void CreatePayment3_ShouldCreateDebt()
        {
            //arrange
            var debtDto = new DebtDto
            {
                Amount = 500.0m,
                DueDate = DateTime.UtcNow.AddDays(30),
                Payments = new List<PaymentDto>
                {
                    new PaymentDto
                    {
                        Amount = 100.0m,
                        Date = DateTime.UtcNow,
                        Note = "Test payment"
                    },
                    new PaymentDto
                    {
                        Amount = 200.0m,
                        Date = DateTime.UtcNow,
                        Note = "Second payment"
                    }
                }
            };

            //act
            var result = ModelFactory.CreateDebt(debtDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Amount.ShouldBe(debtDto.Amount);
            result.DueDate.ShouldBe(debtDto.DueDate);
            result.Payments.ShouldNotBeEmpty();
            result.Payments.Count.ShouldBe(2);
            result.Payments[0].Id.ShouldNotBe(Guid.Empty);
            result.Payments[0].Amount.ShouldBe(debtDto.Payments[0].Amount);
            result.Payments[0].Date.ShouldBe(debtDto.Payments[0].Date);
            result.Payments[0].Note.ShouldBe(debtDto.Payments[0].Note);
            result.Payments[1].Id.ShouldNotBe(Guid.Empty);
            result.Payments[1].Amount.ShouldBe(debtDto.Payments[1].Amount);
            result.Payments[1].Date.ShouldBe(debtDto.Payments[1].Date);
            result.Payments[1].Note.ShouldBe(debtDto.Payments[1].Note);
        }

        [Fact]
        public void CreateClient_ShouldCreateClient()
        {
            //arrange
            var clientDto = new CreateClientDto
            {
                FirstName = "John",
                LastName = "Doe",
                BankAccountNumber = "1234567890"
            };

            //act
            var result = ModelFactory.CreateClient(clientDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.FirstName.ShouldBe(clientDto.FirstName);
            result.LastName.ShouldBe(clientDto.LastName);
            result.BankAccountNumber.ShouldBe(clientDto.BankAccountNumber);
            result.Debts.ShouldBeEmpty();
        }

        [Fact]
        public void CreateClient2_ShouldCreateClient()
        {
            //arrange
            var clientDto = new CreateClientDto
            {
                FirstName = "John",
                LastName = "Doe",
                BankAccountNumber = "1234567890",
                Debts = new List<DebtDto>
                {
                    new DebtDto
                    {
                        Amount = 500.0m,
                        DueDate = DateTime.UtcNow.AddDays(30),
                        Payments = new List<PaymentDto>
                        {
                            new PaymentDto
                            {
                                Amount = 100.0m,
                                Date = DateTime.UtcNow,
                                Note = "Test payment"
                            }
                        }
                    }
                }
            };

            //act
            var result = ModelFactory.CreateClient(clientDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.FirstName.ShouldBe(clientDto.FirstName);
            result.LastName.ShouldBe(clientDto.LastName);
            result.BankAccountNumber.ShouldBe(clientDto.BankAccountNumber);
            result.Debts.ShouldNotBeEmpty();
            result.Debts.Count.ShouldBe(1);
            result.Debts[0].Id.ShouldNotBe(Guid.Empty);
            result.Debts[0].Amount.ShouldBe(clientDto.Debts[0].Amount);
            result.Debts[0].DueDate.ShouldBe(clientDto.Debts[0].DueDate);
            result.Debts[0].Payments.ShouldNotBeEmpty();
            result.Debts[0].Payments[0].Id.ShouldNotBe(Guid.Empty);
            result.Debts[0].Payments[0].Amount.ShouldBe(clientDto.Debts[0].Payments[0].Amount);
            result.Debts[0].Payments[0].Date.ShouldBe(clientDto.Debts[0].Payments[0].Date);
            result.Debts[0].Payments[0].Note.ShouldBe(clientDto.Debts[0].Payments[0].Note);
        }

        [Fact]
        public void CreateClient3_ShouldCreateClient()
        {
            //arrange
            var clientDto = new CreateClientDto
            {
                FirstName = "John",
                LastName = "Doe",
                BankAccountNumber = "1234567890",
                Debts = new List<DebtDto>
                {
                    new DebtDto
                    {
                        Amount = 500.0m,
                        DueDate = DateTime.UtcNow.AddDays(30),
                        Payments = new List<PaymentDto>
                        {
                            new PaymentDto
                            {
                                Amount = 100.0m,
                                Date = DateTime.UtcNow,
                                Note = "Test payment"
                            },
                            new PaymentDto
                            {
                                Amount = 200.0m,
                                Date = DateTime.UtcNow,
                                Note = "Second payment"
                            }
                        }
                    },
                    new DebtDto
                    {
                        Amount = 700.0m,
                        DueDate = DateTime.UtcNow.AddDays(60),
                        Payments = new List<PaymentDto>
                        {
                            new PaymentDto
                            {
                                Amount = 300.0m,
                                Date = DateTime.UtcNow,
                                Note = "Another payment"
                            },
                            new PaymentDto
                            {
                                Amount = 300.0m,
                                Date = DateTime.UtcNow,
                                Note = "Second another payment"
                            }
                        }
                    }
                }
            };

            //act
            var result = ModelFactory.CreateClient(clientDto);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.FirstName.ShouldBe(clientDto.FirstName);
            result.LastName.ShouldBe(clientDto.LastName);
            result.BankAccountNumber.ShouldBe(clientDto.BankAccountNumber);
            result.Debts.ShouldNotBeEmpty();

            result.Debts.Count.ShouldBe(2);
            result.Debts[0].Id.ShouldNotBe(Guid.Empty);
            result.Debts[0].Amount.ShouldBe(clientDto.Debts[0].Amount);
            result.Debts[0].DueDate.ShouldBe(clientDto.Debts[0].DueDate);
            result.Debts[0].Payments.Count.ShouldBe(2);
            result.Debts[0].Payments[0].Id.ShouldNotBe(Guid.Empty);
            result.Debts[0].Payments[0].Amount.ShouldBe(clientDto.Debts[0].Payments[0].Amount);
            result.Debts[0].Payments[0].Date.ShouldBe(clientDto.Debts[0].Payments[0].Date);
            result.Debts[0].Payments[0].Note.ShouldBe(clientDto.Debts[0].Payments[0].Note);
            result.Debts[0].Payments[1].Id.ShouldNotBe(Guid.Empty);
            result.Debts[0].Payments[1].Amount.ShouldBe(clientDto.Debts[0].Payments[1].Amount);
            result.Debts[0].Payments[1].Date.ShouldBe(clientDto.Debts[0].Payments[1].Date);
            result.Debts[0].Payments[1].Note.ShouldBe(clientDto.Debts[0].Payments[1].Note);

            result.Debts[1].Id.ShouldNotBe(Guid.Empty);
            result.Debts[1].Amount.ShouldBe(clientDto.Debts[1].Amount);
            result.Debts[1].DueDate.ShouldBe(clientDto.Debts[1].DueDate);
            result.Debts[1].Payments.Count.ShouldBe(2);
            result.Debts[1].Payments[0].Id.ShouldNotBe(Guid.Empty);
            result.Debts[1].Payments[0].Amount.ShouldBe(clientDto.Debts[1].Payments[0].Amount);
            result.Debts[1].Payments[0].Date.ShouldBe(clientDto.Debts[1].Payments[0].Date);
            result.Debts[1].Payments[0].Note.ShouldBe(clientDto.Debts[1].Payments[0].Note);
            result.Debts[1].Payments[1].Id.ShouldNotBe(Guid.Empty);
            result.Debts[1].Payments[1].Amount.ShouldBe(clientDto.Debts[1].Payments[1].Amount);
            result.Debts[1].Payments[1].Date.ShouldBe(clientDto.Debts[1].Payments[1].Date);
            result.Debts[1].Payments[1].Note.ShouldBe(clientDto.Debts[1].Payments[1].Note);
        }
    }
}
