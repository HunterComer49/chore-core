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
        #region Private variables
        private IUserController _userController;
        private IConstantUserInstance _constantUserInstance;
        private IPhotoPickerService _photoPickerService;
        private INavigationService _navigationService;
        private User _user;
        private ImageSource _profilePic;
        private string _errorMessage;
        #endregion

        public ProfileSettingsViewModel(IUserController userController = null, IConstantUserInstance constantUserInstance = null,
            IPhotoPickerService photoPickerService = null, INavigationService navigationService = null)
        {
            _userController = userController ?? (IUserController)Locator.Current.GetService(typeof(IUserController));
            _constantUserInstance = constantUserInstance ?? (IConstantUserInstance)Locator.Current.GetService(typeof(IConstantUserInstance));
            _photoPickerService = photoPickerService ?? DependencyService.Get<IPhotoPickerService>();
            _navigationService = navigationService ?? (INavigationService)Locator.Current.GetService(typeof(INavigationService));

            ChangeProfilePicture = new RelayCommand(OnChangeProfilePic);
            CancelCommand = new RelayCommand(OnCancel);
            SubmitCommand = new RelayCommand(OnSubmit);

            Init();
        }

        #region Public Properties
        public User User
        {
            get { return _user; }
            set { SetAndRaise(ref _user, value); }
        }
        public ImageSource ProfilePic
        {
            get { return _profilePic; }
            set { SetAndRaise(ref _profilePic, value); }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetAndRaise(ref _errorMessage, value); }
        }
        public List<string> States => new List<string>(Enum.GetNames(typeof(States)));
        #endregion

        public ICommand ChangeProfilePicture { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        private void Init()
        {
            User = _constantUserInstance.GetUser();

            byte[] imageByte = _constantUserInstance.GetProfilePic();

            // if stream is null, use the empty profile picture
            ProfilePic = imageByte != null ? ByteToImage(imageByte) : ImageSource.FromFile("Assets/Images/emptyProfile.png");
        }

        internal async void OnChangeProfilePic()
        {
            Stream stream = await _photoPickerService.GetImageStreamAsync();

            if (stream != null)
            {
                string message = await _userController.ChangeProfilePicture(User.Id, stream);

                if (string.IsNullOrEmpty(message))
                {
                    ProfilePic = ByteToImage(_constantUserInstance.GetProfilePic());
                }
            }
        }

        internal async void OnCancel()
        {
            //Navigate back to profile page
            await _navigationService.NavigateToHomePage(Constants.ProfileIndex);
        }

        internal async void OnSubmit()
        {
            string result = await _userController.UpdateUser(User);

            if (!string.IsNullOrEmpty(result))
            {
                ErrorMessage = result;
            }
            else
            {
                //Navigate back to profile page
                await _navigationService.NavigateToHomePage(Constants.ProfileIndex);
            }
        }
    }
}
