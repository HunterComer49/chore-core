using System.ComponentModel;

namespace ChoreCore.Models
{
    public enum ProjectCategory
    {
        [Description("Manual Labor")]
        ManualLabor,
        [Description("License Required")]
        LicenceRequired
    };
}
