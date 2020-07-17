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

        public async Task NavigateToHomePage(int index = 0)
        {
            await Application.Current.MainPage.Navigation
                  .PushAsync(new NavigationPage(index));
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

        public async Task NavigateToNewUserInfo()
        {
            await Application.Current.MainPage.Navigation
                  .PushAsync(new NewUserInfoPage());
        }
    }
}
