using ChoreCore.Controllers;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public class MainNavigationViewModel : BaseViewModel, IMainNavigationViewModel
    {
        private INavigationService _navigationService;

        public MainNavigationViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));

            AddProjectCommand = new RelayCommand(OnAddProject);
        }

        public ICommand AddProjectCommand { get; set; }

        private async void OnAddProject()
        {
            await _navigationService.NavigateToAddProject();
        }
    }
}
