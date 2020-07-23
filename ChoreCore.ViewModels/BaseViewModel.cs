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

        protected bool SetAndRaise<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
            {
                return false;
            }

            property = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected ImageSource ByteToImage(byte[] image)
        {
            return ImageSource.FromStream(() => new MemoryStream(image.ToArray()));
        }
    }
}
