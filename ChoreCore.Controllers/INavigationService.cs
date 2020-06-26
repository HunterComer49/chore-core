using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface INavigationService
    {
        Task NavigateToLogin();
        Task NavigateToForgotPassword();
        Task NavigateToNewUser();
        Task NavigateToHomePage();
    }
}
