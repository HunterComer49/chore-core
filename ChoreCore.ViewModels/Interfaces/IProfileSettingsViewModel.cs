using ChoreCore.Models;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public interface IProfileSettingsViewModel
    {
        User User { get; set; }
        ImageSource ProfilePic { get; set; }
    }
}