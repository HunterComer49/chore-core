using ChoreCore.Models;
using GoogleMaps.LocationServices;
using Plugin.CloudFirestore;
using System;
using System.Text.RegularExpressions;

namespace ChoreCore.Managers
{
    public class AddressValidation : IAddressValidation
    {
        private IGeopointManager _geopointManager;

        public AddressValidation(IGeopointManager geopointManager = null)
        {
            _geopointManager = geopointManager ?? (IGeopointManager)Splat.Locator.Current.GetService(typeof(IGeopointManager));
        }

        public void ValidateAddress(Address address)
        {
            ValidateState(address);
            ValidateZipCode(address);
        }

        public GeoPoint GetGeopoint(Address address)
        {
            if (!string.IsNullOrEmpty(address.Street) && !string.IsNullOrEmpty(address.City) && !string.IsNullOrEmpty(address.State)
                && !string.IsNullOrEmpty(address.Zip))
            {
                string addressString = $"{address.Street} {address.City}, {address.State} {address.Zip}";
                MapPoint geo = _geopointManager.GetGeopoint(addressString);
                return new GeoPoint(geo.Latitude, geo.Longitude);
            }

            return new GeoPoint();
        }

        internal void ValidateState(Address address)
        {
            if (!string.IsNullOrEmpty(address.State) && !Enum.IsDefined(typeof(States), address.State))
            {
                throw new Exception("Invalid state.");
            }
        }

        internal void ValidateZipCode(Address address)
        {
            if (!string.IsNullOrEmpty(address.Zip) && !Regex.Match(address.Zip, RegexValidation.ZipPattern).Success)
            {
                throw new Exception("Invalid zip code.");
            }
        }
    }
}
