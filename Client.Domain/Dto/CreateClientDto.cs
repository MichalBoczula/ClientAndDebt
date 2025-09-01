namespace Client.Domain.Dto
{
    public class CreateClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BankAccountNumber { get; set; }
        public List<DebtDto>? Debts { get; set; }
    }
}
