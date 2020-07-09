using ChoreCore.Managers;
using ChoreCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plugin.CloudFirestore;
using Plugin.FirebaseStorage;
using System.Threading.Tasks;

namespace ChoreCore.Tests.Manager_Tests
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public async Task TestCreateUserSuccess()
        {
            Mock<IUserValidation> userValidationMock = new Mock<IUserValidation>(MockBehavior.Strict);
            userValidationMock.Setup(a => a.ValidateUser(It.IsAny<User>())).Returns("");

            Mock<ICollectionReference> collectionRefMock = new Mock<ICollectionReference>(MockBehavior.Strict);
            collectionRefMock.Setup(a => a.AddDocumentAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            Mock<IStorageReference> storageRefMock = new Mock<IStorageReference>(MockBehavior.Strict);

            UserManager userManager = new UserManager(userValidationMock.Object, collectionRefMock.Object,
                storageRefMock.Object);

            User user = new User()
            {
                Email = "hunt@gmail.com"
            };

            string errorMessage = await userManager.CreateUser(user);

            userValidationMock.Verify(a => a.ValidateUser(It.IsAny<User>()), Times.Once);
            collectionRefMock.Verify(a => a.AddDocumentAsync(It.IsAny<User>()), Times.Once);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public async Task TestCreateUserFail()
        {
            Mock<IUserValidation> userValidationMock = new Mock<IUserValidation>(MockBehavior.Strict);
            userValidationMock.Setup(a => a.ValidateUser(It.IsAny<User>())).Returns("ERROR");

            Mock<ICollectionReference> collectionRefMock = new Mock<ICollectionReference>(MockBehavior.Strict);
            collectionRefMock.Setup(a => a.AddDocumentAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            Mock<IStorageReference> storageRefMock = new Mock<IStorageReference>(MockBehavior.Strict);

            UserManager userManager = new UserManager(userValidationMock.Object, collectionRefMock.Object,
                storageRefMock.Object);

            User user = new User()
            {
                Email = "invalid"
            };

            string errorMessage = await userManager.CreateUser(user);

            userValidationMock.Verify(a => a.ValidateUser(It.IsAny<User>()), Times.Once);
            collectionRefMock.Verify(a => a.AddDocumentAsync(It.IsAny<User>()), Times.Never);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }
    }
}
