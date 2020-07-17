using ChoreCore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentView
    {
        private IProfileViewModel viewModel;

        public ProfilePage()
        {
            InitializeComponent();

            viewModel = (IProfileViewModel)Splat.Locator.Current.GetService(typeof(IProfileViewModel));
            BindingContext = viewModel;
        }
    }
}