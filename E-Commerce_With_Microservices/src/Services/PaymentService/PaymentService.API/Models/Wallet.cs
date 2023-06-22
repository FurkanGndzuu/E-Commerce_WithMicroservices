using MongoDB.Bson;

namespace PaymentService.API.Models
{
    public class Wallet
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
