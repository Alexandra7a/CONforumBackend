using System.Collections;
using System.ComponentModel.DataAnnotations;
using PUT_Backend.Models;
using static PUT_Backend.CommentService;

namespace PUT_Backend{
    public interface ICommentService
    {
         Task<ValidationResult<Comment>>  CreateComment(String id,CreateCommentRequest newComm);
        Task<bool> DeletePost(string id_comm);
        Task<IEnumerable<Comment>> GetComments(string id, string parent_id);
        Task<ValidationResult<Comment>> UpdateComment(Comment updateComm);
        Task<Comment> GetCommentByIdAsync(string id);
        Task<Comment> findComment(string id_comm);
    }
}