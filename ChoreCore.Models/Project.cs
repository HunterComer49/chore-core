using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;

namespace ChoreCore.Models
{
    public partial class Project
    {
        [Id]
        public string Id { get; set; }

        public string Email { get; set; }

        public string CoreMemberEmail { get; set; }

        [ServerTimestamp(CanReplace = false)]
        public Timestamp PostedDate { get; set; }

        [ServerTimestamp]
        public Timestamp UpdatedDate { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public GeoPoint Location { get; set; }

        public Address Address { get; set; }

        public int NumberOfBids { get; set; }

        public string Status { get; set; }

        public string SpecialInstructions { get; set; }

        public string EstimatedTimeOfCompletion { get; set; }

        public string NeedsCompletedBy { get; set; }

        public double Rating { get; set; }
    }
}
