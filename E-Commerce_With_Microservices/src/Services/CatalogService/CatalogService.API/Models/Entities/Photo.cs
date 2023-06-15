namespace CatalogService.API.Models.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string photoUrl { get; set; }
        public bool CurrentPhoto { get; set; }
        public Product Product { get; set; }
    }
}
