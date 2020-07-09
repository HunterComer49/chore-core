using GoogleMaps.LocationServices;

namespace ChoreCore.Managers
{
    public interface IGeopointManager
    {
        MapPoint GetGeopoint(string address);
    }
}