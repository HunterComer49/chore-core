using ChoreCore.Models;

namespace ChoreCore.Managers
{
    public class ConstantUserInstance : IConstantUserInstance
    {
        private User _user;
        private byte[] _profilePic = new byte[16 * 1024];

        public ConstantUserInstance() { }

        public void SetUser(User user) => _user = user;

        public User GetUser() => _user;

        public void SetProfilePic(byte[] image) => _profilePic = image;

        public byte[] GetProfilePic() => _profilePic;

    }
}