using ChoreCore.Models;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IUserController
    {
        Task<User> CreateNewUser(string email);
        Task<string> UpdateUser(User user, Stream stream = null);
    }
}