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
                 .PushAsync(new ForgotPasswordPage(DependencyService.Get<IAuth>(),
                                DependencyService.Get<INavigationService>()));
        }

        public Task NavigateToHomePage()
        {
            throw new System.NotImplementedException();
        }

        public async Task NavigateToLogin()
        {
            await Application.Current.MainPage.Navigation
                  .PushAsync(new LoginPage(DependencyService.Get<IAuth>(), 
                                 DependencyService.Get<INavigationService>()));
        }

        public Task NavigateToNewUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
