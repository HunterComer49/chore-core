using ChoreCore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserInfoPage : ContentPage
    {
        private readonly NewUserInfoViewModel viewModel;

        public NewUserInfoPage()
        {
            InitializeComponent();

            viewModel = new NewUserInfoViewModel();
            BindingContext = viewModel;
        }
    }
}