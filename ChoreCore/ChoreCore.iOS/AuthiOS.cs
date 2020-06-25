using ChoreCore.Controllers;
using ChoreCore.iOS;
using Firebase.Auth;
using Foundation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthiOS))]
namespace ChoreCore.iOS
{
    public class AuthiOS : IAuth
    {
        public bool IsSignIn()
        {
            User user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            AuthDataResult user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return await user.User.GetIdTokenAsync();
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            try
            {
                await Auth.DefaultInstance.SendPasswordResetAsync(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SignOut()
        {
            try
            {
                Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                AuthDataResult user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
