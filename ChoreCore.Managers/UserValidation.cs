using ChoreCore.Models;
using GoogleMaps.LocationServices;
using Plugin.CloudFirestore;
using System;
using System.Text.RegularExpressions;

namespace ChoreCore.Managers
{
    public class UserValidation : IUserValidation
    {
        private User _user;

        public UserValidation() { }

        public string ValidateUser(User user)
        {
            _user = user;

            try
            {
                ValidateEmail();
                ValidatePhoneNumber();
                ValidateZipCode();
                SetGeopoint();

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void ValidateEmail()
        {
            if (!Regex.Match(_user.Email, RegexValidation.EmailPattern).Success)
            {
                throw new Exception("Invalid email.");
            }
        }

        private void SetGeopoint()
        {
            if (!string.IsNullOrEmpty(_user.Street) && !string.IsNullOrEmpty(_user.City) 
                && !string.IsNullOrEmpty(_user.State) && _user.Zip > 0)
            {
                string address = $"{_user.Street} ${_user.City}, ${_user.State} ${_user.Zip}";
                GoogleLocationService locationServices = new GoogleLocationService();
                MapPoint geo = locationServices.GetLatLongFromAddress(address);
                _user.GeoPoint = new GeoPoint(geo.Latitude, geo.Longitude);
            }
        }

        private void ValidatePhoneNumber()
        {
            if (!Regex.Match(_user.PhoneNumber, RegexValidation.PhoneNumberPattern).Success)
            {
                throw new Exception("Invalid phone number.");
            }
        }

        private void ValidateState()
        {
            if (!Enum.IsDefined(typeof(States), _user.State))
            {
                throw new Exception("Invalid state.");
            }
        }

        private void ValidateZipCode()
        {
            if (!Regex.Match(_user.Zip.ToString(), RegexValidation.ZipPattern).Success)
            {
                throw new Exception("Invalid zip code.");
            }
        }
    }
}
