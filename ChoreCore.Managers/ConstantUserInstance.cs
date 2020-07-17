using ChoreCore.Models;
using System.IO;

namespace ChoreCore.Managers
{
    public class ConstantUserInstance : IConstantUserInstance
    {
        private User _user;
        private byte[] _profilePicStream;

        public ConstantUserInstance() { }

        public void SetUser(User user) => _user = user;

        public User GetUser() => _user;

        public void SetProfilePic(Stream stream)
        {
            _profilePicStream = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;

                while ((read = stream.Read(_profilePicStream, 0, _profilePicStream.Length)) > 0)
                {
                    ms.Write(_profilePicStream, 0, read);
                }

                _profilePicStream = ms.ToArray();
            }
        }

        public byte[] GetProfilePic() => _profilePicStream;

    }
}