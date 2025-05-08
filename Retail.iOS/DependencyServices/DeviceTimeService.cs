using System;
using System.Threading.Tasks;
using CoreTelephony;
using Foundation;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices;
using Xamarin.Forms;
using static CoreFoundation.DispatchSource;
using System.Net.Http;

[assembly: Dependency(typeof(DeviceTimeService))]
namespace Retail.iOS.DependencyServices
{
    public class DeviceTimeService : IDeviceTimeService
    {
        public async Task<bool> IsDeviceTimeValid()
        {
            try
            {

                // Fetch server time from a trusted server
                DateTime serverTime = await GetServerTime();

                // Get the current device time
                DateTime deviceTime = DateTime.Now;

                // Set a threshold for acceptable time difference, e.g., 5 seconds
                double timeDifferenceThreshold = 5.0;

                double timeDifference = Math.Abs((deviceTime - serverTime).TotalSeconds);

                // If the time difference is beyond the threshold, consider it invalid
                return timeDifference <= timeDifferenceThreshold;
            }
            catch (Exception)
            {
                // Handle exceptions when fetching server time
                return false;
            }
        }

        private async Task<DateTime> GetServerTime()
        {
            // Implement logic to fetch the current server time
            // This might involve making a request to a server API
            // and parsing the response to get the server time
            // Example: using HttpClient to fetch server time from an API

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://mcgapp.org.com/getservertime");

                if (response.IsSuccessStatusCode)
                {
                    string serverTimeStr = await response.Content.ReadAsStringAsync();
                    if (DateTime.TryParse(serverTimeStr, out DateTime serverTime))
                    {
                        return serverTime;
                    }
                }
            }

            throw new Exception("Error fetching server time");
        }

        //public bool IsDeviceTimeAutomatic()
        //{



        //    //// Use native iOS APIs to check if the device time setting is automatic
        //    //// The NSTimeZone class in Xamarin.iOS provides information about the time zone,
        //    //// including whether the time setting is automatic or not.
        //    //NSTimeZone timeZone = NSTimeZone.SystemTimeZone;
        //    //return timeZone.IsDaylightSavingsTime((NSDate)DateTime.Now);

        //    //// Retrieve the system time zone
        //    //TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;

        //    //// Get the UTC offset for the system time zone
        //    //TimeSpan utcOffset = systemTimeZone.GetUtcOffset(DateTime.Now);

        //    //// If the UTC offset is not fixed (e.g., -04:00), it indicates automatic time setting
        //    //return utcOffset == systemTimeZone.BaseUtcOffset;
        //}



        ////private DateTime referenceTime;

        // //public DateTime GetReferenceTime()
        //   //{
        //     //    referenceTime = DateTime.Now;
        //       //    return referenceTime;//
        ////}

        //public bool HasDeviceTimeChang//ed()
        //   //{
        //    DateTime currentTime = DateTi//me.Now;

        //    // Set a threshold for acceptable time difference, e.g.,// 5 seconds
        //    double timeDifferenceThres//hold = 5.0;

        //    double timeDifference = Math.Abs((currentTime - referenceTime).T//otalSeconds);

        //    return timeDifference > timeDiff//erenceThreshold;
        //}

        //public bool IsDeviceTimeAutomatic()
        //{
        //    // Use native iOS APIs to check if the device time setting is automatic
        //    NSTimeZone timeZone = NSTimeZone.SystemTimeZone;
        //    //return timeZone.AutoupdatingCurrent;
        //    return timeZone.Abbreviation();
        //}

        //private DateTime lastKnownTime;

        //public DeviceTimeService()
        //{
        //    lastKnownTime = DateTime.Now;
        //    NSTimer.CreateRepeatingScheduledTimer(1, CheckDeviceTimeChanged);
        //}

        //public bool IsDeviceTimeAutomatic()
        //{
        //    //NSTimeZone timeZone = NSTimeZone.SystemTimeZone;
        //    //return timeZone.AutoupdatingCurrent;

        //    var telephonyInfo = new CTTelephonyNetworkInfo();
        //    return telephonyInfo.CurrentRadioAccessTechnology != null;
        //}

        //public DateTime GetCurrentDeviceTime()
        //{
        //    return DateTime.Now;
        //}

        //private void CheckDeviceTimeChanged(NSTimer timer)
        //{
        //    DateTime currentTime = DateTime.Now;

        //    if (currentTime != lastKnownTime)
        //    {
        //        lastKnownTime = currentTime;

        //        // Raise the event to indicate that the device time has changed
        //        DeviceTimeChanged?.Invoke(this, EventArgs.Empty);
        //    }
        //}

        //public event EventHandler DeviceTimeChanged;
    }
}
