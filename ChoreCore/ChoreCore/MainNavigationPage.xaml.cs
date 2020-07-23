using ChoreCore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage : ContentPage
    {
        private IMainNavigationViewModel viewModel;

        public MainNavigationPage()
        {
            InitializeComponent();

            viewModel = (IMainNavigationViewModel)Splat.Locator.Current.GetService(typeof(IMainNavigationViewModel));

            BindingContext = viewModel;
        }
    }
}