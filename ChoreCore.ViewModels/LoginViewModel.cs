﻿using ChoreCore.Controllers;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        #region Private Variables
        private IAuth _auth;
        private INavigationService _navigation;
        private IUserController _userController;
        private string _email;
        private string _password;
        private bool _errorVis;
        #endregion

        public LoginViewModel(IAuth authentication = null, INavigationService navigation = null, 
            IUserController userController = null)
        {
            _auth = authentication ?? DependencyService.Get<IAuth>();
            _navigation = navigation ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));
            _userController = userController ?? (IUserController)Splat.Locator.Current.GetService(typeof(IUserController));

            ErrorVis = false;
            Email = "aadmproductions@gmail.com";
            Password = "Passw0rd1#";

            LoginCommand = new RelayCommand(OnLogin);
            ForgotPasswordCommand = new RelayCommand(OnForgotPassword);
            CreateAccountCommand = new RelayCommand(OnCreateAccount);
        }

        #region Public Properties
        public string Email
        {
            get { return _email; }
            set { SetAndRaise(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetAndRaise(ref _password, value); }
        }
        public bool ErrorVis
        {
            get { return _errorVis; }
            set { SetAndRaise(ref _errorVis, value); }
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

                    await _userController.GetUserByEmail(Email);

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