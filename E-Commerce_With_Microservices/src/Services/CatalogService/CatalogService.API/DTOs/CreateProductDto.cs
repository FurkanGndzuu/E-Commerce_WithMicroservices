using CatalogService.API.Models.Entities;

namespace CatalogService.API.DTOs
{
    public class CreateProductDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }

    }
}
