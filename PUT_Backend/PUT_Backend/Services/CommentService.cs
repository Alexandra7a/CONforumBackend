using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using PUT_Backend.Models;
using PUT_Backend.Repositories;

namespace PUT_Backend{
    public class CommentService : ICommentService
    {

         ICommentRepository _commentRepository;
        public CommentService(ICommentRepository repository)
        {
            this._commentRepository = repository;
        }

        public async  Task<ValidationResult<Comment>> CreateComment(String id,Comment newComm)
        {
           var errors = ValidateComment(newComm);
            if (errors.Any())
                return new ValidationResult<Comment> { IsValid = false, Errors = errors };

            newComm.PostId=id;
            newComm.Votes=0;
            newComm.AddedAt=DateTime.UtcNow;
            newComm.Edited=false;
            newComm.IsDeleted=false;

            var created = await _commentRepository.CreateComment(newComm);

            return new ValidationResult<Comment> { IsValid = true, Entity = created };
        }

        public async Task<bool> DeletePost(string id_comm)
        {
            return await _commentRepository.DeleteComment(id_comm);
        }

        public async Task<Comment> findComment(string id_comm)
        {
            return await _commentRepository.GetComment(id_comm);
        }

        public async Task<IEnumerable<Comment>> GetComments(string id, string parent_id)
        {
           return await _commentRepository.GetCommentsOrRepliesAsync(id,parent_id);
        }

        public async Task<ValidationResult<Comment>> UpdateComment(Comment updateComm)
        {
            var errors = ValidateComment(updateComm);
            if (errors.Any())
                return new ValidationResult<Comment> { IsValid = false, Errors = errors };

            var created = await _commentRepository.UpdateComment(updateComm);

            return new ValidationResult<Comment> { IsValid = true, Entity = created };
        }

        public async Task<Comment> GetCommentByIdAsync(string id)
        {
            // Ensure that the comment exists before returning
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {id} not found.");
            }
            return comment;
        }

         private List<string> ValidateComment(Comment comm)
        {
            var errors = new List<string>();

            if (comm == null)
            {
                errors.Add("comm cannot be null.");
                return errors;
            }
            return errors;
        }

          public class ValidationResult<T>
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; } = new List<string>();
            public T Entity { get; set; }
        }
    }
}

