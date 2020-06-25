using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> SignUpWithEmailPassword(string email, string password);
        Task<bool> SendPasswordResetEmailAsync(string email);
        bool SignOut();
        bool IsSignIn();
    }
}
