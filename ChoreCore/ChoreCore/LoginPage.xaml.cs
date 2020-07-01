using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel viewModel;

        public LoginPage()
        { 
            InitializeComponent();
            viewModel = new LoginViewModel();
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}