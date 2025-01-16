using MongoDB.Bson.Serialization.Attributes;

namespace PUT_Backend.Models
{
    public class CreateCommentRequest
    {
        public string PostId { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public string RepliedTo { get; set; }
    }
}
