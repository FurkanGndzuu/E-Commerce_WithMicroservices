using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasketService.API.DTOs
{
    public class AddOrUpdateOrDeleteId
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
