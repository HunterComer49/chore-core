using ChoreCore.Controllers;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IAuth _auth;
        private string _email;
        private string _password;

        public LoginViewModel(IAuth authentication)
        {
            _auth = authentication;

            LoginCommand = new RelayCommand(OnLogin);
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; private set; }

        public async void OnLogin()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                string token = await _auth.LoginWithEmailPassword(Email, Password);

                if (token == "")
                {
                    // need to show error
                    // await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again.", "Ok");
                }
            }
        }
    }
}
