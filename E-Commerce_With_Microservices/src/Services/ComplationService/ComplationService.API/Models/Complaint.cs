using MongoDB.Bson;

namespace ComplationService.API.Models
{
    public class Complaint
    {
        [MongoDB.Bson.Serialization.Attributes.BsonIdAttribute]
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string ComplaintDescription { get; set; }
    }
}
