namespace BasketService.API.DTOs
{
    public class BasketDTO
    {
        public string UserId { get; set; }
        public string DiscountCode  { get; set; }
        public string DiscountRate { get; set; }
        public List<BasketItemDTO> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.ProductPrice * x.Quantity);
        }

        public BasketDTO()
        {
                BasketItems = new List<BasketItemDTO>();
        }
    }
}
