using ChoreCore.Controllers;
using ChoreCore.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly ForgotPasswordViewModel viewModel;

        public ForgotPasswordPage(IAuth auth, INavigationService navigation)
        {
            InitializeComponent();
            viewModel = new ForgotPasswordViewModel(auth, navigation);
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}