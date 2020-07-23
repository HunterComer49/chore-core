using ChoreCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IProjectController
    {
        Task<string> AddImages(string id, List<Stream> images);
        Task<string> CreateProject(Project project, List<Stream> images);
        Task<string> DeleteImages(string id, List<string> images);
        Task<Tuple<Project, List<byte[]>>> GetProject(string id);
        Task<string> UpdateProject(Project project);
    }
}