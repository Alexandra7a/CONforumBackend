using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PUT_Backend.Models
{
    public class UserData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("liked_posts_ids")]
        public List<string> LikedPostsIds { get; set; } = new List<string>();

        [BsonElement("posts_ids")]
        public List<string> PostsIds { get; set; } = new List<string>();

        [BsonElement("comments_ids")]
        public List<string> CommentsIds { get; set; } = new List<string>();

        [BsonElement("stinks_nr")]
        public int StinksNr { get; set; }
    }
}
