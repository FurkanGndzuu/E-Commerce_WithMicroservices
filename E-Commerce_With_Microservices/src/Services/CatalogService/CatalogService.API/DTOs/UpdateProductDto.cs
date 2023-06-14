using CatalogService.API.Models.Entities;

namespace CatalogService.API.DTOs
{
    public class UpdateProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }
}
