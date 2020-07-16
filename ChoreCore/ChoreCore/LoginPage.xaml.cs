using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class LoginPage : ContentPage
    {
        private readonly ILoginViewModel viewModel;

        public LoginPage()
        { 
            InitializeComponent();

            viewModel = (ILoginViewModel)Locator.Current.GetService(typeof(ILoginViewModel));
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}