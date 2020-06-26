using System.Threading.Tasks;
using ChoreCore.Controllers;
using ChoreCore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChoreCore.UnitTests
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

            LoginViewModel viewModel = new LoginViewModel(authMock.Object);

            viewModel.OnLogin();

            authMock.Verify(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void TestLoginFail()
        {
            Mock<IAuth> authMock = new Mock<IAuth>(MockBehavior.Strict);
            authMock.Setup(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(""));

            LoginViewModel viewModel = new LoginViewModel(authMock.Object);

            viewModel.OnLogin();

            authMock.Verify(a => a.LoginWithEmailPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        }
    }
}
