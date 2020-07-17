using ChoreCore.Controllers;
using ChoreCore.Managers;
using ChoreCore.Models;
using ChoreCore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Tests.ViewModel_Tests
{
    [TestClass]
    public class ProfileSettingsVMTests
    {
        [TestMethod]
        public void TestInitNullStream()
        {
            Stream stream = null;

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);

            Mock<IConstantUserInstance> constantUserMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            constantUserMock.Setup(a => a.GetUser()).Returns(new User());
            constantUserMock.Setup(a => a.GetProfilePic()).Returns(stream);

            Mock<IPhotoPickerService> photoPickerMock = new Mock<IPhotoPickerService>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            ProfileSettingsViewModel viewModel = new ProfileSettingsViewModel(userControllerMock.Object,
                constantUserMock.Object, photoPickerMock.Object, navigationMock.Object);

            Assert.IsNotNull(viewModel.User);
            constantUserMock.Verify(a => a.GetUser(), Times.Once);
            constantUserMock.Verify(a => a.GetProfilePic(), Times.Once);
            //Can't test ImageSource.....
        }

        [TestMethod]
        public void TestOnChangeProfilePic()
        {
            Mock<Stream> streamMock = new Mock<Stream>(MockBehavior.Strict);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.ChangeProfilePicture(It.IsAny<string>(), It.IsAny<Stream>()))
                .Returns(Task.FromResult(""));

            Mock<IConstantUserInstance> constantUserMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            constantUserMock.Setup(a => a.GetUser()).Returns(new User());
            constantUserMock.Setup(a => a.GetProfilePic()).Returns(streamMock.Object);

            Mock<IPhotoPickerService> photoPickerMock = new Mock<IPhotoPickerService>(MockBehavior.Strict);
            photoPickerMock.Setup(a => a.GetImageStreamAsync()).Returns(Task.FromResult(streamMock.Object));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            ProfileSettingsViewModel viewModel = new ProfileSettingsViewModel(userControllerMock.Object,
                constantUserMock.Object, photoPickerMock.Object, navigationMock.Object);

            viewModel.OnChangeProfilePic();

            photoPickerMock.Verify(a => a.GetImageStreamAsync(), Times.Once);
            userControllerMock.Verify(a => a.ChangeProfilePicture(It.IsAny<string>(), It.IsAny<Stream>()), Times.Once);
            //Once from Init function and one in change profile pic function
            constantUserMock.Verify(a => a.GetProfilePic(), Times.Exactly(2));
        }

        [TestMethod]
        public void TestOnCancel()
        {
            Stream stream = null;

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);

            Mock<IConstantUserInstance> constantUserMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            constantUserMock.Setup(a => a.GetUser()).Returns(new User());
            constantUserMock.Setup(a => a.GetProfilePic()).Returns(stream);

            Mock<IPhotoPickerService> photoPickerMock = new Mock<IPhotoPickerService>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(Constants.ProfileIndex)).Returns(Task.CompletedTask);

            ProfileSettingsViewModel viewModel = new ProfileSettingsViewModel(userControllerMock.Object,
                constantUserMock.Object, photoPickerMock.Object, navigationMock.Object);

            viewModel.OnCancel();

            navigationMock.Verify(a => a.NavigateToHomePage(Constants.ProfileIndex), Times.Once);
        }

        [TestMethod]
        public void TestOnSubmitSuccess()
        {
            Stream stream = null;

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.UpdateUser(It.IsAny<User>(), null)).Returns(Task.FromResult(""));

            Mock<IConstantUserInstance> constantUserMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            constantUserMock.Setup(a => a.GetUser()).Returns(new User());
            constantUserMock.Setup(a => a.GetProfilePic()).Returns(stream);

            Mock<IPhotoPickerService> photoPickerMock = new Mock<IPhotoPickerService>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(Constants.ProfileIndex)).Returns(Task.CompletedTask);

            ProfileSettingsViewModel viewModel = new ProfileSettingsViewModel(userControllerMock.Object,
                constantUserMock.Object, photoPickerMock.Object, navigationMock.Object);

            viewModel.OnSubmit();

            userControllerMock.Verify(a => a.UpdateUser(It.IsAny<User>(), null), Times.Once);
            navigationMock.Verify(a => a.NavigateToHomePage(Constants.ProfileIndex), Times.Once);
        }


        [TestMethod]
        public void TestOnSubmitFail()
        {
            Stream stream = null;

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.UpdateUser(It.IsAny<User>(), null)).Returns(Task.FromResult("ERROR"));

            Mock<IConstantUserInstance> constantUserMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            constantUserMock.Setup(a => a.GetUser()).Returns(new User());
            constantUserMock.Setup(a => a.GetProfilePic()).Returns(stream);

            Mock<IPhotoPickerService> photoPickerMock = new Mock<IPhotoPickerService>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            ProfileSettingsViewModel viewModel = new ProfileSettingsViewModel(userControllerMock.Object,
                constantUserMock.Object, photoPickerMock.Object, navigationMock.Object);

            viewModel.OnSubmit();

            userControllerMock.Verify(a => a.UpdateUser(It.IsAny<User>(), null), Times.Once);
            Assert.AreEqual(viewModel.ErrorMessage, "ERROR");
            navigationMock.Verify(a => a.NavigateToHomePage(Constants.ProfileIndex), Times.Never);
        }
    }
}
