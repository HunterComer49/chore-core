using ChoreCore.Controllers;
using ChoreCore.Managers;
using Splat;

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
            Locator.CurrentMutable.Register(() => new NavigationService(), typeof(INavigationService));

            //User registers
            Locator.CurrentMutable.Register(() => new UserValidation(), typeof(IUserValidation));
            Locator.CurrentMutable.Register(() => new UserManager(), typeof(IUserManager));
            Locator.CurrentMutable.Register(() => new UserController(), typeof(IUserController));

            //Geopoint 
            Locator.CurrentMutable.Register(() => new GeopointManager(), typeof(IGeopointManager));

            //Singleton
            Locator.CurrentMutable.RegisterLazySingleton(() => new ConstantUserInstance(), typeof(IConstantUserInstance));
        }
    }
}
