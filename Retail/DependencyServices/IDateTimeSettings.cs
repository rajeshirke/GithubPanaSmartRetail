using System;
namespace Retail.DependencyServices
{
    public interface IDateTimeSettings
    {
        //DateTime GetDeviceTime();
        //DateTime ConvertToDeviceTimeZone(DateTime dateTime, string timeZoneId);

        bool IsAutomaticDateTimeEnabled();
        void OpenDateTimeSettings();
    }
}
