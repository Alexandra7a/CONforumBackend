using System.ComponentModel;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PUT_Backend.Models;

namespace PUT_Backend.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;


        private readonly int pageSize = 5;
        public PostController(IPostService service, ICommentService commentService)
        {
            this._postService = service;
            this._commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortPost>>> GetAllShortPosts([FromQuery] Category category = Category.All, [FromQuery] int pageNumber = 1)
        {
            if (pageNumber <= 0) pageNumber = 1;

            var all_posts = await _postService.GetAllShortPostsAsync(pageNumber, pageSize, category);
            if (all_posts.IsNullOrEmpty())
                return NotFound("No more items to show");
            return Ok(all_posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(string id)
        {
            Post post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post not found");
            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post newPost)
        {
            var result = await _postService.CreatePost(newPost);
            if (!result.IsValid)
                return BadRequest(result.Errors);
            else
                return Ok(newPost);
        }

        [HttpPut]
        public async Task<ActionResult<Post>> UpdatePost([FromBody] Post updatePost)
        {

            if (updatePost == null)
                return NotFound("Post not found.");

            var result = await _postService.UpdatePost(updatePost);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return Ok(result.Post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(string id)
        {
            var success = await _postService.DeletePost(id);
            if (!success)
                return NotFound("Post not found or could not be deleted.");

            return Ok();
        }

        //[{operation,path_to_value, new_value},[patch2],[patch3],...]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PathPost(string id, [FromBody] JsonPatchDocument<Post> patchUpdate)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return BadRequest("id post not found");
            }

            patchUpdate.ApplyTo(post);

            var result = await _postService.UpdatePost(post);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Post updated");
        }



        //COMMENTS

        //ia comentariile de la postare sau ia reply-urile 
        [HttpGet("{id}/comments")]
        public async Task<ActionResult> GetComments(string id, [FromQuery] string parent_id = "")
        {
            var all_comments = await _commentService.GetComments(id, parent_id);
            foreach (var comm in all_comments)
            {
                Console.WriteLine(comm);
            }
            return Ok(all_comments);
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<Post>> CreateComment(string id, [FromBody] Comment newComm)
        {
            var result = await _commentService.CreateComment(id, newComm);
            if (!result.IsValid)
                return BadRequest(result.Errors);
            else
                return Ok(newComm);
        }


        /*[HttpPut("{id}/comments")]
        public async Task<ActionResult<Post>> UpdateComment(string id, [FromBody] Comment updateComm)
        {

            //putem lasa cu put sau sa vad cu patch -> content
            try
            {
                if (UpdateComment == null)
                    return NotFound("Comment not found.");

                var result = await _commentService.UpdateComment(updateComm);

                if (!result.IsValid)
                    return BadRequest(result.Errors);

                return Ok(result.Entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        [HttpPatch("{id}/comments/{id_comm}")]
        public async Task<ActionResult> PathComment(string id, string id_comm,JsonPatchDocument<Comment> commentPatch)
        {
            //patch only with id_comm -> votes
            if(id_comm.IsNullOrEmpty()){
                return BadRequest("id_comm cannot be empty");
            }
            var comment = await _commentService.findComment(id_comm);
            if(comment==null)
            return BadRequest("comment not found");
            commentPatch.ApplyTo(comment);
            
            var result = await _commentService.UpdateComment(comment);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Comment updated");

        }


        [HttpDelete("{id}/comments/{id_comm}")]
        public async Task<ActionResult> DeleteComment(string id, string id_comm)
        {
            var success = await _commentService.DeletePost(id_comm);
            if (!success)
                return NotFound("Post not found or could not be deleted.");

            return Ok();
        }


    }

}

/*
Notes:
Cand adaugi o postare sa o pun in db in functie de dat sau categorie mai populara.
O sa vad cum fac cu cele mai populare primele -> poate un filtru cu cele mai populare pe care il alege userul(gen vreau events si sortate dupa cele mai populare. ceva de genul )
*/