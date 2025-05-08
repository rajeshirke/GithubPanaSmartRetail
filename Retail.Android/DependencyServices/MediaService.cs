using System;
using Android.Content;
using Plugin.CurrentActivity;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace Retail.Droid.DependencyServices
{
    public class MediaService : IMediaService
    {
        Android.Content.Context CurrentContext => CrossCurrentActivity.Current.Activity;

        public string GetBaseUrl()
        {
            return "";
        }

        [Obsolete]
        public void SaveImageFromByte(byte[] imageByte, string filename)
        {
            try
            {
                Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                string path = System.IO.Path.Combine(storagePath.ToString(), filename);
                System.IO.File.WriteAllBytes(path, imageByte);
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
                CurrentContext.SendBroadcast(mediaScanIntent);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
