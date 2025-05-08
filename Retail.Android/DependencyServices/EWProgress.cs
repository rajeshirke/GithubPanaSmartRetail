using System;
using AndroidHUD;
using Com.Syncfusion.Sfbusyindicator;
using Com.Syncfusion.Sfbusyindicator.Enums;
using Plugin.CurrentActivity;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(EWProgress))]
namespace Retail.Droid.DependencyServices
{
    public class EWProgress : IEWProgress
    {
        public void Dismiss()
        {
            AndHUD.Shared.Dismiss(CrossCurrentActivity.Current.Activity);
            //MessagingCenter.Send<string>("Hide", "ShowLoaderAnim");
        }

        public void Show()
        {
            AndHUD.Shared.Show(CrossCurrentActivity.Current.Activity, "Please Wait", 1);
            //MessagingCenter.Send<string>("Show", "ShowLoaderAnim");
        }

        public void Show(string message)
        {
            AndHUD.Shared.Show(CrossCurrentActivity.Current.Activity, message, 1);
            //MessagingCenter.Send<string>("Show", "ShowLoaderAnim");
        }
    }
}
