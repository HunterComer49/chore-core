using ChoreCore.Models;
using Plugin.CloudFirestore;

namespace ChoreCore.Managers
{
    public interface IAddressValidation
    {
        GeoPoint GetGeopoint(Address address);
        void ValidateAddress(Address address);
    }
}