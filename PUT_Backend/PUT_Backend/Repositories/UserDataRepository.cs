using MongoDB.Driver;
using PUT_Backend.Models;

namespace PUT_Backend.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IMongoCollection<UserData> _userData;

        public UserDataRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:MongoConnectionString").Value);
            var database = client.GetDatabase("put-db");
            _userData = database.GetCollection<UserData>("user_data");
        }

        public async Task<UserData> GetByUserIdAsync(string userId)
        {
            return await _userData.Find(data => data.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task AddUserDataAsync(UserData userData)
        {
            await _userData.InsertOneAsync(userData);
        }

        public async Task UpdateUserDataAsync(string userId, UserData updatedData)
        {
            await _userData.ReplaceOneAsync(data => data.UserId == userId, updatedData);
        }
    }
}
