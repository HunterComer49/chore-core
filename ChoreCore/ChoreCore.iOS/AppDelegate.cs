﻿
using ChoreCore.Controllers;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ChoreCore.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            DependencyService.Register<IAuth, AuthiOS>();
            DependencyService.Register<INavigationService, NavigationService>();

            Forms.Init();
            LoadApplication(new App(DependencyService.Get<IAuth>(), DependencyService.Get<INavigationService>()));

            Firebase.Core.App.Configure();
            return base.FinishedLaunching(app, options);
        }
    }
}
