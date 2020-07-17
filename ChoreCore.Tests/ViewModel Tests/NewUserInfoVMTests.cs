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
    public class NewUserInfoVMTests
    {
        [TestMethod]
        public void TestOnSkip()
        {
            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);

            Mock<IConstantUserInstance> userInstanceMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            userInstanceMock.Setup(a => a.GetUser()).Returns(new User() { Email = "email@gmail.com" });

            NewUserInfoViewModel viewModel = new NewUserInfoViewModel(navigationMock.Object, userControllerMock.Object,
                userInstanceMock.Object);

            viewModel.OnSkip();

            navigationMock.Verify(a => a.NavigateToHomePage(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestOnSubmitNoChanges()
        {
            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>())).Returns(Task.FromResult(""));

            Mock<IConstantUserInstance> userInstanceMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            userInstanceMock.Setup(a => a.GetUser()).Returns(new User() { Email = "email@gmail.com" });

            NewUserInfoViewModel viewModel = new NewUserInfoViewModel(navigationMock.Object, userControllerMock.Object,
                userInstanceMock.Object);

            viewModel.OnSubmit();

            navigationMock.Verify(a => a.NavigateToHomePage(It.IsAny<int>()), Times.Once);
            userControllerMock.Verify(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>()), Times.Never);
        }

        [TestMethod]
        public void TestOnSubmitSuccess()
        {
            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>())).Returns(Task.FromResult(""));

            Mock<IConstantUserInstance> userInstanceMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            userInstanceMock.Setup(a => a.GetUser()).Returns(new User() { Email = "email@gmail.com" });

            NewUserInfoViewModel viewModel = new NewUserInfoViewModel(navigationMock.Object, userControllerMock.Object,
                userInstanceMock.Object)
            {
                User = new User()
                {
                    FirstName = "Hunter",
                    LastName = "Scanlan"
                }
            };

            viewModel.OnSubmit();

            userControllerMock.Verify(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>()), Times.Once);
            navigationMock.Verify(a => a.NavigateToHomePage(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestOnSubmitFail()
        {
            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>())).Returns(Task.FromResult("Failed"));

            Mock<IConstantUserInstance> userInstanceMock = new Mock<IConstantUserInstance>(MockBehavior.Strict);
            userInstanceMock.Setup(a => a.GetUser()).Returns(new User() { Email = "email@gmail.com" });

            NewUserInfoViewModel viewModel = new NewUserInfoViewModel(navigationMock.Object, userControllerMock.Object,
                userInstanceMock.Object)
            {
                User = new User()
                {
                    FirstName = "Hunter",
                    LastName = "Scanlan"
                }
            };

            viewModel.OnSubmit();

            userControllerMock.Verify(a => a.UpdateUser(It.IsAny<User>(), It.IsAny<Stream>()), Times.Once);
            Assert.AreEqual("Failed", viewModel.ErrorMessage);
            navigationMock.Verify(a => a.NavigateToHomePage(It.IsAny<int>()), Times.Never);
        }
    }
}
