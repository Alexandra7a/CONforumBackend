using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
namespace PUT_Backend.Repositories
{
    public interface ICommentRepository
    {


        Task<IEnumerable<Comment>> GetCommentsOrRepliesAsync(string id,string parent_id);
        
        Task<Comment> CreateComment(Comment newComment);
        
        Task<Comment> UpdateComment(Comment updateComment);
        
        Task<bool> DeleteComment(string id);

        Task<Comment> GetComment(string id_comm);

    }
}
