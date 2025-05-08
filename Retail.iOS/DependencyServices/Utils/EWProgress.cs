using System;
using BigTed;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices.Utils;
using Xamarin.Forms;
using static BigTed.ProgressHUD;


[assembly: Dependency(typeof(EWProgress))]
namespace Retail.iOS.DependencyServices.Utils
{
    public class EWProgress : IEWProgress
    {
        public void Dismiss()
        {
            BTProgressHUD.Dismiss();
            //MessagingCenter.Send<string>("Hide", "ShowLoaderAnim");
        }

        public void Show()
        {
            BTProgressHUD.Show("Please Wait..", 1, MaskType.Gradient);
            //MessagingCenter.Send<string>("Show", "ShowLoaderAnim");
        }

        public void Show(string message)
        {
            BTProgressHUD.Show(message);
            //MessagingCenter.Send<string>("Show", "ShowLoaderAnim");
        }
    }
}
