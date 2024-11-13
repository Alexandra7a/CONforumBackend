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
        public async Task<ActionResult<IEnumerable<ShortPost>>> GetAllShortPosts([FromQuery]Category category=Category.All,[FromQuery] int pageNumber = 1)
        {
            if (pageNumber <= 0) pageNumber = 1;
            
            var all_posts = await _postService.GetAllShortPostsAsync(pageNumber, pageSize,category);
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
/*
Notes:
Cand adaugi o postare sa o pun in db in functie de dat sau categorie mai populara.
O sa vad cum fac cu cele mai populare primele -> poate un filtru cu cele mai populare pe care il alege userul(gen vreau events si sortate dupa cele mai populare. ceva de genul )
*/