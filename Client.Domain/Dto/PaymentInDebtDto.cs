using Client.Domain.Models;

namespace Client.Domain.Dto
{
    public class PaymentInDebtDto
    {
        public required Debt Debt { get; set; }
        public required Payment NewPayment { get; set; }
    }
}
