using ChoreCore.Managers;
using ChoreCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public class ProjectController : IProjectController
    {
        private IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager = null)
        {
            _projectManager = projectManager ?? (IProjectManager)Splat.Locator.Current.GetService(typeof(IProjectManager));
        }

        public async Task<string> CreateProject(Project project, List<Stream> images)
        {
            return await _projectManager.CreateProject(project, images);
        }

        public async Task<string> UpdateProject(Project project)
        {
            return await _projectManager.UpdateProject(project);
        }

        public async Task<string> DeleteImages(string id, List<string> images)
        {
            return await _projectManager.DeleteImages(id, images);
        }

        public async Task<string> AddImages(string id, List<Stream> images)
        {
            return await _projectManager.AddProjectImages(id, images);
        }

        public async Task<Tuple<Project, List<byte[]>>> GetProject(string id)
        {
            Project project = await _projectManager.GetProject(id);

            List<byte[]> images = await _projectManager.GetProjectImages(id);

            return Tuple.Create(project, images);
        }


    }
}
