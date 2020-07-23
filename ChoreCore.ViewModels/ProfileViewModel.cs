using ChoreCore.Controllers;
using ChoreCore.Managers;
using ChoreCore.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public class ProfileViewModel : BaseViewModel, IProfileViewModel
    {
        private IConstantUserInstance _constantUserInstance;
        private INavigationService _navigationService;
        private User _user;
        private ImageSource _profilePicture;
        private int _tabIndex;

        public ProfileViewModel(IConstantUserInstance constantUserInstance = null, INavigationService navigationService = null)
        {
            _constantUserInstance = constantUserInstance ?? (IConstantUserInstance)Splat.Locator.Current.GetService(typeof(IConstantUserInstance));
            _navigationService = navigationService ?? (INavigationService)Splat.Locator.Current.GetService(typeof(INavigationService));

            Init();

            SettingsCommand = new RelayCommand(OnSettings);
        }

        #region Public Properties
        public User User
        {
            get { return _user; }
            set { SetAndRaise(ref _user, value); }
        }
        public ImageSource ProfilePicture
        {
            get { return _profilePicture; }
            set { SetAndRaise(ref _profilePicture, value); }
        }
        public string Name
        {
            get { return ($"{User.FirstName} {User.LastName}").ToString(); }
        }
        public int TabIndex
        {
            get { return _tabIndex; }
            set { SetAndRaise(ref _tabIndex, value); }
        }
        #endregion

        public ICommand SettingsCommand { get; set; }

        private void Init()
        {
            TabIndex = Constants.ProfilePastProjectsIndex;

            User = _constantUserInstance.GetUser();

            byte[] imageByte = _constantUserInstance.GetProfilePic();

            // if stream is null, use the empty profile picture
            ProfilePicture = imageByte != null ? ByteToImage(imageByte) : ImageSource.FromFile("Assets/Images/emptyProfile.png");
        }

        internal async void OnSettings()
        {
            await _navigationService.NavigateToProfileSettings();
        }
    }
}
