using ChoreCore.Models;
using Plugin.CloudFirestore;
using System.Linq;
using System.Threading.Tasks;

namespace ChoreCore.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ICollectionReference _collectionReference;
        private IUserValidation _userValidation;

        public UserManager(IUserValidation userValidation = null)
        {
            _userValidation = userValidation ?? (IUserValidation)Splat.Locator.Current.GetService(typeof(IUserValidation));

            _collectionReference = CrossCloudFirestore.Current.Instance.GetCollection(FirestoreCollections.USERS);
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
        public async Task<string> UpdateUser(string userId, User user)
        {
            string error = _userValidation.ValidateUser(user);

            if (string.IsNullOrEmpty(error))
            {
                await _collectionReference.GetDocument(userId).UpdateDataAsync(user);
            }

            return error;
        }

        public async Task<User> GetUser(string email)
        {
            IQuerySnapshot query = await _collectionReference.WhereEqualsTo(nameof(User.Email), email).GetDocumentsAsync();

            return query.ToObjects<User>().First();
        }
    }
}