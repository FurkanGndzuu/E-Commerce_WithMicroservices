using CatalogService.API.Models.Entities;

namespace CatalogService.API.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? MainPictureUrl { get; set; }
        public IList<string>? AllPictureUrl { get; set; }
        public string CategoryId { get; set; }
    }
}
