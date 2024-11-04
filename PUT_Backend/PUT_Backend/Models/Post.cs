
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*what is seen in the list*/
namespace PUT_Backend.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")] 
        [Required ]
        public string Title { get; set; }
        [BsonElement("brief")]
        public string Brief { get; set; }
        [BsonElement("votes")]
        public int Votes { get; set; }
        
        [BsonElement("added_at")]
        public DateTime  AddedAt { get; set; }
        
        [BsonElement("categories")]
        [BsonRepresentation(BsonType.String)]
        public List<Category> Categories { get; set; }= new List<Category>();
        
        [BsonElement("anonim")]
        public bool Anonim { get; set; }
        
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}