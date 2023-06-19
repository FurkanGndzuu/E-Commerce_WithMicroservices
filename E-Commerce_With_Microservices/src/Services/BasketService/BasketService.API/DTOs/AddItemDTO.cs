namespace BasketService.API.DTOs
{
    public class AddItemDTO
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
