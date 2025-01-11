
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*all post kept in db */
namespace PUT_Backend.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")] 
        
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

         [BsonElement("content")]
        public string Content{get; set;}

        [BsonElement("edited")]
        public bool Edited{get;set;}

        [BsonElement("best_comment_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BestCommentId{get;set;}
    }
}