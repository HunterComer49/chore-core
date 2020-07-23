using GoogleMaps.LocationServices;
using Plugin.CloudFirestore;

namespace ChoreCore.Managers
{
    public interface IProjectValidation
    {
        string ValidateProject(Models.Project project);
    }
}