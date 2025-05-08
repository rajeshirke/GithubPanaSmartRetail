using System;
using System.Diagnostics;
using Retail.DependencyServices;
using Retail.Resx;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.Hepler
{
    public interface IAlertHandler
    {
        //void SendNotification();
    }

    public class AlertHandler : IAlertHandler
    {

        public AlertHandler()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            try
            {
                var access = e.NetworkAccess;
                var profiles = e.ConnectionProfiles;
                //e.NetworkAccess;
                if (access == NetworkAccess.None)
                {
                    await Application.Current.MainPage.DisplayAlert("", "No internet connection", AppResources.lblOk);
                }
                else
                {
                    var messageToast = DependencyService.Get<IMessage>();
                    messageToast.DismissAlert();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }
    }
}
