using GoogleMaps.LocationServices;

namespace ChoreCore.Managers
{
    public class GeopointManager : IGeopointManager
    {
        public MapPoint GetGeopoint(string address)
        {
            GoogleLocationService locationServices = new GoogleLocationService(Models.Constants.GoogleAPIKey);
            return locationServices.GetLatLongFromAddress(address);
        }

    }
}
