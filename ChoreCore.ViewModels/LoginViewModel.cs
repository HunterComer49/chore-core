using ChoreCore.Controllers;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Private Variables
        private IAuth _auth;
        private INavigationService _navigation;
        private string _email;
        private string _password;
        private bool _errorVis;
        #endregion

        public LoginViewModel(IAuth authentication, INavigationService navigation)
        {
            _auth = authentication;
            _navigation = navigation;

            ErrorVis = false;

            LoginCommand = new RelayCommand(OnLogin);
            ForgotPasswordCommand = new RelayCommand(OnForgotPassword);
        }

        #region Public Properties
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

        public bool ErrorVis
        {
            get { return _errorVis; }
            set
            {
                if (_errorVis != value)
                {
                    _errorVis = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public ICommand LoginCommand { get; private set; }
        public ICommand ForgotPasswordCommand { get; set; }

        public async void OnLogin()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                string token = await _auth.LoginWithEmailPassword(Email, Password);

                if (token == "")
                {
                    ErrorVis = true;
                }
                else
                {
                    ErrorVis = false;
                    // navigate
                    await _navigation.NavigateToHomePage();
                }
            }
        }

        public async void OnForgotPassword()
        {
            Email = "";
            Password = "";

            await _navigation.NavigateToForgotPassword();
        }
    }
}
