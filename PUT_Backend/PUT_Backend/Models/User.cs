using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PUT_Backend.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password_hash")]
        public string PasswordHash { get; set; }

        [BsonElement("is_admin")]
        public bool IsAdmin { get; set; }

        [BsonElement("banned")]
        public bool Banned { get; set; }
    }
}
