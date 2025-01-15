using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
using static PUT_Backend.PostService;

namespace PUT_Backend{
    public interface IPostService{
         Task<Post> GetPostByIdAsync(string id);
        Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize);
        
         Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize,Category category);

        Task<ValidationResult<Post>>  CreatePost(CreatePostRequest dto);

        Task<ValidationResult<Post>>  UpdatePost(Post updated_post);
        
        Task<IEnumerable<Post>> GetPostsByUserAsync(string userId, int pageNumber, int pageSize);
        
         Task<bool> DeletePost(string id);


    }
}