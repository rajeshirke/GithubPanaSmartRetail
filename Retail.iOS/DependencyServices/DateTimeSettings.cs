using System;
using Foundation;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DateTimeSettings))]
namespace Retail.iOS.DependencyServices
{
    public class DateTimeSettings : IDateTimeSettings
    {
        public void OpenDateTimeSettings()
        {
            try
            {
                // Open the Date & Time settings page
                UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl("App-Prefs:root=General&path=DATE_AND_TIME"));
            }
            catch (Exception)
            {
                // Handle exceptions as needed
            }
        }
        public bool IsAutomaticDateTimeEnabled()
        {
            try
            {
                // On iOS, you can check if the "Set Automatically" option is enabled in Date & Time settings
                // Note: There isn't a direct API to check this, so this is a workaround

                // Retrieve the current time zone
                NSTimeZone currentTimeZone = NSTimeZone.LocalTimeZone;
                //string timeZoneAbbreviation = currentTimeZone.Abbreviation();
                // Check if it's an automatic time zone (Set Automatically is enabled)
                //return currentTimeZone.Abbreviation?.StartsWith("GMT") ?? false;

                return true;// currentTimeZone.Abbreviation?.StartsWith("GMT") ?? false;

                //NSSystemClockDidChangeNotification.



                }
            catch (Exception)
            {
                // Handle exceptions as needed
                return false;
            }
        }
    }
    
}
