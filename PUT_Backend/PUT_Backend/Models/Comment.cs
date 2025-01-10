using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PUT_Backend.Models
{

    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("post_id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }


        [BsonElement("user_id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }


        [BsonElement("added_at")]
        public DateTime AddedAt { get; set; }

        [BsonElement("votes")]
        public int Votes { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; }// for reports report -> go to admin -> marked as deleted if needed

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("replied_to")]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string RepliedTo { get; set; }
        
        [BsonElement("edited")]
        public bool Edited{get;set;}
    }
}
