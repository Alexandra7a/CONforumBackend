using System.Text.Json;
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


        public async Task<ValidationResult<Post>> CreatePost(CreatePostRequest dto)
        {

            Post post= new Post(dto.Title,dto.Content,dto.UserId,dto.Categories,dto.Anonim);
            var errors = ValidatePost(post);
            if (errors.Any())
                return new ValidationResult<Post> { IsValid = false, Errors = errors };

            var createdPost = await _postRepository.CreatePost(post);

            return new ValidationResult<Post> { IsValid = true, Post = createdPost };
        }

        public async Task<ValidationResult<Post>> UpdatePost(Post updated_post)
        {
        
            var errors = ValidatePost(updated_post);
            if (errors.Any())
                return new ValidationResult<Post> { IsValid = false, Errors = errors };

            updated_post.Edited = true;
            
            var createdPost = await _postRepository.UpdatePost(updated_post);

            return new ValidationResult<Post> { IsValid = true, Post = createdPost };
        }
    
        public async Task<bool> DeletePost(string id)
        {
            return await _postRepository.DeletePost(id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize)
        {
            return await _postRepository.GetAllPostsAsync(pageNumber, pageSize);
        }

        public Task<IEnumerable<ShortPost>> GetAllShortPostsAsync(int pageNumber, int pageSize, Category category)
        {
            return _postRepository.GetAllShortPostsAsync(pageNumber, pageSize, category);
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        private List<string> ValidatePost(Post post)
        {
            var errors = new List<string>();

            /*trebuie sa adaug ca user id e valid*/
            if (post == null)
            {
                errors.Add("Post cannot be null.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(post.Title))
                errors.Add("Title is required.");

            if (post.Categories == null || !post.Categories.Any())
                errors.Add("At least one category must be specified.");

            if (string.IsNullOrWhiteSpace(post.Content) || post.Content.Length < 10)
                errors.Add("Content must be at least 10 characters long.");

            return errors;
        }


        public class ValidationResult<T>
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; } = new List<string>();
            public T Post { get; set; }
        }
    }
}