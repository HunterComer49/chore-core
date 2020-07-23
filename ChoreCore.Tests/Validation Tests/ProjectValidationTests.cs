using ChoreCore.Managers;
using ChoreCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ChoreCore.Tests.Validation_Tests
{
    [TestClass]
    public class ProjectValidationTests
    {
        public ProjectValidation GetProjectValidation()
        {
            Mock<IAddressValidation> addressValidationMock = new Mock<IAddressValidation>(MockBehavior.Strict);

            return new ProjectValidation(addressValidationMock.Object);
        }

        [DataRow("huntcomer49@gmail.com", true)]
        [DataRow("huntcomer49@gmail", false)]
        [DataTestMethod]
        public void TestValidateEmail(string email, bool pass)
        {
            ProjectValidation projectValidation = GetProjectValidation();

            Project project = new Project()
            {
                Email = email
            };

            if (pass)
            {
                Valid(project, projectValidation.ValidateEmail);
            }
            else
            {
                Invalid(project, projectValidation.ValidateEmail);
            }
        }

        [DataRow("ManualLabor", true)]
        [DataRow("LicenceRequired", true)]
        [DataRow("Not a category", false)]
        [DataTestMethod]
        public void TestValidateCategory(string category, bool pass)
        {
            ProjectValidation projectValidation = GetProjectValidation();

            Project project = new Project()
            {
                Category = category
            };

            if (pass)
            {
                Valid(project, projectValidation.ValidateCategory);
            }
            else
            {
                Invalid(project, projectValidation.ValidateCategory);
            }
        }

        [DataRow("Open", true)]
        [DataRow("Pending", true)]
        [DataRow("Voided", true)]
        [DataRow("Complete", true)]
        [DataRow("Canceled", false)]
        [DataTestMethod]
        public void TestValidateStatus(string status, bool pass)
        {
            ProjectValidation projectValidation = GetProjectValidation();

            Project project = new Project()
            {
                Status = status
            };

            if (pass)
            {
                Valid(project, projectValidation.ValidateStatus);
            }
            else
            {
                Invalid(project, projectValidation.ValidateStatus);
            }
        }

        private void Valid(Project project, Action<Project> methodName)
        {
            try
            {
                methodName(project);

                //If we got here, email was validated correctly
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        private void Invalid(Project project, Action<Project> methodName)
        {
            try
            {
                methodName(project);

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
