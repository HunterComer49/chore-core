using System.ComponentModel;

namespace ChoreCore.Models
{
    public enum ProjectStatus
    {
        [Description("Open")]
        Open,
        [Description("Pending")]
        Pending,
        [Description("Voided")]
        Voided,
        [Description("Complete")]
        Complete
    };
}
