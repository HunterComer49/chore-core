using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ChoreCore.Tests.ViewModel_Tests
{
    [TestClass]
    public class CreateUserVMTests
    {
        [TestMethod]
        public void TestOnSignUpFail()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.SignUpWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("error message"));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);

            CreateUserViewModel viewModel = new CreateUserViewModel(authMock.Object, navigationMock.Object,
                userControllerMock.Object)
            {
                Email = "test@email.com",
                Password = "Passw0rd%",
                ConfirmPassword = "Passw0rd%"
            };

            viewModel.OnSignUp();

            Assert.IsFalse(viewModel.PasswordErrorVis);
            Assert.IsFalse(viewModel.EmailErrorVis);
            Assert.IsFalse(viewModel.PasswordIncorrectErrorVis);
            authMock.Verify(a => a.SignUpWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsTrue(viewModel.CreateUserErrorVis);
            Assert.IsFalse(string.IsNullOrEmpty(viewModel.EmailError));
        }

        [TestMethod]
        public void TestOnSignUpPasswordMismatch()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);

            CreateUserViewModel viewModel = new CreateUserViewModel(authMock.Object, navigationMock.Object,
                userControllerMock.Object)
            {
                Email = "test@email.com",
                Password = "Passw0rd%",
                ConfirmPassword = "does not match"
            };

            viewModel.OnSignUp();

            Assert.IsFalse(viewModel.EmailErrorVis);
            Assert.IsFalse(viewModel.PasswordIncorrectErrorVis);
            authMock.Verify(a => a.SignUpWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsTrue(viewModel.PasswordErrorVis);
        }

        [TestMethod]
        public void TestOnSignUpSuccess()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.SignUpWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult("token"));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToNewUserInfo()).Returns(Task.CompletedTask);

            Mock<IUserController> userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
            userControllerMock.Setup(a => a.CreateNewUser(It.IsAny<string>())).Returns(Task.CompletedTask);

            CreateUserViewModel viewModel = new CreateUserViewModel(authMock.Object, navigationMock.Object,
                userControllerMock.Object)
            {
                Email = "test@email.com",
                Password = "Passw0rd%",
                ConfirmPassword = "Passw0rd%"
            };

            viewModel.OnSignUp();

            Assert.IsFalse(viewModel.PasswordErrorVis);
            Assert.IsFalse(viewModel.EmailErrorVis);
            Assert.IsFalse(viewModel.PasswordIncorrectErrorVis);
            authMock.Verify(a => a.SignUpWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(viewModel.CreateUserErrorVis);
            userControllerMock.Verify(a => a.CreateNewUser(It.IsAny<string>()), Times.Once);
            navigationMock.Verify(a => a.NavigateToNewUserInfo(), Times.Once);
        }
    }
}
