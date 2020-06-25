using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Xamarin.Forms;

namespace ChoreCore
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(IAuth auth)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(auth);
        }
    }
}