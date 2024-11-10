using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PUT_Backend.Models;

namespace PUT_Backend.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostContoller : ControllerBase
    {
        private readonly IPostService _postService;
    
        private readonly int pageSize = 5;
        public PostContoller(IPostService service)
        {
            this._postService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortPost>>> GetAllShortPosts([FromQuery] int pageNumber = 1)
        {
            if (pageNumber <= 0) pageNumber = 1;
            var all_posts = await _postService.GetAllShortPostsAsync(pageNumber, pageSize);
            if (all_posts.IsNullOrEmpty())
                return NotFound("No more items to show");
            return Ok(all_posts);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(string id){
            Post post = await _postService.GetPostByIdAsync(id);
            if(post==null)
            return NotFound("Post not found");
            return post;
        }

    }

}