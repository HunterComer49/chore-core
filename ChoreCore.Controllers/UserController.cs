using ChoreCore.Managers;
using ChoreCore.Models;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public class UserController : IUserController
    {
        private IUserManager _userManager;

        public UserController(IUserManager userManager = null)
        {
            _userManager = userManager ?? (IUserManager)Splat.Locator.Current.GetService(typeof(IUserManager));
        }

        public async Task CreateNewUser(string email)
        {
            await _userManager.CreateUser(new User()
            {
                Email = email
            });
        }
    }
}
