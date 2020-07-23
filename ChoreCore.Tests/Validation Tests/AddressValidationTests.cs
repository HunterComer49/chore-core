using ChoreCore.Managers;
using ChoreCore.Models;
using GoogleMaps.LocationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plugin.CloudFirestore;
using System;

namespace ChoreCore.Tests.Validation_Tests
{
    [TestClass]
    public class AddressValidationTests
    {
        public AddressValidation GetAddressValidation()
        {
            Mock<IGeopointManager> geoPointManagerMock = new Mock<IGeopointManager>(MockBehavior.Strict);

            return new AddressValidation(geoPointManagerMock.Object);
        }

        [TestMethod]
        public void TestGetGeopointBlank()
        {
            Mock<IGeopointManager> geoPointManagerMock = new Mock<IGeopointManager>(MockBehavior.Strict);

            AddressValidation addressValidation = new AddressValidation(geoPointManagerMock.Object);

            GeoPoint result = addressValidation.GetGeopoint(new Address());

            geoPointManagerMock.Verify(a => a.GetGeopoint(It.IsAny<string>()), Times.Never);
            Assert.AreEqual(0, result.Latitude);
            Assert.AreEqual(0, result.Longitude);
        }

        [TestMethod]
        public void TestGetGeopoint()
        {
            Mock<IGeopointManager> geoPointManagerMock = new Mock<IGeopointManager>(MockBehavior.Strict);
            geoPointManagerMock.Setup(a => a.GetGeopoint(It.IsAny<string>())).Returns(new MapPoint()
            {
                Longitude = 10,
                Latitude = 10
            });

            AddressValidation addressValidation = new AddressValidation(geoPointManagerMock.Object);

            GeoPoint result = addressValidation.GetGeopoint(new Address()
            {
                Street = "here",
                City = "there",
                State = States.AK.ToString(),
                Zip = "12345"
            });

            geoPointManagerMock.Verify(a => a.GetGeopoint(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result.Latitude);
            Assert.IsNotNull(result.Longitude);
        }

        [DataRow("NE", true)]
        [DataRow("WXY", false)]
        [DataTestMethod]
        public void TestValidateState(string state, bool pass)
        {
            AddressValidation addressValidation = GetAddressValidation();

            Address address = new Address()
            {
                State = state
            };

            if (pass)
            {
                Valid(address, addressValidation.ValidateState);
            }
            else
            {
                Invalid(address, addressValidation.ValidateState);
            }
        }

        [DataRow("12345", true)]
        [DataRow("abc", false)]
        [DataTestMethod]
        public void TestValidateZipCode(string zip, bool pass)
        {
            AddressValidation addressValidation = GetAddressValidation();

            Address address = new Address()
            {
                Zip = zip
            };

            if (pass)
            {
                Valid(address, addressValidation.ValidateZipCode);
            }
            else
            {
                Invalid(address, addressValidation.ValidateZipCode);
            }
        }

        private void Valid(Address address, Action<Address> methodName)
        {
            try
            {
                methodName(address);

                //If we got here, email was validated correctly
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        private void Invalid(Address address, Action<Address> methodName)
        {
            try
            {
                methodName(address);

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
