using ChoreCore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserPage : ContentPage
    {
        private readonly CreateUserViewModel viewModel;

        public CreateUserPage()
        {
            InitializeComponent();

            viewModel = new CreateUserViewModel();
            BindingContext = viewModel;
        }
    }
}