using Client.Domain.Models;

namespace Client.Domain.Dto
{
    public class PaymentInDebt
    {
        public required Debt Debt { get; set; }
        public required Payment NewPayment { get; set; }
    }
}
