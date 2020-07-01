using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IUserController
    {
        Task CreateNewUser(string email);
    }
}