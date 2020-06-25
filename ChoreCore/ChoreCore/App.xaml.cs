using ChoreCore.Controllers;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class App : Application
    {
        public App(IAuth auth)
        {
            InitializeComponent();

            MainPage = new LoginPage(auth);
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
