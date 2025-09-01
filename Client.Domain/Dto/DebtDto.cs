namespace Client.Domain.Dto
{
    public class DebtDto
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public List<PaymentDto>? Payments { get; set; }
    }
}
