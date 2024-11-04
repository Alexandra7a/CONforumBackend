using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PUT_Backend{
    class PostData
    {
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get;set;}

        [BsonElement("id_post")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdPos{get;set;}

        [BsonElement("content")]
        public string Content{get; set;}

        [BsonElement("edited")]
        public bool Edited{get;set;}

        [BsonElement("best_comment_id")]
        public string BestCommentId{get;set;}


    }

}