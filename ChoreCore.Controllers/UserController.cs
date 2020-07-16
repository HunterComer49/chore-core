using ChoreCore.Managers;
using ChoreCore.Models;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public class UserController : IUserController
    {
        private IUserManager _userManager;
        private IConstantUserInstance _constantUserInstance;

        public UserController(IUserManager userManager = null, IConstantUserInstance constantUserInstance = null)
        {
            _userManager = userManager ?? (IUserManager)Splat.Locator.Current.GetService(typeof(IUserManager));
            _constantUserInstance = constantUserInstance ?? (IConstantUserInstance)Splat.Locator.Current.GetService(typeof(IConstantUserInstance));
        }

        public async Task<User> CreateNewUser(string email)
        {
            await _userManager.CreateUser(new User()
            {
                Email = email
            });

            User user = await _userManager.GetUser(email);

            _constantUserInstance.SetUser(user);

            return user;
        }

        public async Task<string> UpdateUser(User user, Stream stream = null)
        {
            if (stream != null)
            {
                await _userManager.UploadProfilePic(user.Id, stream);
                _constantUserInstance.SetProfilePic(stream);
            }

            string result = await _userManager.UpdateUser(user);

            if (string.IsNullOrEmpty(result))
            {
                _constantUserInstance.SetUser(user);
            }

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _userManager.GetUser(email);

            _constantUserInstance.SetUser(user);

            Stream profilePic = await _userManager.GetProfilePic(user.Id);

            if (profilePic != null)
            {
                _constantUserInstance.SetProfilePic(profilePic);
            }

            return user;
        }

        public async Task<string> ChangeProfilePicture(string userId, Stream stream)
        {
            string message = await _userManager.UploadProfilePic(userId, stream);

            if (string.IsNullOrEmpty(message))
            {
                _constantUserInstance.SetProfilePic(await _userManager.GetProfilePic(userId));
            }

            return message;
        }
    }
}
