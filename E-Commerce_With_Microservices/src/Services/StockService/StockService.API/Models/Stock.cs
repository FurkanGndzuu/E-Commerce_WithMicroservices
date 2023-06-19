using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Net;

namespace StockService.API.Models
{
    public class Stock
    {
        public ObjectId Id { get; set; }
        public string ProductName { get; set; }
        [BsonElement("product_id")]
        public string ProductId { get; set; }
        public int StockQuantity { get; set; }

    }
}
