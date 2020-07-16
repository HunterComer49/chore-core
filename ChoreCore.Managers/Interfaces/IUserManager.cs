using ChoreCore.Models;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public interface IUserManager
    {
        Task<string> CreateUser(User user);
        Task<Stream> GetProfilePic(string userId);
        Task<User> GetUser(string email);
        Task<string> UpdateUser(User user);
        Task<string> UploadProfilePic(string id, Stream stream);
    }
}