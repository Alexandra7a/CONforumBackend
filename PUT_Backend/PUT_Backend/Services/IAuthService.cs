using System.Threading.Tasks;

namespace PUT_Backend.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password);
    }
}
