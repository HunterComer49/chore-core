using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationPage : Xamarin.Forms.TabbedPage
    {
        public NavigationPage(int index)
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            CurrentPage = Children[index];
        }
    }
}