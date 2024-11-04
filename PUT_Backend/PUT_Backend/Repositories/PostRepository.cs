using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PUT_Backend.Models;
using PUT_Backend.Repositories;

namespace PUT_Backend.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository(IConfiguration configuration)
        {

            var client = new MongoClient(configuration.GetSection("DatabaseSettings:MongoConnectionString").Value);
            var database = client.GetDatabase("put-db");
            _posts = database.GetCollection<Post>("posts");
            //pot aici sa accesez si whole-posts collection?
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize; 

            var posts = await _posts.Find(_ => true)
                                    .Skip(skip)
                                    .Limit(pageSize)
                                    .ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await _posts.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

    }
}
