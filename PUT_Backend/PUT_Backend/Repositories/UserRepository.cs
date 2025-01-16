using MongoDB.Driver;
using PUT_Backend.Models;

namespace PUT_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:MongoConnectionString").Value);
            var database = client.GetDatabase("put-db");
            _users = database.GetCollection<User>("users");
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
    }
}
