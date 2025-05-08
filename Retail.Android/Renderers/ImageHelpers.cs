using System;
using System.IO;
using Android.Graphics;
using Android.Media;
using Retail.DependencyServices;

//[assembly: Xamarin.Forms.Dependency(typeof(Retail.Droid.Renderers.ImageHelpers))]
namespace Retail.Droid.Renderers
{
    public static class ImageHelpers
    {

        public static byte[] RotateImage(string path, float scaleFactor, int quality = 90)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var rotation = GetRotation(path);
            var width = (originalImage.Width * scaleFactor);
            var height = (originalImage.Height * scaleFactor);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }

            using (var ms = new MemoryStream())
            {
                rotatedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                imageBytes = ms.ToArray();
            }


            originalImage?.Dispose();
            rotatedImage?.Dispose();
            GC.Collect();

            return imageBytes;
        }

        static int GetRotation(string filePath)
        {
            using (var ei = new ExifInterface(filePath))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }
    }
    //public class ImageHelpers : ICompressImages
    //{
    //    //collectionName is the name of the folder in your Android Pictures directory.
    //    public static readonly string collectionName = "TmpPictures";

    //    public string SaveFile(byte[] imageByte, string fileName)
    //    {
    //        Java.IO.File fileDir;
    //        if ((int)Android.OS.Build.VERSION.SdkInt >= 29)
    //        {
    //            fileDir = new Java.IO.File(Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures), collectionName);
    //        }
    //        else
    //        {
    //            fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
    //        }

    //        if (!fileDir.Exists())
    //        {
    //            fileDir.Mkdirs();
    //        }

    //        var file = new Java.IO.File(fileDir, fileName);
    //        System.IO.File.WriteAllBytes(file.Path, imageByte);

    //        return file.Path;
    //    }

    //    public string CompressImage(string path)
    //    {
    //        byte[] imageBytes;

    //        //Get the bitmap.
    //        var originalImage = BitmapFactory.DecodeFile(path);

    //        //Set imageSize and imageCompression parameters.
    //        var imageSize = .86;
    //        var imageCompression = 67;

    //        //Resize it and then compress it to Jpeg.
    //        var width = (originalImage.Width * imageSize);
    //        var height = (originalImage.Height * imageSize);
    //        var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            scaledImage.Compress(Bitmap.CompressFormat.Jpeg, imageCompression, ms);
    //            imageBytes = ms.ToArray();
    //        }

    //        originalImage.Recycle();
    //        originalImage.Dispose();
    //        GC.Collect();

    //        return SaveFile(imageBytes, Guid.NewGuid().ToString());
    //    }
    //}
}
