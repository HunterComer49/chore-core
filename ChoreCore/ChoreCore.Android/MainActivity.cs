using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using ChoreCore.Controllers;
using ChoreCore.Android;
using Android;
using ImageCircle.Forms.Plugin.Droid;
using Android.Net;
using System.IO;
using Android.Content;
using System.Threading.Tasks;

namespace ChoreCore.Droid
{
    [Activity(Label = "ChoreCore", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            RegisterServices();

            base.OnCreate(savedInstanceState);

            ImageCircleRenderer.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());

            Instance = this;
        }

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
            }
        }

        #region Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { get; set; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    //Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
        #endregion

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        //{
        //    if (requestCode == RequestLocationId)
        //    {
        //        if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
        //            // Permissions granted - display a message.
        //        else
        //            // Permissions denied - display a message.
        //    }
        //    else
        //    {
        //        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    }
        //}

        private static void RegisterServices()
        {
            DependencyService.Register<IAuth, AuthDroid>();
            DependencyService.Register<IPhotoPickerService, PhotoPickerService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}