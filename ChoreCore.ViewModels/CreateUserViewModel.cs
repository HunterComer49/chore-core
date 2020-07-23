using ChoreCore.Controllers;
using ChoreCore.Models;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System;
using Xamarin.Forms;

[assembly: InternalsVisibleTo("ChoreCore.Tests")]
namespace ChoreCore.ViewModels
{
    public class CreateUserViewModel : BaseViewModel, ICreateUserViewModel
    {
        #region Private Variables
        private IAuth _auth;
        private INavigationService _navigation;
        private IUserController _userController;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private bool _passwordErrorVis;
        private bool _createUserErrorVis;
        private bool _emailErrorVis;
        private bool _passwordIncorrectErrorVis;
        private string _emailError;
        #endregion

        public CreateUserViewModel(IAuth auth = null, INavigationService navigation = null,
            IUserController userController = null)
        {
            _auth = auth ?? DependencyService.Get<IAuth>();
            _navigation = navigation ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));
            _userController = userController ?? (IUserController)Splat.Locator.Current.GetService(typeof(IUserController));

            CreateUserErrorVis = false;
            PasswordErrorVis = false;
            EmailErrorVis = false;
            PasswordIncorrectErrorVis = false;

            SignUpCommand = new RelayCommand(OnSignUp);
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

                    EmailErrorVis = !Regex.IsMatch(_email, RegexValidation.EmailPattern);
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

                    PasswordIncorrectErrorVis = !Regex.IsMatch(_password, RegexValidation.PasswordPattern);
                }
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();

                    PasswordErrorVis = !string.Equals(_confirmPassword, Password);
                }
            }
        }
        public bool PasswordErrorVis
        {
            get { return _passwordErrorVis; }
            set { SetAndRaise(ref _passwordErrorVis, value); }
        }
        public bool CreateUserErrorVis
        {
            get { return _createUserErrorVis; }
            set { SetAndRaise(ref _createUserErrorVis, value); }
        }
        public bool EmailErrorVis
        {
            get { return _emailErrorVis; }
            set { SetAndRaise(ref _emailErrorVis, value); }
        }
        public bool PasswordIncorrectErrorVis
        {
            get { return _passwordIncorrectErrorVis; }
            set { SetAndRaise(ref _passwordIncorrectErrorVis, value); }
        }
        public string EmailError
        {
            get { return _emailError; }
            set { SetAndRaise(ref _emailError, value); }
        }
        #endregion

        public ICommand SignUpCommand { get; set; }

        internal async void OnSignUp()
        {
            // Do not create new if email and password are not valid and if
            // passwords to not match
            if (!EmailErrorVis && !PasswordIncorrectErrorVis && !PasswordErrorVis
                && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    string token = await _auth.SignUpWithEmailPassword(Email, Password);
                    await _userController.CreateNewUser(Email);
                    CreateUserErrorVis = false;
                    await _navigation.NavigateToNewUserInfo();
                }
                catch (Exception e)
                {
                    EmailError = e.Message;
                    CreateUserErrorVis = true;
                }
            }
        }
    }
}
