﻿using ChoreCore.Controllers;
using ChoreCore.Managers;
using ChoreCore.ViewModels;
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

            //Address
            Locator.CurrentMutable.Register(() => new AddressValidation(), typeof(IAddressValidation));

            //User 
            Locator.CurrentMutable.Register(() => new UserValidation(), typeof(IUserValidation));
            Locator.CurrentMutable.Register(() => new UserManager(), typeof(IUserManager));
            Locator.CurrentMutable.Register(() => new UserController(), typeof(IUserController));

            //Project
            Locator.CurrentMutable.Register(() => new ProjectValidation(), typeof(IProjectValidation));
            Locator.CurrentMutable.Register(() => new ProjectManager(), typeof(IProjectManager));
            Locator.CurrentMutable.Register(() => new ProjectController(), typeof(IProjectController));

            //Geopoint 
            Locator.CurrentMutable.Register(() => new GeopointManager(), typeof(IGeopointManager));

            //Singleton
            Locator.CurrentMutable.RegisterLazySingleton(() => new ConstantUserInstance(), typeof(IConstantUserInstance));

            //View Models
            Locator.CurrentMutable.Register(() => new MainNavigationViewModel(), typeof(IMainNavigationViewModel));
            Locator.CurrentMutable.Register(() => new CreateUserViewModel(), typeof(ICreateUserViewModel));
            Locator.CurrentMutable.Register(() => new ProfileViewModel(), typeof(IProfileViewModel));
            Locator.CurrentMutable.Register(() => new ProfileSettingsViewModel(), typeof(IProfileSettingsViewModel));
            Locator.CurrentMutable.Register(() => new ForgotPasswordViewModel(), typeof(IForgotPasswordViewModel));
            Locator.CurrentMutable.Register(() => new LoginViewModel(), typeof(ILoginViewModel));
            Locator.CurrentMutable.Register(() => new NewUserInfoViewModel(), typeof(INewUserInfoViewModel));
            Locator.CurrentMutable.Register(() => new MapViewModel(), typeof(IMapViewModel));
        }
    }
}
