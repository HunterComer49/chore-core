using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public interface IMainNavigationViewModel
    {
        ICommand AddProjectCommand { get; set; }
    }
}