using ChoreCore.Controllers;
using ChoreCore.Models;
using ChoreCore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ChoreCore.Tests.ViewModel_Tests
{
    [TestClass]
    public class LoginVMTests
    {
        [TestMethod]
        public void TestLoginSuccess()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.FromResult("token"));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult(new User()));

            LoginViewModel viewModel = new LoginViewModel(authMock.Object, navigationMock.Object,
                userControllerMock.Object)
            {
                Email = "email@gmail.com",
                Password = "password"
            };

            viewModel.OnLogin();

            authMock.Verify(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            navigationMock.Verify(a => a.NavigateToHomePage(It.IsAny<int>()), Times.Once);
            Assert.IsFalse(viewModel.ErrorVis);
        }

        [TestMethod]
        public void TestLoginFail()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(""));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            LoginViewModel viewModel = new LoginViewModel(authMock.Object, navigationMock.Object)
            {
                Email = "email@gmail.com",
                Password = "password"
            };

            viewModel.OnLogin();

            authMock.Verify(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsTrue(viewModel.ErrorVis);
        }

        [TestMethod]
        public void TestForgotPassword()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToHomePage(It.IsAny<int>())).Returns(Task.CompletedTask);

            LoginViewModel viewModel = new LoginViewModel(authMock.Object, navigationMock.Object)
            {
                Email = "email@gmail.com",
                Password = "password"
            };

            viewModel.OnForgotPassword();

            navigationMock.Verify(a => a.NavigateToForgotPassword(), Times.Once);
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.Email));
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.Password));
        }

        [TestMethod]
        public void TestOnCreateAccount()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToCreateUser()).Returns(Task.CompletedTask);

            LoginViewModel viewModel = new LoginViewModel(authMock.Object, navigationMock.Object)
            {
                Email = "email@gmail.com",
                Password = "password"
            };

            viewModel.OnCreateAccount();

            Assert.IsTrue(string.IsNullOrEmpty(viewModel.Email));
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.Password));
            navigationMock.Verify(a => a.NavigateToCreateUser(), Times.Once);
        }
    }
}
