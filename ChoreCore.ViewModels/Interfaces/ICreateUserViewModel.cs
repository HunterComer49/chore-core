using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public interface ICreateUserViewModel
    {
        string ConfirmPassword { get; set; }
        bool CreateUserErrorVis { get; set; }
        string Email { get; set; }
        string EmailError { get; set; }
        bool EmailErrorVis { get; set; }
        string Password { get; set; }
        bool PasswordErrorVis { get; set; }
        bool PasswordIncorrectErrorVis { get; set; }
        ICommand SignUpCommand { get; set; }
    }
}