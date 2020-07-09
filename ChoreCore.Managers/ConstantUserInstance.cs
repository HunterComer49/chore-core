using ChoreCore.Models;
using System.IO;

namespace ChoreCore.Managers
{
    public class ConstantUserInstance : IConstantUserInstance
    {
        private User _user;
        private Stream _profilePicStream;

        public ConstantUserInstance() { }

        public void SetUser(User user) => _user = user;

        public User GetUser() => _user;

        public void SetProfilePic(Stream stream) => _profilePicStream = stream;

        public Stream GetProfilePic() => _profilePicStream;

    }
}