using ChoreCore.Models;
using Plugin.CloudFirestore;
using Plugin.FirebaseStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public class ProjectManager : IProjectManager
    {
        private ICollectionReference _collectionReference;
        private IStorageReference _storageReference;
        private IProjectValidation _projectValidation;

        public ProjectManager(IProjectValidation projectValidation = null, ICollectionReference collectionReference = null,
            IStorageReference storageReference = null)
        {
            _projectValidation = projectValidation ?? (IProjectValidation)Splat.Locator.Current.GetService(typeof(IProjectValidation));

            _collectionReference = collectionReference ?? CrossCloudFirestore.Current.Instance.GetCollection(FirestoreCollections.PROJECTS);

            _storageReference = storageReference ?? CrossFirebaseStorage.Current.Instance.RootReference;
        }

        public async Task<string> CreateProject(Project project, List<Stream> images)
        {
            string error = _projectValidation.ValidateProject(project);

            if (string.IsNullOrEmpty(error))
            {
                IDocumentReference document = _collectionReference.CreateDocument();

                await document.SetDataAsync(project);

                error = await AddProjectImages(document.Id, images);
            }

            return error;
        }

        public async Task<string> UpdateProject(Project project)
        { 
            string error = _projectValidation.ValidateProject(project);

            if (string.IsNullOrEmpty(error))
            {
                await _collectionReference.GetDocument(project.Id).UpdateDataAsync(project);
            }

            return error;
        }

        public async Task<Project> GetProject(string id)
        {
            return (await _collectionReference.WhereEqualsTo(nameof(Project.Id), id)
                                              .GetDocumentsAsync())
                                              .ToObjects<Project>()
                                              .First();
        }

        public async Task<List<byte[]>> GetProjectImages(string projectId)
        {
            List<byte[]> images = new List<byte[]>();
            int i = 0;
            bool error = false;

            IStorageReference reference = _storageReference.GetChild($"Projects/{projectId}");

            while (!error)
            {
                try
                {
                    images.Add(await reference.GetChild($"images-{i++}.jpg").GetBytesAsync(1024 * 1024));
                }
                catch (FirebaseStorageException)
                {
                    // Did not find file... does not exist
                    error = true;
                }
            }

            return images;
        }

        public async Task<string> AddProjectImages(string id, List<Stream> images)
        {
            int i = 0;

            foreach (Stream stream in images)
            {
                IStorageReference reference = _storageReference.GetChild($"Projects/{id}/image-{i++}.jpg");

                try
                {
                    await reference.PutStreamAsync(stream);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return string.Empty;
        }

        public async Task<string> DeleteImages(string id, List<string> images)
        {
            foreach (string fileName in images)
            {
                IStorageReference reference = _storageReference.GetChild($"Projects/{id}/{fileName}");

                try
                {
                    await reference.DeleteAsync();
                }
                catch (FirebaseStorageException e)
                {
                    return e.Message;
                }
            }

            return string.Empty;
        }
    }
}
