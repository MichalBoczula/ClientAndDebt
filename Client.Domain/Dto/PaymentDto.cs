namespace Client.Domain.Dto
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
    }
}
