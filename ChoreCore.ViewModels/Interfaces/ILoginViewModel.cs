using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public interface ILoginViewModel
    {
        ICommand CreateAccountCommand { get; set; }
        string Email { get; set; }
        bool ErrorVis { get; set; }
        ICommand ForgotPasswordCommand { get; set; }
        ICommand LoginCommand { get; }
        string Password { get; set; }

        void OnCreateAccount();
        void OnForgotPassword();
        void OnLogin();
    }
}