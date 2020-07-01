using ChoreCore.Models;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public interface IUserManager
    {
        Task<string> CreateUser(User user);
        Task<User> GetUser(string email);
        Task<string> UpdateUser(string userId, User user);
    }
}