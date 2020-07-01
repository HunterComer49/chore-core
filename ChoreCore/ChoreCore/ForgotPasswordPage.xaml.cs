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

        public ForgotPasswordPage()
        {
            InitializeComponent();
            viewModel = new ForgotPasswordViewModel();
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}