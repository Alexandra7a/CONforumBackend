using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
using static PUT_Backend.PostService;

namespace PUT_Backend{
    public interface IPostService{
         Task<Post> GetPostByIdAsync(string id);
        Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize);
        
         Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize,Category category);

        Task<ValidationResult<Post>>  CreatePost(Post post);

        Task<ValidationResult<Post>>  UpdatePost(Post updated_post);
        
         Task<bool> DeletePost(string id);


    }
}