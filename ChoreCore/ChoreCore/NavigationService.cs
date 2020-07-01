using ChoreCore.Controllers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChoreCore
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToForgotPassword()
        {
            await Application.Current.MainPage.Navigation
                 .PushAsync(new ForgotPasswordPage());
        }

        public Task NavigateToHomePage()
        {
            throw new System.NotImplementedException();
        }

        public async Task NavigateToLogin()
        {
            await Application.Current.MainPage.Navigation
                  .PushAsync(new LoginPage());
        }

        public async Task NavigateToCreateUser()
        {
            await Application.Current.MainPage.Navigation
                  .PushAsync(new CreateUserPage());
        }

        public Task NavigateToNewUserInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
