using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ChoreCore.Tests.ViewModel_Tests
{
    [TestClass]
    public class ForgotPasswordVMTests
    {
        [TestMethod]
        public void TestOnResetPasswordSucess()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(true));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);
            navigationMock.Setup(a => a.NavigateToLogin()).Returns(Task.CompletedTask);


            ForgotPasswordViewModel forgotPassVM = new ForgotPasswordViewModel(authMock.Object, navigationMock.Object)
            {
                Email = "test@email.com"
            };

            forgotPassVM.OnResetPassword();

            Assert.IsFalse(forgotPassVM.ErrorVis);
            authMock.Verify(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()), Times.Once);
            navigationMock.Verify(a => a.NavigateToLogin(), Times.Once);
        }

        [TestMethod]
        public void TestOnResetPasswordFailEmpty()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(true));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            ForgotPasswordViewModel forgotPassVM = new ForgotPasswordViewModel(authMock.Object, navigationMock.Object);

            forgotPassVM.OnResetPassword();

            Assert.IsTrue(forgotPassVM.ErrorVis);
            authMock.Verify(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void TestOnResetPasswordFail()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(false));

            Mock<INavigationService> navigationMock = new Mock<INavigationService>(MockBehavior.Strict);

            ForgotPasswordViewModel forgotPassVM = new ForgotPasswordViewModel(authMock.Object, navigationMock.Object)
            {
                Email = "test@email.com"
            };

            forgotPassVM.OnResetPassword();

            Assert.IsTrue(forgotPassVM.ErrorVis);
            authMock.Verify(a => a.SendPasswordResetEmailAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
