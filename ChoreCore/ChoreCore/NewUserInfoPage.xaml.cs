using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserInfoPage : ContentPage
    {
        private readonly INewUserInfoViewModel viewModel;

        public NewUserInfoPage()
        {
            InitializeComponent();

            viewModel = (INewUserInfoViewModel)Locator.Current.GetService(typeof(INewUserInfoViewModel));
            BindingContext = viewModel;
        }
    }
}