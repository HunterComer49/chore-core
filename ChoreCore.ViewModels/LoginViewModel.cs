using ChoreCore.Controllers;
using System.Windows.Input;
using Xamarin.Forms;

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

        public LoginViewModel(IAuth authentication = null, INavigationService navigation = null)
        {
            _auth = authentication ?? DependencyService.Get<IAuth>();
            _navigation = navigation ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));

            ErrorVis = false;

            LoginCommand = new RelayCommand(OnLogin);
            ForgotPasswordCommand = new RelayCommand(OnForgotPassword);
            CreateAccountCommand = new RelayCommand(OnCreateAccount);
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
        public ICommand CreateAccountCommand { get; set; }

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
                    await _navigation.NavigateToHomePage();
                }
            }
        }

        public async void OnForgotPassword()
        {
            Email = string.Empty;
            Password = string.Empty;

            await _navigation.NavigateToForgotPassword();
        }

        public async void OnCreateAccount()
        {
            Email = string.Empty;
            Password = string.Empty;

            await _navigation.NavigateToCreateUser();
        }
    }
}