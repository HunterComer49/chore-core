using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserPage : ContentPage
    {
        private readonly ICreateUserViewModel viewModel;

        public CreateUserPage()
        {
            InitializeComponent();

            viewModel = (ICreateUserViewModel)Locator.Current.GetService(typeof(ICreateUserViewModel));
            BindingContext = viewModel;
        }
    }
}