using ChoreCore.Models;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("ChoreCore.Tests")]
namespace ChoreCore.Managers
{
    public class UserValidation : IUserValidation
    {
        private IAddressValidation _addressValidation;

        public UserValidation(IAddressValidation addressValidation = null)
        {
            _addressValidation = addressValidation ?? (IAddressValidation)Splat.Locator.Current.GetService(typeof(IAddressValidation));
        }

        public string ValidateUser(User user)
        {
            try
            {
                ValidateEmail(user);
                ValidatePhoneNumber(user);
                _addressValidation.ValidateAddress(user.Address);

                user.GeoPoint = _addressValidation.GetGeopoint(user.Address);
                
                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        internal void ValidateEmail(User user)
        {
            if (!string.IsNullOrEmpty(user.Email) && !Regex.Match(user.Email, RegexValidation.EmailPattern).Success)
            {
                throw new Exception("Invalid email.");
            }
        }

        internal void ValidatePhoneNumber(User user)
        {
            if (!string.IsNullOrEmpty(user.PhoneNumber) && !Regex.Match(user.PhoneNumber, RegexValidation.PhoneNumberPattern).Success)
            {
                throw new Exception("Invalid phone number.");
            }
        }
    }
}
