
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*all post kept in db */
namespace PUT_Backend.Models
{
    public class Post
    {
        private static int maxBriefLength = 100;

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
        public DateTime AddedAt { get; set; }

        [BsonElement("categories")]
        [BsonRepresentation(BsonType.String)]
        public List<Category> Categories { get; set; } = new List<Category>();

        [BsonElement("anonim")]
        public bool Anonim { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("edited")]
        public bool Edited { get; set; }

        [BsonElement("best_comment_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BestCommentId { get; set; }


        public Post(string title, string content, string userId, List<Category> categories, bool anonim = false)
        {
            Id = "";
            Title = title;
            Content = content;
            UserId = userId;
            AddedAt = DateTime.UtcNow;
            Votes = 0;
            Anonim = anonim;
            Edited = false;
            if (categories != null)
                Categories = categories;
            else
                Categories = new List<Category>();

            Brief = createBrief(content);
        }

        public string createBrief(string content)
        {
            if (content.Length <= maxBriefLength)
                return content;

            int briefLength = (int)(content.Length * 0.1);
            string brief = content.Substring(0, Math.Min(maxBriefLength, briefLength));

            if (!content.Equals(brief))
                brief += "...";

            return brief;
        }
    }
}