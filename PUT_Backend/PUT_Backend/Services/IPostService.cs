using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;

namespace PUT_Backend{
    public interface IPostService{
         Task<Post> GetPostByIdAsync(string id);
        Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize);
        
         Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize);

        Task<IActionResult> CreatePost(Post post);
    }
}