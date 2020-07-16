using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public interface IForgotPasswordViewModel
    {
        string Email { get; set; }
        bool ErrorVis { get; set; }
        ICommand ResetCommand { get; set; }

        void OnResetPassword();
    }
}