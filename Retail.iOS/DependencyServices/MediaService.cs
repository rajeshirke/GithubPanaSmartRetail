using System;
using Foundation;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace Retail.iOS.DependencyServices
{
    public class MediaService : IMediaService
    {
        public void SaveImageFromByte(byte[] imageByte, string fileName)
        {
            try
            {
                var imageData = new UIImage(NSData.FromArray(imageByte));
                imageData.SaveToPhotosAlbum((image, error) =>
                {
                //you can retrieve the saved UI Image as well if needed using  
                //var i = image as UIImage;  
                if (error != null)
                    {
                        Console.WriteLine(error.ToString());
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
        public string GetBaseUrl()
        {
            // return "";
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
