using ChoreCore.Controllers;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel, IForgotPasswordViewModel
    {
        #region Private variables
        private IAuth _auth;
        private INavigationService _navigation;
        private string _email;
        private bool _errorVis;
        #endregion

        public ForgotPasswordViewModel(IAuth auth = null, INavigationService navigation = null)
        {
            _auth = auth ?? DependencyService.Get<IAuth>();
            _navigation = navigation ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));

            ErrorVis = false;

            ResetCommand = new RelayCommand(OnResetPassword);
        }

        #region Public Properties
        public string Email
        {
            get { return _email; }
            set { SetAndRaise(ref _email, value); }
        }
        public bool ErrorVis
        {
            get { return _errorVis; }
            set { SetAndRaise(ref _errorVis, value); }
        }
        #endregion

        public ICommand ResetCommand { get; set; }

        public async void OnResetPassword()
        {
            if (!string.IsNullOrEmpty(Email) && await _auth.SendPasswordResetEmailAsync(Email))
            {
                ErrorVis = false;
                await _navigation.NavigateToLogin();
            }
            else
            {
                ErrorVis = true;
            }
        }
    }
}
