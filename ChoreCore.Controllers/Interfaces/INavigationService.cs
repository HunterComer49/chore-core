using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface INavigationService
    {
        Task NavigateToLogin();
        Task NavigateToForgotPassword();
        Task NavigateToCreateUser();
        Task NavigateToHomePage();
        Task NavigateToNewUserInfo();
    }
}
