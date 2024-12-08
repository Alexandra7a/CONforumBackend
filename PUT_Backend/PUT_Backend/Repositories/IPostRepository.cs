using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
namespace PUT_Backend.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(string id);
        Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize);
        
        Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize,Category category);

        Task<Post> CreatePost(Post newPost);
        
        Task<Post> UpdatePost(Post updatedPost);
        
        Task<bool> DeletePost(string id);




    }
}