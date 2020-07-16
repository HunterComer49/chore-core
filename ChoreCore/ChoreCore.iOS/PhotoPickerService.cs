using System;
using System.IO;
using System.Threading.Tasks;
using ChoreCore.Controllers;
using ChoreCore.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace ChoreCore.iOS
{
    public class PhotoPickerService : IPhotoPickerService
    {
        private TaskCompletionSource<Stream> _taskCompletionSource;
        private UIImagePickerController _imagePicker;

        public Task<Stream> GetImageStreamAsync()
        {
            //Create and define UIImagePickerController
            _imagePicker = new UIImagePickerController()
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            //Set event handlers
            _imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled += OnImagePickerCancelled;

            //Present UIImagePickerController
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            UIViewController viewController = window.RootViewController;
            viewController.PresentViewController(_imagePicker, true, null);

            //Return task object
            _taskCompletionSource = new TaskCompletionSource<Stream>();
            return _taskCompletionSource.Task;
        }

        private void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null)
            {
                //Convert UIImage to .NET Stream object
                NSData data;

                if (args.ReferenceUrl.PathExtension.Equals("PNG") || args.ReferenceUrl.PathExtension.Equals("png"))
                {
                    data = image.AsPNG();
                }
                else
                {
                    data = image.AsJPEG();
                }

                Stream stream = data.AsStream();

                UnregisterEventHandlers();

                //Set the STream as the completion of the Task
                _taskCompletionSource.SetResult(stream);
            }
            else
            {
                UnregisterEventHandlers();
                _taskCompletionSource.SetResult(null);
            }

            _imagePicker.DismissModalViewController(true);
        }

        private void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            _taskCompletionSource.SetResult(null);
            _imagePicker.DismissModalViewController(true);
        }

        private void UnregisterEventHandlers()
        {
            _imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}