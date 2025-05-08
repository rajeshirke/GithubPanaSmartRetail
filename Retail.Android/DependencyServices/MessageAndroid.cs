using System;
using Android.App;
using Android.Widget;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Retail.Droid.DependencyServices
{
    public class MessageAndroid : IMessage
    {
        public void DismissAlert()
        {

        }

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}
