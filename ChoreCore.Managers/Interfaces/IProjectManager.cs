using ChoreCore.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public interface IProjectManager
    {
        Task<string> CreateProject(Project project, List<Stream> images);
        Task<Models.Project> GetProject(string id);
        Task<List<byte[]>> GetProjectImages(string projectId);
        Task<string> UpdateProject(Project project);
        Task<string> DeleteImages(string id, List<string> images);
        Task<string> AddProjectImages(string id, List<Stream> images);
    }
}