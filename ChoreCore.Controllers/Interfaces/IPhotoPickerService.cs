using System.IO;
using System.Threading.Tasks;

namespace ChoreCore.Controllers
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
