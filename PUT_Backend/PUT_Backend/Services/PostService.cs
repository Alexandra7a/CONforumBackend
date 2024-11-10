using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
using PUT_Backend.Repositories;

namespace PUT_Backend
{
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        public PostService(IPostRepository repository)
        {
            this._postRepository = repository;
        }

        public Task<IActionResult> CreatePost(Post post)
        {

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize)
        {
            return await _postRepository.GetAllPostsAsync(pageNumber,pageSize);
        }

        public Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize)
        {
            return _postRepository.GetAllShortPostsAsync(pageNumber,pageSize);
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }
    }
}