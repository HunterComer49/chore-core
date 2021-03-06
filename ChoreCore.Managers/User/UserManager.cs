﻿using ChoreCore.Models;
using Plugin.CloudFirestore;
using Plugin.FirebaseStorage;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ICollectionReference _collectionReference;
        private readonly IStorageReference _storageReference;
        private IUserValidation _userValidation;

        public UserManager(IUserValidation userValidation = null, ICollectionReference collectionReference = null,
            IStorageReference storageReference = null)
        {
            _userValidation = userValidation ?? (IUserValidation)Splat.Locator.Current.GetService(typeof(IUserValidation));

            _collectionReference = collectionReference ?? CrossCloudFirestore.Current.Instance.GetCollection(FirestoreCollections.USERS);

            _storageReference = storageReference ?? CrossFirebaseStorage.Current.Instance.RootReference;
        }

        /// <summary>
        /// Returns blank string if no error occurs. Returns error string if issue
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> CreateUser(User user)
        {
            string error = _userValidation.ValidateUser(user);

            if (string.IsNullOrEmpty(error))
            {
                await _collectionReference.AddDocumentAsync(user);
            }

            return error;
        }

        /// <summary>
        /// Returns blank string if no error occurs. Returns error string if issue
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> UpdateUser(User user)
        {
            string id = (await GetUser(user.Email)).Id;

            string error = _userValidation.ValidateUser(user);

            if (string.IsNullOrEmpty(error))
            {
                await _collectionReference.GetDocument(id).UpdateDataAsync(user);
            }

            return error;
        }

        public async Task<User> GetUser(string email)
        {
            return (await _collectionReference.WhereEqualsTo(nameof(User.Email), email)
                                              .GetDocumentsAsync())
                                              .ToObjects<User>()
                                              .First();
        }

        public async Task<string> UploadProfilePic(string userId, Stream stream)
        {
            string message = "";

            IStorageReference reference = _storageReference.GetChild($"ProfilePics/{userId}.jpg");

            try
            {
                await reference.PutStreamAsync(stream);
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return message;
        }

        public async Task<byte[]> GetProfilePic(string userId)
        {
            byte[] picture = new byte[1024 * 1024];

            IStorageReference reference = _storageReference.GetChild($"ProfilePics/{userId}.jpg");

            try
            {
                picture = await reference.GetBytesAsync(1024 * 1024);

            }
            catch (FirebaseStorageException)
            {
                // possible do something here later??
            }

            return picture;
        }
    }
}