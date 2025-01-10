using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PUT_Backend.Models;
using MongoDB.Driver.Linq;
using MongoDB.Bson;


namespace PUT_Backend.Repositories
{

    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:MongoConnectionString").Value);
            var database = client.GetDatabase("put-db");
            _comments = database.GetCollection<Comment>("comments");
        }

        /*Fetch the post comment if parent_id empty and the reply of a comment if parent_id exists*/
        public async Task<IEnumerable<Comment>> GetCommentsOrRepliesAsync(string id, string parent_id)
        {
            var filterBuilder = Builders<Comment>.Filter;
            var filter = filterBuilder.Empty;

            if (string.IsNullOrEmpty(parent_id))
            {
                Console.WriteLine("Fetching top-level comments for post");
                // Fetch toplevel comments for the post
                filter = filterBuilder.And(
                    filterBuilder.Eq(comment => comment.PostId, id),
                     filterBuilder.Eq(comment => comment.RepliedTo, string.Empty),
                    filterBuilder.Eq(comment => comment.IsDeleted, false)
                );
            }
            else
            {
                Console.WriteLine("Fetching replies to parent comment");
                // Fetch replies for a specific parent comment
                filter = filterBuilder.And(
                    filterBuilder.Eq(comment => comment.PostId, id),
                    filterBuilder.Eq(comment => comment.RepliedTo, parent_id),
                    filterBuilder.Eq(comment => comment.IsDeleted, false)
                );
            }

            var l = _comments
               .Find(filter)
               .SortBy(comment => comment.AddedAt)
               .ToListAsync();

            return await l;
        }

        public async Task<Comment> CreateComment(Comment newComment)
        {
            newComment.Id = "";
            await _comments.InsertOneAsync(newComment);
            return newComment;
        }

        public async Task<Comment> UpdateComment(Comment updatedComment)
        {
            var comment = await _comments.Find(c => c.Id == updatedComment.Id && !c.IsDeleted).FirstOrDefaultAsync();
            if (comment == null) throw new Exception("Comment not found or already deleted.");


            //de dat enable mai in colo
           // var timeSincePosting = DateTime.UtcNow - comment.AddedAt;
            //if (timeSincePosting.TotalMinutes > 200)//DE SCHIMBAT CU 15
              //  throw new Exception("Comment can only be updated within the first 15 minutes of posting.");

            var filter = Builders<Comment>.Filter.Eq(c => c.Id, updatedComment.Id);

            var update = Builders<Comment>.Update
                .Set(c => c.Content, updatedComment.Content)
                .Set(c => c.Edited, true); 

            var result = await _comments.UpdateOneAsync(filter, update);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("Failed to update the comment.");
            }
            return updatedComment;
        }


        public async Task<bool> DeleteComment(string id)
        {
            var comment = await _comments.Find(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new Exception("Comment not found or already deleted.");
            }

            var filter = Builders<Comment>.Filter.Eq(c => c.Id, id);
            var update = Builders<Comment>.Update.Set(c => c.IsDeleted, true);

            var result = await _comments.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<Comment> GetComment(string id_comm)
        {
            return await _comments.Find(i => i.Id == id_comm).FirstOrDefaultAsync();
        }
    }
}



 /*public async Task<Comment> UpdateComment(Comment updateComment)
        {
            //make the comment editable only in the first 15 minutes
            // Check if the comment is within the first 15 minutes of posting
            var comment = await _comments.Find(c => c.Id == updateComment.Id && !c.IsDeleted).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new Exception("Comment not found or already deleted.");
            }

            var timeSincePosting = DateTime.UtcNow - comment.AddedAt;
            if (timeSincePosting.TotalMinutes > 2)//DE SCHIMBAT CU 15
            {
                throw new Exception("Comment can only be updated within the first 15 minutes of posting.");
            }

            var filter = Builders<Comment>.Filter.Eq(c => c.Id, updateComment.Id);
            var update = Builders<Comment>.Update
                .Set(c => c.Content, updateComment.Content)
                .Set(c => c.Edited, true);
            var result = await _comments.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Failed to update the comment.");
            }

            return await _comments.Find(c => c.Id == updateComment.Id).FirstOrDefaultAsync();
        }*/
