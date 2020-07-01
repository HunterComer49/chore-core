using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;

namespace ChoreCore.Models
{
    public class User
    {
        [Id]
        public string Id { get; set; }

        public string Email { get; set; }

        [ServerTimestamp(CanReplace = false)]
        public Timestamp CreatedDate { get; set; }

        [ServerTimestamp]
        public Timestamp UpdatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public GeoPoint GeoPoint { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; } 

        public int Zip { get; set; }
    }
}
