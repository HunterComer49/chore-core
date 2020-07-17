using ChoreCore.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public interface IProfileViewModel
    {
        string Name { get; }
        ImageSource ProfilePicture { get; set; }
        ICommand SettingsCommand { get; set; }
        User User { get; set; }
    }
}