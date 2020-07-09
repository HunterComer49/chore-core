using GoogleMaps.LocationServices;

namespace ChoreCore.Managers
{
    public class GeopointManager : IGeopointManager
    {
        public MapPoint GetGeopoint(string address)
        {
            GoogleLocationService locationServices = new GoogleLocationService("AIzaSyDpwsXtkIEk9y9m2Uo0y51uSlraZSEqmSY");
            return locationServices.GetLatLongFromAddress(address);
        }

    }
}
