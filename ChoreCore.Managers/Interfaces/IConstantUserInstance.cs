using ChoreCore.Models;
using System.IO;

namespace ChoreCore.Managers
{
    public interface IConstantUserInstance
    {
        Stream GetProfilePic();
        User GetUser();
        void SetProfilePic(Stream stream);
        void SetUser(User user);
    }
}