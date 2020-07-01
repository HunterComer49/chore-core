using ChoreCore.Controllers;
using ChoreCore.Managers;

namespace ChoreCore
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            Splat.Locator.CurrentMutable.Register(() => new NavigationService(), typeof(INavigationService));

            //User registers
            Splat.Locator.CurrentMutable.Register(() => new UserValidation(), typeof(IUserValidation));
            Splat.Locator.CurrentMutable.Register(() => new UserManager(), typeof(IUserManager));
            Splat.Locator.CurrentMutable.Register(() => new UserController(), typeof(IUserController));
        }
    }
}
