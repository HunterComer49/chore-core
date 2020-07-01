using System;
using System.Threading.Tasks;
using ChoreCore.Android;
using ChoreCore.Controllers;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace ChoreCore.Android
{
    public class AuthDroid : IAuth
    {
        public bool IsSignIn()
        {
            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                IAuthResult user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                GetTokenResult token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
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
                FirebaseAuth.Instance.SignOut();
                return true;
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
                IAuthResult user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                GetTokenResult token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}