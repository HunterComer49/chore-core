using ChoreCore.Models;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IUserController
    {
        Task<string> ChangeProfilePicture(string userId, Stream stream);
        Task<User> CreateNewUser(string email);
        Task<User> GetUserByEmail(string email);
        Task<string> UpdateUser(User user, Stream stream = null);
    }
}