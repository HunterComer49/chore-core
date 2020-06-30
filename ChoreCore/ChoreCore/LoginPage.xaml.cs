using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel viewModel;

        public LoginPage(IAuth auth, INavigationService navigation)
        { 
            InitializeComponent();
            viewModel = new LoginViewModel(auth, navigation);
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}