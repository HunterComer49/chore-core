using ChoreCore.Controllers;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            new AppBootstrapper();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
