using ChoreCore.Models;
using GoogleMaps.LocationServices;
using Plugin.CloudFirestore;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("ChoreCore.Tests")]
namespace ChoreCore.Managers
{
    public class UserValidation : IUserValidation
    {
        private IGeopointManager _geopointManager;

        public UserValidation(IGeopointManager geopointManager = null)
        {
            _geopointManager = geopointManager ?? (IGeopointManager)Splat.Locator.Current.GetService(typeof(IGeopointManager));
        }

        public string ValidateUser(User user)
        {
            try
            {
                ValidateEmail(user);
                ValidatePhoneNumber(user);
                ValidateState(user);
                ValidateZipCode(user);

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public User SetGeopoint(User user)
        {
            if (!string.IsNullOrEmpty(user.Street) && !string.IsNullOrEmpty(user.City)
                && !string.IsNullOrEmpty(user.State) && !string.IsNullOrEmpty(user.Zip))
            {
                string address = $"{user.Street} {user.City}, {user.State} {user.Zip}";
                MapPoint geo = _geopointManager.GetGeopoint(address);
                user.GeoPoint = new GeoPoint(geo.Latitude, geo.Longitude);
            }

            return user;
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

        internal void ValidateState(User user)
        {
            if (!string.IsNullOrEmpty(user.State) && !Enum.IsDefined(typeof(States), user.State))
            {
                throw new Exception("Invalid state.");
            }
        }

        internal void ValidateZipCode(User user)
        {
            if (!string.IsNullOrEmpty(user.Zip) && !Regex.Match(user.Zip, RegexValidation.ZipPattern).Success)
            {
                throw new Exception("Invalid zip code.");
            }
        }
    }
}
