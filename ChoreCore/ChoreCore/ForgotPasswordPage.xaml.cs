using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly IForgotPasswordViewModel viewModel;

        public ForgotPasswordPage()
        {
            InitializeComponent();

            viewModel = (IForgotPasswordViewModel)Locator.Current.GetService(typeof(IForgotPasswordViewModel));
            BindingContext = viewModel;

            logoImage.Source = ImageSource.FromFile("logo.png");
        }
    }
}