﻿using ChoreCore.Controllers;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Sharpnado.Presentation.Forms.iOS;
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
            RegisterServices();

            Forms.Init();

            Xamarin.FormsMaps.Init();

            ImageCircleRenderer.Init();
            SharpnadoInitializer.Initialize();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();

            LoadApplication(new App());

            Firebase.Core.App.Configure();
            return base.FinishedLaunching(app, options);
        }

        private static void RegisterServices()
        {
            DependencyService.Register<IAuth, AuthiOS>();
            DependencyService.Register<IPhotoPickerService, PhotoPickerService>();
        }
    }
}
