using System;
using System.Drawing;
using eWarranty.iOS.DependencyServices;
using Retail.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer_iOS))]
namespace eWarranty.iOS.DependencyServices
{
    public class ImageResizer_iOS : IImageResizer
    {
        public byte[] ResizeImage(byte[] imageData, double maxWidth, double maxHeight)
        {
            UIImage originalImage = ImageFromByteArray(imageData);

            double width = 300, height = 300;

            double maxAspect = (double)maxWidth / (double)maxHeight;
            double aspect = (double)originalImage.Size.Width / (double)originalImage.Size.Height;

            if (maxAspect > aspect && originalImage.Size.Width > maxWidth)
            {
                //Width is the bigger dimension relative to max bounds
                width = maxWidth;
                height = maxWidth / aspect;
            }
            else if (maxAspect <= aspect && originalImage.Size.Height > maxHeight)
            {
                //Height is the bigger dimension
                height = maxHeight;
                width = maxHeight * aspect;
            }

            return originalImage.Scale(new SizeF((float)width, (float)height)).AsJPEG().ToArray();
        }


        public static UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIImage image;
            try
            {
                image = new UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}