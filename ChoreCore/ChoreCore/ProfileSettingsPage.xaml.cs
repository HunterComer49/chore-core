using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileSettingsPage : ContentPage
    {
        private IProfileSettingsViewModel viewModel;

        public ProfileSettingsPage()
        {
            InitializeComponent();

            viewModel = (IProfileSettingsViewModel)Locator.Current.GetService(typeof(IProfileSettingsViewModel));

            BindingContext = viewModel;
        }
    }
}