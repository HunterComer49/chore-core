using ChoreCore.Controllers;
using ChoreCore.Managers;
using ChoreCore.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public class NewUserInfoViewModel : BaseViewModel, INewUserInfoViewModel
    {
        #region Private Variables
        private User _user;
        private INavigationService _navigation;
        private IUserController _userController;
        private IConstantUserInstance _constantUserInstance;
        private string _errorMessage;
        #endregion

        public NewUserInfoViewModel(INavigationService navigation = null, IUserController userController = null, IConstantUserInstance constantUserInstance = null)
        {
            _navigation = navigation ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));
            _userController = userController ?? (IUserController)Splat.Locator.Current.GetService(typeof(IUserController));
            _constantUserInstance = constantUserInstance ?? (IConstantUserInstance)Splat.Locator.Current.GetService(typeof(IConstantUserInstance));

            InitUser();

            SkipCommand = new RelayCommand(OnSkip);
            SubmitCommand = new RelayCommand(OnSubmit);
        }

        #region Public Properties
        public User User
        {
            get { return _user; }
            set { SetAndRaise(ref _user, value); }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetAndRaise(ref _errorMessage, value); }
        }
        public List<string> States => new List<string>(Enum.GetNames(typeof(States)));
        #endregion

        public ICommand SkipCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        private void InitUser()
        {
            User = _constantUserInstance.GetUser();
        }

        internal async void OnSkip()
        {
            await _navigation.NavigateToHomePage();
        }

        internal async void OnSubmit()
        {
            //Do not call database if they didn't actually fill any of the values out
            if (string.IsNullOrEmpty(User.FirstName) && string.IsNullOrEmpty(User.LastName)
                && string.IsNullOrEmpty(User.PhoneNumber) && string.IsNullOrEmpty(User.Address.Street)
                && string.IsNullOrEmpty(User.Address.City) && string.IsNullOrEmpty(User.Address.State)
                && string.IsNullOrEmpty(User.Address.Zip))
            {
                await _navigation.NavigateToHomePage();
                return;
            }

            string result = await _userController.UpdateUser(User);

            if (!string.IsNullOrEmpty(result))
            {
                ErrorMessage = result;
            }
            else
            {
                await _navigation.NavigateToHomePage();
            }
        }
    }
}
