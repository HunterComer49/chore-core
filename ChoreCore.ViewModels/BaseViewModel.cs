using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ChoreCore.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ImageSource ByteToImage(byte[] image)
        {
            return ImageSource.FromStream(() => new MemoryStream(image.ToArray()));
        }
    }
}
