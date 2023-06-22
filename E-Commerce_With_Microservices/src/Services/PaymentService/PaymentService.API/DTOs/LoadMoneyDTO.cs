namespace PaymentService.API.DTOs
{
    public class LoadMoneyDTO
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpritionDate { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}
