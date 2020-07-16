using ChoreCore.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public interface INewUserInfoViewModel
    {
        string ErrorMessage { get; set; }
        ICommand SkipCommand { get; set; }
        List<string> States { get; }
        ICommand SubmitCommand { get; set; }
        User User { get; set; }
    }
}