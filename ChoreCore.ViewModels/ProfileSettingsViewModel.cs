using ChoreCore.Controllers;
using ChoreCore.Managers;
using ChoreCore.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public class ProfileSettingsViewModel : BaseViewModel, IProfileSettingsViewModel
    {
        private IUserController _userController;
        private IConstantUserInstance _constantUserInstance;
        private IPhotoPickerService _photoPickerService;
        private User _user;
        private ImageSource _profilePic;

        public ProfileSettingsViewModel(IUserController userController = null, IConstantUserInstance constantUserInstance = null,
            IPhotoPickerService photoPickerService = null)
        {
            _userController = userController ?? (IUserController)Locator.Current.GetService(typeof(IUserController));
            _constantUserInstance = constantUserInstance ?? (IConstantUserInstance)Locator.Current.GetService(typeof(IConstantUserInstance));
            _photoPickerService = photoPickerService ?? DependencyService.Get<IPhotoPickerService>();

            ChangeProfilePicture = new RelayCommand(OnChangeProfilePic);

            Init();
        }

        #region Public Properties
        public User User
        {
            get { return _user; }
            set
            {
                if(_user != value)
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
        }
        public ImageSource ProfilePic
        {
            get { return _profilePic; }
            set
            {
                if (_profilePic != value)
                {
                    _profilePic = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> States => new List<string>(Enum.GetNames(typeof(States)));
        #endregion

        public ICommand ChangeProfilePicture { get; set; }

        private void Init()
        {
            User = _constantUserInstance.GetUser();

            Stream imageStream = _constantUserInstance.GetProfilePic();

            // if stream is null, use the empty profile picture
            ProfilePic = imageStream != null ? ImageSource.FromStream(() => imageStream) : ImageSource.FromFile("Assets/Images/emptyProfile.png");
        }

        private async void OnChangeProfilePic()
        {
            Stream stream = await _photoPickerService.GetImageStreamAsync();

            if (stream != null)
            {
                string message = await _userController.ChangeProfilePicture(User.Id, stream);

                if (string.IsNullOrEmpty(message))
                {
                    stream = _constantUserInstance.GetProfilePic();

                    ProfilePic = ImageSource.FromStream(() => stream);
                }                
            }
        }
    }
}
