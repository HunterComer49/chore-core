using ChoreCore.Managers;
using ChoreCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ChoreCore.Tests.Validation_Tests
{
    [TestClass]
    public class UserValidationTests
    {
        public UserValidation GetUserValidation()
        {
            Mock<IGeopointManager> geopointManagerMock = new Mock<IGeopointManager>(MockBehavior.Strict);

            return new UserValidation(geopointManagerMock.Object);
        }

        [DataRow("huntcomer49@gmail.com", true)]
        [DataRow("huntcomer49@gmail", false)]
        [DataTestMethod]
        public void TestValidateEmail(string email, bool pass)
        {
            UserValidation userValidation = GetUserValidation();

            User user = new User()
            {
                Email = email
            };

            if (pass)
            {
                Valid(user, userValidation.ValidateEmail);
            }
            else
            {
                Invalid(user, userValidation.ValidateEmail);
            }
        }

        [DataRow("6056416612", true)]
        [DataRow("(605) 641-6612", true)]
        [DataRow("(605)641-6612", true)]
        [DataRow("(605) 6416612", true)]
        [DataRow("(605)6416612", true)]
        [DataRow("605-641-6612", true)]
        [DataRow("abc", false)]
        [DataRow("() 123-456", false)]
        [DataRow("123-456", false)]
        [DataTestMethod]
        public void TestValidatePhoneNumber(string phoneNumber, bool pass)
        {
            UserValidation userValidation = GetUserValidation();

            User user = new User()
            {
                PhoneNumber = phoneNumber
            };

            if (pass)
            {
                Valid(user, userValidation.ValidatePhoneNumber);
            }
            else
            {
                Invalid(user, userValidation.ValidatePhoneNumber);
            }
        }

        [DataRow("NE", true)]
        [DataRow("WXY", false)]
        [DataTestMethod]
        public void TestValidateState(string state, bool pass)
        {
            UserValidation userValidation = GetUserValidation();

            User user = new User()
            {
                State = state
            };

            if (pass)
            {
                Valid(user, userValidation.ValidateState);
            }
            else
            {
                Invalid(user, userValidation.ValidateState);
            }
        }

        [DataRow("12345", true)]
        [DataRow("abc", false)]
        [DataTestMethod]
        public void TestValidateZipCode(string zip, bool pass)
        {
            UserValidation userValidation = GetUserValidation();

            User user = new User()
            {
                Zip = zip
            };

            if (pass)
            {
                Valid(user, userValidation.ValidateZipCode);
            }
            else
            {
                Invalid(user, userValidation.ValidateZipCode);
            }
        }

        private void Valid(User user, Action<User> methodName)
        {
            try
            {
                methodName(user);

                //If we got here, email was validated correctly
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        private void Invalid(User user, Action<User> methodName)
        {
            UserValidation userValidation = GetUserValidation();

            try
            {
                methodName(user);

                // Should not get here, should fail
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
        }
    }
}
