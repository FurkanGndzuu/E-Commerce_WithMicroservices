namespace StockService.API.DTOs
{
    public class StockDTO
    {
        public string? Id { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int StockQuantity { get; set; }
    }
}
